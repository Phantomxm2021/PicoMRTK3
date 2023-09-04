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
using UnityEngine;

namespace Unity.XR.PXR
{
    public partial class PXR_EnterprisePlugin
    {
#if PICO_PLATFORM
            private static AndroidJavaClass sysActivity;
            private static AndroidJavaClass batteryReceiver;
            private static AndroidJavaClass audioReceiver;
#endif
        
        public static void UPxr_InitSystem()
        {
#if PICO_PLATFORM
                sysActivity = new AndroidJavaClass("com.psmart.aosoperation.SysActivity");
                batteryReceiver = new AndroidJavaClass("com.psmart.aosoperation.BatteryReceiver");
                audioReceiver = new AndroidJavaClass("com.psmart.aosoperation.AudioReceiver");
#endif
        }

        public static bool UPxr_StopBatteryReceiver()
        {
#if PICO_PLATFORM
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

        public static bool UPxr_StartBatteryReceiver(string objName)
        {
#if PICO_PLATFORM
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

        public static bool UPxr_InitAudioDevice()
        {
#if PICO_PLATFORM
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

        public static bool UPxr_SetBrightness(int brightness)
        {
#if PICO_PLATFORM
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
#if PICO_PLATFORM
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
#if PICO_PLATFORM
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
#if PICO_PLATFORM
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
#if PICO_PLATFORM
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
#if PICO_PLATFORM
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
#if PICO_PLATFORM
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
#if PICO_PLATFORM
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
#if PICO_PLATFORM
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
#if PICO_PLATFORM
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
#if PICO_PLATFORM
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
    }
}