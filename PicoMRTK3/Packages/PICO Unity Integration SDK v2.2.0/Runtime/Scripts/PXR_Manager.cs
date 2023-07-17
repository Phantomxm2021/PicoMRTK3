/*******************************************************************************
Copyright © 2015-2022 PICO Technology Co., Ltd.All rights reserved.  

NOTICE：All information contained herein is, and remains the property of 
PICO Technology Co., Ltd. The intellectual and technical concepts 
contained herein are proprietary to PICO Technology Co., Ltd. and may be 
covered by patents, patents in process, and are protected by trade secret or 
copyright law. Dissemination of this information or reproduction of this 
material is strictly forbidden unless prior written permission is obtained from
PICO Technology Co., Ltd. 
*******************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR;
using UnityEngine.XR.Management;

namespace Unity.XR.PXR
{
    public class PXR_Manager : MonoBehaviour
    {
        private const string TAG = "[PXR_Manager]";
        private static PXR_Manager instance = null;
        public static PXR_Manager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<PXR_Manager>();
                    if (instance == null)
                    {
                        GameObject go = new GameObject("[PXR_Manager]");
                        DontDestroyOnLoad(go);
                        instance = go.AddComponent<PXR_Manager>();
                        Debug.LogError("PXRLog instance is not initialized!");
                    }
                }
                return instance;
            }
        }

        private float refreshRate = -1.0f;
        private Camera[] eyeCamera;
        private bool appSpaceWarp;
        private Transform m_AppSpaceTransform;
        private DepthTextureMode m_CachedDepthTextureMode;

        [HideInInspector]
        public bool screenFade;
        [HideInInspector]
        public bool eyeTracking;
        [HideInInspector]
        public FaceTrackingMode trackingMode = FaceTrackingMode.None;
        [HideInInspector]
        public bool faceTracking;
        [HideInInspector]
        public bool lipsyncTracking;
        [HideInInspector]
        public bool lateLatching;
        [HideInInspector]
        public bool latelatchingDebug;
        [HideInInspector]
        public bool bodyTracking;
        [HideInInspector]
        public FoveationLevel foveationLevel = FoveationLevel.None;

        //MRC
        #region MRCData
        [HideInInspector]
        public bool openMRC = true;
        [HideInInspector]
        public LayerMask foregroundLayerMask = -1;
        [HideInInspector]
        public LayerMask backLayerMask = -1;
        private static bool initMRCSucceed = false;

        private Texture[] swapChain = new Texture[2];
        private struct LayerTexture
        {
            public Texture[] swapChain;
        };
        private LayerTexture[] layerTexturesInfo;
        private bool createMRCOverlaySucceed = false;
        private int imageIndex;
        private UInt32 imageCounts = 0;

        private static CameraData xmlCamData;

        private bool mrcCamObjActived = false;
        private float[] cameraAttribute;
        private PxrLayerParam layerParam = new PxrLayerParam();
        [HideInInspector]
        public GameObject backgroundCamObj = null;
        [HideInInspector]
        public GameObject foregroundCamObj = null;
        [HideInInspector]
        public RenderTexture mrcBackgroundRT = null;
        [HideInInspector]
        public RenderTexture mrcForegroundRT = null;
        private Color foregroundColor = new Color(0, 1, 0, 1);
        private static float height;

        #endregion

        private bool isNeedResume = false;

        public Action<float> DisplayRefreshRateChanged;

        [HideInInspector]
        public bool useRecommendedAntiAliasingLevel = true;

        void Awake()
        {
            //version log
            Debug.Log("PXRLog XR Platform----SDK Version:" + PXR_Plugin.System.UPxr_GetSDKVersion());

            //log level
            int logLevel = PXR_Plugin.System.UPxr_GetConfigInt(ConfigType.UnityLogLevel);
            Debug.Log("PXRLog XR Platform----SDK logLevel:" + logLevel);
            PLog.logLevel = (PLog.LogLevel)logLevel;
            eyeCamera = new Camera[3];
            Camera[] cam = gameObject.GetComponentsInChildren<Camera>();
            for (int i = 0; i < cam.Length; i++)
            {
                if (cam[i].stereoTargetEye == StereoTargetEyeMask.Both && cam[i] == Camera.main)
                {
                    eyeCamera[0] = cam[i];
                }
                else if (cam[i].stereoTargetEye == StereoTargetEyeMask.Left)
                {
                    eyeCamera[1] = cam[i];
                }
                else if (cam[i].stereoTargetEye == StereoTargetEyeMask.Right)
                {
                    eyeCamera[2] = cam[i];
                }
            }

            PXR_Plugin.Render.UPxr_SetFoveationLevel(foveationLevel);
            PXR_Plugin.System.UPxr_EnableEyeTracking(eyeTracking);
            PXR_Plugin.System.UPxr_EnableFaceTracking(faceTracking);
            PXR_Plugin.System.UPxr_EnableLipSync(lipsyncTracking);

            int recommendedAntiAliasingLevel = PXR_Plugin.System.UPxr_GetConfigInt(ConfigType.AntiAliasingLevelRecommended);
            if (useRecommendedAntiAliasingLevel && QualitySettings.antiAliasing != recommendedAntiAliasingLevel)
            {
                QualitySettings.antiAliasing = recommendedAntiAliasingLevel;
                List<XRDisplaySubsystem> displaySubsystems = new List<XRDisplaySubsystem>();
                SubsystemManager.GetInstances(displaySubsystems);

                if (displaySubsystems.Count > 0)
                {
                    displaySubsystems[0].SetMSAALevel(recommendedAntiAliasingLevel);
                }
            }

            if (bodyTracking)
            {
                PXR_Plugin.Controller.UPxr_SetBodyTrackingMode(1);
            }

            Debug.LogFormat(TAG_MRC + "Awake openMRC = {0} ,MRCInitSucceed = {1}.", openMRC, initMRCSucceed);
            if (openMRC && initMRCSucceed == false)
            {
                MRCInitialize();
            }
            PXR_Plugin.System.UPxr_LogSdkApi("pico_msaa|" + QualitySettings.antiAliasing.ToString());
        }

        void OnApplicationPause(bool pause)
        {
            if (!pause)
            {
                if (isNeedResume)
                {
                    StartCoroutine("StartXR");
                    isNeedResume = false;
                }
            }
        }

        private void OnApplicationQuit()
        {
            Debug.LogFormat(TAG_MRC + "OnApplicationQuit openMRC = {0} ,MRCInitSucceed = {1}.", openMRC, initMRCSucceed);
            if (openMRC && initMRCSucceed)
            {
                PXR_Plugin.Render.UPxr_DestroyLayer(LAYER_MRC);
            }
        }

        public IEnumerator StartXR()
        {
            yield return XRGeneralSettings.Instance.Manager.InitializeLoader();

            if (XRGeneralSettings.Instance.Manager.activeLoader == null)
            {
                Debug.LogError("PXRLog Initializing XR Failed. Check log for details.");
            }
            else
            {
                XRGeneralSettings.Instance.Manager.StartSubsystems();
            }
        }

        void StopXR()
        {
            XRGeneralSettings.Instance.Manager.StopSubsystems();
            XRGeneralSettings.Instance.Manager.DeinitializeLoader();
        }

        void Start()
        {
#if UNITY_EDITOR
            Application.targetFrameRate = 72;
#endif
            PXR_Plugin.Controller.UPxr_SetControllerDelay();
        }

        void Update()
        {
            if (Math.Abs(refreshRate - PXR_Plugin.System.UPxr_RefreshRateChanged()) > 0.1f)
            {
                refreshRate = PXR_Plugin.System.UPxr_RefreshRateChanged();
                if (DisplayRefreshRateChanged != null)
                    DisplayRefreshRateChanged(refreshRate);
            }
            //recenter callback
            if (PXR_Plugin.System.UPxr_GetHomeKey())
            {
                if (PXR_Plugin.System.RecenterSuccess != null)
                {
                    PXR_Plugin.System.RecenterSuccess();
                }
                PXR_Plugin.System.UPxr_InitHomeKey();
            }

            UpdateMRCCam();
        }

        void OnEnable()
        {
            if (PXR_OverLay.Instances.Count > 0)
            {
                if (Camera.main.gameObject.GetComponent<PXR_OverlayManager>() == null)
                {
                    Camera.main.gameObject.AddComponent<PXR_OverlayManager>();
                }

                foreach (var layer in PXR_OverLay.Instances)
                {
                    if (eyeCamera[0] != null && eyeCamera[0].enabled)
                    {
                        layer.RefreshCamera(eyeCamera[0], eyeCamera[0]);
                    }
                    else if (eyeCamera[1] != null && eyeCamera[1].enabled)
                    {
                        layer.RefreshCamera(eyeCamera[1], eyeCamera[2]);
                    }
                }
            }

            if (openMRC)
            {
                if (GraphicsSettings.renderPipelineAsset != null)
                {
                    RenderPipelineManager.beginFrameRendering += BeginRendering;
                    RenderPipelineManager.endFrameRendering += EndRendering;
                }
                else
                {
                    Camera.onPreRender += OnPreRenderCallBack;
                    Camera.onPostRender += OnPostRenderCallBack;
                }

                PXR_Plugin.System.RecenterSuccess += CalibrationMRCCam;
            }

            PXR_Plugin.System.LoglevelChangedChanged += LoglevelChangedCallback;
        }

        private void LateUpdate()
        {
            if (appSpaceWarp && m_AppSpaceTransform != null)
            {
                PXR_Plugin.Render.UPxr_SetAppSpacePosition(m_AppSpaceTransform.position.x, m_AppSpaceTransform.position.y, m_AppSpaceTransform.position.z);
                PXR_Plugin.Render.UPxr_SetAppSpaceRotation(m_AppSpaceTransform.rotation.x, m_AppSpaceTransform.rotation.y, m_AppSpaceTransform.rotation.z, m_AppSpaceTransform.rotation.w);
            }
        }

        public void SetSpaceWarp(bool enabled)
        {
            for (int i = 0; i < 3; i++)
            {
                if (eyeCamera[i] != null && eyeCamera[i].enabled)
                {
                    if (enabled)
                    {
                        m_CachedDepthTextureMode = eyeCamera[i].depthTextureMode;
                        eyeCamera[i].depthTextureMode |= (DepthTextureMode.MotionVectors | DepthTextureMode.Depth);

                        if (eyeCamera[i].transform.parent == null)
                        {
                            m_AppSpaceTransform.position = Vector3.zero;
                            m_AppSpaceTransform.rotation = Quaternion.identity;
                        }
                        else
                        {
                            m_AppSpaceTransform = eyeCamera[i].transform.parent;
                        }
                    }
                    else
                    {
                        eyeCamera[i].depthTextureMode = m_CachedDepthTextureMode;
                        m_AppSpaceTransform = null;
                    }
                }
            }
            PXR_Plugin.Render.UPxr_SetSpaceWarp(enabled);
            appSpaceWarp = enabled;
        }


        void OnDisable()
        {
            StopAllCoroutines();

            if (openMRC)
            {
                if (GraphicsSettings.renderPipelineAsset != null)
                {
                    RenderPipelineManager.beginFrameRendering -= BeginRendering;
                    RenderPipelineManager.endFrameRendering -= EndRendering;
                }
                else
                {
                    Camera.onPreRender -= OnPreRenderCallBack;
                    Camera.onPostRender -= OnPostRenderCallBack;
                }
                PXR_Plugin.System.RecenterSuccess -= CalibrationMRCCam;
            }

            PXR_Plugin.System.LoglevelChangedChanged -= LoglevelChangedCallback;
        }

        void LoglevelChangedCallback(int value)
        {
            PLog.logLevel = (PLog.LogLevel)value;
            PLog.i(TAG, "LoglevelChangedCallback value " + value);
        }

        public Camera[] GetEyeCamera()
        {
            return eyeCamera;
        }


        #region MRC FUNC
        private const string TAG_MRC = "PXR MRC ";
        private const int LAYER_MRC = 99999;

        public void MRCInitialize()
        {
            xmlCamData = new CameraData();
            string path = Application.persistentDataPath + "/mrc.xml";
            cameraAttribute = PXR_Plugin.PlatformSetting.UPxr_MRCCalibration(path);
            PLog.i(TAG_MRC, "cameraDataLength: " + cameraAttribute.Length);
            for (int i = 0; i < cameraAttribute.Length; i++)
            {
                PLog.i(TAG_MRC, "cameraData: " + i.ToString() + ": " + cameraAttribute[i].ToString());
            }
            xmlCamData.imageW = cameraAttribute[0];
            xmlCamData.imageH = cameraAttribute[1];
            xmlCamData.yFov = cameraAttribute[2];
            xmlCamData.position.x = cameraAttribute[3];
            xmlCamData.position.y = cameraAttribute[4];
            xmlCamData.position.z = cameraAttribute[5];
            xmlCamData.orientation.x = cameraAttribute[6];
            xmlCamData.orientation.y = cameraAttribute[7];
            xmlCamData.orientation.z = cameraAttribute[8];
            xmlCamData.orientation.w = cameraAttribute[9];

            Invoke("Pxr_GetHeight", 0.5f);
            PXR_Plugin.System.UPxr_SetIsSupportMovingMrc(true);
            PxrPosef pose = new PxrPosef();
            pose.position = xmlCamData.position;
            pose.orientation = xmlCamData.orientation;
            PXR_Plugin.System.UPxr_SetMrcPose(ref pose);
            PXR_Plugin.System.UPxr_GetMrcPose(ref pose);
            xmlCamData.position = pose.position;
            xmlCamData.orientation = pose.orientation;
            mrcCamObjActived = false;

            PXR_Plugin.System.UPxr_SetMrcTextutrWidth((ulong)xmlCamData.imageW);
            PXR_Plugin.System.UPxr_SetMrcTextutrHeight((ulong)xmlCamData.imageH);

            if (xmlCamData.imageW <= 0 || xmlCamData.imageH <= 0)
            {
                initMRCSucceed = false;
                PLog.e(TAG_MRC, "Abnormal calibration data, so MRC init failed!");
                return;
            }
            layerParam.layerId = LAYER_MRC;
            layerParam.layerShape = PXR_OverLay.OverlayShape.Quad;
            layerParam.layerType = PXR_OverLay.OverlayType.Overlay;
            layerParam.layerLayout = PXR_OverLay.LayerLayout.Stereo;
            layerParam.format = (UInt64)RenderTextureFormat.Default;
            layerParam.width = (uint)xmlCamData.imageW;
            layerParam.height = (uint)xmlCamData.imageH;
            layerParam.sampleCount = 1;
            layerParam.faceCount = 1;
            layerParam.arraySize = 1;
            layerParam.mipmapCount = 0;
            layerParam.layerFlags = 0;
            PXR_Plugin.Render.UPxr_CreateLayerParam(layerParam);

            initMRCSucceed = true;
            PLog.i(TAG_MRC, "Init Succeed.");
        }

        private void BeginRendering(ScriptableRenderContext arg1, Camera[] arg2)
        {
            foreach (Camera cam in arg2)
            {
                if (cam != null && Camera.main == cam)
                {
                    OnPreRenderCallBack(cam);
                }
            }
        }

        private void EndRendering(ScriptableRenderContext arg1, Camera[] arg2)
        {
            foreach (Camera cam in arg2)
            {
                if (cam != null && Camera.main == cam)
                {
                    OnPostRenderCallBack(cam);
                }
            }
        }

        private void OnPreRenderCallBack(Camera cam)
        {
            if (!initMRCSucceed || createMRCOverlaySucceed) return;

            if (null == layerTexturesInfo)
            {
                layerTexturesInfo = new LayerTexture[2];
            }

            for (int i = 0; i < 2; i++)
            {
                int ret = PXR_Plugin.Render.UPxr_GetLayerImageCount(LAYER_MRC, (EyeType)i, ref imageCounts);
                if (ret != 0 || imageCounts < 1)
                {
                    PLog.e(TAG_MRC, "UPxr_GetLayerImageCount failed, i:" + i);
                    continue;
                }
                if (layerTexturesInfo[i].swapChain == null)
                {
                    layerTexturesInfo[i].swapChain = new Texture[imageCounts];
                }
                for (int j = 0; j < imageCounts; j++)
                {
                    IntPtr ptr = IntPtr.Zero;
                    PXR_Plugin.Render.UPxr_GetLayerImagePtr(LAYER_MRC, (EyeType)i, j, ref ptr);

                    if (IntPtr.Zero == ptr)
                    {
                        PLog.e(TAG_MRC, "UPxr_GetLayerImagePtr is Zero, i:" + i);
                        continue;
                    }

                    Texture texture = Texture2D.CreateExternalTexture((int)xmlCamData.imageW, (int)xmlCamData.imageH, TextureFormat.RGBA32, false, true, ptr);

                    if (null == texture)
                    {
                        PLog.e(TAG_MRC, "CreateExternalTexture texture null, i:" + i);
                        continue;
                    }

                    layerTexturesInfo[i].swapChain[j] = texture;
                }

                createMRCOverlaySucceed = true;
                PLog.i(TAG_MRC, " UPxr_GetLayerImagePtr createMRCOverlaySucceed : true. i:" + i);
            }
        }

        public void OnPostRenderCallBack(Camera cam)
        {
            if (!initMRCSucceed || !createMRCOverlaySucceed || !PXR_Plugin.System.UPxr_GetMRCEnable()) return;

            if (cam == null || cam != Camera.main || cam.stereoActiveEye == Camera.MonoOrStereoscopicEye.Right) return;

            PXR_Plugin.Render.UPxr_GetLayerNextImageIndex(LAYER_MRC, ref imageIndex);

            for (int eyeId = 0; eyeId < 2; ++eyeId)
            {
                Texture dstT = layerTexturesInfo[eyeId].swapChain[imageIndex];

                if (dstT == null)
                {
                    PLog.e(TAG_MRC, "dstT is null, eyeId:" + eyeId);
                    continue;
                }

                RenderTexture rt = (0 == eyeId) ? mrcBackgroundRT : mrcForegroundRT;

                RenderTexture tempRT = null;

                if (!(QualitySettings.activeColorSpace == ColorSpace.Gamma && rt != null && rt.format == RenderTextureFormat.ARGB32))
                {
                    RenderTextureDescriptor descriptor = new RenderTextureDescriptor((int)xmlCamData.imageW, (int)xmlCamData.imageH, RenderTextureFormat.ARGB32, 0);
                    descriptor.msaaSamples = 1;
                    descriptor.useMipMap = false;
                    descriptor.autoGenerateMips = false;
                    descriptor.sRGB = false;
                    tempRT = RenderTexture.GetTemporary(descriptor);

                    if (!tempRT.IsCreated())
                    {
                        tempRT.Create();
                    }
                    rt.DiscardContents();
                    tempRT.DiscardContents();

                    Graphics.Blit(rt, tempRT);
                    Graphics.CopyTexture(tempRT, 0, 0, dstT, 0, 0);
                }
                else
                {
                    Graphics.CopyTexture(rt, 0, 0, dstT, 0, 0);
                }

                if (tempRT != null)
                {
                    RenderTexture.ReleaseTemporary(tempRT);
                }
            }

            PxrLayerQuad layerSubmit = new PxrLayerQuad();
            layerSubmit.header.layerId = LAYER_MRC;
            layerSubmit.header.layerFlags = (UInt32)PxrLayerSubmitFlagsEXT.PxrLayerFlagMRCComposition;
            layerSubmit.width = 1.0f;
            layerSubmit.height = 1.0f;
            layerSubmit.header.colorScaleX = 1.0f;
            layerSubmit.header.colorScaleY = 1.0f;
            layerSubmit.header.colorScaleZ = 1.0f;
            layerSubmit.header.colorScaleW = 1.0f;
            layerSubmit.pose.orientation.w = 1.0f;
            layerSubmit.header.headPose.orientation.x = 0;
            layerSubmit.header.headPose.orientation.y = 0;
            layerSubmit.header.headPose.orientation.z = 0;
            layerSubmit.header.headPose.orientation.w = 1;
            PXR_Plugin.Render.UPxr_SubmitLayerQuad(layerSubmit);
        }

        private void UpdateMRCCam()
        {
            if (!openMRC || !initMRCSucceed) return;

            if (!PXR_Plugin.System.UPxr_GetMRCEnable())
            {
                if (mrcCamObjActived)
                {
                    mrcCamObjActived = false;
                    backgroundCamObj.SetActive(false);
                    foregroundCamObj.SetActive(false);
                }
                return;
            }

            if (null != Camera.main.transform && (null == backgroundCamObj || !mrcCamObjActived))
            {
                CreateMRCCam();
            }

            if (null != foregroundCamObj)
            {
                Vector3 cameraLookAt = Camera.main.transform.position - foregroundCamObj.transform.position;
                float distance = Vector3.Dot(cameraLookAt, foregroundCamObj.transform.forward);
                foregroundCamObj.GetComponent<Camera>().farClipPlane = Mathf.Max(foregroundCamObj.GetComponent<Camera>().nearClipPlane + 0.001f, distance);
            }

            CalibrationMRCCam();
        }

        public void CreateMRCCam()
        {
            if (backgroundCamObj == null)
            {
                backgroundCamObj = new GameObject("myBackgroundCamera");
                backgroundCamObj.transform.parent = Camera.main.transform.parent;
                backgroundCamObj.AddComponent<Camera>();
                PLog.i(TAG_MRC, "create background camera object.");
            }
            InitBackgroungCam(backgroundCamObj.GetComponent<Camera>());
            backgroundCamObj.SetActive(true);

            if (foregroundCamObj == null)
            {
                foregroundCamObj = new GameObject("myForegroundCamera");
                foregroundCamObj.transform.parent = Camera.main.transform.parent;
                foregroundCamObj.AddComponent<Camera>();
                PLog.i(TAG_MRC, "create foreground camera object.");
            }
            InitForegroundCam(foregroundCamObj.GetComponent<Camera>());
            foregroundCamObj.SetActive(true);

            mrcCamObjActived = true;

            PLog.i(TAG_MRC, "Camera Obj Actived. mrcCamObjActived : true.");
        }

        private void InitBackgroungCam(Camera camera)
        {
            camera.clearFlags = CameraClearFlags.Skybox;
            camera.stereoTargetEye = StereoTargetEyeMask.None;
            camera.transform.localScale = Vector3.one;
            camera.transform.localPosition = Vector3.zero;
            camera.transform.localEulerAngles = Vector3.zero;
            camera.depth = 9999;
            camera.gameObject.layer = 0;
            camera.orthographic = false;
            camera.fieldOfView = xmlCamData.yFov;
            camera.aspect = xmlCamData.imageW / xmlCamData.imageH;
            camera.allowMSAA = true;
            camera.cullingMask = backLayerMask;
            if (mrcBackgroundRT == null)
            {
                mrcBackgroundRT = new RenderTexture((int)xmlCamData.imageW, (int)xmlCamData.imageH, 24, RenderTextureFormat.ARGB32);
            }
            mrcBackgroundRT.name = "backgroundMrcRenderTexture";
            camera.targetTexture = mrcBackgroundRT;
            PLog.i(TAG_MRC, "init background camera.");
        }

        void InitForegroundCam(Camera camera)
        {
            camera.backgroundColor = foregroundColor;
            camera.clearFlags = CameraClearFlags.SolidColor;
            camera.stereoTargetEye = StereoTargetEyeMask.None;
            camera.transform.localScale = Vector3.one;
            camera.transform.localPosition = Vector3.zero;
            camera.transform.localEulerAngles = Vector3.zero;
            camera.depth = 10000;
            camera.gameObject.layer = 0;
            camera.orthographic = false;
            camera.fieldOfView = xmlCamData.yFov;
            camera.aspect = xmlCamData.imageW / xmlCamData.imageH;
            camera.allowMSAA = true;
            camera.cullingMask = foregroundLayerMask;
            if (mrcForegroundRT == null)
            {
                mrcForegroundRT = new RenderTexture((int)xmlCamData.imageW, (int)xmlCamData.imageH, 24, RenderTextureFormat.ARGB32);
            }
            mrcForegroundRT.name = "foregroundMrcRenderTexture";
            camera.targetTexture = mrcForegroundRT;
            PLog.i(TAG_MRC, "init foreground camera.");
        }

        public void CalibrationMRCCam()
        {
            if (!PXR_Plugin.System.UPxr_GetMRCEnable() || null == backgroundCamObj || null == foregroundCamObj) return;

            PxrPosef pose = new PxrPosef();
            pose.orientation.x = 0;
            pose.orientation.y = 0;
            pose.orientation.z = 0;
            pose.orientation.w = 0;
            pose.position.x = 0;
            pose.position.y = 0;
            pose.position.z = 0;
            PXR_Plugin.System.UPxr_GetMrcPose(ref pose);
            backgroundCamObj.transform.localPosition = new Vector3(pose.position.x, pose.position.y + height, (-pose.position.z) * 1f);
            foregroundCamObj.transform.localPosition = new Vector3(pose.position.x, pose.position.y + height, (-pose.position.z) * 1f);
            Vector3 rototion = new Quaternion(pose.orientation.x, pose.orientation.y, pose.orientation.z, pose.orientation.w).eulerAngles;
            backgroundCamObj.transform.localEulerAngles = new Vector3(-rototion.x, -rototion.y, -rototion.z);
            foregroundCamObj.transform.localEulerAngles = new Vector3(-rototion.x, -rototion.y, -rototion.z);
        }

        public Vector3 UPxr_ToVector3(float[] translation)
        {
            Debug.Log("translation:" + new Vector3(translation[0], translation[1], -translation[2]).ToString());
            return new Vector3(translation[0], translation[1] + height, (-translation[2]) * 1f);
        }

        public Vector3 UPxr_ToRotation(float[] rotation)
        {
            Quaternion quaternion = new Quaternion(rotation[0], rotation[1], rotation[2], rotation[3]);
            Vector3 vector3 = quaternion.eulerAngles;
            Debug.Log("rotation:" + vector3.ToString());
            return new Vector3(-vector3.x, -vector3.y, -vector3.z);
        }

        public void Pxr_GetHeight()
        {
            height = Camera.main.transform.localPosition.y - PXR_Plugin.System.UPxr_GetMrcY();
            PLog.i(TAG_MRC, "Pxr_GetMrcY+:" + PXR_Plugin.System.UPxr_GetMrcY().ToString());
        }

        #endregion
    }
    public struct CameraData
    {
        public string id;
        public string name;
        public float imageW;
        public float imageH;
        public float yFov;
        public PxrVector3f position;
        public PxrVector4f orientation;
    }
}