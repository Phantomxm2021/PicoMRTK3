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
using UnityEngine;

namespace Unity.XR.PXR
{
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

    public enum ControllerPairTimeEnum
    {
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

    public enum PICOCastUrlTypeEnum
    {
        NORMAL_URL,
        NO_CONFIRM_URL,
        RTMP_URL
    }

    public enum PICOCastOptionOrStatusEnum
    {
        OPTION_RESOLUTION_LEVEL,
        OPTION_BITRATE_LEVEL,
        OPTION_AUDIO_ENABLE,
        PICO_CAST_STATUS
    }

    public enum PICOCastOptionValueEnum
    {
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

    public enum ScreencastAudioOutputEnum
    {
        AUDIO_SINK = 0,
        AUDIO_TARGET = 1,
        AUDIO_SINK_TARGET = 2,
        AUDIO_ERROR = 5
    }

    public enum CustomizeSettingsTabEnum
    {
        CUSTOMIZE_SETTINGS_TAB_WLAN = 0,
        CUSTOMIZE_SETTINGS_TAB_CONTROLLER = 1,
        CUSTOMIZE_SETTINGS_TAB_BLUETOOTH = 2,
        CUSTOMIZE_SETTINGS_TAB_DISPLAY = 3,
        CUSTOMIZE_SETTINGS_TAB_LAB = 4,
        CUSTOMIZE_SETTINGS_TAB_GENERAL_LOCKSCREEN = 5,
        CUSTOMIZE_SETTINGS_TAB_GENERAL_FACTORY_RESET = 6
    }

    public enum ControllerKeyEnum
    {
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

    public class PXR_EnterprisePlugin
    {
        private const string TAG = "[PXR_EnterprisePlugin]";
#if UNITY_ANDROID && !UNITY_EDITOR
            private static AndroidJavaClass unityPlayer;
            private static AndroidJavaObject currentActivity;
            private static AndroidJavaClass sysActivity;
            private static AndroidJavaClass batteryReceiver;
            private static AndroidJavaClass audioReceiver;
            private static AndroidJavaObject tobHelper;
            private static AndroidJavaClass tobHelperClass;
#endif

        public static Action<bool> BoolCallback;
        public static Action<int> IntCallback;
        public static Action<long> LongCallback;
        public static Action<string> StringCallback;

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

        public static void UPxr_InitEnterpriseService()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
                tobHelperClass = new AndroidJavaClass("com.pvr.tobservice.ToBServiceHelper");
                tobHelper = tobHelperClass.CallStatic<AndroidJavaObject>("getInstance");
                unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
                sysActivity = new AndroidJavaClass("com.psmart.aosoperation.SysActivity");
                batteryReceiver = new AndroidJavaClass("com.psmart.aosoperation.BatteryReceiver");
                audioReceiver = new AndroidJavaClass("com.psmart.aosoperation.AudioReceiver");
#endif
        }

        public static void UPxr_SetUnityObjectName(string obj)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
                tobHelper.Call("setUnityObjectName", obj);
#endif
        }

        public static void UPxr_BindEnterpriseService()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
                tobHelper.Call("bindTobService", currentActivity);
#endif
        }

        public static void UPxr_UnBindEnterpriseService()
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

        public static void UPxr_SetControllerPairTime(ControllerPairTimeEnum timeEnum, Action<int> callback)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
                if (callback != null) IntCallback = callback;
                tobHelper.Call("pbsSetControllerPairTime", GetEnumType(timeEnum),null, 0);
#endif
        }

        public static void UPxr_GetControllerPairTime(Action<int> callback)
        {
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

        public static int UPxr_InstallOTAPackage(String otaPackagePath)
        {
            int value = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
            value = tobHelper.Call<int>("pbsInstallOTAPackage",otaPackagePath, 0);
#endif
            return value;
        }

        public static string UPxr_GetAutoConnectWiFiConfig()
        {
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

        public static int UPxr_GetControllerKeyState(ControllerKeyEnum pxrControllerKey)
        {
            int volue = 1;
#if UNITY_ANDROID && !UNITY_EDITOR
                volue = tobHelper.Call<int>("pbsGetControllerKeyState", GetEnumType(pxrControllerKey),0);
#endif
            return volue;
        }

        public static int UPxr_SetControllerKeyState(ControllerKeyEnum controllerKeyEnum, SwitchEnum status)
        {
            int volue = 1;
#if UNITY_ANDROID && !UNITY_EDITOR
                volue = tobHelper.Call<int>("pbsSetControllerKeyState", GetEnumType(controllerKeyEnum),GetEnumType(status),0);
#endif
            return volue;
        }

        public static SwitchEnum UPxr_ControlGetPowerOffWithUSBCable()
        {
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

        public static ScreenOffDelayTimeEnum UPxr_PropertyGetScreenOffDelay()
        {
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

        public static SleepDelayTimeEnum UPxr_PropertyGetSleepDelay()
        {
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

        public static string UPxr_PropertyGetPowerKeyStatus()
        {
            string str = "";
#if UNITY_ANDROID && !UNITY_EDITOR
                str = tobHelper.Call<string>("pbsPropertyGetPowerKeyStatus", 0);
#endif
            return str;
        }

        public static int UPxr_GetEnterKeyStatus()
        {
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

        public static string UPxr_PropertyGetHomKeyStatus(HomeEventEnum homeEvent)
        {
            string volue = "";
#if UNITY_ANDROID && !UNITY_EDITOR
                volue = tobHelper.Call<string>("pbsPropertyGetHomKeyStatus", GetEnumType(homeEvent),0);
#endif
            return volue;
        }

        public static void UPxr_GetSwitchSystemFunctionStatus(SystemFunctionSwitchEnum systemFunction, Action<int> callback)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
                if (callback != null) IntCallback = callback;
                tobHelper.Call("pbsGetSwitchSystemFunctionStatus", GetEnumType(systemFunction),null,0);
#endif
        }

        public static string UPxr_SwitchGetUsbConfigurationOption()
        {
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

        public static int UPxr_PICOCastInit(Action<int> callback)
        {
            int value = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                if (callback != null) IntCallback = callback;
                value = tobHelper.Call<int>("pbsPicoCastInit",null,0);
#endif
            return value;
        }

        public static int UPxr_PICOCastSetShowAuthorization(int authZ)
        {
            int value = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                value = tobHelper.Call<int>("pbsPicoCastSetShowAuthorization",authZ,0);
#endif
            return value;
        }

        public static int UPxr_PICOCastGetShowAuthorization()
        {
            int value = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                value = tobHelper.Call<int>("pbsPicoCastGetShowAuthorization",0);
#endif
            return value;
        }

        public static string UPxr_PICOCastGetUrl(PICOCastUrlTypeEnum urlType)
        {
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

        public static int UPxr_PICOCastSetOption(PICOCastOptionOrStatusEnum castOptionOrStatus, PICOCastOptionValueEnum castOptionValue)
        {
            int value = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                value = tobHelper.Call<int>("pbsPicoCastSetOption",GetEnumType(castOptionOrStatus),GetEnumType(castOptionValue),0);
#endif
            return value;
        }

        public static PICOCastOptionValueEnum UPxr_PICOCastGetOptionOrStatus(PICOCastOptionOrStatusEnum castOptionOrStatus)
        {
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

        public static int UPxr_SetSystemLanguage(String language)
        {
            int num = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                num = tobHelper.Call<int>("pbsSetSystemLanguage", language, 0);
#endif
            return num;
        }

        public static String UPxr_GetSystemLanguage()
        {
            string str = "";
#if UNITY_ANDROID && !UNITY_EDITOR
                str = tobHelper.Call<string>("pbsGetSystemLanguage", 0);
#endif
            return str;
        }

        public static int UPxr_ConfigWifi(String ssid, String pwd)
        {
            int num = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                num = tobHelper.Call<int>("pbsConfigWifi",ssid,pwd, 0);
#endif
            return num;
        }

        public static String[] UPxr_GetConfiguredWifi()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
                return tobHelper.Call<string[]>("pbsGetConfiguredWifi",0);
#endif
            return null;
        }

        public static int UPxr_SetSystemCountryCode(String countryCode, Action<int> callback)
        {
            int num = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                if (callback != null) IntCallback = callback;
                num = tobHelper.Call<int>("pbsSetSystemCountryCode",countryCode,null,0);
#endif
            return num;
        }

        public static string UPxr_GetSystemCountryCode()
        {
            string str = "";
#if UNITY_ANDROID && !UNITY_EDITOR
                str = tobHelper.Call<string>("pbsGetSystemCountryCode",0);
#endif
            return str;
        }

        public static int UPxr_SetSkipInitSettingPage(int flag)
        {
            int num = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                num = tobHelper.Call<int>("pbsSetSkipInitSettingPage",flag,0);
#endif
            return num;
        }

        public static int UPxr_GetSkipInitSettingPage()
        {
            int num = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                num = tobHelper.Call<int>("pbsGetSkipInitSettingPage",0);
#endif
            return num;
        }

        public static int UPxr_IsInitSettingComplete()
        {
            int num = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                num = tobHelper.Call<int>("pbsIsInitSettingComplete",0);
#endif
            return num;
        }

        public static int UPxr_StartActivity(String packageName, String className, String action, String extra, String[] categories, int[] flags)
        {
            int num = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                num = tobHelper.Call<int>("pbsStartActivity", packageName, className, action, extra, categories, flags, 0);
#endif

            return num;
        }

        public static int UPxr_CustomizeAppLibrary(String[] packageNames, SwitchEnum switchEnum)
        {
            int num = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                num = tobHelper.Call<int>("pbsCustomizeAppLibrary", packageNames,GetEnumType(switchEnum), 0);
#endif
            return num;
        }

        public static int[] UPxr_GetControllerBattery()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
                return tobHelper.Call<int[]>("pbsGetControllerBattery", 0);
#endif
            return null;
        }

        public static int UPxr_GetControllerConnectState()
        {
            int num = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                num = tobHelper.Call<int>("pbsGetControllerConnectState",0);
#endif
            return num;
        }

        public static string UPxr_GetAppLibraryHideList()
        {
            string str = " ";
#if UNITY_ANDROID && !UNITY_EDITOR
                str = tobHelper.Call<string>("pbsGetAppLibraryHideList",0);
#endif
            return str;
        }

        public static int UPxr_SetScreenCastAudioOutput(ScreencastAudioOutputEnum screencastAudioOutput)
        {
            int value = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                value = tobHelper.Call<int>("pbsSetScreenCastAudioOutput",GetEnumType(screencastAudioOutput),0);
#endif
            return value;
        }

        public static ScreencastAudioOutputEnum UPxr_GetScreenCastAudioOutput()
        {
            ScreencastAudioOutputEnum value = ScreencastAudioOutputEnum.AUDIO_ERROR;
#if UNITY_ANDROID && !UNITY_EDITOR
                int num = 0;               
                num = tobHelper.Call<int>("pbsGetScreenCastAudioOutput",0);
                switch (num)
                {
                    case 0:
                        value = ScreencastAudioOutputEnum.AUDIO_SINK;
                        break;
                    case 1:
                        value = ScreencastAudioOutputEnum.AUDIO_TARGET;
                        break;
                    case 2:
                        value = ScreencastAudioOutputEnum.AUDIO_SINK_TARGET;
                        break;
                }
#endif
            return value;
        }

        public static int UPxr_CustomizeSettingsTabStatus(CustomizeSettingsTabEnum customizeSettingsTabEnum, SwitchEnum switchEnum)
        {
            int value = 0;
#if UNITY_ANDROID && !UNITY_EDITOR
                value = tobHelper.Call<int>("pbsCustomizeSettingsTabStatus",GetEnumType(customizeSettingsTabEnum),GetEnumType(switchEnum),0);
#endif
            return value;
        }

        public static SwitchEnum UPxr_GetCustomizeSettingsTabStatus(CustomizeSettingsTabEnum customizeSettingsTabEnum)
        {
            SwitchEnum switchEnum = SwitchEnum.S_OFF;
#if UNITY_ANDROID && !UNITY_EDITOR
                int num = 0;
                num = tobHelper.Call<int>("pbsGetCustomizeSettingsTabStatus",GetEnumType(customizeSettingsTabEnum),0);
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
    }
}