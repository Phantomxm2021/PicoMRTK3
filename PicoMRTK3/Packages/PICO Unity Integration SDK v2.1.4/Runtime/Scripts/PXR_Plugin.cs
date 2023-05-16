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
        PxrBothEyeFOV
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

    public enum SystemInfoEnum
    {
        ELECTRIC_QUANTITY,
        PUI_VERSION,
        EQUIPMENT_MODEL,
        EQUIPMENT_SN,
        CUSTOMER_SN,
        INTERNAL_STORAGE_SPACE_OF_THE_DEVICE,
        DEVICE_BLUETOOTH_STATUS,
        BLUETOOTH_NAME_CONNECTED,
        BLUETOOTH_MAC_ADDRESS,
        DEVICE_WIFI_STATUS,
        WIFI_NAME_CONNECTED,
        WLAN_MAC_ADDRESS,
        DEVICE_IP,
        CHARGING_STATUS
    }

    public enum DeviceControlEnum
    {
        DEVICE_CONTROL_REBOOT,
        DEVICE_CONTROL_SHUTDOWN
    }

    public enum PackageControlEnum
    {
        PACKAGE_SILENCE_INSTALL,
        PACKAGE_SILENCE_UNINSTALL
    }

    public enum SwitchEnum
    {
        S_ON,
        S_OFF
    }

    public enum HomeEventEnum
    {
        SINGLE_CLICK,
        DOUBLE_CLICK
    }

    public enum HomeFunctionEnum
    {
        VALUE_HOME_GO_TO_SETTING = 0,
        VALUE_HOME_RECENTER = 2,
        VALUE_HOME_DISABLE = 4,
        VALUE_HOME_GO_TO_HOME = 5
    }

    public enum ScreenOffDelayTimeEnum
    {
        THREE = 3,
        TEN = 10,
        THIRTY = 30,
        SIXTY = 60,
        THREE_HUNDRED = 300,
        SIX_HUNDRED = 600,
        NEVER = -1
    }

    public enum SleepDelayTimeEnum
    {
        FIFTEEN = 15,
        THIRTY = 30,
        SIXTY = 60,
        THREE_HUNDRED = 300,
        SIX_HUNDRED = 600,
        ONE_THOUSAND_AND_EIGHT_HUNDRED = 1800,
        NEVER = -1
    }

    public enum ControllerPairTimeEnum {
        DEFAULT,
        FIFTEEN,
        SIXTY,
        ONE_HUNDRED_AND_TWENTY,
        SIX_HUNDRED,
        NEVER
    }

    public enum SystemFunctionSwitchEnum
    {
        SFS_USB,
        SFS_AUTOSLEEP,
        SFS_SCREENON_CHARGING,
        SFS_OTG_CHARGING,
        SFS_RETURN_MENU_IN_2DMODE,
        SFS_COMBINATION_KEY,
        SFS_CALIBRATION_WITH_POWER_ON,
        SFS_SYSTEM_UPDATE,
        SFS_CAST_SERVICE,
        SFS_EYE_PROTECTION,
        SFS_SECURITY_ZONE_PERMANENTLY,
        SFS_GLOBAL_CALIBRATION,
        SFS_Auto_Calibration,
        SFS_USB_BOOT,
        SFS_VOLUME_UI,
        SFS_CONTROLLER_UI,
        SFS_NAVGATION_SWITCH,
        SFS_SHORTCUT_SHOW_RECORD_UI,
        SFS_SHORTCUT_SHOW_FIT_UI,
        SFS_SHORTCUT_SHOW_CAST_UI,
        SFS_SHORTCUT_SHOW_CAPTURE_UI,
        SFS_STOP_MEM_INFO_SERVICE,
        SFS_USB_FORCE_HOST,
        SFS_SET_DEFAULT_SAFETY_ZONE,
        SFS_ALLOW_RESET_BOUNDARY,
        SFS_BOUNDARY_CONFIRMATION_SCREEN,
        SFS_LONG_PRESS_HOME_TO_RECENTER,
        SFS_POWER_CTRL_WIFI_ENABLE,
        SFS_WIFI_DISABLE,
        SFS_SIX_DOF_SWITCH,
        SFS_INVERSE_DISPERSION,
        SFS_LOGCAT,
        SFS_PSENSOR,
        SFS_SYSTEM_UPDATE_OTA,
        SFS_SYSTEM_UPDATE_APP,
        SFS_SHORTCUT_SHOW_WLAN_UI,
        SFS_SHORTCUT_SHOW_BOUNDARY_UI,
        SFS_SHORTCUT_SHOW_BLUETOOTH_UI,
        SFS_SHORTCUT_SHOW_CLEAN_TASK_UI,
        SFS_SHORTCUT_SHOW_IPD_ADJUSTMENT_UI,
        SFS_SHORTCUT_SHOW_POWER_UI,
        SFS_SHORTCUT_SHOW_EDIT_UI,
        SFS_BASIC_SETTING_APP_LIBRARY_UI,
        SFS_BASIC_SETTING_SHORTCUT_UI

    }
    public enum StartVRSettingsEnum
    {
        START_VR_SETTINGS_ITEM_WIFI,
        START_VR_SETTINGS_ITEM_BLUETOOTH,
        START_VR_SETTINGS_ITEM_CONTROLLER,
        START_VR_SETTINGS_ITEM_LAB,
        START_VR_SETTINGS_ITEM_BRIGHTNESS,
        START_VR_SETTINGS_ITEM_GENERAL,
        START_VR_SETTINGS_ITEM_NOTIFICATION
    }

    public enum USBConfigModeEnum
    {
        MTP,
        CHARGE
    }

    public enum PxrLayerCreateFlags
    {
        PxrLayerFlagAndroidSurface = 1 << 0,
        PxrLayerFlagProtectedContent = 1 << 1,
        PxrLayerFlagStaticImage = 1 << 2,
        PxrLayerFlagUseExternalImages = 1 << 4,
        PxrLayerFlag3DLeftRightSurface = 1 << 5,
        PxrLayerFlag3DTopBottomSurface = 1 << 6
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

    public enum PICOCastUrlTypeEnum {
        NORMAL_URL,
        NO_CONFIRM_URL,
        RTMP_URL
    }

    public enum PICOCastOptionOrStatusEnum {
        OPTION_RESOLUTION_LEVEL,
        OPTION_BITRATE_LEVEL,
        OPTION_AUDIO_ENABLE,
        PICO_CAST_STATUS
    }

    public enum PICOCastOptionValueEnum {
        OPTION_VALUE_RESOLUTION_HIGH,
        OPTION_VALUE_RESOLUTION_MIDDL,
        OPTION_VALUE_RESOLUTION_AUTO,
        OPTION_VALUE_RESOLUTION_HIGH_2K,
        OPTION_VALUE_RESOLUTION_HIGH_4K,

        OPTION_VALUE_BITRATE_HIGH,
        OPTION_VALUE_BITRATE_MIDDLE,
        OPTION_VALUE_BITRATE_LOW,

        OPTION_VALUE_AUDIO_ON,
        OPTION_VALUE_AUDIO_OFF,

        STATUS_VALUE_STATE_STARTED,
        STATUS_VALUE_STATE_STOPPED,
        STATUS_VALUE_ERROR
    }

    public enum ControllerKeyEnum {
        CONTROLLER_KEY_JOYSTICK,
        CONTROLLER_KEY_MENU,
        CONTROLLER_KEY_TRIGGER,
        CONTROLLER_KEY_RIGHT_A,
        CONTROLLER_KEY_RIGHT_B,
        CONTROLLER_KEY_LEFT_X,
        CONTROLLER_KEY_LEFT_Y,
        CONTROLLER_KEY_LEFT_GRIP,
        CONTROLLER_KEY_RIGHT_GRIP
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
    public struct PxrFaceTrackingInfo
    {
        public Int64 timestamp;                         // us
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 72)]
        public float[] blendShapeWeight;                //72（52+20）Expression component weight
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public float[] videoInputValid;                 // Input validity of upper and lower face
        public float laughingProb;                      // Coefficient of laughter
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public float[] emotionProb;                     // Emotional factor
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
        public float[] reserved;
    };


    public static class PXR_Plugin
    {
        private const string PXR_SDK_Version = "2.1.4.3";
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

        #endregion

        public static class System
        {
            public static Action RecenterSuccess;
            public static Action FocusStateAcquired;
            public static Action FocusStateLost;
            public static Action SensorReady;
            public static Action<int> InputDeviceChanged;
            public static Action<int> SeethroughStateChangedChanged;

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
                PLog.i(TAG, "UPxr_GetPredictedDisplayTime()");
                double predictedDisplayTime = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                Pxr_GetPredictedDisplayTime(ref predictedDisplayTime);
#endif
                PLog.i(TAG, "UPxr_GetPredictedDisplayTime() predictedDisplayTime：" + predictedDisplayTime);
                return predictedDisplayTime;
            }

            public static bool UPxr_SetExtraLatencyMode(int mode)
            {
                PLog.i(TAG, "UPxr_SetExtraLatencyMode() mode:" + mode);
                bool result = false;
#if UNITY_ANDROID && !UNITY_EDITOR
                result = Pxr_SetExtraLatencyMode(mode);
#endif
                PLog.i(TAG, "UPxr_SetExtraLatencyMode() result:" + result);
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
                if (PXR_Plugin.System.UPxr_GetAPIVersion() >= 0x2000300)
                {
#if !UNITY_EDITOR && UNITY_ANDROID
                return Pxr_GetMRCEnable();
#endif
                }
                return false;
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
                if (PXR_Plugin.System.UPxr_GetAPIVersion() >= 0x2000300)
                {
#if UNITY_ANDROID && !UNITY_EDITOR
                Pxr_GetMrcPose(ref pose);
#endif
                }
                return true;
            }

            public static bool UPxr_SetMrcPose(ref PxrPosef pose)
            {
                if (PXR_Plugin.System.UPxr_GetAPIVersion() >= 0x2000300)
                {
#if UNITY_ANDROID && !UNITY_EDITOR
                Pxr_SetMrcPose(ref pose);
#endif
                }
                return true;
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
                PLog.i(TAG, "UPxr_GetConfigFloat() type:" + type);
                float value = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                Pxr_GetConfigFloat(type, ref value);
#endif
                PLog.i(TAG, "UPxr_GetConfigFloat() value:" + value);
                return value;
            }

            public static int UPxr_GetConfigInt(ConfigType type)
            {
                PLog.i(TAG, "UPxr_GetConfigInt() type:" + type);
                int value = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                Pxr_GetConfigInt(type, ref value);
#endif
                PLog.i(TAG, "UPxr_GetConfigInt() value:" + value);
                return value;
            }

            public static int UPxr_SetConfigInt(ConfigType configSetIndex, int configSetData)
            {
                PLog.i(TAG, "UPxr_SetConfigInt() configSetIndex:" + configSetIndex + " configSetData:" + configSetData);
                int result = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                result = Pxr_SetConfigInt(configSetIndex, configSetData);
#endif
                PLog.i(TAG, "UPxr_SetConfigInt() result:" + result);
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
                PLog.i(TAG, "UPxr_SetConfigString() configSetIndex:" + configSetIndex + " configSetData:" + configSetData);
                int result = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                result = Pxr_SetConfigString(configSetIndex, configSetData);
#endif
                PLog.i(TAG, "UPxr_SetConfigString() result:" + result);
                return result;
            }

            public static int UPxr_SetSystemDisplayFrequency(float rate)
            {
                PLog.i(TAG, "UPxr_SetDisplayRefreshRate() rate:" + rate);
                int result = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                result = Pxr_SetDisplayRefreshRate(rate);
#endif
                PLog.i(TAG, "UPxr_SetDisplayRefreshRate() result:" + result);
                return result;
            }

            public static int UPxr_SetPerformanceLevels(PxrPerfSettings which, PxrSettingsLevel level)
            {
                PLog.i(TAG, "UPxr_SetPerformanceLevels() which:" + which + ", level:" + level);
                int result = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                if (PXR_Plugin.System.UPxr_GetAPIVersion() >= 0x200030A)
                {
                    result = Pxr_SetPerformanceLevels(which, level);
                }
#endif
                PLog.i(TAG, "UPxr_SetPerformanceLevels() result:" + result);
                return result;
            }
            
            public static PxrSettingsLevel UPxr_GetPerformanceLevels(PxrPerfSettings which)
            {
                PLog.i(TAG, "UPxr_GetPerformanceLevels() which:" + which);
                int result = 0;
                PxrSettingsLevel level = PxrSettingsLevel.POWER_SAVINGS;
#if UNITY_ANDROID && !UNITY_EDITOR
                if (PXR_Plugin.System.UPxr_GetAPIVersion() >= 0x200030A)
                {
                    result = Pxr_GetPerformanceLevels(which, ref level);
                }
#endif
                PLog.i(TAG, "UPxr_GetPerformanceLevels() result:" + result + ", level:" + level);
                return level;
            }


            public static bool UPxr_InitAudioDevice()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
            try
            {
                if (sysActivity != null)
                {
                    sysActivity.CallStatic("pxr_InitAudioDevice", currentActivity); 
                    return true;
                }
                else
                    return false;
            }
            catch (Exception e)
            {
                PLog.e(TAG, "UPxr_InitAudioDevice Error :" + e.ToString());
                return false;
            }
#else
                return true;
#endif
            }

            public static bool UPxr_StartBatteryReceiver(string objName)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
            try
            {
                batteryReceiver.CallStatic("pxr_StartReceiver", currentActivity, objName);
                return true;
            }
            catch (Exception e)
            {
                PLog.e(TAG, "UPxr_StartBatteryReceiver Error :" + e.ToString());
                return false;
            }
#else
                return true;
#endif
            }

            public static bool UPxr_StopBatteryReceiver()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
            try
            {
                batteryReceiver.CallStatic("pxr_StopReceiver", currentActivity);
                return true;
            }
            catch (Exception e)
            {
                PLog.e(TAG, "UPxr_StopBatteryReceiver Error :" + e.ToString());
                return false;
            }
#else
                return true;
#endif
            }

            public static bool UPxr_SetBrightness(int brightness)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
            try
            {
                sysActivity.CallStatic("pxr_SetScreen_Brightness", brightness, currentActivity);
                return true;
            }
            catch (Exception e)
            {
                PLog.e(TAG, "UPxr_SetBrightness Error :" + e.ToString());
                return false;
            }
#else
                return true;
#endif
            }

            public static int UPxr_GetCurrentBrightness()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
            int currentlight = 0;
            try
            {
                currentlight = sysActivity.CallStatic<int>("pxr_GetScreen_Brightness", currentActivity);
            }
            catch (Exception e)
            {
                PLog.e(TAG, "UPxr_GetCurrentBrightness Error :" + e.ToString());
            }
            return currentlight;
#else
                return 0;
#endif
            }

            public static int[] UPxr_GetScreenBrightnessLevel()
            {
                int[] currentlight = { 0 };
#if UNITY_ANDROID && !UNITY_EDITOR
            try
            {
                currentlight = sysActivity.CallStatic<int[]>("getScreenBrightnessLevel");
            }
            catch (Exception e)
            {
                PLog.e(TAG, "UPxr_GetScreenBrightnessLevel Error :" + e.ToString());
            }
#endif
                return currentlight;
            }

            public static void UPxr_SetScreenBrightnessLevel(int vrBrightness, int level)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
            try
            {
                sysActivity.CallStatic("setScreenBrightnessLevel",vrBrightness,level);
            }
            catch (Exception e)
            {
                PLog.e(TAG, "UPxr_SetScreenBrightnessLevel Error :" + e.ToString());
            }
#endif
            }

            public static bool UPxr_StartAudioReceiver(string startreceivre)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
            try
            {
                audioReceiver.CallStatic("pxr_StartReceiver", currentActivity, startreceivre);
                return true;
            }
            catch (Exception e)
            {
                PLog.e(TAG, "UPxr_StartAudioReceiver Error :" + e.ToString());
                return false;
            }
#else
                return true;
#endif
            }

            public static bool UPxr_StopAudioReceiver()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
            try
            {
                audioReceiver.CallStatic("pxr_StopReceiver", currentActivity);
                return true;
            }
            catch (Exception e)
            {
                PLog.e(TAG, "UPxr_StopAudioReceiver Error :" + e.ToString());
                return false;
            }

#else
                return true;
#endif
            }

            public static int UPxr_GetMaxVolumeNumber()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
            int maxvolm = 0;
            try
            {  
                maxvolm = sysActivity.CallStatic<int>("pxr_GetMaxAudionumber");
            }
            catch (Exception e)
            {
                PLog.e(TAG,"UPxr_GetMaxVolumeNumber Error :" + e.ToString());
            }
            return maxvolm;
#else
                return 0;
#endif
            }

            public static int UPxr_GetCurrentVolumeNumber()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
            int currentvolm = 0;
            try
            {
                currentvolm = sysActivity.CallStatic<int>("pxr_GetAudionumber");
            }
            catch (Exception e)
            {
                    PLog.e(TAG, "UPxr_GetCurrentVolumeNumber Error :" + e.ToString());
            }
            return currentvolm;
#else
                return 0;
#endif
            }

            public static bool UPxr_VolumeUp()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
            try
            {
                sysActivity.CallStatic("pxr_UpAudio");
                return true;
            }
            catch (Exception e)
            {
                    PLog.e(TAG, "UPxr_VolumeUp Error :" + e.ToString());
                return false;
            }
#else
                return true;
#endif
            }

            public static bool UPxr_VolumeDown()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
            try
            {
                sysActivity.CallStatic("pxr_DownAudio");
                return true;
            }
            catch (Exception e)
            {
                    PLog.e(TAG, "UPxr_VolumeDown Error :" + e.ToString());
                return false;
            }
#else
                return true;
#endif
            }

            public static bool UPxr_SetVolumeNum(int volume)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
            try
            {
                sysActivity.CallStatic("pxr_ChangeAudio", volume);
                return true;
            }
            catch (Exception e)
            {
                    PLog.e(TAG, "UPxr_SetVolumeNum Error :" + e.ToString());
                return false;
            }
#else
                return true;
#endif
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

            public static Action<bool> BoolCallback;
            public static Action<int> IntCallback;
            public static Action<long> LongCallback;
            public static Action<string> StringCallback;

#if UNITY_ANDROID && !UNITY_EDITOR
            private static AndroidJavaObject tobHelper;
            private static AndroidJavaClass tobHelperClass;
#endif

            public static void UPxr_InitToBService()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                tobHelperClass = new AndroidJavaClass("com.pvr.tobservice.ToBServiceHelper");
                tobHelper = tobHelperClass.CallStatic<AndroidJavaObject>("getInstance");
                unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
#endif
            }

            public static void UPxr_SetUnityObjectName(string obj)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                tobHelper.Call("setUnityObjectName", obj);
#endif
            }

            public static void UPxr_BindSystemService()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                tobHelper.Call("bindTobService", currentActivity);
#endif
            }

            public static void UPxr_UnBindSystemService()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                tobHelper.Call("unBindTobService", currentActivity);
#endif
            }

            private static AndroidJavaObject GetEnumType(Enum enumType)
            {
                AndroidJavaClass enumjs = new AndroidJavaClass("com.pvr.tobservice.enums" + enumType.GetType().ToString().Replace("Unity.XR.PXR.", ".PBS_"));
                AndroidJavaObject enumjo = enumjs.GetStatic<AndroidJavaObject>(enumType.ToString());
                return enumjo;
            }

            public static string UPxr_StateGetDeviceInfo(SystemInfoEnum type)
            {
                string result = "";
#if UNITY_ANDROID && !UNITY_EDITOR
                result = tobHelper.Call<string>("pbsStateGetDeviceInfo", GetEnumType(type), 0);
#endif
                return result;
            }

            public static void UPxr_ControlSetDeviceAction(DeviceControlEnum deviceControl, Action<int> callback)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                if (callback != null) IntCallback = callback;
                tobHelper.Call("pbsControlSetDeviceAction", GetEnumType(deviceControl), null);
#endif
            }

            public static void UPxr_ControlAPPManager(PackageControlEnum packageControl, string path, Action<int> callback)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                if (callback != null) IntCallback = callback;
                tobHelper.Call("pbsControlAPPManger", GetEnumType(packageControl), path, 0, null);
#endif
            }

            public static void UPxr_ControlSetAutoConnectWIFI(string ssid, string pwd, Action<bool> callback)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                if (callback != null) BoolCallback = callback;
                tobHelper.Call("pbsControlSetAutoConnectWIFI", ssid, pwd, 0, null);
#endif
            }

            public static void UPxr_ControlClearAutoConnectWIFI(Action<bool> callback)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                if (callback != null) BoolCallback = callback;
                tobHelper.Call("pbsControlClearAutoConnectWIFI", null);
#endif
            }

            public static void UPxr_PropertySetHomeKey(HomeEventEnum eventEnum, HomeFunctionEnum function, Action<bool> callback)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                if (callback != null) BoolCallback = callback;
                tobHelper.Call("pbsPropertySetHomeKey", GetEnumType(eventEnum), GetEnumType(function), null);
#endif
            }

            public static void UPxr_PropertySetHomeKeyAll(HomeEventEnum eventEnum, HomeFunctionEnum function, int timesetup, string pkg, string className, Action<bool> callback)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                if (callback != null) BoolCallback = callback;
                tobHelper.Call("pbsPropertySetHomeKeyAll", GetEnumType(eventEnum), GetEnumType(function), timesetup, pkg, className, null);
#endif
            }

            public static void UPxr_PropertyDisablePowerKey(bool isSingleTap, bool enable, Action<int> callback)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                if (callback != null) IntCallback = callback;
                tobHelper.Call("pbsPropertyDisablePowerKey", isSingleTap, enable, null);
#endif
            }

            public static void UPxr_PropertySetScreenOffDelay(ScreenOffDelayTimeEnum timeEnum, Action<int> callback)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                if (callback != null) IntCallback = callback;
                tobHelper.Call("pbsPropertySetScreenOffDelay", GetEnumType(timeEnum), null);
#endif
            }

            public static void UPxr_PropertySetSleepDelay(SleepDelayTimeEnum timeEnum)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                tobHelper.Call("pbsPropertySetSleepDelay", GetEnumType(timeEnum));
#endif
            }

            public static void UPxr_SwitchSystemFunction(SystemFunctionSwitchEnum systemFunction, SwitchEnum switchEnum)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                tobHelper.Call("pbsSwitchSystemFunction", GetEnumType(systemFunction), GetEnumType(switchEnum), 0);
#endif
            }

            public static void UPxr_SwitchSetUsbConfigurationOption(USBConfigModeEnum uSBConfigModeEnum)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                tobHelper.Call("pbsSwitchSetUsbConfigurationOption", GetEnumType(uSBConfigModeEnum), 0);
#endif
            }

            public static void UPxr_SetControllerPairTime(ControllerPairTimeEnum timeEnum, Action<int> callback) {
#if UNITY_ANDROID && !UNITY_EDITOR
                if (callback != null) IntCallback = callback;
                tobHelper.Call("pbsSetControllerPairTime", GetEnumType(timeEnum),null, 0);
#endif
            }

            public static void UPxr_GetControllerPairTime(Action<int> callback) {
#if UNITY_ANDROID && !UNITY_EDITOR
                if (callback != null) IntCallback = callback;
                tobHelper.Call("pbsGetControllerPairTime",null, 0);
#endif
            }

            public static void UPxr_ScreenOn()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                tobHelper.Call("pbsScreenOn");
#endif
            }

            public static void UPxr_ScreenOff()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
            tobHelper.Call("pbsScreenOff");
#endif
            }

            public static void UPxr_AcquireWakeLock()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
            tobHelper.Call("pbsAcquireWakeLock");
#endif
            }

            public static void UPxr_ReleaseWakeLock()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
            tobHelper.Call("pbsReleaseWakeLock");
#endif
            }

            public static void UPxr_EnableEnterKey()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
            tobHelper.Call("pbsEnableEnterKey");
#endif
            }

            public static void UPxr_DisableEnterKey()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
            tobHelper.Call("pbsDisableEnterKey");
#endif
            }

            public static void UPxr_EnableVolumeKey()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
            tobHelper.Call("pbsEnableVolumeKey");
#endif
            }

            public static void UPxr_DisableVolumeKey()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
            tobHelper.Call("pbsDisableVolumeKey");
#endif
            }

            public static void UPxr_EnableBackKey()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
            tobHelper.Call("pbsEnableBackKey");
#endif
            }

            public static void UPxr_DisableBackKey()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
            tobHelper.Call("pbsDisableBackKey");
#endif
            }

            public static void UPxr_WriteConfigFileToDataLocal(string path, string content, Action<bool> callback)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
            if (callback != null) BoolCallback = callback;
            tobHelper.Call("pbsWriteConfigFileToDataLocal", path, content, null);
#endif
            }

            public static void UPxr_ResetAllKeyToDefault(Action<bool> callback)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
            if (callback != null) BoolCallback = callback;
            tobHelper.Call("pbsResetAllKeyToDefault", null);
#endif
            }

            public static void UPxr_SetAPPAsHome(SwitchEnum switchEnum, string packageName)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
            tobHelper.Call("pbsAppSetAPPAsHomeTwo", GetEnumType(switchEnum), packageName);
#endif
            }

            public static void UPxr_KillAppsByPidOrPackageName(int[] pids, string[] packageNames)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                tobHelper.Call("pbsKillAppsByPidOrPackageName", pids, packageNames, 0);
#endif
            }

            public static void UPxr_KillBackgroundAppsWithWhiteList(string[] packageNames)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                tobHelper.Call("pbsKillBackgroundAppsWithWhiteList",packageNames, 0);
#endif
            }

            public static void UPxr_FreezeScreen(bool freeze)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                tobHelper.Call("pbsFreezeScreen", freeze);
#endif
            }

            public static void UPxr_OpenMiracast()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                tobHelper.Call("pbsOpenMiracast");
#endif
            }

            public static bool UPxr_IsMiracastOn()
            {
                bool value = false;
#if UNITY_ANDROID && !UNITY_EDITOR
                value = tobHelper.Call<bool>("pbsIsMiracastOn");
#endif
                return value;
            }

            public static void UPxr_CloseMiracast()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                tobHelper.Call("pbsCloseMiracast");
#endif
            }

            public static void UPxr_StartScan()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                tobHelper.Call("pbsStartScan");
#endif
            }

            public static void UPxr_StopScan()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                tobHelper.Call("pbsStopScan");
#endif
            }

            public static void UPxr_ConnectWifiDisplay(string modelJson)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                tobHelper.Call("pbsUnityConnectWifiDisplay", modelJson);
#endif
            }

            public static void UPxr_DisConnectWifiDisplay()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                tobHelper.Call("pbsDisConnectWifiDisplay");
#endif
            }

            public static void UPxr_ForgetWifiDisplay(string address)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                tobHelper.Call("pbsForgetWifiDisplay", address);
#endif
            }

            public static void UPxr_RenameWifiDisplay(string address, string newName)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                tobHelper.Call("pbsRenameWifiDisplay", address, newName);
#endif
            }

            public static void UPxr_SetWDModelsCallback()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                tobHelper.Call("pbsSetWDModelsCallback", null);
#endif
            }

            public static void UPxr_SetWDJsonCallback()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                tobHelper.Call("pbsSetWDJsonCallback", null);
#endif
            }

            public static void UPxr_UpdateWifiDisplays(Action<string> callback)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                if (callback != null) StringCallback = callback;
                tobHelper.Call("pbsUpdateWifiDisplays");
#endif
            }

            public static string UPxr_GetConnectedWD()
            {
                string result = "";
#if UNITY_ANDROID && !UNITY_EDITOR
                result = tobHelper.Call<string>("pbsUnityGetConnectedWD");
#endif
                return result;
            }

            public static void UPxr_SwitchLargeSpaceScene(bool open, Action<bool> callback)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                if (callback != null) BoolCallback = callback;
                tobHelper.Call("pbsSwitchLargeSpaceScene",null, open, 0);
#endif
            }

            public static void UPxr_GetSwitchLargeSpaceStatus(Action<string> callback)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                if (callback != null) StringCallback = callback;
                tobHelper.Call("pbsGetSwitchLargeSpaceStatus",null, 0);
#endif
            }

            public static bool UPxr_SaveLargeSpaceMaps()
            {
                bool value = false;
#if UNITY_ANDROID && !UNITY_EDITOR
                value = tobHelper.Call<bool>("pbsSaveLargeSpaceMaps", 0);
#endif
                return value;
            }

            public static void UPxr_ExportMaps(Action<bool> callback)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                if (callback != null) BoolCallback = callback;
                tobHelper.Call("pbsExportMaps", null,0);
#endif
            }

            public static void UPxr_ImportMaps(Action<bool> callback)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                if (callback != null) BoolCallback = callback;
                tobHelper.Call("pbsImportMaps", null, 0);
#endif
            }

            public static float[] UPxr_GetCpuUsages()
            {
                float[] data = null;
#if UNITY_ANDROID && !UNITY_EDITOR
                data = tobHelper.Call<float[]>("pbsGetCpuUsages");
#endif
                return data;
            }

            public static float[] UPxr_GetDeviceTemperatures(int type, int source)
            {
                float[] data = null;
#if UNITY_ANDROID && !UNITY_EDITOR
                data = tobHelper.Call<float[]>("pbsGetDeviceTemperatures", type, source);
#endif

                return data;
            }

            public static void UPxr_Capture()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                tobHelper.Call("pbsCapture");
#endif
            }

            public static void UPxr_Record()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                tobHelper.Call("pbsRecord");
#endif
            }

            public static void UPxr_ControlSetAutoConnectWIFIWithErrorCodeCallback(String ssid, String pwd, int ext, Action<int> callback)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
            if (callback != null) IntCallback = callback;
            tobHelper.Call("pbsControlSetAutoConnectWIFIWithErrorCodeCallback",ssid,pwd,ext,null);
#endif
            }

            public static void UPxr_AppKeepAlive(String appPackageName, bool keepAlive, int ext)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                tobHelper.Call("pbsAppKeepAlive",appPackageName,keepAlive,ext);
#endif
            }

            public static void UPxr_TimingStartup(int year, int month, int day, int hour, int minute, bool open)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                tobHelper.Call("pbsTimingStartup", year, month, day, hour, minute, open);
#endif
            }
            public static void UPxr_TimingShutdown(int year, int month, int day, int hour, int minute, bool open)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                tobHelper.Call("pbsTimingShutdown", year, month, day, hour, minute, open);
#endif
            }
            public static void UPxr_StartVrSettingsItem(StartVRSettingsEnum settingsEnum, bool hideOtherItem, int ext)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                tobHelper.Call("pbsStartVrSettingsItem", GetEnumType(settingsEnum), hideOtherItem, ext);
#endif
            }
            public static void UPxr_SwitchVolumeToHomeAndEnter(SwitchEnum switchEnum, int ext)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                tobHelper.Call("pbsSwitchVolumeToHomeAndEnter", GetEnumType(switchEnum), ext);
#endif
            }
            public static SwitchEnum UPxr_IsVolumeChangeToHomeAndEnter()
            {
                SwitchEnum switchEnum = SwitchEnum.S_OFF;
#if UNITY_ANDROID && !UNITY_EDITOR
                int num = 0;
                num = tobHelper.Call<int>("pbsIsVolumeChangeToHomeAndEnter");
                if (num == 0)
                {
                    switchEnum = SwitchEnum.S_ON;
                }
                else if (num == 1) {
                    switchEnum = SwitchEnum.S_OFF;
                }
#endif
                return switchEnum;
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

            public static int UPxr_InstallOTAPackage(String otaPackagePath) {
                int value = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
            value = tobHelper.Call<int>("pbsInstallOTAPackage",otaPackagePath, 0);
#endif
                return value;
            }

            public static string UPxr_GetAutoConnectWiFiConfig() {
                string str = "";
#if UNITY_ANDROID && !UNITY_EDITOR
                str = tobHelper.Call<string>("pbsGetAutoConnectWiFiConfig", 0);
#endif
                return str;
            }

            public static string UPxr_GetTimingStartupStatus()
            {
                string str = "";
#if UNITY_ANDROID && !UNITY_EDITOR
                str = tobHelper.Call<string>("pbsGetTimingStartupStatus", 0);
#endif
                return str;
            }

            public static string UPxr_GetTimingShutdownStatus()
            {
                string str = "";
#if UNITY_ANDROID && !UNITY_EDITOR
                str = tobHelper.Call<string>("pbsGetTimingShutdownStatus", 0);
#endif
                return str;
            }

            public static int UPxr_GetControllerKeyState(ControllerKeyEnum pxrControllerKey) {
                int volue = 1;
#if UNITY_ANDROID && !UNITY_EDITOR
                volue = tobHelper.Call<int>("pbsGetControllerKeyState", GetEnumType(pxrControllerKey),0);
#endif
                return volue;
            }

            public static int UPxr_SetControllerKeyState(ControllerKeyEnum controllerKeyEnum, SwitchEnum status) {
                int volue = 1;
#if UNITY_ANDROID && !UNITY_EDITOR
                volue = tobHelper.Call<int>("pbsSetControllerKeyState", GetEnumType(controllerKeyEnum),GetEnumType(status),0);
#endif
                return volue;
            }

            public static SwitchEnum UPxr_ControlGetPowerOffWithUSBCable() {
                SwitchEnum switchEnum = SwitchEnum.S_OFF;
#if UNITY_ANDROID && !UNITY_EDITOR
                int num = 0;
                num = tobHelper.Call<int>("pbsControlGetPowerOffWithUSBCable",0);
                if (num == 0)
                {
                    switchEnum = SwitchEnum.S_ON;
                }
                else if (num == 1) {
                    switchEnum = SwitchEnum.S_OFF;
                }
#endif
                return switchEnum;
            }

            public static ScreenOffDelayTimeEnum UPxr_PropertyGetScreenOffDelay() {
                ScreenOffDelayTimeEnum screenOffDelayTimeEnum = ScreenOffDelayTimeEnum.NEVER;
#if UNITY_ANDROID && !UNITY_EDITOR
                int num = 0;
                num = tobHelper.Call<int>("pbsPropertyGetScreenOffDelay", 0);
                switch (num) {
                    case 0:
                        screenOffDelayTimeEnum = ScreenOffDelayTimeEnum.THREE;
                        break;
                    case 1:
                        screenOffDelayTimeEnum = ScreenOffDelayTimeEnum.TEN;
                        break;
                    case 2:
                        screenOffDelayTimeEnum = ScreenOffDelayTimeEnum.THIRTY;
                        break;
                    case 3:
                        screenOffDelayTimeEnum = ScreenOffDelayTimeEnum.SIXTY;
                        break;
                    case 4:
                        screenOffDelayTimeEnum = ScreenOffDelayTimeEnum.THREE_HUNDRED;
                        break;
                    case 5:
                        screenOffDelayTimeEnum = ScreenOffDelayTimeEnum.SIX_HUNDRED;
                        break;
                    case 6:
                        screenOffDelayTimeEnum = ScreenOffDelayTimeEnum.NEVER;
                        break;
                }
#endif
                return screenOffDelayTimeEnum;
            }

            public static SleepDelayTimeEnum UPxr_PropertyGetSleepDelay() {
                SleepDelayTimeEnum sleepDelayTime = SleepDelayTimeEnum.NEVER;
#if UNITY_ANDROID && !UNITY_EDITOR
                int num = 0;
                num = tobHelper.Call<int>("pbsPropertyGetSleepDelay", 0);
                switch (num)
                {
                    case 0:
                        sleepDelayTime = SleepDelayTimeEnum.FIFTEEN;
                        break;
                    case 1:
                        sleepDelayTime = SleepDelayTimeEnum.THIRTY;
                        break;
                    case 2:
                        sleepDelayTime = SleepDelayTimeEnum.SIXTY;
                        break;
                    case 3:
                        sleepDelayTime = SleepDelayTimeEnum.THREE_HUNDRED;
                        break;
                    case 4:
                        sleepDelayTime = SleepDelayTimeEnum.SIX_HUNDRED;
                        break;
                    case 5:
                        sleepDelayTime = SleepDelayTimeEnum.ONE_THOUSAND_AND_EIGHT_HUNDRED;
                        break;
                    case 6:
                        sleepDelayTime = SleepDelayTimeEnum.NEVER;
                        break;
                }
#endif
                return sleepDelayTime;
            }

            public static string UPxr_PropertyGetPowerKeyStatus() {
                string str = "";
#if UNITY_ANDROID && !UNITY_EDITOR
                str = tobHelper.Call<string>("pbsPropertyGetPowerKeyStatus", 0);
#endif
                return str;
            }

            public static int UPxr_GetEnterKeyStatus() {
                int volue = 1;
#if UNITY_ANDROID && !UNITY_EDITOR
                volue = tobHelper.Call<int>("pbsGetEnterKeyStatus",0);
#endif
                return volue;
            }

            public static int UPxr_GetVolumeKeyStatus()
            {
                int volue = 1;
#if UNITY_ANDROID && !UNITY_EDITOR
                volue = tobHelper.Call<int>("pbsGetVolumeKeyStatus",0);
#endif
                return volue;
            }

            public static int UPxr_GetBackKeyStatus()
            {
                int volue = 1;
#if UNITY_ANDROID && !UNITY_EDITOR
                volue = tobHelper.Call<int>("pbsGetBackKeyStatus",0);
#endif
                return volue;
            }

            public static string UPxr_PropertyGetHomKeyStatus(HomeEventEnum homeEvent) {
                string volue = "";
#if UNITY_ANDROID && !UNITY_EDITOR
                volue = tobHelper.Call<string>("pbsPropertyGetHomKeyStatus", GetEnumType(homeEvent),0);
#endif
                return volue;
            }

            public static void UPxr_GetSwitchSystemFunctionStatus(SystemFunctionSwitchEnum systemFunction,Action<int> callback) {
#if UNITY_ANDROID && !UNITY_EDITOR
                if (callback != null) IntCallback = callback;
                tobHelper.Call("pbsGetSwitchSystemFunctionStatus", GetEnumType(systemFunction),null,0);
#endif
            }

            public static string UPxr_SwitchGetUsbConfigurationOption() {
                string volue = "";
#if UNITY_ANDROID && !UNITY_EDITOR
                volue = tobHelper.Call<string>("pbsSwitchGetUsbConfigurationOption", 0);
#endif
                return volue;
            }

            public static string UPxr_GetCurrentLauncher()
            {
                string volue = "";
#if UNITY_ANDROID && !UNITY_EDITOR
                volue = tobHelper.Call<string>("pbsGetCurrentLauncher", 0);
#endif
                return volue;
            }

            public static int UPxr_PICOCastInit(Action<int> callback) {
                int value = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                if (callback != null) IntCallback = callback;
                value = tobHelper.Call<int>("pbsPicoCastInit",null,0);
#endif
                return value;
            }

            public static int UPxr_PICOCastSetShowAuthorization(int authZ) {
                int value = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                value = tobHelper.Call<int>("pbsPicoCastSetShowAuthorization",authZ,0);
#endif
                return value;
            }

            public static int UPxr_PICOCastGetShowAuthorization() {
                int value = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                value = tobHelper.Call<int>("pbsPicoCastGetShowAuthorization",0);
#endif
                return value;
            }

            public static string UPxr_PICOCastGetUrl(PICOCastUrlTypeEnum urlType) {
                string volue = "";
#if UNITY_ANDROID && !UNITY_EDITOR
                volue = tobHelper.Call<string>("pbsPicoCastGetUrl",GetEnumType(urlType), 0);
#endif
                return volue;
            }

            public static int UPxr_PICOCastStopCast()
            {
                int value = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                value = tobHelper.Call<int>("pbsPicoCastStopCast",0);
#endif
                return value;
            }

            public static int UPxr_PICOCastSetOption(PICOCastOptionOrStatusEnum castOptionOrStatus, PICOCastOptionValueEnum castOptionValue) {
                int value = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                value = tobHelper.Call<int>("pbsPicoCastSetOption",GetEnumType(castOptionOrStatus),GetEnumType(castOptionValue),0);
#endif
                return value;
            }

            public static PICOCastOptionValueEnum UPxr_PICOCastGetOptionOrStatus(PICOCastOptionOrStatusEnum castOptionOrStatus) {
                PICOCastOptionValueEnum value = PICOCastOptionValueEnum.STATUS_VALUE_ERROR;
#if UNITY_ANDROID && !UNITY_EDITOR
                int num = 0;
                num = tobHelper.Call<int>("pbsPicoCastGetOptionOrStatus", GetEnumType(castOptionOrStatus), 0);
                switch (num)
                {
                    case 0:
                        value = PICOCastOptionValueEnum.OPTION_VALUE_RESOLUTION_HIGH;
                        break;
                    case 1:
                        value = PICOCastOptionValueEnum.OPTION_VALUE_RESOLUTION_MIDDL;
                        break;
                    case 2:
                        value = PICOCastOptionValueEnum.OPTION_VALUE_RESOLUTION_AUTO;
                        break;
                    case 3:
                        value = PICOCastOptionValueEnum.OPTION_VALUE_RESOLUTION_HIGH_2K;
                        break;
                    case 4:
                        value = PICOCastOptionValueEnum.OPTION_VALUE_RESOLUTION_HIGH_4K;
                        break;
                    case 5:
                        value = PICOCastOptionValueEnum.OPTION_VALUE_BITRATE_HIGH;
                        break;
                    case 6:
                        value = PICOCastOptionValueEnum.OPTION_VALUE_BITRATE_MIDDLE;
                        break;
                    case 7:
                        value = PICOCastOptionValueEnum.OPTION_VALUE_BITRATE_LOW;
                        break;
                    case 8:
                        value = PICOCastOptionValueEnum.OPTION_VALUE_AUDIO_ON;
                        break;
                    case 9:
                        value = PICOCastOptionValueEnum.OPTION_VALUE_AUDIO_OFF;
                        break;
                    case 10:
                        value = PICOCastOptionValueEnum.STATUS_VALUE_STATE_STARTED;
                        break;
                    case 11:
                        value = PICOCastOptionValueEnum.STATUS_VALUE_STATE_STOPPED;
                        break;
                    case 12:
                        value = PICOCastOptionValueEnum.STATUS_VALUE_ERROR;
                        break;
                }
#endif
                return value;
            }


            public static int UPxr_SetSystemLanguage(String language) {
                int num = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                num = tobHelper.Call<int>("pbsSetSystemLanguage", language, 0);
#endif
                return num;
            }

            public static String UPxr_GetSystemLanguage() {
                string str = "";
#if UNITY_ANDROID && !UNITY_EDITOR
                str = tobHelper.Call<string>("pbsGetSystemLanguage", 0);
#endif
                return str;
            }

            public static int UPxr_ConfigWifi(String ssid, String pwd) {
                int num = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                num = tobHelper.Call<int>("pbsConfigWifi",ssid,pwd, 0);
#endif
                return num;
            }

            public static String[] UPxr_GetConfiguredWifi() {
#if UNITY_ANDROID && !UNITY_EDITOR
                return tobHelper.Call<string[]>("pbsGetConfiguredWifi",0);
#endif
                return null;
            }

            public static int UPxr_SetSystemCountryCode(String countryCode, Action<int> callback) {
                int num = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                if (callback != null) IntCallback = callback;
                num = tobHelper.Call<int>("pbsSetSystemCountryCode",countryCode,null,0);
#endif
                return num;
            }

            public static string UPxr_GetSystemCountryCode() {
                string str = "";
#if UNITY_ANDROID && !UNITY_EDITOR
                str = tobHelper.Call<string>("pbsGetSystemCountryCode",0);
#endif
                return str;
            }

            public static int UPxr_SetSkipInitSettingPage(int flag) {
                int num = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                num = tobHelper.Call<int>("pbsSetSkipInitSettingPage",flag,0);
#endif
                return num;
            }

            public static int UPxr_GetSkipInitSettingPage() {
                int num = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                num = tobHelper.Call<int>("pbsGetSkipInitSettingPage",0);
#endif
                return num;
            }

            public static int UPxr_IsInitSettingComplete() {
                int num = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                num = tobHelper.Call<int>("pbsIsInitSettingComplete",0);
#endif
                return num;
            }

            public static int UPxr_StartActivity(String packageName, String className, String action, String extra, String[] categories, int[] flags) {
                int num = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                num = tobHelper.Call<int>("pbsStartActivity", packageName, className, action, extra, categories, flags, 0);
#endif

                return num;
            }

            public static int UPxr_CustomizeAppLibrary(String[] packageNames, SwitchEnum switchEnum) {
                int num = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                num = tobHelper.Call<int>("pbsCustomizeAppLibrary", packageNames,GetEnumType(switchEnum), 0);
#endif
                return num;
            }

            public static int[] UPxr_GetControllerBattery() {
#if UNITY_ANDROID && !UNITY_EDITOR
                return tobHelper.Call<int[]>("pbsGetControllerBattery", 0);
#endif
                return null;
            }

            public static int UPxr_GetControllerConnectState() {
                int num = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                num = tobHelper.Call<int>("pbsGetControllerConnectState",0);
#endif
                return num;
            }

            public static string UPxr_GetAppLibraryHideList() {
                string str = " ";
#if UNITY_ANDROID && !UNITY_EDITOR
                str = tobHelper.Call<string>("pbsGetAppLibraryHideList",0);
#endif
                return str;
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
                PLog.i(TAG, "UPxr_SetFoveationLevel() level:" + level);
#if UNITY_ANDROID && !UNITY_EDITOR
                Pxr_SetFoveationLevelEnable((int)level);
#endif
            }

            public static FoveationLevel UPxr_GetFoveationLevel()
            {
                FoveationLevel result = FoveationLevel.None;
#if UNITY_ANDROID && !UNITY_EDITOR
                result = Pxr_GetFoveationLevel();
#endif
                PLog.i(TAG, "UPxr_GetFoveationLevel() result:" + result);
                return result;
            }

            public static int UPxr_SetFoveationParameters(float foveationGainX, float foveationGainY, float foveationArea, float foveationMinimum)
            {
                PLog.i(TAG, "UPxr_SetFoveationParameters() foveationGainX:" + foveationGainX + " foveationGainY:" + foveationGainY + " foveationArea:" + foveationArea + " foveationMinimum:" + foveationMinimum);
                int result = 0;
#if UNITY_ANDROID && !UNITY_EDITOR

                FoveationParams foveationParams = new FoveationParams();
                foveationParams.foveationGainX = foveationGainX;
                foveationParams.foveationGainY = foveationGainY;
                foveationParams.foveationArea = foveationArea;
                foveationParams.foveationMinimum = foveationMinimum;

                result = Pxr_SetFoveationParams(foveationParams);
#endif
                PLog.i(TAG, "UPxr_SetFoveationParameters() result:" + result);
                return result;
            }

            public static int UPxr_GetFrustum(EyeType eye, ref float fovLeft, ref float fovRight, ref float fovUp, ref float fovDown, ref float near, ref float far)
            {
                int result = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                result = Pxr_GetFrustum(eye, ref fovLeft, ref fovRight, ref fovUp, ref fovDown, ref near, ref far);
#endif
                PLog.i(TAG, "UPxr_GetFrustum() result:" + result + " eye:" + eye + " fovLeft:" + fovLeft + " fovRight:" + fovRight + " fovUp:" + fovUp + " fovDown:" + fovDown + " near:" + near + " far:" + far);
                return result;
            }

            public static int UPxr_SetFrustum(EyeType eye, float fovLeft, float fovRight, float fovUp, float fovDown, float near, float far)
            {
                int result = 1;
#if UNITY_ANDROID && !UNITY_EDITOR
                result = Pxr_SetFrustum(eye, fovLeft, fovRight, fovUp, fovDown, near, far);
#endif
                PLog.i(TAG, "UPxr_SetFrustum() result:" + result + " eye:" + eye + " fovLeft:" + fovLeft + " fovRight:" + fovRight + " fovUp:" + fovUp + " fovDown:" + fovDown + " near:" + near + " far:" + far);
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
                PLog.i(TAG, string.Format("UPxr_SetEyeFOV Pxr_SetConfigFloatArray type = {0}, fovData[0] = {1},  fovData[1] = {2},  fovData[2] = {3},  fovData[3] = {4}, result = {5}", type, fovData[0], fovData[1], fovData[2], fovData[3], result));
                return result;
            }

            public static void UPxr_CreateLayer(IntPtr layerParam)
            {
                PLog.i(TAG, "UPxr_CreateLayer() ");
#if UNITY_ANDROID && !UNITY_EDITOR
                Pxr_CreateLayer(layerParam);
#endif
            }

            public static void UPxr_CreateLayerParam(PxrLayerParam layerParam)
            {
                PLog.i(TAG, "UPxr_CreateLayerParam() ");
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
                PLog.i(TAG, "UPxr_GetLayerNextImageIndex() layerId:" + layerId + " imageIndex:" + imageIndex + " result:" + result);
                return result;
            }

            public static int UPxr_GetLayerImageCount(int layerId, EyeType eye, ref UInt32 imageCount)
            {
                int result = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                result = Pxr_GetLayerImageCount(layerId, eye, ref imageCount);
#endif
                PLog.i(TAG, "UPxr_GetLayerImageCount() layerId:" + layerId + " eye:" + eye + " imageCount:" + imageCount + " result:" + result);
                return result;
            }

            public static int UPxr_GetLayerImage(int layerId, EyeType eye, int imageIndex, ref UInt64 image)
            {
                int result = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                result = Pxr_GetLayerImage(layerId, eye, imageIndex, ref image);
#endif
                PLog.i(TAG, "UPxr_GetLayerImage() layerId:" + layerId + " eye:" + eye + " imageIndex:" + imageIndex + " image:" + image + " result:" + result);
                return result;
            }

            public static void UPxr_GetLayerImagePtr(int layerId, EyeType eye, int imageIndex, ref IntPtr image)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                Pxr_GetLayerImagePtr(layerId, eye, imageIndex, ref image);
#endif
                PLog.i(TAG, "UPxr_GetLayerImagePtr() layerId:" + layerId + " eye:" + eye + " imageIndex:" + imageIndex + " image:" + image);
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

            public static int UPxr_GetLayerAndroidSurface(int layerId, EyeType eye, ref IntPtr androidSurface)
            {
                int result = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                result = Pxr_GetLayerAndroidSurface(layerId, eye, ref androidSurface);
#endif
                PLog.i(TAG, "UPxr_GetLayerAndroidSurface() layerId:" + layerId + " eye:" + eye + " androidSurface:" + androidSurface + " result:" + result);
                return result;
            }

            public static int UPxr_DestroyLayer(int layerId)
            {
                int result = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                result = Pxr_DestroyLayer(layerId);
#endif
                PLog.i(TAG, "UPxr_DestroyLayer() layerId:" + layerId + " result:" + result);
                return result;
            }

            public static void UPxr_DestroyLayerByRender(int layerId)
            {
                PLog.i(TAG, "UPxr_DestroyLayerByRender() layerId:" + layerId);
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
                PLog.i(TAG, "UPxr_SubmitLayer() layer:" + layer + " result:" + result);
                return result;
            }

            public static int UPxr_SubmitLayerQuad(PxrLayerQuad layer)
            {
                int result = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                result = Pxr_SubmitLayerQuad(layer);
#endif
                PLog.i(TAG, "UPxr_SubmitLayerQuad() layer:" + layer + " result:" + result);
                return result;
            }

            public static bool UPxr_SubmitLayerQuad2(PxrLayerQuad2 layer)
            {
                int result = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                result = Pxr_SubmitLayerQuad2(layer);
#endif
                PLog.i(TAG, "UPxr_SubmitLayerQuad2() layer:" + layer + " result:" + result);
                return result == -8;
            }

            public static int UPxr_SubmitLayerCylinder(PxrLayerCylinder layer)
            {
                int result = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                result = Pxr_SubmitLayerCylinder(layer);
#endif
                PLog.i(TAG, "UPxr_SubmitLayerCylinder() layer:" + layer + " result:" + result);
                return result;
            }

            public static bool UPxr_SubmitLayerCylinder2(PxrLayerCylinder2 layer)
            {
                int result = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                result = Pxr_SubmitLayerCylinder2(layer);
#endif
                PLog.i(TAG, "UPxr_SubmitLayerCylinder2() layer:" + layer + " result:" + result);
                return result == -8;
            }

            public static bool UPxr_SubmitLayerEquirect(PxrLayerEquirect layer) // shape 3
            {
                int result = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                result = Pxr_SubmitLayerEquirect(layer);
#endif
                PLog.i(TAG, "UPxr_SubmitLayerEquirect() layer:" + layer + " result:" + result);
                return result == -8;
            }

            public static bool UPxr_SubmitLayerEquirect2(PxrLayerEquirect2 layer) // shape 4
            {
                int result = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                result = Pxr_SubmitLayerEquirect2(layer);
#endif
                PLog.i(TAG, "UPxr_SubmitLayerEquirect2() layer:" + layer + " result:" + result);
                return result == -8;
            }

            public static int UPxr_SubmitLayerCube2(PxrLayerCube2 layer)
            {
                int result = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                result = Pxr_SubmitLayerCube2(layer);
#endif
                PLog.i(TAG, "UPxr_SubmitLayerCube2() layer:" + layer + " result:" + result);
                return result;
            }

            public static void UPxr_SetLayerBlend(bool enable, PxrLayerBlend layerBlend)
            {
                PLog.i(TAG, "UPxr_SetLayerBlend() enable:" + enable + " layerBlend.srcColor:" + layerBlend.srcColor + " dstColor:" + layerBlend.dstColor + " srcAlpha:" + layerBlend.srcAlpha + " dstAlpha:" + layerBlend.dstAlpha);
#if UNITY_ANDROID && !UNITY_EDITOR
                Pxr_SetLayerBlend(enable, layerBlend);
#endif
            }

            public static void UPxr_SetSpaceWarp(bool value)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                Pxr_SetSpaceWarp(value?1:0);
#endif
                PLog.i(TAG, "UPxr_SetSpaceWarp " + value);
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
                PLog.i(TAG, string.Format("UPxr_ResetSensor : {0}", resetSensorOption));
                int result = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                result = Pxr_ResetSensor(resetSensorOption);
#endif
                PLog.i(TAG, string.Format("UPxr_ResetSensor result: {0}", result));
                return result;
            }

            public static int UPvr_Enable6DofModule(bool enable)
            {
                PLog.i(TAG, string.Format("UPvr_Enable6DofModule : {0}", enable));
#if UNITY_ANDROID && !UNITY_EDITOR
                return Pxr_SetConfigInt(ConfigType.Ability6Dof, enable?1:0);
#else
                return 0;
#endif
            }

            public static void UPxr_InitPsensor()
            {
                PLog.i(TAG, "UPxr_InitPsensor()");
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
                PLog.i(TAG, "UPxr_GetPSensorState()");
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
                PLog.i(TAG, "UPxr_GetPSensorState() psensor:" + psensor);
                return psensor;
            }

            public static void UPxr_UnregisterPsensor()
            {
                PLog.i(TAG, "UPxr_UnregisterPsensor()");
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
            private static AndroidJavaClass verifyTool = new AndroidJavaClass("com.psmart.aosoperation.VerifyTool");
            private static AndroidJavaClass MRCCalibration = new AndroidJavaClass("com.psmart.aosoperation.MRCCalibration");
            private static AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            private static AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            private static AndroidJavaObject verifyToolObj = new AndroidJavaObject("com.psmart.aosoperation.VerifyTool");
#endif
            public static PXR_PlatformSetting.simulationType UPxr_IsCurrentDeviceValid()
            {
                if (PXR_PlatformSetting.Instance.entitlementCheckSimulation)
                {
                    if (PXR_PlatformSetting.Instance.deviceSN.Count <= 0)
                    {
                        return PXR_PlatformSetting.simulationType.Null;
                    }
                    else
                    {
                        foreach (var t in PXR_PlatformSetting.Instance.deviceSN)
                        {
                            if (System.UPxr_GetDeviceSN() == t)
                            {
                                return PXR_PlatformSetting.simulationType.Valid;
                            }
                        }

                        return PXR_PlatformSetting.simulationType.Invalid;
                    }
                }
                else
                {
                    return PXR_PlatformSetting.simulationType.Invalid;
                }
            }

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

            public static void UPxr_BindVerifyService(string objectName)
            {
                PLog.i(TAG, "UPxr_BindVerifyService() objectName:" + objectName);
                bool state = false;
#if UNITY_ANDROID && !UNITY_EDITOR
                state = verifyToolObj.Call<bool>("bindVerifyService", currentActivity, objectName);
#endif
                PLog.i(TAG, "UPxr_BindVerifyService() state:" + state);
            }

            public static bool UPxr_AppEntitlementCheck(string appid)
            {
                PLog.i(TAG, "UPxr_AppEntitlementCheck() appid:" + appid);
                bool state = false;
#if UNITY_ANDROID && !UNITY_EDITOR
                try
                {
                    state = verifyTool.CallStatic<bool>("verifyAPP", currentActivity, appid, "");
                }
                catch (Exception e)
                {
                    PLog.e(TAG, "UPxr_AppEntitlementCheck Error :" + e.ToString());
                }
#endif
                PLog.i(TAG, "UPxr_AppEntitlementCheck() state:" + state);
                return state;
            }

            public static bool UPxr_KeyEntitlementCheck(string publicKey)
            {
                PLog.i(TAG, "UPxr_KeyEntitlementCheck() publicKey:" + publicKey);
                bool state = false;
#if UNITY_ANDROID && !UNITY_EDITOR
                try
                {
                    state = verifyTool.CallStatic<bool>("verifyAPP", currentActivity, "", publicKey);
                }
                catch (Exception e)
                {
                    PLog.e(TAG, "UPxr_KeyEntitlementCheck Error :" + e.ToString());
                }
#endif
                PLog.i(TAG, "UPxr_KeyEntitlementCheck() state:" + state);
                return state;
            }

            //0:success -1:invalid params -2:service not exist -3:time out
            public static int UPxr_AppEntitlementCheckExtra(string appId)
            {
                PLog.i(TAG, "UPxr_AppEntitlementCheckExtra() appId:" + appId);
                int state = -1;
#if UNITY_ANDROID && !UNITY_EDITOR
                try
                {
                    state = verifyTool.CallStatic<int>("verifyAPPExt", currentActivity, appId, "");
                }
                catch (Exception e)
                {
                    PLog.e(TAG, "UPxr_AppEntitlementCheckExtra Error :" + e.ToString());
                }
#endif
                PLog.i(TAG, "UPxr_AppEntitlementCheckExtra() state:" + state);
                return state;
            }

            //0:success -1:invalid params -2:service not exist -3:time out
            public static int UPxr_KeyEntitlementCheckExtra(string publicKey)
            {
                PLog.i(TAG, "UPxr_KeyEntitlementCheckExtra() publicKey:" + publicKey);
                int state = -1;
#if UNITY_ANDROID && !UNITY_EDITOR
                try
                {
                    state = verifyTool.CallStatic<int>("verifyAPPExt", currentActivity, "", publicKey);
                }
                catch (Exception e)
                {
                    PLog.e(TAG, "UPxr_KeyEntitlementCheckExtra Error :" + e.ToString());
                }
#endif
                PLog.i(TAG, "UPxr_KeyEntitlementCheckExtra() state:" + state);
                return state;
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


            public static int UPxr_SetControllerVibrationEvent(UInt32 hand, int frequency, float strength, int time)
            {
                if (PXR_Plugin.System.UPxr_GetAPIVersion() >= 0x2000305)
                {
#if UNITY_ANDROID && !UNITY_EDITOR
                Debug.Log("[VCMotor_SDK] UPxr_SetControllerVibrationEvent " + hand + " frequency: " + frequency.ToString() + " strength:" + strength.ToString() + " time:" + time.ToString());
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
                PLog.i(TAG, "UPxr_GetControllerType()" + type);
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
                PLog.i(TAG, "Pxr_GetControllerMainInputHandle()" + hand.ToString());
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