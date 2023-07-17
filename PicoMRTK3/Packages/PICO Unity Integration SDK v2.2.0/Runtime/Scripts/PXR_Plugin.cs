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

using System.ComponentModel;
using System.Drawing;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Unity.XR.PXR
{
    [StructLayout(LayoutKind.Sequential)]
    public struct UserDefinedSettings
    {
        public ushort stereoRenderingMode;
        public ushort colorSpace;
        public ushort systemDisplayFrequency;
        public ushort useContentProtect;
        public ushort optimizeBufferDiscards;
        public ushort enableAppSpaceWarp;
        public ushort enableSubsampled;
    }

    public enum RenderEvent
    {
        CreateTexture,
        DeleteTexture,
        UpdateTexture
    }

    public enum ResUtilsType
    {
        TypeTextSize,
        TypeColor,
        TypeText,
        TypeFont,
        TypeValue,
        TypeDrawable,
        TypeObject,
        TypeObjectArray,
    }

    public enum GraphicsAPI
    {
        OpenGLES,
        Vulkan
    };

    public enum EyeType
    {
        EyeLeft,
        EyeRight,
        EyeBoth
    };

    public enum ConfigType
    {
        RenderTextureWidth,
        RenderTextureHeight,
        ShowFps,
        RuntimeLogLevel,
        PluginLogLevel,
        UnityLogLevel,
        UnrealLogLevel,
        NativeLogLevel,
        TargetFrameRate,
        NeckModelX,
        NeckModelY,
        NeckModelZ,
        DisplayRefreshRate,
        Ability6Dof,
        DeviceModel,
        PhysicalIPD,
        ToDelaSensorY,
        SystemDisplayRate,
        FoveationSubsampledEnabled,
        TrackingOriginHeight,
        EngineVersion,
        UnrealOpenglNoError,
        EnableCPT,
        MRCTextureID,
        RenderFPS,
        AntiAliasingLevelRecommended,
        MRCTextureID2,
        PxrSetSurfaceView,
        PxrAPIVersion,
        PxrMrcPosiyionYOffset,
        PxrMrcTextureWidth,
        PxrMrcTextureHeight,
        PxrAndroidLayerDimensions = 34,
        PxrANDROID_SN,
        PxrSetDesiredFPS,
        PxrGetSeethroughState,
        PxrSetLayerBlend,
        PxrLeftEyeFOV,
        PxrRightEyeFOV,
        PxrBothEyeFOV,
        SupportQuickSeethrough,
        SetFilterType,
        SetSubmitLayerEXTItemColorMatrix,
    };

    public enum FoveationLevel
    {
        None = -1,
        Low,
        Med,
        High,
        TopHigh
    }

    public enum BoundaryType
    {
        OuterBoundary,
        PlayArea
    }

    public enum BoundaryTrackingNode
    {
        HandLeft,
        HandRight,
        Head
    }

    public enum PxrTrackingState
    {
        LostNoReason,
        LostCamera,
        LostHighLight,
        LostLowLight,
        LostLowFeatureCount,
        LostReLocation,
        LostInitialization,
        LostNoCamera,
        LostNoIMU,
        LostIMUJitter,
        LostUnknown,
    }

    public enum ResetSensorOption
    {
        ResetPosition,
        ResetRotation,
        ResetRotationYOnly,
        ResetAll
    };

    public enum PxrLayerCreateFlags
    {
        PxrLayerFlagAndroidSurface = 1 << 0,
        PxrLayerFlagProtectedContent = 1 << 1,
        PxrLayerFlagStaticImage = 1 << 2,
        PxrLayerFlagUseExternalImages = 1 << 4,
        PxrLayerFlag3DLeftRightSurface = 1 << 5,
        PxrLayerFlag3DTopBottomSurface = 1 << 6,
        PxrLayerFlagEnableFrameExtrapolation = 1 << 7,
        PxrLayerFlagEnableSubsampled = 1 << 8,
        PxrLayerFlagEnableFrameExtrapolationPTW = 1 << 9,
        PxrLayerFlagSharedImagesBetweenLayers = 1 << 10,
    }

    public enum PxrLayerSubmitFlagsEXT
    {
        PxrLayerFlagMRCComposition = 1 << 30,
    }

    public enum PxrLayerSubmitFlags
    {
        PxrLayerFlagNoCompositionDepthTesting = 1 << 3,
        PxrLayerFlagUseExternalHeadPose = 1 << 5,
        PxrLayerFlagLayerPoseNotInTrackingSpace = 1 << 6,
        PxrLayerFlagHeadLocked = 1 << 7,
        PxrLayerFlagUseExternalImageIndex = 1 << 8,
    }

    public enum PxrControllerKeyMap
    {
        PXR_CONTROLLER_KEY_HOME = 0,
        PXR_CONTROLLER_KEY_AX = 1,
        PXR_CONTROLLER_KEY_BY = 2,
        PXR_CONTROLLER_KEY_BACK = 3,
        PXR_CONTROLLER_KEY_TRIGGER = 4,
        PXR_CONTROLLER_KEY_VOL_UP = 5,
        PXR_CONTROLLER_KEY_VOL_DOWN = 6,
        PXR_CONTROLLER_KEY_ROCKER = 7,
        PXR_CONTROLLER_KEY_GRIP = 8,
        PXR_CONTROLLER_KEY_TOUCHPAD = 9,
        PXR_CONTROLLER_KEY_LASTONE = 127,

        PXR_CONTROLLER_TOUCH_AX = 128,
        PXR_CONTROLLER_TOUCH_BY = 129,
        PXR_CONTROLLER_TOUCH_ROCKER = 130,
        PXR_CONTROLLER_TOUCH_TRIGGER = 131,
        PXR_CONTROLLER_TOUCH_THUMB = 132,
        PXR_CONTROLLER_TOUCH_LASTONE = 255
    }

    public enum GetDataType
    {
        PXR_GET_FACE_DATA_DEFAULT = 0,
        PXR_GET_FACE_DATA = 3,
        PXR_GET_LIP_DATA = 4,
        PXR_GET_FACELIP_DATA = 5,
    }

    public enum BodyTrackerRole
    {
        Pelvis = 0,
        LEFT_HIP = 1,
        RIGHT_HIP = 2,
        SPINE1 = 3,
        LEFT_KNEE = 4,
        RIGHT_KNEE = 5,
        SPINE2 = 6,
        LEFT_ANKLE = 7,
        RIGHT_ANKLE = 8,
        SPINE3 = 9,
        LEFT_FOOT = 10,
        RIGHT_FOOT = 11,
        NECK = 12,
        LEFT_COLLAR = 13,
        RIGHT_COLLAR = 14,
        HEAD = 15,
        LEFT_SHOULDER = 16,
        RIGHT_SHOULDER = 17,
        LEFT_ELBOW = 18,
        RIGHT_ELBOW = 19,
        LEFT_WRIST = 20,
        RIGHT_WRIST = 21,
        LEFT_HAND = 22,
        RIGHT_HAND = 23,
        NONE_ROLE = 24,                // unvalid
        MIN_ROLE = 0,                 // min value
        MAX_ROLE = 23,                // max value
        ROLE_NUM = 24,
    }

    public enum BodyActionList
    {
        PxrNoneAction = 0x00000000,
        PxrTouchGround = 0x00000001,
        PxrKeepStatic = 0x00000002,
    }

    public struct BodyTrackerTransPose
    {
        public Int64 TimeStamp;                // time stamp of imu
        public double PosX;                     // position of x
        public double PosY;                     // position of y
        public double PosZ;                     // position of z
        public double RotQx;                    // x components of Quaternion
        public double RotQy;                    // y components of Quaternion
        public double RotQz;                    // z components of Quaternion
        public double RotQw;                    // w components of Quaternion
    }


    [StructLayout(LayoutKind.Sequential)]
    public struct BodyTrackerTransform
    {
        public BodyTrackerRole bone;                // bone name. if bone == NONE_ROLE, this bone is not calculated
        public BodyTrackerTransPose localpose;
        public BodyTrackerTransPose globalpose;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public double[] velo;                     // velocity of x,y,z
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public double[] acce;                     // acceleration of x,y,z
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public double[] wvelo;                    // angular velocity of x,y,z
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public double[] wacce;                    // angular acceleration of x,y,z
        public UInt32 Action;              // multiple actions can be supported at the same time by means of OR
    }

    public struct BodyTrackerResult
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
        public BodyTrackerTransform[] trackingdata;
    }


    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct PxrFitnessBandConnectState
    {
        public Byte num;
        public fixed Byte trackerID[12];
    }


    public struct FoveationParams
    {
        public float foveationGainX;
        public float foveationGainY;
        public float foveationArea;
        public float foveationMinimum;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct EyeTrackingGazeRay
    {
        public Vector3 direction;
        public bool isValid;
        public Vector3 origin;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PxrSensorState
    {
        public int status;
        public PxrPosef pose;
        public PxrVector3f angularVelocity;
        public PxrVector3f linearVelocity;
        public PxrVector3f angularAcceleration;
        public PxrVector3f linearAcceleration;
        public UInt64 poseTimeStampNs;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PxrSensorState2
    {
        public int status;
        public PxrPosef pose;
        public PxrPosef globalPose;
        public PxrVector3f angularVelocity;
        public PxrVector3f linearVelocity;
        public PxrVector3f angularAcceleration;
        public PxrVector3f linearAcceleration;
        public UInt64 poseTimeStampNs;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PxrControllerTracking
    {
        public PxrSensorState localControllerPose;
        public PxrSensorState globalControllerPose;
    }

    public enum PxrControllerType
    {
        PxrInputG2 = 3,
        PxrInputNeo2 = 4,
        PxrInputNeo3 = 5,
        PxrInputPICO_4 = 6
    }

    public enum PxrControllerDof
    {
        PxrController3Dof,
        PxrController6Dof
    }

    public enum PxrControllerBond
    {
        PxrControllerIsBond,
        PxrControllerUnBond
    }

    public enum PxrBlendFactor
    {
        PxrBlendFactorZero = 0,
        PxrBlendFactorOne = 1,
        PxrBlendFactorSrcAlpha = 2,
        PxrBlendFactorOneMinusSrcAlpha = 3,
        PxrBlendFactorDstAlpha = 4,
        PxrBlendFactorOneMinusDstAlpha = 5
    };

    public enum PxrDeviceAbilities
    {
        PxrTrackingModeRotationBit,
        PxrTrackingModePositionBit,
        PxrTrackingModeEyeBit,
        PxrTrackingModeFaceBit,
        PxrTrackingModeBroadBandMontorBit,
        PxrTrackingModeHandBit
    }

    public enum FaceTrackingMode {
        None,
        Hybrid,
        FaceOnly,
        LipsyncOnly
    }

    public enum SkipInitSettingFlag {
        SkipHandleConnectionTeaching = 1,
        SkipTriggerKeyTeaching       = 1 << 1,
        SkipLanguage                 = 1 << 2,
        SkipCountry                  = 1 << 3,
        SkipWIFI                     = 1 << 4,
        SkipQuickSetting             = 1 << 5
    }
    
    public enum PxrPerfSettings {
        CPU = 1,
        GPU = 2,
    }
    
    public enum PxrSettingsLevel {
        POWER_SAVINGS = 0,
        SUSTAINED_LOW = 1,
        SUSTAINED_HIGH = 3,
        BOOST = 5,
    }

     public enum PxrFtLipsyncValue
    {
        STOP_FT,
        STOP_LIPSYNC,
        START_FT,
        START_LIPSYNC,
    }

     public enum PxrGazeType
    {
        Never,
        DuringMotion,
        Always
    }

    public enum PxrArmModelType
    {
        Controller,
        Wrist,
        Elbow,
        Shoulder
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct PxrControllerCapability
    {
        public PxrControllerType type;
        public PxrControllerDof inputDof;
        public PxrControllerBond inputBond;
        public UInt64 Abilities;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PxrLayerParam
    {
        public int layerId;
        public PXR_OverLay.OverlayShape layerShape;
        public PXR_OverLay.OverlayType layerType;
        public PXR_OverLay.LayerLayout layerLayout;
        public UInt64 format;
        public UInt32 width;
        public UInt32 height;
        public UInt32 sampleCount;
        public UInt32 faceCount;
        public UInt32 arraySize;
        public UInt32 mipmapCount;
        public UInt32 layerFlags;
        public UInt32 externalImageCount;
        public IntPtr leftExternalImages;
        public IntPtr rightExternalImages;
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct PxrVector4f
    {
        public float x;
        public float y;
        public float z;
        public float w;
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct PxrVector3f
    {
        public float x;
        public float y;
        public float z;
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct PxrVector2f
    {
        public float x;
        public float y;
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct PxrBoundaryTriggerInfo
    {
        public bool isTriggering;
        public float closestDistance;
        public PxrVector3f closestPoint;
        public PxrVector3f closestPointNormal;
        public bool valid;
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct PxrPosef
    {
        public PxrVector4f orientation;
        public PxrVector3f position;
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct PxrRecti
    {
        public int x;
        public int y;
        public int width;
        public int height;
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct PxrLayerBlend
    {
        public PxrBlendFactor srcColor;
        public PxrBlendFactor dstColor;
        public PxrBlendFactor srcAlpha;
        public PxrBlendFactor dstAlpha;
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct PxrLayerHeader
    {
        public int layerId;
        public UInt32 layerFlags;
        public float colorScaleX;
        public float colorScaleY;
        public float colorScaleZ;
        public float colorScaleW;
        public float colorBiasX;
        public float colorBiasY;
        public float colorBiasZ;
        public float colorBiasW;
        public int compositionDepth;
        public int sensorFrameIndex;
        public int imageIndex;
        public PxrPosef headPose;
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct PxrLayerHeader2
    {
        public int layerId;
        public UInt32 layerFlags;
        public float colorScaleX;
        public float colorScaleY;
        public float colorScaleZ;
        public float colorScaleW;
        public float colorBiasX;
        public float colorBiasY;
        public float colorBiasZ;
        public float colorBiasW;
        public int compositionDepth;
        public int sensorFrameIndex;
        public int imageIndex;
        public PxrPosef headPose;
        public PXR_OverLay.OverlayShape layerShape;
        public UInt32 useLayerBlend;
        public PxrLayerBlend layerBlend;
        public UInt32 useImageRect;
        public PxrRecti imageRectLeft;
        public PxrRecti imageRectRight;
        public UInt64 reserved0;
        public UInt64 reserved1;
        public UInt64 reserved2;
        public UInt64 reserved3;
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct PxrLayerQuad
    {
        public PxrLayerHeader header;
        public PxrPosef pose;
        public float width;
        public float height;
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct PxrLayerQuad2
    {
        public PxrLayerHeader2 header;
        public PxrPosef poseLeft;
        public PxrPosef poseRight;
        public PxrVector2f sizeLeft;
        public PxrVector2f sizeRight;
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct PxrLayerCylinder
    {
        public PxrLayerHeader header;
        public PxrPosef pose;
        public float radius;
        public float centralAngle;
        public float height;
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct PxrLayerCylinder2
    {
        public PxrLayerHeader2 header;
        public PxrPosef poseLeft;
        public PxrPosef poseRight;
        public float radiusLeft;
        public float radiusRight;
        public float centralAngleLeft;
        public float centralAngleRight;
        public float heightLeft;
        public float heightRight;
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct PxrLayerEquirect
    {
        public PxrLayerHeader2 header;
        public PxrPosef poseLeft;
        public PxrPosef poseRight;
        public float radiusLeft;
        public float radiusRight;
        public float scaleXLeft;
        public float scaleXRight;
        public float scaleYLeft;
        public float scaleYRight;
        public float biasXLeft;
        public float biasXRight;
        public float biasYLeft;
        public float biasYRight;
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct PxrLayerEquirect2
    {
        public PxrLayerHeader2 header;
        public PxrPosef poseLeft;
        public PxrPosef poseRight;
        public float radiusLeft;
        public float radiusRight;
        public float centralHorizontalAngleLeft;
        public float centralHorizontalAngleRight;
        public float upperVerticalAngleLeft;
        public float upperVerticalAngleRight;
        public float lowerVerticalAngleLeft;
        public float lowerVerticalAngleRight;
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct PxrLayerCube2
    {
        public PxrLayerHeader2 header;
        public PxrPosef poseLeft;
        public PxrPosef poseRight;
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct PxrLayerEac2
    {
        public PxrLayerHeader2 header;
        public PxrPosef poseLeft;
        public PxrPosef poseRight;
        public PxrVector3f offsetPosLeft;
        public PxrVector3f offsetPosRight;
        public PxrVector4f offsetRotLeft;
        public PxrVector4f offsetRotRight;
        public UInt32 degreeType;
        public float overlapFactor;
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct AudioClipData
    {
        public int slot;//手柄
        public UInt64 buffersize;//数据大小
        public int sampleRate;// 采样率
        public int channelCounts;//通道数
        public int bitrate;//bit率
        public int reversal;//反转
        public int isCache;//是否缓存
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct VibrateInfo {
        public uint slot;
        public uint reversal;
        public float amp;
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct PxrPhfParams {
        public UInt64 frameseq;
        public UInt16 play;
        public UInt16 frequency;
        public UInt16 loop;
        public float gain;
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct PxrPhfFile
    {
        public string phfVersion;
        public int frameDuration;
        public PxrPhfParams[] patternData_L;
        public PxrPhfParams[] patternData_R;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PxrPhfParamsNum {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
        public PxrPhfParams[] phfParams;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct PxrFaceTrackingInfo
    {
        public Int64 timestamp;                         // us
        public fixed float blendShapeWeight[72];                //72（52+20）Expression component weight
        public fixed float videoInputValid[10];                 // Input validity of upper and lower face
        public float laughingProb;                      // Coefficient of laughter
        public fixed float emotionProb[10];                     // Emotional factor
        public fixed float reserved[128];
    };


    public static class PXR_Plugin
    {
        private const string PXR_SDK_Version = "2.2.3";
        private const string PXR_PLATFORM_DLL = "PxrPlatform";
        public const string PXR_API_DLL = "pxr_api";
        private static int PXR_API_Version = 0;

        #region DLLImports
        //PassThrough
        [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_CameraStart();

        [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_CameraStop();

        [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_CameraDestroy();

        [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Pxr_CameraGetRenderEventFunc();

        [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Pxr_CameraSetRenderEventPending();

        [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Pxr_CameraWaitForRenderEvent();

        [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_CameraUpdateFrame(int eye);

        [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_CameraCreateTexturesMainThread();

        [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_CameraDeleteTexturesMainThread();

        [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_CameraUpdateTexturesMainThread();

        [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Pxr_SetFoveationLevelEnable(int enable);

        [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Pxr_SetFFRSubsampled(bool enable);

        //System
        [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Pxr_LoadPlugin();

        [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Pxr_UnloadPlugin();

        [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Pxr_GetHomeKey();

        [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Pxr_InitHomeKey();

        [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Pxr_GetMRCEnable();

        [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Pxr_SetUserDefinedSettings(UserDefinedSettings settings);

        [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Pxr_Construct(PXR_Loader.ConvertRotationWith2VectorDelegate fromToRotation);

        [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern float Pxr_RefreshRateChanged();

        [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Pxr_GetFocusState();
        [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Pxr_IsSensorReady();

        [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_GetSensorStatus();

        [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Pxr_GetLayerImagePtr(int layerId, EyeType eye, int imageIndex, ref IntPtr image);

        [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern void Pxr_CreateLayerParam(PxrLayerParam layerParam);

        [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern void Pxr_DestroyLayerByRender(int layerId);

        [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern void Pxr_EnableEyeTracking(bool enable);

        [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern void Pxr_EnableFaceTracking(bool enable);

        [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern void Pxr_EnableLipsync(bool enable);

        [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern void Pxr_SetInputDeviceChangedCallBack(InputDeviceChangedCallBack callback);

        [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern void Pxr_SetSeethroughStateChangedCallBack(SeethroughStateChangedCallBack callback);

        [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern void Pxr_SetFitnessBandNumberOfConnectionsCallBack(FitnessBandNumberOfConnectionsCallBack callback);

        [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern void Pxr_SetFitnessBandElectricQuantityCallBack(FitnessBandElectricQuantityCallBack callback);

        [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern void Pxr_SetFitnessBandAbnormalCalibrationDataCallBack(FitnessBandAbnormalCalibrationDataCallBack callback);

        [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern void Pxr_SetLoglevelChangedCallBack(LoglevelChangedCallBack callback);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern void Pxr_SetGraphicOption(GraphicsAPI option);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int Pxr_CreateLayer(IntPtr layerParam);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int Pxr_GetLayerNextImageIndex(int layerId, ref int imageIndex);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int Pxr_GetLayerImageCount(int layerId, EyeType eye, ref UInt32 imageCount);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int Pxr_GetLayerImage(int layerId, EyeType eye, int imageIndex, ref UInt64 image);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_SetConfigIntArray(ConfigType configIndex, int[] configSetData, int dataCount);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_SetConfigFloatArray(ConfigType configIndex, float[] configSetData, int dataCount);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int Pxr_GetLayerAndroidSurface(int layerId, EyeType eye, ref IntPtr androidSurface);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int Pxr_DestroyLayer(int layerId);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int Pxr_SubmitLayer(IntPtr layer);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int Pxr_SubmitLayerQuad(PxrLayerQuad layer);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int Pxr_SubmitLayerQuad2(PxrLayerQuad2 layer);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int Pxr_SubmitLayerCylinder(PxrLayerCylinder layer);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int Pxr_SubmitLayerCylinder2(PxrLayerCylinder2 layer);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int Pxr_SubmitLayerEquirect(PxrLayerEquirect layer);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int Pxr_SubmitLayerEquirect2(PxrLayerEquirect2 layer);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int Pxr_SubmitLayerCube2(PxrLayerCube2 layer);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int Pxr_SubmitLayerEac2(PxrLayerEac2 layer);

        [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern void Pxr_SetLayerBlend(bool enable, PxrLayerBlend layerBlend);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern FoveationLevel Pxr_GetFoveationLevel();

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int Pxr_SetFoveationParams(FoveationParams foveationParams);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_SetFrustum(EyeType eye, float fovLeft, float fovRight, float fovUp, float fovDown, float near, float far);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_GetFrustum(EyeType eye, ref float fovLeft, ref float fovRight, ref float fovUp, ref float fovDown, ref float near, ref float far);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_GetConfigFloat(ConfigType configIndex, ref float value);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_GetConfigInt(ConfigType configIndex, ref int value);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_SetConfigInt(ConfigType configSetIndex, int configSetData);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_SetConfigString(ConfigType configSetIndex, string configSetData);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_SetConfigUint64(ConfigType configSetIndex, UInt64 configSetData);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_ResetSensor(ResetSensorOption option);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_SetSensorLostCustomMode(bool value);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_SetSensorLostCMST(bool value);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Pxr_GetDisplayRefreshRatesAvailable(ref int configCount, ref IntPtr configArray);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_SetDisplayRefreshRate(float refreshRate);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_GetPredictedDisplayTime(ref double predictedDisplayTime);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Pxr_SetExtraLatencyMode(int mode);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_GetTrackingMode(ref UInt64 trackingModeFlags);
        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_SetGLTileRender(bool isTileRender);

        //Tracking Sensor
        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_GetPredictedMainSensorState2(double predictTimeMs, ref PxrSensorState2 sensorState, ref int sensorFrameIndex);

        //Controller
        [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern void Pxr_SetControllerOriginOffset(int controllerID, Vector3 offset);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int Pxr_GetControllerTrackingState(UInt32 deviceID, double predictTime, float[] headSensorData, ref PxrControllerTracking tracking);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int Pxr_SetControllerMainInputHandle(UInt32 deviceID);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int Pxr_GetControllerMainInputHandle(ref int deviceID);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int Pxr_SetControllerVibration(UInt32 deviceID, float strength, int time);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int Pxr_SetControllerEnableKey(bool isEnable, PxrControllerKeyMap Key);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int Pxr_ResetController(UInt32 deviceID);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int Pxr_SetArmModelParameters(PxrGazeType gazetype, PxrArmModelType armmodeltype, float elbowHeight, float elbowDepth, float pointerTiltAngle);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int Pxr_GetControllerHandness(ref int handness);

        //Vibration

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int Pxr_SetControllerVibrationEvent(UInt32 deviceID, int frequency, float strength, int time);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int Pxr_GetControllerCapabilities(UInt32 deviceID, ref PxrControllerCapability capability);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int Pxr_StopControllerVCMotor(int clientId);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int Pxr_StartControllerVCMotor(string file, int slot);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int Pxr_SetControllerAmp(float mode);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int Pxr_SetControllerDelay(int delay);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern string Pxr_GetVibrateDelayTime(ref int length);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int Pxr_StartVibrateBySharemF(float[] data, ref AudioClipData parameter, ref int source_id);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int Pxr_StartVibrateByCache(int clicpid);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int Pxr_ClearVibrateByCache(int clicpid);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int Pxr_StartVibrateByPHF(string data, int buffersize, ref int sourceID, ref VibrateInfo vibrateInfo);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int Pxr_PauseVibrate(int sourceID);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int Pxr_ResumeVibrate(int sourceID);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int Pxr_UpdateVibrateParams(int clicp_id, ref VibrateInfo vibrateInfo);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int Pxr_CreateHapticStream(string phfVersion, UInt32 frameDurationMs, ref VibrateInfo hapticInfo, float speed, ref int id);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int Pxr_WriteHapticStream(int id, ref PxrPhfParamsNum frames, UInt32 numFrames);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int Pxr_SetPHFHapticSpeed(int id, float speed);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int Pxr_GetPHFHapticSpeed(int id, ref float speed);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int Pxr_GetCurrentFrameSequence(int id, ref UInt64 frameSequence);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int Pxr_StartPHFHaptic(int source_id);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int Pxr_StopPHFHaptic(int source_id);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern int Pxr_RemovePHFHaptic(int source_id);

        [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern void Pxr_SetLogInfoActive(bool value);



        //Boundary
        [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_GetSeeThroughState();

        [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Pxr_SetVideoSeethroughState(bool value);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_TestNodeIsInBoundary(BoundaryTrackingNode node, bool isPlayArea, ref PxrBoundaryTriggerInfo info);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_TestPointIsInBoundary(ref PxrVector3f point, bool isPlayArea, ref PxrBoundaryTriggerInfo info);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_GetBoundaryGeometry(bool isPlayArea, UInt32 pointsCountInput, ref UInt32 pointsCountOutput, PxrVector3f[] outPoints);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_GetBoundaryDimensions(bool isPlayArea, out PxrVector3f dimension);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Pxr_GetBoundaryConfigured();

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Pxr_GetBoundaryEnabled();

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_SetBoundaryVisible(bool value);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_SetSeeThroughBackground(bool value);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Pxr_GetBoundaryVisible();
        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Pxr_ResetSensorHard();
        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_GetTrackingState();

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_SetGuardianSystemDisable(bool disable);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_ResumeGuardianSystemForSTS();

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_PauseGuardianSystemForSTS();

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_ShutdownSdkGuardianSystem();

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_GetRoomModeState();

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_DisableBoundary();

        //MRC
        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Pxr_GetMrcStatus();

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_GetMrcPose(ref PxrPosef pose);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_SetMrcPose(ref PxrPosef pose);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_SetIsSupportMovingMrc(bool support);

        //Face tracking
        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_SetTrackingMode(double trackingMode);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_GetFaceTrackingData(Int64 ts, int flags, ref PxrFaceTrackingInfo faceTrackingInfo);
        
        //Application SpaceWarp
        [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Pxr_SetSpaceWarp(int value);

        [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Pxr_SetAppSpacePosition(float x, float y, float z);

        [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Pxr_SetAppSpaceRotation(float x, float y, float z, float w);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_SetTrackingStatus(String key, String value);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_SetPerformanceLevels(PxrPerfSettings which, PxrSettingsLevel level);
        
        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_GetPerformanceLevels(PxrPerfSettings which, ref PxrSettingsLevel level);
      
        //Body tracking
        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_GetBodyTrackingPose(double predictTime, ref BodyTrackerResult bodyTrackerResult);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_SetBodyTrackingMode(int mode);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_GetFitnessBandConnectState(ref PxrFitnessBandConnectState state);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_GetFitnessBandBattery(int trackerId, ref int battery);

        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_GetFitnessBandCalibState(ref int calibrated);
              
        [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Pxr_LogSdkApi(string sdkInfo);

        #endregion

        public static class System
        {
            public static Action RecenterSuccess;
            public static Action FocusStateAcquired;
            public static Action FocusStateLost;
            public static Action SensorReady;
            public static Action<int> InputDeviceChanged;
            public static Action<int> LoglevelChangedChanged;
            public static Action<int> SeethroughStateChangedChanged;
            public static Action<int, int> FitnessBandNumberOfConnections;
            public static Action<int, int> FitnessBandElectricQuantity;
            public static Action<int, int> FitnessBandAbnormalCalibrationData;

            public static bool UPxr_LoadPICOPlugin()
            {
                PLog.d(TAG, "UPxr_Load PICO Plugin");
#if UNITY_ANDROID && !UNITY_EDITOR
                return Pxr_LoadPlugin();
#else  
                return false;
#endif
            }

            public static void UPxr_UnloadPICOPlugin()
            {
                PLog.d(TAG, "UPxr_Unload PICO Plugin");
#if UNITY_ANDROID && !UNITY_EDITOR
                Pxr_UnloadPlugin();
#endif
            }

            public static int UPxr_SetGLTileRender(bool isTileRender)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                Pxr_SetGLTileRender(isTileRender);
#endif
                return 0;
            }

            public static bool UPxr_QueryDeviceAbilities(PxrDeviceAbilities abilities)
            {
                UInt64 flags = UInt64.MinValue;
#if UNITY_ANDROID && !UNITY_EDITOR
                if (UPxr_GetAPIVersion() >= 0x2000304)
                {
                    Pxr_GetTrackingMode(ref flags);
                }
#endif
                switch (abilities)
                {
                    case PxrDeviceAbilities.PxrTrackingModeRotationBit:
                        {
                            return Convert.ToBoolean(flags & 0x00000001);
                        }
                    case PxrDeviceAbilities.PxrTrackingModePositionBit:
                        {
                            return Convert.ToBoolean(flags & 0x00000002);
                        }
                    case PxrDeviceAbilities.PxrTrackingModeEyeBit:
                        {
                            return Convert.ToBoolean(flags & 0x00000004);
                        }
                    case PxrDeviceAbilities.PxrTrackingModeFaceBit:
                        {
                            return Convert.ToBoolean(flags & 0x00000008);
                        }
                    case PxrDeviceAbilities.PxrTrackingModeBroadBandMontorBit:
                        {
                            return Convert.ToBoolean(flags & 0x00000010);
                        }
                    case PxrDeviceAbilities.PxrTrackingModeHandBit:
                        {
                            return Convert.ToBoolean(flags & 0x00000020);
                        }
                    default:
                        throw new ArgumentOutOfRangeException(nameof(abilities), abilities, null);
                }
            }

            public static void UPxr_InitializeFocusCallback()
            {
                Application.onBeforeRender += UPxr_FocusUpdate;
                Application.onBeforeRender += UPxr_SensorReadyStateUpdate;
            }

            public static void UPxr_DeinitializeFocusCallback()
            {
                Application.onBeforeRender -= UPxr_FocusUpdate;
                Application.onBeforeRender -= UPxr_SensorReadyStateUpdate;
            }

            public static void UPxr_SetInputDeviceChangedCallBack(InputDeviceChangedCallBack callback)
            {
#if !UNITY_EDITOR && UNITY_ANDROID
                Pxr_SetInputDeviceChangedCallBack(callback);
#endif
            }

            public static void UPxr_SetSeethroughStateChangedCallBack(SeethroughStateChangedCallBack callback)
            {
#if !UNITY_EDITOR && UNITY_ANDROID
                Pxr_SetSeethroughStateChangedCallBack(callback);
#endif
            }

            public static void UPxr_SetFitnessBandNumberOfConnectionsCallBack(FitnessBandNumberOfConnectionsCallBack callback) {
#if !UNITY_EDITOR && UNITY_ANDROID
                Pxr_SetFitnessBandNumberOfConnectionsCallBack(callback);
#endif
            }

            public static void UPxr_SetFitnessBandAbnormalCalibrationDataCallBack(FitnessBandAbnormalCalibrationDataCallBack callback)
            {
#if !UNITY_EDITOR && UNITY_ANDROID
                Pxr_SetFitnessBandAbnormalCalibrationDataCallBack(callback);
#endif
            }

            public static void UPxr_SetFitnessBandElectricQuantityCallBack(FitnessBandElectricQuantityCallBack callback)
            {
#if !UNITY_EDITOR && UNITY_ANDROID
                Pxr_SetFitnessBandElectricQuantityCallBack(callback);
#endif
            }

            public static void UPxr_SetLoglevelChangedCallBack(LoglevelChangedCallBack callback)
            {
#if !UNITY_EDITOR && UNITY_ANDROID
                Pxr_SetLoglevelChangedCallBack(callback);
#endif
            }

            public static bool UPxr_GetFocusState()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                return Pxr_GetFocusState();
#else
                return false;
#endif
            }

            public static bool UPxr_IsSensorReady()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                return Pxr_IsSensorReady();
#else
                return false;
#endif
            }

            private static bool lastAppFocusState = false;
            private static void UPxr_FocusUpdate()
            {
                bool appfocus = UPxr_GetFocusState();
                if (appfocus && !lastAppFocusState)
                {
                    if (FocusStateAcquired != null)
                    {
                        FocusStateAcquired();
                    }
                }

                if (!appfocus && lastAppFocusState)
                {
                    if (FocusStateLost != null)
                    {
                        FocusStateLost();
                    }
                }

                lastAppFocusState = appfocus;
            }

            private static bool lastSensorReadyState = false;
            private static void UPxr_SensorReadyStateUpdate()
            {
                bool sensorReady = UPxr_IsSensorReady();
                if (sensorReady && !lastSensorReadyState)
                {
                    if (SensorReady != null)
                    {
                        SensorReady();
                    }
                }

                lastSensorReadyState = sensorReady;
            }

            public static string UPxr_GetSDKVersion()
            {
                return PXR_SDK_Version;
            }

            public static int UPxr_LogSdkApi(string sdkInfo)
            {
                PLog.d(TAG, "UPxr_LogSdkApi() sdkInfo:" + sdkInfo);
                int result = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                result = Pxr_LogSdkApi(sdkInfo);
#endif
                PLog.d(TAG, "UPxr_LogSdkApi() result:" + result);
                return result;
            }

            public static float UPxr_GetSystemDisplayFrequency()
            {
                return UPxr_GetConfigFloat(ConfigType.SystemDisplayRate);
            }

            public static float UPxr_GetMrcY()
            {
                if (PXR_Plugin.System.UPxr_GetAPIVersion() >= 0x2000300)
                {
                    return UPxr_GetConfigFloat(ConfigType.PxrMrcPosiyionYOffset);
                }
                else
                {
                    return 0;
                }
            }

            public static double UPxr_GetPredictedDisplayTime()
            {
                PLog.d(TAG, "UPxr_GetPredictedDisplayTime()");
                double predictedDisplayTime = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                Pxr_GetPredictedDisplayTime(ref predictedDisplayTime);
#endif
                PLog.d(TAG, "UPxr_GetPredictedDisplayTime() predictedDisplayTime：" + predictedDisplayTime);
                return predictedDisplayTime;
            }

            public static bool UPxr_SetExtraLatencyMode(int mode)
            {
                PLog.d(TAG, "UPxr_SetExtraLatencyMode() mode:" + mode);
                bool result = false;
#if UNITY_ANDROID && !UNITY_EDITOR
                result = Pxr_SetExtraLatencyMode(mode);
#endif
                PLog.d(TAG, "UPxr_SetExtraLatencyMode() result:" + result);
                return result;
            }

            public static void UPxr_SetUserDefinedSettings(UserDefinedSettings settings)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                Pxr_SetUserDefinedSettings(settings);
#endif
            }

            public static void UPxr_Construct(PXR_Loader.ConvertRotationWith2VectorDelegate fromToRotation)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                Pxr_Construct(fromToRotation);
#endif
            }

            public static bool UPxr_GetHomeKey()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                return Pxr_GetHomeKey();
#endif
                return false;
            }

            public static void UPxr_InitHomeKey()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                Pxr_InitHomeKey();
#endif
            }

            public static bool UPxr_GetMRCEnable()
            {
                bool result = false;
                if (PXR_Plugin.System.UPxr_GetAPIVersion() >= 0x2000300)
                {
#if !UNITY_EDITOR && UNITY_ANDROID
                    result = Pxr_GetMRCEnable();
#endif
                }
                PLog.d(TAG, "UPxr_GetMRCEnable() result:" + result);
                return result;
            }

            public static int UPxr_SetMRCTextureID(UInt64 IDData)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                Debug.Log("ConfigType.MRCTextureID:"+IDData);
                return Pxr_SetConfigUint64(ConfigType.MRCTextureID, IDData);
#else
                return 0;
#endif
            }

            public static int UPxr_SetMRCTextureID2(UInt64 IDData)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                Debug.Log("ConfigType.MRCTextureID2:"+IDData);
                return Pxr_SetConfigUint64(ConfigType.MRCTextureID2, IDData);
#else
                return 0;
#endif
            }

            public static int UPxr_SetMrcTextutrWidth(UInt64 width)
            {
                if (PXR_Plugin.System.UPxr_GetAPIVersion() >= 0x2000300)
                {
#if UNITY_ANDROID && !UNITY_EDITOR
                return Pxr_SetConfigUint64(ConfigType.PxrMrcTextureWidth, width);
#endif
                }
                return 0;
            }

            public static int UPxr_SetMrcTextutrHeight(UInt64 height)
            {
                if (PXR_Plugin.System.UPxr_GetAPIVersion() >= 0x2000300)
                {
#if UNITY_ANDROID && !UNITY_EDITOR
                    return Pxr_SetConfigUint64(ConfigType.PxrMrcTextureHeight, height);
#endif
                }
                return 0;
            }

            public static bool UPxr_GetMrcPose(ref PxrPosef pose)
            {
                int result = 0;
                if (PXR_Plugin.System.UPxr_GetAPIVersion() >= 0x2000300)
                {
#if UNITY_ANDROID && !UNITY_EDITOR
                    result = Pxr_GetMrcPose(ref pose);
#endif
                }

                PLog.d(TAG, "UPxr_GetMrcPose() result:" + result + " pos.x:" + pose.position.x + " pos.y:" + pose.position.y + " pos.z:" + pose.position.z
                    + " ori.x:" + pose.orientation.x + " ori.y:" + pose.orientation.y + " ori.z:" + pose.orientation.z + " ori.w:" + pose.orientation.w);
                return result == 0;
            }

            public static bool UPxr_SetMrcPose(ref PxrPosef pose)
            {
                int result = 0;
                if (PXR_Plugin.System.UPxr_GetAPIVersion() >= 0x2000300)
                {
#if UNITY_ANDROID && !UNITY_EDITOR
                    result = Pxr_SetMrcPose(ref pose);
#endif
                }

                PLog.d(TAG, "UPxr_SetMrcPose() result:" + result + " pos.x:" + pose.position.x + " pos.y:" + pose.position.y + " pos.z:" + pose.position.z
                    + " ori.x:" + pose.orientation.x + " ori.y:" + pose.orientation.y + " ori.z:" + pose.orientation.z + " ori.w:" + pose.orientation.w);
                return result == 0;
            }

            public static void UPxr_SetIsSupportMovingMrc(bool support)
            {
                if (PXR_Plugin.System.UPxr_GetAPIVersion() >= 0x2000306)
                {
#if UNITY_ANDROID && !UNITY_EDITOR
                Pxr_SetIsSupportMovingMrc(support);
#endif
                }
            }

            public static bool UPxr_GetMrcStatus()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
            if(PXR_Plugin.System.UPxr_GetAPIVersion()>= 0x2000300) {
                return Pxr_GetMrcStatus();
            }else{
                return false;
            }
#else
                return false;
#endif
            }

            public static void UPxr_EnableEyeTracking(bool enable)
            {
                Debug.Log(TAG + "UPxr_EnableEyeTracking() enable:" + enable);
#if UNITY_ANDROID && !UNITY_EDITOR
                Pxr_EnableEyeTracking(enable);
#endif
            }

            public static void UPxr_EnableFaceTracking(bool enable)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                Pxr_EnableFaceTracking(enable);
#endif
            }

            public static void UPxr_EnableLipSync(bool enable){
#if UNITY_ANDROID && !UNITY_EDITOR
                Pxr_EnableLipsync(enable);
#endif
            }

            public static int UPxr_GetFaceTrackingData(Int64 ts, int flags, ref PxrFaceTrackingInfo faceTrackingInfo)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                if(PXR_Plugin.System.UPxr_GetAPIVersion()>= 0x2000309) {
                    Pxr_GetFaceTrackingData(ts, flags, ref faceTrackingInfo );
                }
#endif
                return 0;
            }

            public static int UPxr_SetFaceTrackingStatus(PxrFtLipsyncValue value) {
                int num = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                if(PXR_Plugin.System.UPxr_GetAPIVersion()>= 0x200030A) {
                    
                    num = Pxr_SetTrackingStatus("ft_lipsync_ctl", ((int)value).ToString());
                }
#endif
                return num;
            }


            private const string TAG = "[PXR_Plugin/System]";
#if UNITY_ANDROID && !UNITY_EDITOR
            private static AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            private static AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            private static AndroidJavaClass sysActivity = new AndroidJavaClass("com.psmart.aosoperation.SysActivity");
            private static AndroidJavaClass batteryReceiver = new AndroidJavaClass("com.psmart.aosoperation.BatteryReceiver");
            private static AndroidJavaClass audioReceiver = new AndroidJavaClass("com.psmart.aosoperation.AudioReceiver");
#endif
            public static string UPxr_GetDeviceMode()
            {
                string devicemode = "";
#if UNITY_ANDROID && !UNITY_EDITOR
            devicemode = SystemInfo.deviceModel;
#endif
                return devicemode;
            }

            public static float UPxr_GetConfigFloat(ConfigType type)
            {
                PLog.d(TAG, "UPxr_GetConfigFloat() type:" + type);
                float value = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                Pxr_GetConfigFloat(type, ref value);
#endif
                PLog.d(TAG, "UPxr_GetConfigFloat() value:" + value);
                return value;
            }

            public static int UPxr_GetConfigInt(ConfigType type)
            {
                PLog.d(TAG, "UPxr_GetConfigInt() type:" + type);
                int value = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                Pxr_GetConfigInt(type, ref value);
#endif
                PLog.d(TAG, "UPxr_GetConfigInt() value:" + value);
                return value;
            }

            public static int UPxr_SetConfigInt(ConfigType configSetIndex, int configSetData)
            {
                PLog.d(TAG, "UPxr_SetConfigInt() configSetIndex:" + configSetIndex + " configSetData:" + configSetData);
                int result = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                result = Pxr_SetConfigInt(configSetIndex, configSetData);
#endif
                PLog.d(TAG, "UPxr_SetConfigInt() result:" + result);
                return result;
            }

            public static int UPxr_ContentProtect(int data)
            {
                int num = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                num = Pxr_SetConfigInt(ConfigType.EnableCPT, data);
#endif
                return num;
            }

            public static int UPxr_SetConfigString(ConfigType configSetIndex, string configSetData)
            {
                PLog.d(TAG, "UPxr_SetConfigString() configSetIndex:" + configSetIndex + " configSetData:" + configSetData);
                int result = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                result = Pxr_SetConfigString(configSetIndex, configSetData);
#endif
                PLog.d(TAG, "UPxr_SetConfigString() result:" + result);
                return result;
            }

            public static int UPxr_SetSystemDisplayFrequency(float rate)
            {
                PLog.d(TAG, "UPxr_SetDisplayRefreshRate() rate:" + rate);
                int result = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                result = Pxr_SetDisplayRefreshRate(rate);
#endif
                PLog.d(TAG, "UPxr_SetDisplayRefreshRate() result:" + result);
                return result;
            }

            public static int UPxr_SetPerformanceLevels(PxrPerfSettings which, PxrSettingsLevel level)
            {
                PLog.d(TAG, "UPxr_SetPerformanceLevels() which:" + which + ", level:" + level);
                int result = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                if (PXR_Plugin.System.UPxr_GetAPIVersion() >= 0x200030A)
                {
                    result = Pxr_SetPerformanceLevels(which, level);
                }
#endif
                PLog.d(TAG, "UPxr_SetPerformanceLevels() result:" + result);
                return result;
            }
            
            public static PxrSettingsLevel UPxr_GetPerformanceLevels(PxrPerfSettings which)
            {
                PLog.d(TAG, "UPxr_GetPerformanceLevels() which:" + which);
                int result = 0;
                PxrSettingsLevel level = PxrSettingsLevel.POWER_SAVINGS;
#if UNITY_ANDROID && !UNITY_EDITOR
                if (PXR_Plugin.System.UPxr_GetAPIVersion() >= 0x200030A)
                {
                    result = Pxr_GetPerformanceLevels(which, ref level);
                }
#endif
                PLog.d(TAG, "UPxr_GetPerformanceLevels() result:" + result + ", level:" + level);
                return level;
            }

            public static string UPxr_GetDeviceSN()
            {
                string serialNum = "UNKONWN";
#if UNITY_ANDROID && !UNITY_EDITOR
                serialNum = sysActivity.CallStatic<string>("getDeviceSN");
#endif
                return serialNum;
            }

            public static void UPxr_Sleep()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                sysActivity.CallStatic("pxr_Sleep");
#endif
            }

            public static void UPxr_SetSecure(bool isOpen)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                sysActivity.CallStatic("SetSecure",currentActivity,isOpen);
#endif
            }

            public static int UPxr_GetColorRes(string name)
            {
                int value = -1;
#if UNITY_ANDROID && !UNITY_EDITOR
                try
                {
                    value = sysActivity.CallStatic<int>("getColorRes", currentActivity, name);
                }
                catch (Exception e)
                {
                    PLog.e(TAG, "GetColorResError :" + e.ToString());
                }
#endif
                return value;
            }

            public static int UPxr_GetConfigInt(string name)
            {
                int value = -1;
#if UNITY_ANDROID && !UNITY_EDITOR
                try
                {
                    value = sysActivity.CallStatic<int>("getConfigInt", currentActivity, name);
                }
                catch (Exception e)
                {
                    PLog.e(TAG, "GetConfigIntError :" + e.ToString());
                }
#endif
                return value;
            }

            public static string UPxr_GetConfigString(string name)
            {
                string value = "";
#if UNITY_ANDROID && !UNITY_EDITOR
                try
                {
                    value = sysActivity.CallStatic<string>("getConfigString", currentActivity, name);
                }
                catch (Exception e)
                {
                    PLog.e(TAG, "GetConfigStringError :" + e.ToString());
                }
#endif
                return value;
            }

            public static string UPxr_GetDrawableLocation(string name)
            {
                string value = "";
#if UNITY_ANDROID && !UNITY_EDITOR
                try
                {
                    value = sysActivity.CallStatic<string>("getDrawableLocation", currentActivity, name);
                }
                catch (Exception e)
                {
                    PLog.e(TAG, "GetDrawableLocationError :" + e.ToString());
                }
#endif
                return value;
            }

            public static int UPxr_GetTextSize(string name)
            {
                int value = -1;
#if UNITY_ANDROID && !UNITY_EDITOR
                try
                {
                    value = sysActivity.CallStatic<int>("getTextSize", currentActivity, name);
                }
                catch (Exception e)
                {
                    PLog.e(TAG, "GetTextSizeError :" + e.ToString());
                }
#endif
                return value;
            }

            public static string UPxr_GetLangString(string name)
            {
                string value = "";
#if UNITY_ANDROID && !UNITY_EDITOR
                try
                {
                    value = sysActivity.CallStatic<string>("getLangString", currentActivity, name);
                }
                catch (Exception e)
                {
                    PLog.e(TAG, "GetLangStringError :" + e.ToString());
                }
#endif
                return value;
            }

            public static string UPxr_GetStringValue(string id, int type)
            {
                string value = "";
#if UNITY_ANDROID && !UNITY_EDITOR
                try
                {
                    value = sysActivity.CallStatic<string>("getStringValue", currentActivity, id, type);
                }
                catch (Exception e)
                {
                    PLog.e(TAG, "GetStringValueError :" + e.ToString());
                }
#endif
                return value;
            }

            public static int UPxr_GetIntValue(string id, int type)
            {
                int value = -1;
#if UNITY_ANDROID && !UNITY_EDITOR
                try
                {
                    value = sysActivity.CallStatic<int>("getIntValue", currentActivity, id, type);
                }
                catch (Exception e)
                {
                    PLog.e(TAG, "GetIntValueError :" + e.ToString());
                }
#endif
                return value;
            }

            public static float UPxr_GetFloatValue(string id)
            {
                float value = -1;
#if UNITY_ANDROID && !UNITY_EDITOR
                try
                {
                    value = sysActivity.CallStatic<float>("getFloatValue", currentActivity, id);
                }
                catch (Exception e)
                {
                    PLog.e(TAG, "GetFloatValueError :" + e.ToString());
                }
#endif
                return value;
            }

            public static string UPxr_GetObjectOrArray(string id, int type)
            {
                string value = "";
#if UNITY_ANDROID && !UNITY_EDITOR
                try
                {
                    value = sysActivity.CallStatic<string>("getObjectOrArray", currentActivity, id, type);
                }
                catch (Exception e)
                {
                    PLog.e(TAG, "GetObjectOrArrayError :" + e.ToString());
                }
#endif
                return value;
            }

            public static int UPxr_GetCharSpace(string id)
            {
                int value = -1;
#if UNITY_ANDROID && !UNITY_EDITOR
                try
                {
                    value = sysActivity.CallStatic<int>("getCharSpace", currentActivity, id);
                }
                catch (Exception e)
                {
                    PLog.e(TAG, "GetCharSpaceError :" + e.ToString());
                }
#endif
                return value;
            }

            public static float UPxr_RefreshRateChanged()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                return Pxr_RefreshRateChanged();
#else
                return -1.0f;
#endif
            }

            public static float[] UPxr_GetDisplayFrequenciesAvailable()
            {
                float[] configArray = null;
#if UNITY_ANDROID && !UNITY_EDITOR
                int configCount = 0;
                IntPtr configHandle = IntPtr.Zero;
                Pxr_GetDisplayRefreshRatesAvailable(ref configCount, ref configHandle);
                configArray = new float[configCount];
                Marshal.Copy(configHandle, configArray, 0, configCount);
                for (int i = 0; i < configCount; i++) {
                    Debug.Log("LLRR: UPxr_GetDisplayFrequenciesAvailable " + configArray[i]);
                }
#endif
                return configArray;
            }

            public static int UPxr_GetSensorStatus()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                return Pxr_GetSensorStatus();
#else
                return 0;
#endif
            }

            public static int UPxr_GetPredictedMainSensorStateNew(ref PxrSensorState2 sensorState, ref int sensorFrameIndex)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                if(UPxr_GetAPIVersion() >= 0x2000201){
                    double predictTime = UPxr_GetPredictedDisplayTime();
                    return Pxr_GetPredictedMainSensorState2(predictTime, ref sensorState, ref sensorFrameIndex);
                }else
                {
                    return 0;
                }
#else
                return 0;
#endif
            }

            public static int UPxr_GetAPIVersion()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                if (PXR_API_Version < 0x0000001)
                {
                    PXR_API_Version = UPxr_GetConfigInt(ConfigType.PxrAPIVersion);
                    PLog.i(TAG, "API xrVersion :0x" + PXR_API_Version.ToString("X2"));
                }
                return PXR_API_Version;
#else
                return 0;
#endif
            }

            public static void UPxr_SetLogInfoActive(bool value)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                Pxr_SetLogInfoActive(value);
#endif
            }

            public static void UPxr_OpenFitnessBandCalibrationAPP()
            {
                UPxr_OpenFitnessBandCalibrationPackage("com.pvr.swift");
            }

            public static void UPxr_OpenPackage(string pkgName)
            {
                AndroidJavaObject activity;
                AndroidJavaClass UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                activity = UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

                using (AndroidJavaObject joPackageManager = activity.Call<AndroidJavaObject>("getPackageManager"))
                {
                    using (AndroidJavaObject joIntent = joPackageManager.Call<AndroidJavaObject>("getLaunchIntentForPackage", pkgName))
                    {
                        if (null != joIntent)
                        {
                            activity.Call("startActivity", joIntent);
                        }
                        else
                        {
                            Debug.Log("This software is not installed: " + pkgName);
                        }
                    }
                }
            }

            public static void UPxr_OpenFitnessBandCalibrationPackage(string pkgName)
            {
                using (AndroidJavaClass jcPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
                {
                    using (AndroidJavaObject joActivity = jcPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
                    {
                        using (AndroidJavaObject joPackageManager = joActivity.Call<AndroidJavaObject>("getPackageManager"))
                        {
                            using (AndroidJavaObject joIntent = joPackageManager.Call<AndroidJavaObject>("getLaunchIntentForPackage", pkgName))
                            {
                                if (null != joIntent)
                                {
                                    //AndroidJavaObject joNIntent = joIntent.Call<AndroidJavaObject>("addFlags", joIntent.GetStatic<int>("FLAG_ACTIVITY_REORDER_TO_FRONT"));
                                    AndroidJavaObject joNIntent = joIntent.Call<AndroidJavaObject>("putExtra", "enter_flag", Application.identifier);
                                    joActivity.Call("startActivity", joNIntent);
                                    joIntent.Dispose();
                                }
                                else
                                {
                                    string msg = "Package <" + pkgName + "> not exsits on device.";
                                    Debug.Log(msg);

                                    using (AndroidJavaClass jT = new AndroidJavaClass("android.widget.Toast"))
                                    {
                                        using (AndroidJavaObject jMsg = new AndroidJavaObject("java.lang.String", msg))
                                        {
                                            using (AndroidJavaObject jC = joActivity.Call<AndroidJavaObject>("getApplicationContext"))
                                            {
                                                int length = jT.GetStatic<int>("LENGTH_SHORT");
                                                using (AndroidJavaObject toast = jT.CallStatic<AndroidJavaObject>("makeText", jC, jMsg, length))
                                                {
                                                    toast.Call("show");
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }


        }

        public static class Boundary
        {
            private const string TAG = "[PXR_Plugin/Boundary]";

            public static PxrBoundaryTriggerInfo UPxr_TestNodeIsInBoundary(BoundaryTrackingNode node, BoundaryType boundaryType)
            {
                PxrBoundaryTriggerInfo testResult = new PxrBoundaryTriggerInfo();
#if UNITY_ANDROID && !UNITY_EDITOR
                Pxr_TestNodeIsInBoundary(node, boundaryType == BoundaryType.PlayArea, ref testResult);
                testResult.closestPoint.z = -testResult.closestPoint.z;
                testResult.closestPointNormal.z = -testResult.closestPointNormal.z;
                if (!testResult.valid)
                {
                    PLog.d(TAG, string.Format("Pxr_TestBoundaryNode({0}, {1}) API call failed!", node, boundaryType));
                }
#endif
                return testResult;
            }

            public static PxrBoundaryTriggerInfo UPxr_TestPointIsInBoundary(PxrVector3f point, BoundaryType boundaryType)
            {
                PxrBoundaryTriggerInfo testResult = new PxrBoundaryTriggerInfo();
#if UNITY_ANDROID && !UNITY_EDITOR
                Pxr_TestPointIsInBoundary(ref point, boundaryType == BoundaryType.PlayArea, ref testResult);

                if (!testResult.valid)
                {
                    PLog.d(TAG, string.Format("Pxr_TestBoundaryPoint({0}, {1}) API call failed!", point, boundaryType));
                }

#endif
                return testResult;
            }

            public static Vector3[] UPxr_GetBoundaryGeometry(BoundaryType boundaryType)
            {
                Vector3[] points = new Vector3[1];
#if UNITY_ANDROID && !UNITY_EDITOR

                UInt32 pointsCountOutput = 0;
                PxrVector3f[] outPointsFirst = null;
                Pxr_GetBoundaryGeometry(boundaryType == BoundaryType.PlayArea, 0, ref pointsCountOutput, outPointsFirst);
                if (pointsCountOutput <= 0)
                {
                    PLog.d(TAG, "Boundary geometry point count = " + pointsCountOutput);
                    return null;
                }

                PxrVector3f[] outPoints = new PxrVector3f[pointsCountOutput];
                Pxr_GetBoundaryGeometry(boundaryType == BoundaryType.PlayArea, pointsCountOutput, ref pointsCountOutput, outPoints);

                points = new Vector3[pointsCountOutput];
                for (int i = 0; i < pointsCountOutput; i++)
                {
                    points[i] = new Vector3()
                    {
                        x = outPoints[i].x,
                        y = outPoints[i].y,
                        z = -outPoints[i].z,
                    };
                }
#endif
                return points;
            }

            public static Vector3 UPxr_GetBoundaryDimensions(BoundaryType boundaryType)
            {
                // float x = 0, y = 0, z = 0;
                PxrVector3f dimension = new PxrVector3f();
#if UNITY_ANDROID && !UNITY_EDITOR
                int ret = 0;
                Pxr_GetBoundaryDimensions( boundaryType == BoundaryType.PlayArea, out dimension);
#endif
                return new Vector3(dimension.x, dimension.y, dimension.z);
            }

            public static void UPxr_SetBoundaryVisiable(bool value)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                Pxr_SetBoundaryVisible(value);
#endif
            }

            public static bool UPxr_GetBoundaryVisiable()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                return Pxr_GetBoundaryVisible();
#else
                return true;
#endif
            }

            public static bool UPxr_GetBoundaryConfigured()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                return Pxr_GetBoundaryConfigured();
#else
                return true;
#endif
            }

            public static bool UPxr_GetBoundaryEnabled()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                return Pxr_GetBoundaryEnabled();
#else
                return true;
#endif
            }

            public static int UPxr_SetSeeThroughBackground(bool value)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                return Pxr_SetSeeThroughBackground(value);
#else
                return 0;
#endif
            }

            public static int UPxr_GetSeeThroughState()
            {
                var state = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                try
                {
                    state = Pxr_GetSeeThroughState();
                    PLog.d(TAG, "UPxr_GetSeeThroughState() state:" + state);
                }
                catch (Exception e)
                {
                    Debug.Log("PXRLog UPxr_GetSeeThroughState :" + e.ToString());
                }
#endif
                return state;
            }

            public static void UPxr_SetSeeThroughState(bool value)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                Pxr_SetVideoSeethroughState(value);
#endif
            }

            public static void UPxr_ResetSeeThroughSensor()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                if (PXR_Plugin.System.UPxr_GetAPIVersion() >= 0x2000301)
                {
                    Pxr_ResetSensorHard();
                }
#endif
            }

            public static PxrTrackingState UPxr_GetSeeThroughTrackingState()
            {
                int state = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                if (PXR_Plugin.System.UPxr_GetAPIVersion() >= 0x2000301)
                {
                    state = Pxr_GetTrackingState();
                }
#endif
                return (PxrTrackingState)state;
            }

            public static int UPxr_SetGuardianSystemDisable(bool value)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                return Pxr_SetGuardianSystemDisable(value);
#else
                return 0;
#endif
            }

            public static int UPxr_ResumeGuardianSystemForSTS()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                return Pxr_ResumeGuardianSystemForSTS();
#else
                return 0;
#endif
            }

            public static int UPxr_PauseGuardianSystemForSTS()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                return Pxr_PauseGuardianSystemForSTS();
#else
                return 0;
#endif
            }

            public static int UPxr_ShutdownSdkGuardianSystem()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                return Pxr_ShutdownSdkGuardianSystem();
#else
                return 0;
#endif
            }

            public static int UPxr_GetRoomModeState()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                return Pxr_GetRoomModeState();
#else
                return 0;
#endif
            }

            public static int UPxr_DisableBoundary()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                return Pxr_DisableBoundary();
#else
                return 0;
#endif
            }

        }

        public static class Render
        {
            private const string TAG = "[PXR_Plugin/Render]";

            public static void UPxr_SetFoveationLevel(FoveationLevel level)
            {
                PLog.d(TAG, "UPxr_SetFoveationLevel() level:" + level);
#if UNITY_ANDROID && !UNITY_EDITOR
                Pxr_SetFoveationLevelEnable((int)level);
#endif
            }

            public static void UPxr_SetFFRSubsampled(bool enable)
            {
                PLog.d(TAG, "UPxr_SetFFRSubsampled() level:" + enable);
#if UNITY_ANDROID && !UNITY_EDITOR
                Pxr_SetFFRSubsampled(enable);
#endif
            }

            public static FoveationLevel UPxr_GetFoveationLevel()
            {
                FoveationLevel result = FoveationLevel.None;
#if UNITY_ANDROID && !UNITY_EDITOR
                result = Pxr_GetFoveationLevel();
#endif
                PLog.d(TAG, "UPxr_GetFoveationLevel() result:" + result);
                return result;
            }

            public static int UPxr_SetFoveationParameters(float foveationGainX, float foveationGainY, float foveationArea, float foveationMinimum)
            {
                PLog.d(TAG, "UPxr_SetFoveationParameters() foveationGainX:" + foveationGainX + " foveationGainY:" + foveationGainY + " foveationArea:" + foveationArea + " foveationMinimum:" + foveationMinimum);
                int result = 0;
#if UNITY_ANDROID && !UNITY_EDITOR

                FoveationParams foveationParams = new FoveationParams();
                foveationParams.foveationGainX = foveationGainX;
                foveationParams.foveationGainY = foveationGainY;
                foveationParams.foveationArea = foveationArea;
                foveationParams.foveationMinimum = foveationMinimum;

                result = Pxr_SetFoveationParams(foveationParams);
#endif
                PLog.d(TAG, "UPxr_SetFoveationParameters() result:" + result);
                return result;
            }

            public static int UPxr_GetFrustum(EyeType eye, ref float fovLeft, ref float fovRight, ref float fovUp, ref float fovDown, ref float near, ref float far)
            {
                int result = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                result = Pxr_GetFrustum(eye, ref fovLeft, ref fovRight, ref fovUp, ref fovDown, ref near, ref far);
#endif
                PLog.d(TAG, "UPxr_GetFrustum() result:" + result + " eye:" + eye + " fovLeft:" + fovLeft + " fovRight:" + fovRight + " fovUp:" + fovUp + " fovDown:" + fovDown + " near:" + near + " far:" + far);
                return result;
            }

            public static int UPxr_SetFrustum(EyeType eye, float fovLeft, float fovRight, float fovUp, float fovDown, float near, float far)
            {
                int result = 1;
#if UNITY_ANDROID && !UNITY_EDITOR
                result = Pxr_SetFrustum(eye, fovLeft, fovRight, fovUp, fovDown, near, far);
#endif
                PLog.d(TAG, "UPxr_SetFrustum() result:" + result + " eye:" + eye + " fovLeft:" + fovLeft + " fovRight:" + fovRight + " fovUp:" + fovUp + " fovDown:" + fovDown + " near:" + near + " far:" + far);
                return result;
            }
            
            public static int UPxr_SetEyeFOV(EyeType eye, float fovLeft, float fovRight, float fovUp, float fovDown)
            {
                int result = 0;
                ConfigType type;
                switch (eye)
                {
                    case EyeType.EyeLeft:
                        type = ConfigType.PxrLeftEyeFOV;
                        break;
                    case EyeType.EyeRight:
                        type = ConfigType.PxrRightEyeFOV;
                        break;
                    default:
                        type = ConfigType.PxrBothEyeFOV;
                        break;
                }

                float[] fovData = new float[4];
                fovData[0] = -Mathf.Deg2Rad * fovLeft;
                fovData[1] = Mathf.Deg2Rad * fovRight;
                fovData[2] = Mathf.Deg2Rad * fovUp;
                fovData[3] = -Mathf.Deg2Rad * fovDown;

#if UNITY_ANDROID && !UNITY_EDITOR
                if (PXR_Plugin.System.UPxr_GetAPIVersion() >= 0x2000300)
                {
                    result = Pxr_SetConfigFloatArray(type, fovData, 4);
                }
#endif
                PLog.d(TAG, string.Format("UPxr_SetEyeFOV Pxr_SetConfigFloatArray type = {0}, fovData[0] = {1},  fovData[1] = {2},  fovData[2] = {3},  fovData[3] = {4}, result = {5}", type, fovData[0], fovData[1], fovData[2], fovData[3], result));
                return result;
            }

            public static void UPxr_CreateLayer(IntPtr layerParam)
            {
                PLog.d(TAG, "UPxr_CreateLayer() ");
#if UNITY_ANDROID && !UNITY_EDITOR
                Pxr_CreateLayer(layerParam);
#endif
            }

            public static void UPxr_CreateLayerParam(PxrLayerParam layerParam)
            {
                PLog.d(TAG, "UPxr_CreateLayerParam() ");
#if UNITY_ANDROID && !UNITY_EDITOR
                Pxr_CreateLayerParam(layerParam);
#endif
            }

            public static int UPxr_GetLayerNextImageIndex(int layerId, ref int imageIndex)
            {
                int result = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                result = Pxr_GetLayerNextImageIndex(layerId, ref imageIndex);
#endif
                PLog.d(TAG, "UPxr_GetLayerNextImageIndex() layerId:" + layerId + " imageIndex:" + imageIndex + " result:" + result);
                return result;
            }

            public static int UPxr_GetLayerImageCount(int layerId, EyeType eye, ref UInt32 imageCount)
            {
                int result = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                result = Pxr_GetLayerImageCount(layerId, eye, ref imageCount);
#endif
                PLog.d(TAG, "UPxr_GetLayerImageCount() layerId:" + layerId + " eye:" + eye + " imageCount:" + imageCount + " result:" + result);
                return result;
            }

            public static int UPxr_GetLayerImage(int layerId, EyeType eye, int imageIndex, ref UInt64 image)
            {
                int result = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                result = Pxr_GetLayerImage(layerId, eye, imageIndex, ref image);
#endif
                PLog.d(TAG, "UPxr_GetLayerImage() layerId:" + layerId + " eye:" + eye + " imageIndex:" + imageIndex + " image:" + image + " result:" + result);
                return result;
            }

            public static void UPxr_GetLayerImagePtr(int layerId, EyeType eye, int imageIndex, ref IntPtr image)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                Pxr_GetLayerImagePtr(layerId, eye, imageIndex, ref image);
#endif
                PLog.d(TAG, "UPxr_GetLayerImagePtr() layerId:" + layerId + " eye:" + eye + " imageIndex:" + imageIndex + " image:" + image);
            }

            public static int UPxr_SetConfigIntArray(int[] configSetData)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                if (PXR_Plugin.System.UPxr_GetAPIVersion() >= 0x2000300)
                {
                    return Pxr_SetConfigIntArray(ConfigType.PxrAndroidLayerDimensions, configSetData, 3);
                }
#endif
                return 0;
            }

            public static int UPxr_SetConfigFloatArray(ConfigType configIndex, float[] configSetData, int dataCount)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                if (PXR_Plugin.System.UPxr_GetAPIVersion() >= 0x2000300)
                {
                    return Pxr_SetConfigFloatArray(configIndex, configSetData, dataCount);
                }
#endif
                return 0;
            }

            public static int UPxr_GetLayerAndroidSurface(int layerId, EyeType eye, ref IntPtr androidSurface)
            {
                int result = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                result = Pxr_GetLayerAndroidSurface(layerId, eye, ref androidSurface);
#endif
                PLog.d(TAG, "UPxr_GetLayerAndroidSurface() layerId:" + layerId + " eye:" + eye + " androidSurface:" + androidSurface + " result:" + result);
                return result;
            }

            public static int UPxr_DestroyLayer(int layerId)
            {
                int result = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                result = Pxr_DestroyLayer(layerId);
#endif
                PLog.d(TAG, "UPxr_DestroyLayer() layerId:" + layerId + " result:" + result);
                return result;
            }

            public static void UPxr_DestroyLayerByRender(int layerId)
            {
                PLog.d(TAG, "UPxr_DestroyLayerByRender() layerId:" + layerId);
#if UNITY_ANDROID && !UNITY_EDITOR
                Pxr_DestroyLayerByRender(layerId);
#endif
            }

            public static int UPxr_SubmitLayer(IntPtr layer)
            {
                int result = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                result = Pxr_SubmitLayer(layer);
#endif
                PLog.d(TAG, "UPxr_SubmitLayer() layer:" + layer + " result:" + result);
                return result;
            }

            public static int UPxr_SubmitLayerQuad(PxrLayerQuad layer)
            {
                int result = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                result = Pxr_SubmitLayerQuad(layer);
#endif
                PLog.d(TAG, "UPxr_SubmitLayerQuad() layer:" + layer + " result:" + result);
                return result;
            }

            public static bool UPxr_SubmitLayerQuad2(PxrLayerQuad2 layer)
            {
                int result = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                result = Pxr_SubmitLayerQuad2(layer);
#endif
                PLog.d(TAG, "UPxr_SubmitLayerQuad2() layer:" + layer + " result:" + result);
                return result == -8;
            }

            public static int UPxr_SubmitLayerCylinder(PxrLayerCylinder layer)
            {
                int result = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                result = Pxr_SubmitLayerCylinder(layer);
#endif
                PLog.d(TAG, "UPxr_SubmitLayerCylinder() layer:" + layer + " result:" + result);
                return result;
            }

            public static bool UPxr_SubmitLayerCylinder2(PxrLayerCylinder2 layer)
            {
                int result = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                result = Pxr_SubmitLayerCylinder2(layer);
#endif
                PLog.d(TAG, "UPxr_SubmitLayerCylinder2() layer:" + layer + " result:" + result);
                return result == -8;
            }

            public static bool UPxr_SubmitLayerEquirect(PxrLayerEquirect layer) // shape 3
            {
                int result = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                result = Pxr_SubmitLayerEquirect(layer);
#endif
                PLog.d(TAG, "UPxr_SubmitLayerEquirect() layer:" + layer + " result:" + result);
                return result == -8;
            }

            public static bool UPxr_SubmitLayerEquirect2(PxrLayerEquirect2 layer) // shape 4
            {
                int result = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                result = Pxr_SubmitLayerEquirect2(layer);
#endif
                PLog.d(TAG, "UPxr_SubmitLayerEquirect2() layer:" + layer + " result:" + result);
                return result == -8;
            }

            public static int UPxr_SubmitLayerCube2(PxrLayerCube2 layer)
            {
                int result = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                result = Pxr_SubmitLayerCube2(layer);
#endif
                PLog.d(TAG, "UPxr_SubmitLayerCube2() layer:" + layer + " result:" + result);
                return result;
            }

            public static int UPxr_SubmitLayerEac2(PxrLayerEac2 layer)
            {
                int result = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                result = Pxr_SubmitLayerEac2(layer);
#endif
                PLog.d(TAG, "UPxr_SubmitLayerEac2() layer:" + layer + " result:" + result);
                return result;
            }

            public static void UPxr_SetLayerBlend(bool enable, PxrLayerBlend layerBlend)
            {
                PLog.d(TAG, "UPxr_SetLayerBlend() enable:" + enable + " layerBlend.srcColor:" + layerBlend.srcColor + " dstColor:" + layerBlend.dstColor + " srcAlpha:" + layerBlend.srcAlpha + " dstAlpha:" + layerBlend.dstAlpha);
#if UNITY_ANDROID && !UNITY_EDITOR
                Pxr_SetLayerBlend(enable, layerBlend);
#endif
            }

            public static void UPxr_SetSpaceWarp(bool value)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                Pxr_SetSpaceWarp(value?1:0);
#endif
                PLog.d(TAG, "UPxr_SetSpaceWarp " + value);
            }

            public static void UPxr_SetAppSpacePosition(float x, float y, float z)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                Pxr_SetAppSpacePosition(x, y, z);
#endif
            }

            public static void UPxr_SetAppSpaceRotation(float x, float y, float z, float w)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                Pxr_SetAppSpaceRotation(x, y, z, w);
#endif
            }
        }

        public static class Sensor
        {
            private const string TAG = "[PXR_Plugin/Sensor]";

#if UNITY_ANDROID && !UNITY_EDITOR
            private static AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            private static AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            private static AndroidJavaClass sysActivity = new AndroidJavaClass("com.psmart.aosoperation.SysActivity");
#endif

            public static int UPxr_ResetSensor(ResetSensorOption resetSensorOption)
            {
                PLog.d(TAG, string.Format("UPxr_ResetSensor : {0}", resetSensorOption));
                int result = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                result = Pxr_ResetSensor(resetSensorOption);
#endif
                PLog.d(TAG, string.Format("UPxr_ResetSensor result: {0}", result));
                return result;
            }

            public static int UPvr_Enable6DofModule(bool enable)
            {
                PLog.d(TAG, string.Format("UPvr_Enable6DofModule : {0}", enable));
#if UNITY_ANDROID && !UNITY_EDITOR
                return Pxr_SetConfigInt(ConfigType.Ability6Dof, enable?1:0);
#else
                return 0;
#endif
            }

            public static void UPxr_InitPsensor()
            {
                PLog.d(TAG, "UPxr_InitPsensor()");
#if UNITY_ANDROID && !UNITY_EDITOR
                try
                {
                    sysActivity.CallStatic("initPsensor", currentActivity);
                }
                catch (Exception e)
                {
                    PLog.e(TAG, "Error :" + e.ToString());
                }
#endif
            }

            public static int UPxr_GetPSensorState()
            {
                PLog.d(TAG, "UPxr_GetPSensorState()");
                int psensor = -1;
#if UNITY_ANDROID && !UNITY_EDITOR
                try
                {
                    psensor = sysActivity.CallStatic<int>("getPsensorState");
                }
                catch (Exception e)
                {
                    PLog.e(TAG, "Error :" + e.ToString());
                }
#endif
                PLog.d(TAG, "UPxr_GetPSensorState() psensor:" + psensor);
                return psensor;
            }

            public static void UPxr_UnregisterPsensor()
            {
                PLog.d(TAG, "UPxr_UnregisterPsensor()");
#if UNITY_ANDROID && !UNITY_EDITOR
                try
                {
                    sysActivity.CallStatic("unregisterListener");
                }
                catch (Exception e)
                {
                    PLog.e(TAG, "Error :" + e.ToString());
                }
#endif
            }

            public static int UPxr_SetSensorLostCustomMode(bool value)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                return Pxr_SetSensorLostCustomMode(value);
#else
                return 0;
#endif
            }

            public static int UPxr_SetSensorLostCMST(bool value)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                return Pxr_SetSensorLostCMST(value);
#else
                return 0;
#endif
            }

        }

        public static class PlatformSetting
        {
            private const string TAG = "[PXR_Plugin/PlatformSetting]";

#if UNITY_ANDROID && !UNITY_EDITOR
            private static AndroidJavaClass MRCCalibration = new AndroidJavaClass("com.psmart.aosoperation.MRCCalibration");
            private static AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            private static AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
#endif

            public static float[] UPxr_MRCCalibration(string path)
            {
                float[] MRCdata = new float[10];
                if (PXR_Plugin.System.UPxr_GetAPIVersion() >= 0x2000300)
                {
                    AndroidJavaObject MrcCalibration = new AndroidJavaObject("com.psmart.aosoperation.MRCCalibration");
#if UNITY_ANDROID && !UNITY_EDITOR
                MRCdata =  MrcCalibration.Call<float[]>("readCalibrationData",path);
#endif
                }
                return MRCdata;
            }
        }

        public static class Controller
        {
            private const string TAG = "[PXR_Plugin/Controller]";

            public static int UPxr_SetControllerVibration(UInt32 hand, float strength, int time)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                return Pxr_SetControllerVibration(hand,strength, time);
#else
                return 0;
#endif
            }

            public static int UPxr_SetControllerEnableKey(bool isEnable, PxrControllerKeyMap Key)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                return Pxr_SetControllerEnableKey(isEnable, Key);
#else
                return 0;
#endif
            }

            public static int UPxr_GetBodyTrackingPose(double predictTime, ref BodyTrackerResult bodyTrackerResult)
            {
                int state = 1;
#if UNITY_ANDROID && !UNITY_EDITOR
                state = Pxr_GetBodyTrackingPose(predictTime,ref bodyTrackerResult);
                for (int i = 0; i < 24; i++) {
                    bodyTrackerResult.trackingdata[i].localpose.PosZ = -bodyTrackerResult.trackingdata[i].localpose.PosZ;
                    bodyTrackerResult.trackingdata[i].localpose.RotQz = -bodyTrackerResult.trackingdata[i].localpose.RotQz;
                    bodyTrackerResult.trackingdata[i].localpose.RotQw = -bodyTrackerResult.trackingdata[i].localpose.RotQw;
                    bodyTrackerResult.trackingdata[i].velo[2] = -bodyTrackerResult.trackingdata[i].velo[2];
                    bodyTrackerResult.trackingdata[i].acce[2] = -bodyTrackerResult.trackingdata[i].acce[2];
                    bodyTrackerResult.trackingdata[i].wvelo[2] = -bodyTrackerResult.trackingdata[i].wvelo[2];
                    bodyTrackerResult.trackingdata[i].wacce[2] = -bodyTrackerResult.trackingdata[i].wacce[2];
                }
#endif
                return state;
            }

            public static int UPxr_SetBodyTrackingMode(int mode) {
#if UNITY_ANDROID && !UNITY_EDITOR
                return Pxr_SetBodyTrackingMode(mode);
#endif
                return 0;
            }

            public static int UPxr_GetFitnessBandConnectState(ref PxrFitnessBandConnectState state)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                return Pxr_GetFitnessBandConnectState(ref state);
#endif
                return 0;
            }

            public static int UPxr_GetFitnessBandBattery(int trackerId, ref int battery) {
#if UNITY_ANDROID && !UNITY_EDITOR
                return Pxr_GetFitnessBandBattery(trackerId, ref battery);
#endif
                return 0;
            }

            public static int UPxr_GetFitnessBandCalibState(ref int calibrated) {
#if UNITY_ANDROID && !UNITY_EDITOR
                return Pxr_GetFitnessBandCalibState(ref calibrated);
#endif
                return 0;
            }

            public static int UPxr_SetControllerVibrationEvent(UInt32 hand, int frequency, float strength, int time)
            {
                if (PXR_Plugin.System.UPxr_GetAPIVersion() >= 0x2000305)
                {
#if UNITY_ANDROID && !UNITY_EDITOR
                return Pxr_SetControllerVibrationEvent(hand, frequency,strength, time);
#endif
                }
                return 0;
            }

            public static int UPxr_GetControllerType()
            {
                var type = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                PxrControllerCapability capability = new PxrControllerCapability();
                Pxr_GetControllerCapabilities(0,ref capability);
                type = (int)capability.type;
#endif
                PLog.d(TAG, "UPxr_GetControllerType()" + type);
                return type;
            }

            //Pxr_StopControllerVCMotor

            public static int UPxr_StopControllerVCMotor(int id)
            {
                if (PXR_Plugin.System.UPxr_GetAPIVersion() >= 0x2000308)
                {
#if UNITY_ANDROID && !UNITY_EDITOR
                Debug.Log("[VCMotor_SDK] StopControllerVCMotor :" + id.ToString());
                return Pxr_StopControllerVCMotor(id);
#endif
                }
                return 0;
            }

            public static int UPxr_StartControllerVCMotor(string file, int slot)
            {
                //0-Left And Right 1-Left 2-Right 3-Left And Right
                //0-Reversal 1-No Reversal
                if (PXR_Plugin.System.UPxr_GetAPIVersion() >= 0x2000308)
                {
#if UNITY_ANDROID && !UNITY_EDITOR
                Debug.Log("[VCMotor_SDK] StartControllerVCMotor " + file + " slot: " + slot.ToString());
                return Pxr_StartControllerVCMotor(file,slot);
#endif
                }
                return 0;
            }

            public static int UPxr_SetControllerAmp(float mode)
            {
                if (PXR_Plugin.System.UPxr_GetAPIVersion() >= 0x2000305)
                {
#if UNITY_ANDROID && !UNITY_EDITOR
                return Pxr_SetControllerAmp(mode);
#endif
                }
                return 0;
            }

            public static int UPxr_SetControllerDelay()
            {
                if (PXR_Plugin.System.UPxr_GetAPIVersion() >= 0x2000305)
                {
#if UNITY_ANDROID && !UNITY_EDITOR
                int delay = 3;
                int Length;
                int num;
                AudioSettings.GetDSPBufferSize(out Length, out num);
                if (Length == 256)
                {
                    delay = 1;
                }
                else if (Length == 512) {
                    delay = 2;
                } else if (Length == 1024) {
                    delay = 3;
                }
                Debug.Log("[VCMotor_SDK] UPxr_SetControllerDelay " + delay.ToString());
                return Pxr_SetControllerDelay(delay);
#endif
                }
                return 0;

            }

            public static string UPxr_GetVibrateDelayTime(ref int x)
            {
                if (PXR_Plugin.System.UPxr_GetAPIVersion() >= 0x2000305)
                {
#if UNITY_ANDROID && !UNITY_EDITOR
                return Pxr_GetVibrateDelayTime(ref x);
#endif
                }
                return " ";
            }

            public static int UPxr_StartVibrateBySharem(float[] data, int slot, int buffersize, int sampleRate, int channelMask, int bitrate ,int channelFlip, ref int sourceId)
            {
                if (PXR_Plugin.System.UPxr_GetAPIVersion() >= 0x2000308)
                {
#if UNITY_ANDROID && !UNITY_EDITOR
                AudioClipData audioClipData = new AudioClipData();
                audioClipData.slot = slot;
                audioClipData.channelCounts = channelMask;
                audioClipData.buffersize = (UInt64)buffersize;
                audioClipData.sampleRate = sampleRate;
                audioClipData.reversal = channelFlip;
                audioClipData.bitrate = bitrate;
                audioClipData.isCache = 0;
                Debug.Log("[VCMotor_SDK] Pxr_StartVibrateBySharem " + " slot: " + audioClipData.slot.ToString() + " buffersize:" + audioClipData.buffersize.ToString() + " sampleRate" + audioClipData.sampleRate.ToString() + " channelCounts:" + audioClipData.channelCounts.ToString()+" bitrate:" + audioClipData.bitrate.ToString());
                return Pxr_StartVibrateBySharemF(data, ref audioClipData, ref sourceId);
#endif
                }
                return 0;
            }

            public static int UPxr_SaveVibrateByCache(float[] data, int slot, int buffersize, int sampleRate, int channelMask, int bitrate, int slotconfig, int enableV , ref int sourceId)
            {

                if (PXR_Plugin.System.UPxr_GetAPIVersion() >= 0x2000308)
                {
#if UNITY_ANDROID && !UNITY_EDITOR
                    AudioClipData audioClipData = new AudioClipData();
                    audioClipData.slot = slot;
                    audioClipData.buffersize = (UInt64)buffersize;
                    audioClipData.sampleRate = sampleRate;
                    audioClipData.channelCounts = channelMask;
                    audioClipData.bitrate = bitrate;
                    audioClipData.reversal = slotconfig;
                    audioClipData.isCache = enableV;
                    Debug.Log("[VCMotor_SDK] UPxr_SaveVibrateByCache " + " slot: " + audioClipData.slot.ToString() + " buffersize:" + audioClipData.buffersize.ToString() + " sampleRate" + audioClipData.sampleRate.ToString() + " channelMask:" + audioClipData.channelCounts.ToString() + " bitrate:" + audioClipData.bitrate.ToString());
                    return Pxr_StartVibrateBySharemF(data, ref audioClipData, ref sourceId);
#endif
                }
                return 0;
            }

            public static int UPxr_StartVibrateByCache(int clicpid)
            {
                if (PXR_Plugin.System.UPxr_GetAPIVersion() >= 0x2000308)
                {
#if UNITY_ANDROID && !UNITY_EDITOR
                    Debug.Log("[VCMotor_SDK] UPxr_StartVibrateByCache " + clicpid.ToString());
                    return Pxr_StartVibrateByCache(clicpid);
#endif
                }
                return 0;
            }

            public static int UPxr_ClearVibrateByCache(int clicpid)
            {
                if (PXR_Plugin.System.UPxr_GetAPIVersion() >= 0x2000308)
                {
#if UNITY_ANDROID && !UNITY_EDITOR
                    Debug.Log("[VCMotor_SDK] UPxr_ClearVibrateByCache " + clicpid.ToString());
                    return Pxr_ClearVibrateByCache(clicpid);
#endif
                }
                return 0;
            }

            public static int UPxr_StartVibrateByPHF(string data, int buffersize, ref int sourceId, int slot, int reversal, float amp) {
                if (PXR_Plugin.System.UPxr_GetAPIVersion() >= 0x2000308)
                {
#if UNITY_ANDROID && !UNITY_EDITOR
                    VibrateInfo vibrateInfo = new VibrateInfo();
                    vibrateInfo.slot = (uint)slot;
                    vibrateInfo.reversal = (uint)reversal;
                    vibrateInfo.amp = amp;
                    Debug.Log("[VCMotor_SDK] Pxr_StartVibrateByPHF " + buffersize.ToString());
                    return Pxr_StartVibrateByPHF(data, buffersize, ref sourceId, ref vibrateInfo);
#endif
                }
                return 0;
            }

            public static int UPxr_PauseVibrate(int sourceID) {
                if (PXR_Plugin.System.UPxr_GetAPIVersion() >= 0x2000308)
                {
#if UNITY_ANDROID && !UNITY_EDITOR
                    Debug.Log("[VCMotor_SDK] Pxr_PauseVibrate " + sourceID.ToString());
                    return Pxr_PauseVibrate(sourceID);
#endif
                }
                return 0;
            }

            public static int UPxr_ResumeVibrate(int sourceID) {
                if (PXR_Plugin.System.UPxr_GetAPIVersion() >= 0x2000308)
                {
#if UNITY_ANDROID && !UNITY_EDITOR
                    Debug.Log("[VCMotor_SDK] Pxr_ResumeVibrate " + sourceID.ToString());
                    return Pxr_ResumeVibrate(sourceID);
#endif
                }
                return 0;
            }

            public static int UPxr_UpdateVibrateParams(int clicp_id,int slot, int reversal, float amp) {
                if (PXR_Plugin.System.UPxr_GetAPIVersion() >= 0x2000308)
                {
#if UNITY_ANDROID && !UNITY_EDITOR
                    VibrateInfo vibrateInfo = new VibrateInfo();
                    vibrateInfo.slot = (uint)slot;
                    vibrateInfo.reversal = (uint)reversal;
                    vibrateInfo.amp = amp;
                    Debug.Log("[VCMotor_SDK] UPxr_UpdateVibrateParams " + clicp_id.ToString() + " solt: " + slot.ToString() + " reversal:" + reversal.ToString() + " AMP:" + amp.ToString());
                    return Pxr_UpdateVibrateParams(clicp_id, ref vibrateInfo);         
#endif
                }
                return 0;
            }

            public static int UPxr_CreateHapticStream(string phfVersion, UInt32 frameDurationMs, ref VibrateInfo hapticInfo, float speed, ref int id) {
                if (PXR_Plugin.System.UPxr_GetAPIVersion() >= 0x200030A)
                {
#if UNITY_ANDROID && !UNITY_EDITOR
                    Debug.Log("[VCMotor_SDK] UPxr_CreateHapticStream ");
                    return Pxr_CreateHapticStream(phfVersion, frameDurationMs, ref hapticInfo, speed, ref id);    
#endif
                }
                return 0;
            }

            public static int UPxr_WriteHapticStream(int id, ref PxrPhfParamsNum frames, UInt32 numFrames) {
                if (PXR_Plugin.System.UPxr_GetAPIVersion() >= 0x200030A)
                {
#if UNITY_ANDROID && !UNITY_EDITOR
                    Debug.Log("[VCMotor_SDK] UPxr_WriteHapticStream ");
                    return Pxr_WriteHapticStream( id, ref  frames,  numFrames); 
#endif
                }
                return 0;
            }

            public static int UPxr_SetPHFHapticSpeed(int id, float speed) {
                if (PXR_Plugin.System.UPxr_GetAPIVersion() >= 0x200030A)
                {
#if UNITY_ANDROID && !UNITY_EDITOR
                    Debug.Log("[VCMotor_SDK] UPxr_SetPHFHapticSpeed ");
                    return Pxr_SetPHFHapticSpeed( id,  speed);
#endif
                }
                return 0;
            }

            public static int UPxr_GetPHFHapticSpeed(int id, ref float speed) {
                if (PXR_Plugin.System.UPxr_GetAPIVersion() >= 0x200030A)
                {
#if UNITY_ANDROID && !UNITY_EDITOR
                    Debug.Log("[VCMotor_SDK] UPxr_GetPHFHapticSpeed ");
                    return Pxr_GetPHFHapticSpeed( id, ref speed);
#endif
                }
                return 0;
            }

            public static int UPxr_GetCurrentFrameSequence(int id, ref UInt64 frameSequence) {
                if (PXR_Plugin.System.UPxr_GetAPIVersion() >= 0x200030A)
                {
#if UNITY_ANDROID && !UNITY_EDITOR
                    Debug.Log("[VCMotor_SDK] UPxr_GetCurrentFrameSequence ");
                    return Pxr_GetCurrentFrameSequence( id, ref  frameSequence);
#endif
                }
                return 0;
            }

            public static int UPxr_StartPHFHaptic(int source_id) {
                if (PXR_Plugin.System.UPxr_GetAPIVersion() >= 0x200030A)
                {
#if UNITY_ANDROID && !UNITY_EDITOR
                    Debug.Log("[VCMotor_SDK] UPxr_StartPHFHaptic ");
                    return Pxr_StartPHFHaptic(source_id);
#endif
                }
                return 0;
            }

            public static int UPxr_StopPHFHaptic(int source_id) {
                if (PXR_Plugin.System.UPxr_GetAPIVersion() >= 0x200030A)
                {
#if UNITY_ANDROID && !UNITY_EDITOR
                    Debug.Log("[VCMotor_SDK] UPxr_StopPHFHaptic ");
                    return Pxr_StopPHFHaptic(source_id);
#endif
                }
                return 0;
            }

            public static int UPxr_RemovePHFHaptic(int source_id) {
                if (PXR_Plugin.System.UPxr_GetAPIVersion() >= 0x200030A)
                {
#if UNITY_ANDROID && !UNITY_EDITOR
                    Debug.Log("[VCMotor_SDK] UPxr_RemovePHFHaptic ");
                    return Pxr_RemovePHFHaptic(source_id);
#endif
                }
                return 0;
            }

            public static int UPxr_SetControllerMainInputHandle(UInt32 hand)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                return Pxr_SetControllerMainInputHandle(hand);
#else
                return 0;
#endif
            }

            public static PXR_Input.Controller UPxr_GetControllerMainInputHandle()
            {
                var hand = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                Pxr_GetControllerMainInputHandle(ref hand);
#endif
                PLog.d(TAG, "Pxr_GetControllerMainInputHandle()" + hand.ToString());
                return (PXR_Input.Controller)hand;
            }

            public static int UPxr_GetControllerTrackingState(UInt32 deviceID, double predictTime, float[] headSensorData, ref PxrControllerTracking tracking)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                return Pxr_GetControllerTrackingState(deviceID,predictTime,headSensorData, ref tracking);
#else
                return 0;
#endif
            }

            public static void UPxr_SetControllerOriginOffset(int controllerID, Vector3 offset)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                Pxr_SetControllerOriginOffset(controllerID, offset);
#endif
            }

            public static void UPxr_ResetController()
            {
                if (System.UPxr_GetAPIVersion() >= 0x200030B)
                {
#if UNITY_ANDROID && !UNITY_EDITOR
                    Pxr_ResetController(0);
#endif
                }
            }

            public static void UPxr_SetArmModelParameters(PxrGazeType gazetype, PxrArmModelType armmodeltype, float elbowHeight, float elbowDepth, float pointerTiltAngle)
            {
                if (System.UPxr_GetAPIVersion() >= 0x200030B)
                {
#if UNITY_ANDROID && !UNITY_EDITOR
                    Pxr_SetArmModelParameters(gazetype, armmodeltype, elbowHeight, elbowDepth, pointerTiltAngle);
#endif
                }
            }

            public static void UPxr_GetControllerHandness(ref int deviceID)
            {
                if (System.UPxr_GetAPIVersion() >= 0x200030B)
                {
#if UNITY_ANDROID && !UNITY_EDITOR
                    Pxr_GetControllerHandness(ref deviceID);
#endif
                }
            }
        }

        public static class HandTracking
        {
            [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
            private static extern int Pxr_GetHandTrackerSettingState(ref bool settingState);

            [DllImport(PXR_API_DLL, CallingConvention = CallingConvention.Cdecl)]
            private static extern int Pxr_GetHandTrackerActiveInputType(ref ActiveInputDevice activeInputDevice);

            [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
            private static extern int Pxr_GetHandTrackerAimState(HandType hand, ref HandAimState aimState);

            [DllImport(PXR_PLATFORM_DLL, CallingConvention = CallingConvention.Cdecl)]
            private static extern int Pxr_GetHandTrackerJointLocations(HandType hand, ref HandJointLocations jointLocations);


            public static bool UPxr_GetHandTrackerSettingState()
            {
                bool val = false;
                if (System.UPxr_GetAPIVersion() >= 0x2000306)
                {
#if !UNITY_EDITOR && UNITY_ANDROID
                Pxr_GetHandTrackerSettingState(ref val);
#endif
                }
                return val;
            }

            public static ActiveInputDevice UPxr_GetHandTrackerActiveInputType()
            {
                ActiveInputDevice val = ActiveInputDevice.HeadActive;
                if (System.UPxr_GetAPIVersion() >= 0x2000307)
                {
#if !UNITY_EDITOR && UNITY_ANDROID
                    Pxr_GetHandTrackerActiveInputType(ref val);
#endif
                }
                return val;
            }

            public static bool UPxr_GetHandTrackerAimState(HandType hand, ref HandAimState aimState)
            {
                bool val = false;
                if (System.UPxr_GetAPIVersion() >= 0x2000306)
                {
#if !UNITY_EDITOR && UNITY_ANDROID
                val = Pxr_GetHandTrackerAimState(hand,ref aimState) == 0;
                aimState.aimRayPose.ToHandPosef(hand);
#endif
                }
                return val;
            }

            public static bool UPxr_GetHandTrackerJointLocations(HandType hand, ref HandJointLocations jointLocations)
            {
                bool val = false;
                if (System.UPxr_GetAPIVersion() >= 0x2000306)
                {
#if !UNITY_EDITOR && UNITY_ANDROID
                    val = Pxr_GetHandTrackerJointLocations(hand, ref jointLocations) == 0;
                    for (int i = 0; i < jointLocations.jointLocations.Length; i++)
                    {
                        if (i == 1)
                            jointLocations.jointLocations[i].pose.ToHandPosef(hand);
                        else
                            jointLocations.jointLocations[i].pose.ToJointPosef(hand);
                    }
#endif
                }
                return val;
            }
        }
    }
}