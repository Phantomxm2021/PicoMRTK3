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
#if (UNITY_ANDROID && !UNITY_EDITOR)
#define PICO_PLATFORM
#endif
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace Unity.XR.PXR
{
    public partial class PXR_EnterprisePlugin
    {
        private const string TAG = "[PXR_EnterprisePlugin]";
#if PICO_PLATFORM
            private static AndroidJavaClass unityPlayer;
            private static AndroidJavaObject currentActivity;
            private static AndroidJavaObject tobHelper;
            private static AndroidJavaClass tobHelperClass;
            private static AndroidJavaObject IToBService;
#endif

        public static Action<bool> BoolCallback;
        public static Action<int> IntCallback;
        public static Action<long> LongCallback;
        public static Action<string> StringCallback;
        
        private static AndroidJavaObject GetEnumType(Enum enumType)
        {
            AndroidJavaClass enumjs =
                new AndroidJavaClass("com.pvr.tobservice.enums" + enumType.GetType().ToString().Replace("Unity.XR.PXR.", ".PBS_"));
            AndroidJavaObject enumjo = enumjs.GetStatic<AndroidJavaObject>(enumType.ToString());
            return enumjo;
        }
        
        public static void UPxr_InitEnterpriseService()
        {
#if PICO_PLATFORM
                tobHelperClass = new AndroidJavaClass("com.picoxr.tobservice.ToBServiceUtils");
                tobHelper = tobHelperClass.CallStatic<AndroidJavaObject>("getInstance");
                unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
#endif
        }
        
        public static void UPxr_SetBindCallBack(BindCallback mBoolCallback)
        {
#if PICO_PLATFORM
            tobHelper.Call("setBindCallBack", mBoolCallback);
#endif
        }

        public static void UPxr_BindEnterpriseService(Action<bool> callback = null)
        {
#if PICO_PLATFORM

            UPxr_SetBindCallBack(new BindCallback(callback));
            tobHelper.Call("bindTobService", currentActivity);
#endif
        }

        public static void UPxr_UnBindEnterpriseService()
        {
#if PICO_PLATFORM
                tobHelper.Call("unBindTobService");
#endif
        }

        public static void GetServiceBinder()
        {
#if PICO_PLATFORM
            IToBService = tobHelper.Call<AndroidJavaObject>("getServiceBinder");
#endif
        }

        public static string UPxr_StateGetDeviceInfo(SystemInfoEnum type)
        {
            string result = "";
#if PICO_PLATFORM
            if (IToBService == null)
            {
                return result;
            }
            result = IToBService.Call<string>("pbsStateGetDeviceInfo", GetEnumType(type), 0);
#endif
            return result;
        }

        public static void UPxr_ControlSetDeviceAction(DeviceControlEnum deviceControl, Action<int> callback)
        {
#if PICO_PLATFORM
            tobHelper.Call("pbsControlSetDeviceAction", GetEnumType(deviceControl), new IntCallback(callback));
#endif
        }

        public static void UPxr_ControlAPPManager(PackageControlEnum packageControl, string path, Action<int> callback)
        {
#if PICO_PLATFORM
            tobHelper.Call("pbsControlAPPManger", GetEnumType(packageControl), path, 0,  new IntCallback(callback));
#endif
        }

        public static void UPxr_ControlSetAutoConnectWIFI(string ssid, string pwd, Action<bool> callback)
        {
#if PICO_PLATFORM
            tobHelper.Call("pbsControlSetAutoConnectWIFI", ssid, pwd, 0, new BoolCallback(callback));
#endif
        }

        public static void UPxr_ControlClearAutoConnectWIFI(Action<bool> callback)
        {
#if PICO_PLATFORM
            tobHelper.Call("pbsControlClearAutoConnectWIFI", new BoolCallback(callback));
#endif
        }

        public static void UPxr_PropertySetHomeKey(HomeEventEnum eventEnum, HomeFunctionEnum function, Action<bool> callback)
        {
#if PICO_PLATFORM
            tobHelper.Call("pbsPropertySetHomeKey", GetEnumType(eventEnum), GetEnumType(function),  new BoolCallback(callback));
#endif
        }

        public static void UPxr_PropertySetHomeKeyAll(HomeEventEnum eventEnum, HomeFunctionEnum function, int timesetup, string pkg, string className, Action<bool> callback)
        {
#if PICO_PLATFORM
              
                tobHelper.Call("pbsPropertySetHomeKeyAll", GetEnumType(eventEnum), GetEnumType(function), timesetup, pkg, className, new BoolCallback(callback));
#endif
        }

        public static void UPxr_PropertyDisablePowerKey(bool isSingleTap, bool enable, Action<int> callback)
        {
#if PICO_PLATFORM
            tobHelper.Call("pbsPropertyDisablePowerKey", isSingleTap, enable,  new IntCallback(callback));
#endif
        }

        public static void UPxr_PropertySetScreenOffDelay(ScreenOffDelayTimeEnum timeEnum, Action<int> callback)
        {
#if PICO_PLATFORM
            tobHelper.Call("pbsPropertySetScreenOffDelay", GetEnumType(timeEnum), new IntCallback(callback));
#endif
        }

        public static void UPxr_PropertySetSleepDelay(SleepDelayTimeEnum timeEnum)
        {
#if PICO_PLATFORM
            if (IToBService == null)
            {
                return;
            }

            IToBService.Call("pbsPropertySetSleepDelay", GetEnumType(timeEnum));
#endif
        }

        public static void UPxr_SwitchSystemFunction(SystemFunctionSwitchEnum systemFunction, SwitchEnum switchEnum)
        {
#if PICO_PLATFORM
            if (IToBService == null)
            {
                return;
            }
            IToBService.Call("pbsSwitchSystemFunction", GetEnumType(systemFunction), GetEnumType(switchEnum), 0);
#endif
        }

        public static void UPxr_SwitchSetUsbConfigurationOption(USBConfigModeEnum uSBConfigModeEnum)
        {
#if PICO_PLATFORM
            if (IToBService == null)
            {
                return;
            }
            IToBService.Call("pbsSwitchSetUsbConfigurationOption", GetEnumType(uSBConfigModeEnum), 0);
#endif
        }

        public static void UPxr_SetControllerPairTime(ControllerPairTimeEnum timeEnum, Action<int> callback)
        {
#if PICO_PLATFORM
            tobHelper.Call("pbsSetControllerPairTime", GetEnumType(timeEnum),new IntCallback(callback), 0);
#endif
        }

        public static void UPxr_GetControllerPairTime(Action<int> callback)
        {
#if PICO_PLATFORM
            tobHelper.Call("pbsGetControllerPairTime",new IntCallback(callback), 0);
#endif
        }

        public static void UPxr_ScreenOn()
        {
#if PICO_PLATFORM
            if (IToBService == null)
            {
                return;
            }
            IToBService.Call("pbsScreenOn");
#endif
        }

        public static void UPxr_ScreenOff()
        {
#if PICO_PLATFORM
            if (IToBService == null)
            {
                return;
            }
            IToBService.Call("pbsScreenOff");
#endif
        }

        public static void UPxr_AcquireWakeLock()
        {
#if PICO_PLATFORM
            if (IToBService == null)
            {
                return;
            }
            IToBService.Call("pbsAcquireWakeLock");
#endif
        }

        public static void UPxr_ReleaseWakeLock()
        {
#if PICO_PLATFORM
            if (IToBService == null)
            {
                return;
            }
            IToBService.Call("pbsReleaseWakeLock");
#endif
        }

        public static void UPxr_EnableEnterKey()
        {
#if PICO_PLATFORM
            if (IToBService == null)
            {
                return;
            }
            IToBService.Call("pbsEnableEnterKey");
#endif
        }

        public static void UPxr_DisableEnterKey()
        {
#if PICO_PLATFORM
            if (IToBService == null)
            {
                return;
            }
            IToBService.Call("pbsDisableEnterKey");
#endif
        }

        public static void UPxr_EnableVolumeKey()
        {
#if PICO_PLATFORM
            if (IToBService == null)
            {
                return;
            }
            IToBService.Call("pbsEnableVolumeKey");
#endif
        }

        public static void UPxr_DisableVolumeKey()
        {
#if PICO_PLATFORM
            if (IToBService == null)
            {
                return;
            }
            IToBService.Call("pbsDisableVolumeKey");
#endif
        }

        public static void UPxr_EnableBackKey()
        {
#if PICO_PLATFORM
            if (IToBService == null)
            {
                return;
            }
            IToBService.Call("pbsEnableBackKey");
#endif
        }

        public static void UPxr_DisableBackKey()
        {
#if PICO_PLATFORM
            if (IToBService == null)
            {
                return;
            }
            IToBService.Call("pbsDisableBackKey");
#endif
        }

        public static void UPxr_WriteConfigFileToDataLocal(string path, string content, Action<bool> callback)
        {
#if PICO_PLATFORM
            tobHelper.Call("pbsWriteConfigFileToDataLocal", path, content, new BoolCallback(callback));
#endif
        }

        public static void UPxr_ResetAllKeyToDefault(Action<bool> callback)
        {
#if PICO_PLATFORM
            tobHelper.Call("pbsResetAllKeyToDefault", new BoolCallback(callback));
#endif
        }

        public static void UPxr_SetAPPAsHome(SwitchEnum switchEnum, string packageName)
        {
#if PICO_PLATFORM
            if (IToBService == null)
            {
                return;
            }
            IToBService.Call("pbsAppSetAPPAsHomeTwo", GetEnumType(switchEnum), packageName);
#endif
        }

        public static void UPxr_KillAppsByPidOrPackageName(int[] pids, string[] packageNames)
        {
#if PICO_PLATFORM
            if (IToBService == null)
            {
                return;
            }
            IToBService.Call("pbsKillAppsByPidOrPackageName", pids, packageNames, 0);
#endif
        }

        public static void UPxr_KillBackgroundAppsWithWhiteList(string[] packageNames)
        {
#if PICO_PLATFORM
            if (IToBService == null)
            {
                return;
            }
            IToBService.Call("pbsKillBackgroundAppsWithWhiteList",packageNames, 0);
#endif
        }

        public static void UPxr_FreezeScreen(bool freeze)
        {
#if PICO_PLATFORM
            if (IToBService == null)
            {
                return;
            }
            IToBService.Call("pbsFreezeScreen", freeze);
#endif
        }

        public static void UPxr_OpenMiracast()
        {
#if PICO_PLATFORM
            if (IToBService == null)
            {
                return;
            }
            IToBService.Call("pbsOpenMiracast");
#endif
        }

        public static bool UPxr_IsMiracastOn()
        {
            bool value = false;
#if PICO_PLATFORM
            if (IToBService == null)
            {
                return value;
            }

            value = IToBService.Call<bool>("pbsIsMiracastOn");
#endif
            return value;
        }

        public static void UPxr_CloseMiracast()
        {
#if PICO_PLATFORM
            if (IToBService == null)
            {
                return;
            }
            IToBService.Call("pbsCloseMiracast");
#endif
        }

        public static void UPxr_StartScan()
        {
#if PICO_PLATFORM
            if (IToBService == null)
            {
                return;
            }
            IToBService.Call("pbsStartScan");
#endif
        }

        public static void UPxr_StopScan()
        {
#if PICO_PLATFORM
            if (IToBService == null)
            {
                return;
            }
            IToBService.Call("pbsStopScan");
#endif
        }

        public static void UPxr_ConnectWifiDisplay(string modelJson)
        {
#if PICO_PLATFORM
            tobHelper.Call("pbsConnectWifiDisplay", modelJson);
#endif
        }

        public static void UPxr_DisConnectWifiDisplay()
        {
#if PICO_PLATFORM
            if (IToBService == null)
            {
                return;
            }
            IToBService.Call("pbsDisConnectWifiDisplay");
#endif
        }

        public static void UPxr_ForgetWifiDisplay(string address)
        {
#if PICO_PLATFORM
            if (IToBService == null)
            {
                return;
            }
            IToBService.Call("pbsForgetWifiDisplay", address);
#endif
        }

        public static void UPxr_RenameWifiDisplay(string address, string newName)
        {
#if PICO_PLATFORM
            if (IToBService == null)
            {
                return;
            }
            IToBService.Call("pbsRenameWifiDisplay", address, newName);
#endif
        }

        public static void UPxr_SetWDModelsCallback(Action<List<WifiDisplayModel>> callback)
        {
#if PICO_PLATFORM
            tobHelper.Call("pbsSetWDModelsCallback", new WifiDisplayModelCallback(callback));
#endif
        }

        public static void UPxr_SetWDJsonCallback(Action<string> callback)
        {
#if PICO_PLATFORM
                tobHelper.Call("pbsSetWDJsonCallback", new StringCallback(callback));
#endif
        }

        public static void UPxr_UpdateWifiDisplays()
        {
#if PICO_PLATFORM
            if (IToBService == null)
            {
                return;
            }
            IToBService.Call("pbsUpdateWifiDisplays");
#endif
        }

        public static string UPxr_GetConnectedWD()
        {
            string result = "";
#if PICO_PLATFORM
            result = tobHelper.Call<string>("pbsGetConnectedWD");
#endif
            return result;
        }

        public static void UPxr_SwitchLargeSpaceScene(bool open, Action<bool> callback)
        {
#if PICO_PLATFORM
            tobHelper.Call("pbsSwitchLargeSpaceScene", new BoolCallback(callback), open, 0);
#endif
        }

        public static void UPxr_GetSwitchLargeSpaceStatus(Action<string> callback)
        {
#if PICO_PLATFORM
            tobHelper.Call("pbsGetSwitchLargeSpaceStatus",new StringCallback(callback), 0);
#endif
        }

        public static bool UPxr_SaveLargeSpaceMaps()
        {
            bool value = false;
#if PICO_PLATFORM
            if (IToBService == null)
            {
                return value;
            }
            value = IToBService.Call<bool>("pbsSaveLargeSpaceMaps", 0);
#endif
            return value;
        }

        public static void UPxr_ExportMaps(Action<bool> callback)
        {
#if PICO_PLATFORM
            
                tobHelper.Call("pbsExportMaps", new BoolCallback(callback),0);
#endif
        }

        public static void UPxr_ImportMaps(Action<bool> callback)
        {
#if PICO_PLATFORM
            tobHelper.Call("pbsImportMaps", new BoolCallback(callback), 0);
#endif
        }

        public static float[] UPxr_GetCpuUsages()
        {
            float[] data = null;
#if PICO_PLATFORM
            data = tobHelper.Call<float[]>("pbsGetCpuUsages");
#endif
            return data;
        }

        public static float[] UPxr_GetDeviceTemperatures(int type, int source)
        {
            float[] data = null;
#if PICO_PLATFORM
            if (IToBService == null)
            {
                return null;
            }

            data = IToBService.Call<float[]>("pbsGetDeviceTemperatures", type, source);
#endif

            return data;
        }

        public static void UPxr_Capture()
        {
#if PICO_PLATFORM
            IToBService.Call("pbsCapture");
#endif
        }

        public static void UPxr_Record()
        {
#if PICO_PLATFORM
            IToBService.Call("pbsRecord");
#endif
        }

        public static void UPxr_ControlSetAutoConnectWIFIWithErrorCodeCallback(String ssid, String pwd, int ext, Action<int> callback)
        {
#if PICO_PLATFORM
            tobHelper.Call("pbsControlSetAutoConnectWIFIWithErrorCodeCallback",ssid,pwd,ext,new IntCallback(callback));
#endif
        }

        public static void UPxr_AppKeepAlive(String appPackageName, bool keepAlive, int ext)
        {
#if PICO_PLATFORM
            if (IToBService == null)
            {
                return ;
            }
            IToBService.Call("pbsAppKeepAlive",appPackageName,keepAlive,ext);
#endif
        }

        public static void UPxr_TimingStartup(int year, int month, int day, int hour, int minute, bool open)
        {
#if PICO_PLATFORM
            if (IToBService == null)
            {
                return ;
            }
            IToBService.Call("pbsTimingStartup", year, month, day, hour, minute, open);
#endif
        }

        public static void UPxr_TimingShutdown(int year, int month, int day, int hour, int minute, bool open)
        {
#if PICO_PLATFORM
            if (IToBService == null)
            {
                return ;
            }
            IToBService.Call("pbsTimingShutdown", year, month, day, hour, minute, open);
#endif
        }

        public static void UPxr_StartVrSettingsItem(StartVRSettingsEnum settingsEnum, bool hideOtherItem, int ext)
        {
#if PICO_PLATFORM
            if (IToBService == null)
            {
                return ;
            }
            IToBService.Call("pbsStartVrSettingsItem", GetEnumType(settingsEnum), hideOtherItem, ext);
#endif
        }

        public static void UPxr_SwitchVolumeToHomeAndEnter(SwitchEnum switchEnum, int ext)
        {
#if PICO_PLATFORM
            if (IToBService == null)
            {
                return ;
            }
            IToBService.Call("pbsSwitchVolumeToHomeAndEnter", GetEnumType(switchEnum), ext);
#endif
        }

        public static SwitchEnum UPxr_IsVolumeChangeToHomeAndEnter()
        {
            SwitchEnum switchEnum = SwitchEnum.S_OFF;
#if PICO_PLATFORM
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
#if PICO_PLATFORM
            if (IToBService == null)
            {
                return value;
            }
            
            value = IToBService.Call<int>("pbsInstallOTAPackage",otaPackagePath, 0);
#endif
            return value;
        }

        public static string UPxr_GetAutoConnectWiFiConfig()
        {
            string value= "";
#if PICO_PLATFORM
            if (IToBService == null)
            {
                return value;
            }

            value = IToBService.Call<string>("pbsGetAutoConnectWiFiConfig", 0);
#endif
            return value;
        }

        public static string UPxr_GetTimingStartupStatus()
        {
            string value = "";
#if PICO_PLATFORM
            if (IToBService == null)
            {
                return value;
            }

            value = IToBService.Call<string>("pbsGetTimingStartupStatus", 0);
#endif
            return value;
        }

        public static string UPxr_GetTimingShutdownStatus()
        {
            string value = "";
#if PICO_PLATFORM
            if (IToBService == null)
            {
                return value;
            }

            value = IToBService.Call<string>("pbsGetTimingShutdownStatus", 0);
#endif
            return value;
        }

        public static int UPxr_GetControllerKeyState(ControllerKeyEnum pxrControllerKey)
        {
            int value = 1;
#if PICO_PLATFORM
            if (IToBService == null)
            {
                return value;
            }

            value = IToBService.Call<int>("pbsGetControllerKeyState", GetEnumType(pxrControllerKey),0);
#endif
            return value;
        }

        public static int UPxr_SetControllerKeyState(ControllerKeyEnum controllerKeyEnum, SwitchEnum status)
        {
            int value = 1;
#if PICO_PLATFORM
            if (IToBService == null)
            {
                return value;
            }

            value = IToBService.Call<int>("pbsSetControllerKeyState", GetEnumType(controllerKeyEnum),GetEnumType(status),0);
#endif
            return value;
        }

        public static SwitchEnum UPxr_ControlGetPowerOffWithUSBCable()
        {
            SwitchEnum switchEnum = SwitchEnum.S_OFF;
#if PICO_PLATFORM
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
#if PICO_PLATFORM
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
#if PICO_PLATFORM
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
            string value = "";
#if PICO_PLATFORM
            if (IToBService == null)
            {
                return value;
            }

            value = IToBService.Call<string>("pbsPropertyGetPowerKeyStatus", 0);
#endif
            return value;
        }

        public static int UPxr_GetEnterKeyStatus()
        {
            int value = 1;
#if PICO_PLATFORM
            if (IToBService == null)
            {
                return value;
            }

            value = IToBService.Call<int>("pbsGetEnterKeyStatus",0);
#endif
            return value;
        }

        public static int UPxr_GetVolumeKeyStatus()
        {
            int value = 1;
#if PICO_PLATFORM
            if (IToBService == null)
            {
                return value;
            }

            value = IToBService.Call<int>("pbsGetVolumeKeyStatus",0);
#endif
            return value;
        }

        public static int UPxr_GetBackKeyStatus()
        {
            int value = 1;
#if PICO_PLATFORM
            if (IToBService == null)
            {
                return value;
            }

            value = IToBService.Call<int>("pbsGetBackKeyStatus",0);
#endif
            return value;
        }

        public static string UPxr_PropertyGetHomeKeyStatus(HomeEventEnum homeEvent)
        {
            string value = "";
#if PICO_PLATFORM
            if (IToBService == null)
            {
                return value;
            }

            value = IToBService.Call<string>("pbsPropertyGetHomKeyStatus", GetEnumType(homeEvent),0);
#endif
            return value;
        }

        public static void UPxr_GetSwitchSystemFunctionStatus(SystemFunctionSwitchEnum systemFunction, Action<int> callback)
        {
#if PICO_PLATFORM
            
                tobHelper.Call("pbsGetSwitchSystemFunctionStatus", GetEnumType(systemFunction),new IntCallback(callback),0);
#endif
        }

        public static string UPxr_SwitchGetUsbConfigurationOption()
        {
            string value = "";
#if PICO_PLATFORM
            if (IToBService == null)
            {
                return value;
            }

            value = IToBService.Call<string>("pbsSwitchGetUsbConfigurationOption", 0);
#endif
            return value;
        }

        public static string UPxr_GetCurrentLauncher()
        {
            string value = "";
#if PICO_PLATFORM
            if (IToBService == null)
            {
                return value;
            }

            value = IToBService.Call<string>("pbsGetCurrentLauncher", 0);
#endif
            return value;
        }

        public static int UPxr_PICOCastInit(Action<int> callback)
        {
            int value = 0;
#if PICO_PLATFORM
            
                value = tobHelper.Call<int>("pbsPicoCastInit",new IntCallback(callback),0);
#endif
            return value;
        }

        public static int UPxr_PICOCastSetShowAuthorization(int authZ)
        {
            int value = 0;
#if PICO_PLATFORM
            if (IToBService == null)
            {
                return value;
            }

            value = IToBService.Call<int>("pbsPicoCastSetShowAuthorization",authZ,0);
#endif
            return value;
        }

        public static int UPxr_PICOCastGetShowAuthorization()
        {
            int value = 0;
#if PICO_PLATFORM
            if (IToBService == null)
            {
                return value;
            }
            value = IToBService.Call<int>("pbsPicoCastGetShowAuthorization",0);
#endif
            return value;
        }

        public static string UPxr_PICOCastGetUrl(PICOCastUrlTypeEnum urlType)
        {
            string value = "";
#if PICO_PLATFORM
            if (IToBService == null)
            {
                return value;
            }
            value = IToBService.Call<string>("pbsPicoCastGetUrl",GetEnumType(urlType), 0);
#endif
            return value;
        }

        public static int UPxr_PICOCastStopCast()
        {
            int value = 0;
#if PICO_PLATFORM
            if (IToBService == null)
            {
                return value;
            }
            value = IToBService.Call<int>("pbsPicoCastStopCast",0);
#endif
            return value;
        }

        public static int UPxr_PICOCastSetOption(PICOCastOptionOrStatusEnum castOptionOrStatus, PICOCastOptionValueEnum castOptionValue)
        {
            int value = 0;
#if PICO_PLATFORM
            if (IToBService == null)
            {
                return value;
            }
            value = IToBService.Call<int>("pbsPicoCastSetOption",GetEnumType(castOptionOrStatus),GetEnumType(castOptionValue),0);
#endif
            return value;
        }

        public static PICOCastOptionValueEnum UPxr_PICOCastGetOptionOrStatus(PICOCastOptionOrStatusEnum castOptionOrStatus)
        {
            PICOCastOptionValueEnum value = PICOCastOptionValueEnum.STATUS_VALUE_ERROR;
#if PICO_PLATFORM
                int num = 0;
                if (IToBService == null)
                {
                    return value;
                 }
                num = IToBService.Call<int>("pbsPicoCastGetOptionOrStatus", GetEnumType(castOptionOrStatus), 0);
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
#if PICO_PLATFORM
                num = IToBService.Call<int>("pbsSetSystemLanguage", language, 0);
#endif
            return num;
        }

        public static String UPxr_GetSystemLanguage()
        {
            string value = "";
#if PICO_PLATFORM
            value = IToBService.Call<string>("pbsGetSystemLanguage", 0);
#endif
            return value;
        }

        public static int UPxr_ConfigWifi(String ssid, String pwd)
        {
            int num = 0;
#if PICO_PLATFORM
                num = IToBService.Call<int>("pbsConfigWifi",ssid,pwd, 0);
#endif
            return num;
        }

        public static String[] UPxr_GetConfiguredWifi()
        {
#if PICO_PLATFORM
                return IToBService.Call<string[]>("pbsGetConfiguredWifi",0);
#endif
            return null;
        }

        public static int UPxr_SetSystemCountryCode(String countryCode, Action<int> callback)
        {
            int num = 0;
#if PICO_PLATFORM
            num = IToBService.Call<int>("pbsSetSystemCountryCode",countryCode,new IntCallback(callback),0);
#endif
            return num;
        }

        public static string UPxr_GetSystemCountryCode()
        {
            string value = "";
#if PICO_PLATFORM
            value = IToBService.Call<string>("pbsGetSystemCountryCode",0);
#endif
            return value;
        }

        public static int UPxr_SetSkipInitSettingPage(int flag)
        {
            int num = 0;
#if PICO_PLATFORM
                num = IToBService.Call<int>("pbsSetSkipInitSettingPage",flag,0);
#endif
            return num;
        }

        public static int UPxr_GetSkipInitSettingPage()
        {
            int num = 0;
#if PICO_PLATFORM
                num = IToBService.Call<int>("pbsGetSkipInitSettingPage",0);
#endif
            return num;
        }

        public static int UPxr_IsInitSettingComplete()
        {
            int num = 0;
#if PICO_PLATFORM
                num = IToBService.Call<int>("pbsIsInitSettingComplete",0);
#endif
            return num;
        }

        public static int UPxr_StartActivity(String packageName, String className, String action, String extra, String[] categories, int[] flags)
        {
            int num = 0;
#if PICO_PLATFORM
                num = IToBService.Call<int>("pbsStartActivity", packageName, className, action, extra, categories, flags, 0);
#endif

            return num;
        }

        public static int UPxr_CustomizeAppLibrary(String[] packageNames, SwitchEnum switchEnum)
        {
            int num = 0;
#if PICO_PLATFORM
            if (IToBService == null)
            {
                return num;
            }
            num = IToBService.Call<int>("pbsCustomizeAppLibrary", packageNames,GetEnumType(switchEnum), 0);
#endif
            return num;
        }

        public static int[] UPxr_GetControllerBattery()
        {
#if PICO_PLATFORM
            if (IToBService == null)
            {
                return null;
            }
            return IToBService.Call<int[]>("pbsGetControllerBattery", 0);
#endif
            return null;
        }

        public static int UPxr_GetControllerConnectState()
        {
            int num = 0;
#if PICO_PLATFORM
            if (IToBService == null)
            {
                return num;
            }
            num = IToBService.Call<int>("pbsGetControllerConnectState",0);
#endif
            return num;
        }

        public static string UPxr_GetAppLibraryHideList()
        {
            string value = " ";
#if PICO_PLATFORM
            if (IToBService == null)
            {
                return value;
            }

            value = IToBService.Call<string>("pbsGetAppLibraryHideList",0);
#endif
            return value;
        }

        public static int UPxr_SetScreenCastAudioOutput(ScreencastAudioOutputEnum screencastAudioOutput)
        {
            int value = 0;
#if PICO_PLATFORM
            if (IToBService == null)
            {
                return value;
            }

            value = IToBService.Call<int>("pbsSetScreenCastAudioOutput",GetEnumType(screencastAudioOutput),0);
#endif
            return value;
        }

        public static ScreencastAudioOutputEnum UPxr_GetScreenCastAudioOutput()
        {
            ScreencastAudioOutputEnum value = ScreencastAudioOutputEnum.AUDIO_ERROR;
#if PICO_PLATFORM
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
#if PICO_PLATFORM
            if (IToBService == null)
            {
                return value;
            }

            value = IToBService.Call<int>("pbsCustomizeSettingsTabStatus", GetEnumType(customizeSettingsTabEnum), GetEnumType(switchEnum), 0);
#endif
            return value;
        }

        public static SwitchEnum UPxr_GetCustomizeSettingsTabStatus(CustomizeSettingsTabEnum customizeSettingsTabEnum)
        {
            SwitchEnum switchEnum = SwitchEnum.S_OFF;
#if PICO_PLATFORM
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
        
        public static void UPxr_SetPowerOffWithUSBCable(SwitchEnum switchEnum)
        {
           
#if PICO_PLATFORM
             if (IToBService==null)
            {
                return;
            }
            IToBService.Call("pbsControlSetPowerOffWithUSBCable", GetEnumType(switchEnum),0);
#endif
        }
        public static void UPxr_RemoveControllerHomeKey(HomeEventEnum EventEnum)
        {
#if PICO_PLATFORM
            if (IToBService==null)
            {
                return;
            }
            IToBService.Call("pbsRemoveControllerHomeKey", GetEnumType(EventEnum));
#endif
        }
        public static void UPxr_SetPowerOnOffLogo(PowerOnOffLogoEnum powerOnOffLogoEnum, String path, Action<bool> callback)
        {
#if PICO_PLATFORM
            tobHelper.Call("pbsPropertySetPowerOnOffLogo",GetEnumType(powerOnOffLogoEnum),path,0, new BoolCallback(callback));
#endif
        }
        public static void UPxr_SetIPD(float ipd, Action<int> callback)
        {
#if PICO_PLATFORM
            tobHelper.Call("pbsSetIPD",ipd, new IntCallback(callback));
#endif
        }
        
        public static string UPxr_GetAutoMiracastConfig()
        {
            string value = " ";
#if PICO_PLATFORM
            if (IToBService == null)
            {
                return value;
            }
            value = IToBService.Call<string>("pbsGetAutoMiracastConfig",0);
#endif
            return value;
        }
        public static int UPxr_SetPicoCastMediaFormat(PicoCastMediaFormat mediaFormat)
        {
            int value = -1;
#if PICO_PLATFORM
            value = tobHelper.Call<int>("setPicoCastMediaFormat",mediaFormat.bitrate,0);
#endif
            return value;
        }
        
        public static int UPxr_setMarkerInfoCallback(TrackingOriginModeFlags trackingMode,float cameraYOffset,Action<List<MarkerInfo>>  mediaFormat)
        {
            int value = -1;

#if PICO_PLATFORM
            value = tobHelper.Call<int>("setMarkerInfoCallback",new MarkerInfoCallback(trackingMode,cameraYOffset,mediaFormat));
#endif
            return value;
        }
    }
}