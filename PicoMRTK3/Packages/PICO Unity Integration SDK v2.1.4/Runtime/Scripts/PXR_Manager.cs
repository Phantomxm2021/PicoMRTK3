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
        private static bool bindVerifyServiceSuccess = false;
        public static PXR_Manager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<PXR_Manager>();
                    if (instance == null)
                    {
                        Debug.LogError("PXRLog instance is not initialized!");
                    }
                }
                return instance;
            }
        }

        private float refreshRate = -1.0f;
        private Camera[] eyeCamera;
        private int[] eyeCameraOriginCullingMask = new int[3];
        private CameraClearFlags[] eyeCameraOriginClearFlag = new CameraClearFlags[3];

        private bool appSpaceWarp;
        private Transform m_AppSpaceTransform;
        private DepthTextureMode m_CachedDepthTextureMode;

        [HideInInspector]
        public bool showFps;
        [HideInInspector]
        public bool useDefaultFps = true;
        [HideInInspector]
        public int customFps;
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
        public FoveationLevel foveationLevel = FoveationLevel.None;

        //MRC
        #region MRCData
        [HideInInspector]
        public bool openMRC = true;
        [HideInInspector]
        public LayerMask foregroundLayerMask = -1;
        [HideInInspector]
        public LayerMask backLayerMask = -1;
        private static bool MRCInitSucceed = false;

        private Texture[] swapChain = new Texture[2];
        private struct LayerTexture
        {
            public Texture[] swapChain;
        };
        private LayerTexture[] layerTexturesInfo;
        private bool createMRCOverlaySucceed = false;
        private int imageIndex;
        private UInt32 imageCounts = 0;

        private static CameraData xmlCameraData;
        private static float imageW;
        private static float imageH;

        private bool mrcPlay = false;
        private float[] cameraAttribute;
        private PxrLayerParam layerParam = new PxrLayerParam();
        private float yFov = 53f;
        [HideInInspector]
        public GameObject backCameraObj = null;
        [HideInInspector]
        public GameObject foregroundCameraObj = null;
        [HideInInspector]
        public RenderTexture mrcRenderTexture = null;
        [HideInInspector]
        public RenderTexture foregroundMrcRenderTexture = null;
        private Color foregroundColor = new Color(0, 1, 0, 1);
        private static float height;
        private static bool layerImageEnable = true;


        #endregion

        private bool isNeedResume = false;

        //Entitlement Check Result
        [HideInInspector]
        public int appCheckResult = 100;
        public delegate void EntitlementCheckResult(int ReturnValue);
        public static event EntitlementCheckResult EntitlementCheckResultEvent;

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
            if (!bindVerifyServiceSuccess)
            {
                PXR_Plugin.PlatformSetting.UPxr_BindVerifyService(gameObject.name);
            }
            eyeCamera = new Camera[3];
            Camera[] cam = gameObject.GetComponentsInChildren<Camera>();
            for (int i = 0; i < cam.Length; i++) {
                if (cam[i].stereoTargetEye == StereoTargetEyeMask.Both) {
                    eyeCamera[0] = cam[i];
                }else if (cam[i].stereoTargetEye == StereoTargetEyeMask.Left)
                {
                    eyeCamera[1] = cam[i];
                }
                else if(cam[i].stereoTargetEye == StereoTargetEyeMask.Right)
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

            if (openMRC && MRCInitSucceed == false)
            {
                UPxr_MRCPoseInitialize();
            }
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
            if (openMRC && MRCInitSucceed)
            {
                PXR_Plugin.Render.UPxr_DestroyLayer(99999);
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
            bool systemFps = false;
#if UNITY_ANDROID && !UNITY_EDITOR
            PXR_Plugin.System.UPxr_GetTextSize("");//load res & get permission of external storage
            systemFps = Convert.ToBoolean(Convert.ToInt16(PXR_Plugin.System.UPxr_GetConfigInt(ConfigType.ShowFps)));
#endif
            if (systemFps || showFps)
            {
                Camera.main.transform.Find("FPS").gameObject.SetActive(true);
            }

            if (PXR_PlatformSetting.Instance.startTimeEntitlementCheck)
            {
                if (PXR_Plugin.PlatformSetting.UPxr_IsCurrentDeviceValid() != PXR_PlatformSetting.simulationType.Valid)
                {
                    Debug.Log("PXRLog Entitlement Check Simulation DO NOT PASS");
                    string appID = PXR_PlatformSetting.Instance.appID;
                    Debug.Log("PXRLog Entitlement Check Enable");
                    // 0:success -1:invalid params -2:service not exist -3:time out
                    PXR_Plugin.PlatformSetting.UPxr_AppEntitlementCheckExtra(appID);
                }
                else
                {
                    Debug.Log("PXRLog Entitlement Check Simulation PASS");
                }
            }
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

            //MRC
            if (openMRC && MRCInitSucceed) {
                UPxr_GetLayerImage();
                if (createMRCOverlaySucceed)
                {
                    MRCUpdata();
                }
            }
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
                UPxr_MRCDataBinding();
            }

            PXR_Plugin.System.SeethroughStateChangedChanged += SeeThroughStateChangedCallback;
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
            if (openMRC) {
                UPxr_UnMRCDataBinding();
            }

            PXR_Plugin.System.SeethroughStateChangedChanged -= SeeThroughStateChangedCallback;
        }   
        
        //bind verify service success call back
        void BindVerifyServiceCallback()
        {
            bindVerifyServiceSuccess = true;
        }

        void SeeThroughStateChangedCallback(int value)
        {
            try
            {
                for (int i = 0; i < 3; i++)
                {
                    if (eyeCamera[i] != null && eyeCamera[i].enabled)
                    {
                        PLog.d(TAG,"SeeThroughStateChangedCallback State "+value);
                        if (value == 2)
                        {
                            if (eyeCamera[i].clearFlags != CameraClearFlags.Nothing)
                            {
                                PLog.d(TAG, "eyeCamera stop rendering");
                                eyeCameraOriginClearFlag[i] = eyeCamera[i].clearFlags;
                                eyeCameraOriginCullingMask[i] = eyeCamera[i].cullingMask;

                                eyeCamera[i].cullingMask = 0;
                                eyeCamera[i].clearFlags = CameraClearFlags.Nothing;
                            }
                        }
                        else
                        {
                            if (eyeCamera[i].clearFlags == CameraClearFlags.Nothing)
                            {
                                PLog.d(TAG, "eyeCamera start rendering");
                                eyeCamera[i].clearFlags = eyeCameraOriginClearFlag[i];
                                eyeCamera[i].cullingMask = eyeCameraOriginCullingMask[i];
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Log("PXRLog SeeThroughStateChangedCallback Error"+e.ToString());
            }

        }

        private void verifyAPPCallback(string callback)
        {
            Debug.Log("PXRLog verifyAPPCallback callback = " + callback);
            appCheckResult = Convert.ToInt32(callback);
            if (EntitlementCheckResultEvent != null)
            {
                EntitlementCheckResultEvent(appCheckResult);
            }
        }

        public Camera[] GetEyeCamera()
        {
            return eyeCamera;
        }


        #region MRC FUNC
        public void UPxr_MRCPoseInitialize()
        {
            layerImageEnable = true;
            xmlCameraData = new CameraData();
            xmlCameraData.translation = new float[3];
            xmlCameraData.rotation = new float[4];
            UPxr_ReadXML(out xmlCameraData);
            Invoke("Pxr_GetHeight", 0.5f);
            PXR_Plugin.System.UPxr_SetIsSupportMovingMrc(true);
            PxrPosef pose = new PxrPosef();
            pose.orientation.x = xmlCameraData.rotation[0];
            pose.orientation.y = xmlCameraData.rotation[1];
            pose.orientation.z = xmlCameraData.rotation[2];
            pose.orientation.w = xmlCameraData.rotation[3];
            pose.position.x = xmlCameraData.translation[0];
            pose.position.y = xmlCameraData.translation[1];
            pose.position.z = xmlCameraData.translation[2];
            PXR_Plugin.System.UPxr_SetMrcPose(ref pose);

            PXR_Plugin.System.UPxr_GetMrcPose(ref pose);
            xmlCameraData.rotation[0] = pose.orientation.x;
            xmlCameraData.rotation[1] = pose.orientation.y;
            xmlCameraData.rotation[2] = pose.orientation.z;
            xmlCameraData.rotation[3] = pose.orientation.w;
            xmlCameraData.translation[0] = pose.position.x;
            xmlCameraData.translation[1] = pose.position.y;
            xmlCameraData.translation[2] = pose.position.z;
            mrcPlay = false;
            UInt64 textureWidth = (UInt64)xmlCameraData.imageWidth;
            UInt64 textureHeight = (UInt64)xmlCameraData.imageHeight;
            imageW = xmlCameraData.imageWidth;
            imageH = xmlCameraData.imageHeight;
            PXR_Plugin.System.UPxr_SetMrcTextutrWidth(textureWidth);
            PXR_Plugin.System.UPxr_SetMrcTextutrHeight(textureHeight);
            UPxr_CreateMRCOverlay((uint)xmlCameraData.imageWidth, (uint)xmlCameraData.imageHeight);
            MRCInitSucceed = true;
            Debug.Log("PXR_MRCInit Succeed");
        }

        public void UPxr_CreateMRCOverlay(uint width, uint height)
        {
            if (width <= 0 || height <= 0) {
                layerImageEnable = false;
                Debug.Log("PXR MRC Abnormal calibration data");
                return;
            }
            layerParam.layerId = 99999;
            layerParam.layerShape = PXR_OverLay.OverlayShape.Quad;
            layerParam.layerType = PXR_OverLay.OverlayType.Overlay;
            layerParam.layerLayout = PXR_OverLay.LayerLayout.Stereo;
            layerParam.format = (UInt64)RenderTextureFormat.Default;
            layerParam.width = width;
            layerParam.height = height;
            layerParam.sampleCount = 1;
            layerParam.faceCount = 1;
            layerParam.arraySize = 1;
            layerParam.mipmapCount = 0;
            layerParam.layerFlags = 0;
            PXR_Plugin.Render.UPxr_CreateLayerParam(layerParam);
        }

        public void UPxr_MRCDataBinding() {
            if (GraphicsSettings.renderPipelineAsset != null)
            {
                RenderPipelineManager.beginFrameRendering += BeginRendering;
            }
            else
            {
                Camera.onPostRender += UPxr_CopyMRCTexture;
            }
            PXR_Plugin.System.RecenterSuccess += UPxr_Calibration;
        }

        public void UPxr_UnMRCDataBinding()
        {
            if (GraphicsSettings.renderPipelineAsset != null)
            {
                RenderPipelineManager.beginFrameRendering -= BeginRendering;
            }
            else
            {
                Camera.onPostRender -= UPxr_CopyMRCTexture;
            }
            PXR_Plugin.System.RecenterSuccess -= UPxr_Calibration;
        }

        private void BeginRendering(ScriptableRenderContext arg1, Camera[] arg2)
        {
            foreach (Camera cam in arg2)
            {
                if (cam != null && Camera.main.tag == cam.tag)
                {
                    UPxr_CopyMRCTexture(cam);
                }
            }
        }

        public void UPxr_CopyMRCTexture(Camera cam)
        {
            if (createMRCOverlaySucceed && PXR_Plugin.System.UPxr_GetMRCEnable())
            {
                if (cam == null || cam.tag != Camera.main.tag || cam.stereoActiveEye == Camera.MonoOrStereoscopicEye.Right) return;
                PXR_Plugin.Render.UPxr_GetLayerNextImageIndex(99999, ref imageIndex);

                for (int eyeId = 0; eyeId < 2; ++eyeId)
                {
                    Texture dstT = layerTexturesInfo[eyeId].swapChain[imageIndex];

                    if (dstT == null)
                        continue;
                    RenderTexture rt;
                    if (eyeId == 0)
                    {
                        rt = mrcRenderTexture as RenderTexture;
                    }
                    else
                    {
                        rt = foregroundMrcRenderTexture as RenderTexture;
                    }
                    RenderTexture tempRT = null;

                    if (!(QualitySettings.activeColorSpace == ColorSpace.Gamma && rt != null && rt.format == RenderTextureFormat.ARGB32))
                    {
                        RenderTextureDescriptor descriptor = new RenderTextureDescriptor((int)imageW, (int)imageH, RenderTextureFormat.ARGB32, 0);
                        descriptor.msaaSamples = 1;
                        descriptor.useMipMap = false;
                        descriptor.autoGenerateMips = false;
                        descriptor.sRGB = false;
                        tempRT = RenderTexture.GetTemporary(descriptor);

                        if (!tempRT.IsCreated())
                        {
                            tempRT.Create();
                        }
                        if (eyeId == 0)
                        {
                            mrcRenderTexture.DiscardContents();
                        }
                        else
                        {
                            foregroundMrcRenderTexture.DiscardContents();
                        }
                        tempRT.DiscardContents();

                        if (eyeId == 0)
                        {
                            Graphics.Blit(mrcRenderTexture, tempRT);
                            Graphics.CopyTexture(tempRT, 0, 0, dstT, 0, 0);
                        }
                        else
                        {
                            Graphics.Blit(foregroundMrcRenderTexture, tempRT);
                            Graphics.CopyTexture(tempRT, 0, 0, dstT, 0, 0);
                        }
                    }
                    else
                    {
                        if (eyeId == 0)
                        {
                            Graphics.CopyTexture(mrcRenderTexture, 0, 0, dstT, 0, 0);
                        }
                        else
                        {
                            Graphics.CopyTexture(foregroundMrcRenderTexture, 0, 0, dstT, 0, 0);
                        }
                    }

                    if (tempRT != null)
                    {
                        RenderTexture.ReleaseTemporary(tempRT);
                    }
                }
                PxrLayerQuad layerSubmit = new PxrLayerQuad();
                layerSubmit.header.layerId = 99999;
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
            else {
                return;
            }
        }

        

        public void UPxr_ReadXML(out CameraData cameradata)
        {
            CameraData cameraDataNew = new CameraData();
            string path = Application.persistentDataPath + "/mrc.xml";
            cameraAttribute = PXR_Plugin.PlatformSetting.UPxr_MRCCalibration(path);
            Debug.Log("cameraDataLength: " + cameraAttribute.Length);
            for (int i = 0; i < cameraAttribute.Length; i++)
            {
                Debug.Log("cameraData: " + i.ToString() + ": " + cameraAttribute[i].ToString());
            }
            cameraDataNew.imageWidth = cameraAttribute[0];
            cameraDataNew.imageHeight = cameraAttribute[1];
            yFov = cameraAttribute[2];
            cameraDataNew.translation = new float[3];
            cameraDataNew.rotation = new float[4];
            for (int i = 0; i < 3; i++)
            {
                cameraDataNew.translation[i] = cameraAttribute[3 + i];
            }
            for (int i = 0; i < 4; i++)
            {
                cameraDataNew.rotation[i] = cameraAttribute[6 + i];
            }
            cameradata = cameraDataNew;
        }

        public void UPxr_CreateCamera()
        {
            if (backCameraObj == null)
            {
                backCameraObj = new GameObject("myBackCamera");
                backCameraObj.tag = "mrc";
                backCameraObj.transform.parent = Camera.main.transform.parent;
                Camera camera = backCameraObj.AddComponent<Camera>();
                camera.stereoTargetEye = StereoTargetEyeMask.None;
                camera.transform.localScale = Vector3.one;
                camera.depth = 9999;
                camera.gameObject.layer = 0;
                camera.clearFlags = CameraClearFlags.Skybox;
                camera.orthographic = false;
                camera.fieldOfView = yFov;
                camera.aspect = imageW / imageH;
                camera.transform.localPosition = Vector3.zero;
                camera.transform.localEulerAngles = Vector3.zero;
                camera.allowMSAA = true;
                camera.cullingMask = backLayerMask;
                if (mrcRenderTexture == null)
                {
                    mrcRenderTexture = new RenderTexture((int)imageW, (int)imageH, 24, RenderTextureFormat.ARGB32);
                }
                mrcRenderTexture.name = "mrcRenderTexture";
                camera.targetTexture = mrcRenderTexture;
            }
            else
            {
                backCameraObj.GetComponent<Camera>().stereoTargetEye = StereoTargetEyeMask.None;
                backCameraObj.GetComponent<Camera>().transform.localPosition = Vector3.zero;
                backCameraObj.GetComponent<Camera>().transform.localEulerAngles = Vector3.zero;
                backCameraObj.GetComponent<Camera>().transform.localScale = Vector3.one;
                backCameraObj.GetComponent<Camera>().depth = 9999;
                backCameraObj.GetComponent<Camera>().gameObject.layer = 0;
                backCameraObj.GetComponent<Camera>().clearFlags = CameraClearFlags.Skybox;
                backCameraObj.GetComponent<Camera>().orthographic = false;
                backCameraObj.GetComponent<Camera>().fieldOfView = yFov;
                backCameraObj.GetComponent<Camera>().aspect = imageW / imageH;
                backCameraObj.GetComponent<Camera>().allowMSAA = true;
                backCameraObj.GetComponent<Camera>().cullingMask = backLayerMask;
                if (mrcRenderTexture == null)
                {
                    mrcRenderTexture = new RenderTexture((int)imageW, (int)imageH, 24, RenderTextureFormat.ARGB32);
                }
                backCameraObj.GetComponent<Camera>().targetTexture = mrcRenderTexture;
                backCameraObj.SetActive(true);
            }
            if (foregroundCameraObj == null)
            {
                foregroundCameraObj = new GameObject("myForegroundCamera");
                foregroundCameraObj.transform.parent = Camera.main.transform.parent;
                Camera camera = foregroundCameraObj.AddComponent<Camera>();
                camera.clearFlags = CameraClearFlags.SolidColor;
                camera.backgroundColor = foregroundColor;
                camera.stereoTargetEye = StereoTargetEyeMask.None;
                camera.transform.localScale = Vector3.one;
                camera.depth = 10000;
                camera.gameObject.layer = 0;
                camera.orthographic = false;
                camera.fieldOfView = yFov;
                camera.aspect = imageW / imageH;
                camera.transform.localPosition = Vector3.zero;
                camera.transform.localEulerAngles = Vector3.zero;
                camera.allowMSAA = true;
                camera.cullingMask = foregroundLayerMask;
                if (foregroundMrcRenderTexture == null)
                {
                    foregroundMrcRenderTexture = new RenderTexture((int)imageW, (int)imageH, 24, RenderTextureFormat.ARGB32);
                }
                foregroundMrcRenderTexture.name = "foregroundMrcRenderTexture";
                camera.targetTexture = foregroundMrcRenderTexture;
            }
            else
            {
                foregroundCameraObj.GetComponent<Camera>().clearFlags = CameraClearFlags.SolidColor;
                foregroundCameraObj.GetComponent<Camera>().backgroundColor = foregroundColor;
                foregroundCameraObj.GetComponent<Camera>().stereoTargetEye = StereoTargetEyeMask.None;
                foregroundCameraObj.GetComponent<Camera>().transform.localPosition = Vector3.zero;
                foregroundCameraObj.GetComponent<Camera>().transform.localEulerAngles = Vector3.zero;
                foregroundCameraObj.GetComponent<Camera>().transform.localScale = Vector3.one;
                foregroundCameraObj.GetComponent<Camera>().depth = 10000;
                foregroundCameraObj.GetComponent<Camera>().gameObject.layer = 0;
                foregroundCameraObj.GetComponent<Camera>().orthographic = false;
                foregroundCameraObj.GetComponent<Camera>().fieldOfView = yFov;
                foregroundCameraObj.GetComponent<Camera>().aspect = imageW / imageH;
                foregroundCameraObj.GetComponent<Camera>().allowMSAA = true;
                foregroundCameraObj.GetComponent<Camera>().cullingMask = foregroundLayerMask;
                if (foregroundMrcRenderTexture == null)
                {
                    foregroundMrcRenderTexture = new RenderTexture((int)imageW, (int)imageH, 24, RenderTextureFormat.ARGB32);
                }
                foregroundCameraObj.GetComponent<Camera>().targetTexture = foregroundMrcRenderTexture;
                foregroundCameraObj.SetActive(true);
            }
            mrcPlay = true;

            Debug.Log("PxrMRC Camera create");
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
            Debug.Log("Pxr_GetMrcY+:" + PXR_Plugin.System.UPxr_GetMrcY().ToString());
        }

        private void MRCUpdata() {
            if (PXR_Plugin.System.UPxr_GetMRCEnable())
            {
                if (backCameraObj == null)
                {
                    if (Camera.main.transform != null)
                    {
                        UPxr_CreateCamera();
                        UPxr_Calibration();
                    }
                }                     
                else
                {
                    if (!mrcPlay)
                    {
                        if (Camera.main.transform != null)
                        {
                            UPxr_CreateCamera();
                            UPxr_Calibration();
                        }
                    }
                }
                if (foregroundCameraObj != null)
                {
                    Vector3 cameraLookAt = Camera.main.transform.position - foregroundCameraObj.transform.position;
                    float distance = Vector3.Dot(cameraLookAt, foregroundCameraObj.transform.forward);
                    foregroundCameraObj.GetComponent<Camera>().farClipPlane = Mathf.Max(foregroundCameraObj.GetComponent<Camera>().nearClipPlane + 0.001f, distance);
                }
                if (backCameraObj != null && foregroundCameraObj != null)
                {
                    UPxr_Calibration();
                }
            }
            else
            {
                if (mrcPlay == true)
                {
                    mrcPlay = false;
                    backCameraObj.SetActive(false);
                    foregroundCameraObj.SetActive(false);
                }
            }
        }

        public void UPxr_GetLayerImage()
        {
            if (layerImageEnable == true && createMRCOverlaySucceed == false)
            {
                if (layerTexturesInfo == null)
                {
                    layerTexturesInfo = new LayerTexture[2];
                }
                if (PXR_Plugin.Render.UPxr_GetLayerImageCount(99999, EyeType.EyeLeft, ref imageCounts) == 0 && imageCounts > 0)
                {
                    if (layerTexturesInfo[0].swapChain == null)
                    {
                        layerTexturesInfo[0].swapChain = new Texture[imageCounts];
                    }
                    for (int j = 0; j < imageCounts; j++)
                    {
                        IntPtr ptr = IntPtr.Zero;
                        PXR_Plugin.Render.UPxr_GetLayerImagePtr(99999, EyeType.EyeLeft, j, ref ptr);
                        if (ptr == IntPtr.Zero)
                        {
                            continue;
                        }
                        Texture sc = Texture2D.CreateExternalTexture((int)imageW, (int)imageH, TextureFormat.RGBA32, false, true, ptr);

                        if (sc == null)
                        {
                            continue;
                        }

                        layerTexturesInfo[0].swapChain[j] = sc;
                    }

                }
                if (PXR_Plugin.Render.UPxr_GetLayerImageCount(99999, EyeType.EyeRight, ref imageCounts) == 0 && imageCounts > 0)
                {
                    if (layerTexturesInfo[1].swapChain == null)
                    {
                        layerTexturesInfo[1].swapChain = new Texture[imageCounts];
                    }

                    for (int j = 0; j < imageCounts; j++)
                    {
                        IntPtr ptr = IntPtr.Zero;
                        PXR_Plugin.Render.UPxr_GetLayerImagePtr(99999, EyeType.EyeRight, j, ref ptr);
                        if (ptr == IntPtr.Zero)
                        {
                            continue;
                        }

                        Texture sc = Texture2D.CreateExternalTexture((int)imageW, (int)imageH, TextureFormat.RGBA32, false, true, ptr);

                        if (sc == null)
                        {
                            continue;
                        }

                        layerTexturesInfo[1].swapChain[j] = sc;
                    }

                    createMRCOverlaySucceed = true;
                    Debug.Log("Pxr_GetMrcLayerImage : true");
                }
            }
        }

        public void UPxr_Calibration()
        {
            if (PXR_Plugin.System.UPxr_GetMRCEnable())
            {
                PxrPosef pose = new PxrPosef();
                pose.orientation.x = 0;
                pose.orientation.y = 0;
                pose.orientation.z = 0;
                pose.orientation.w = 0;
                pose.position.x = 0;
                pose.position.y = 0;
                pose.position.z = 0;
                PXR_Plugin.System.UPxr_GetMrcPose(ref pose);
                backCameraObj.transform.localPosition = new Vector3(pose.position.x, pose.position.y + height, (-pose.position.z) * 1f);
                foregroundCameraObj.transform.localPosition = new Vector3(pose.position.x, pose.position.y + height, (-pose.position.z) * 1f);
                Vector3 rototion = new Quaternion(pose.orientation.x, pose.orientation.y, pose.orientation.z, pose.orientation.w).eulerAngles;
                backCameraObj.transform.localEulerAngles = new Vector3(-rototion.x, -rototion.y, -rototion.z);
                foregroundCameraObj.transform.localEulerAngles = new Vector3(-rototion.x, -rototion.y, -rototion.z);
            }
        }

        #endregion
    }
    public struct CameraData
    {
        public string id;
        public string cameraName;
        public float imageWidth;
        public float imageHeight;
        public float[] translation;
        public float[] rotation;
    }
}

