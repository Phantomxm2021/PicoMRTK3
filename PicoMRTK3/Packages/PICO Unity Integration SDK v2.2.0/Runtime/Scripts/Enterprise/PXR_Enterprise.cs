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

namespace Unity.XR.PXR
{
    /**
     * Enterprise APIs are only supported by enterprise devices, including PICO Neo2, Neo2 Eye, Neo3 Pro、Neo3 Pro Eye, G2 4K/4K E/4K Plus (system version 4.0.3 or later), and PICO 4 Enterprise.
     * Do not use them on consumer devices.
     */
    public class PXR_Enterprise
    {
        /// <summary>
        /// Initializes the enterprise service for a specified object. Must be called before calling other enterprise APIs.
        /// </summary>
        /// <param name="objectName">The name of the object to initialize the enterprise service for.</param>
        public static void InitEnterpriseService(string objectName)
        {
            PXR_EnterprisePlugin.UPxr_InitEnterpriseService();
            PXR_EnterprisePlugin.UPxr_SetUnityObjectName(objectName);
            PXR_EnterprisePlugin.UPxr_InitAudioDevice();
        }

        /// <summary>
        /// Binds the enterprise service. Must be called before calling other system related functions.
        /// </summary>
        public static void BindEnterpriseService()
        {
            PXR_EnterprisePlugin.UPxr_BindEnterpriseService();
        }

        /// <summary>
        /// Unbinds the enterprise service.
        /// </summary>
        public static void UnBindEnterpriseService()
        {
            PXR_EnterprisePlugin.UPxr_UnBindEnterpriseService();
        }

        /// <summary>
        /// Turns on the power service for a specified object.
        /// </summary>
        /// <param name="objName">The name of the object to turn on the power service for.</param>
        /// <returns>Whether the power service has been turned on:
        /// * `true`: success
        /// * `false`: failure
        /// </returns>
        public static bool StartBatteryReceiver(string objName)
        {
            return PXR_EnterprisePlugin.UPxr_StartBatteryReceiver(objName);
        }

        /// <summary>
        /// Turns off the power service.
        /// </summary>
        /// <returns>Whether the power service has been turned off:
        /// * `true`: success
        /// * `false`: failure
        /// </returns>
        public static bool StopBatteryReceiver()
        {
            return PXR_EnterprisePlugin.UPxr_StopBatteryReceiver();
        }

        /// <summary>
        /// Sets the brightness for the current HMD.
        /// </summary>
        /// <param name="brightness">Target brightness. Value range: [0,255].</param>
        /// <returns>Whether the brightness has been set successfully:
        /// * `true`: success
        /// * `false`: failure
        /// </returns>
        public static bool SetCommonBrightness(int brightness)
        {
            return PXR_EnterprisePlugin.UPxr_SetBrightness(brightness);
        }

        /// <summary>
        /// Gets the brightness of the current HMD.
        /// </summary>
        /// <returns>An int value that indicates the brightness. Value range: [0,255].</returns>
        public static int GetCommonBrightness()
        {
            return PXR_EnterprisePlugin.UPxr_GetCurrentBrightness();
        }

        /// <summary>
        /// Gets the brightness level of the current screen.
        /// </summary>
        /// <returns>An int array. The first bit is the total brightness level supported, the second bit is the current brightness level, and it is the interval value of the brightness level from the third bit to the end bit.</returns>
        public static int[] GetScreenBrightnessLevel()
        {
            return PXR_EnterprisePlugin.UPxr_GetScreenBrightnessLevel();
        }

        /// <summary>
        /// Sets a brightness level for the current screen.
        /// </summary>
        /// <param name="brightness">Brightness mode:
        /// * `0`: system default brightness setting.
        /// * `1`: custom brightness setting, you can then set param `level`.
        /// </param>
        /// <param name="level">Brightness level. Value range: [1,255].</param>
        public static void SetScreenBrightnessLevel(int brightness, int level)
        {
            PXR_EnterprisePlugin.UPxr_SetScreenBrightnessLevel(brightness, level);
        }

        /// <summary>
        /// Initializes the audio device.
        /// </summary>
        /// <returns>Whether the audio device has been initialized:
        /// * `true`: success
        /// * `false`: failure
        /// </returns>
        public static bool InitAudioDevice()
        {
            return PXR_EnterprisePlugin.UPxr_InitAudioDevice();
        }

        /// <summary>
        /// Turns on the volume service for a specified object.
        /// </summary>
        /// <param name="objName">The name of the object to turn on the volume service for.</param>
        /// <returns>Whether the volume service has been turned on:
        /// * `true`: success
        /// * `false`: failure
        /// </returns>
        public static bool StartAudioReceiver(string objName)
        {
            return PXR_EnterprisePlugin.UPxr_StartAudioReceiver(objName);
        }

        /// <summary>
        /// Turns off the volume service.
        /// </summary>
        /// <returns>Whether the volume service has been turned off:
        /// * `true`: success
        /// * `false`: failure
        /// </returns>
        public static bool StopAudioReceiver()
        {
            return PXR_EnterprisePlugin.UPxr_StopAudioReceiver();
        }

        /// <summary>
        /// Gets the maximum volume. Call `InitAudioDevice` to initialize the audio device before using this API.
        /// </summary>
        /// <returns>An int value that indicates the maximum volume.</returns>
        public static int GetMaxVolumeNumber()
        {
            return PXR_EnterprisePlugin.UPxr_GetMaxVolumeNumber();
        }

        /// <summary>
        /// Gets the current volume. Call `InitAudioDevice` to initialize the audio device before using this API.
        /// </summary>
        /// <returns>An int value that indicates the current volume. Value range: [0,15].</returns>
        public static int GetCurrentVolumeNumber()
        {
            return PXR_EnterprisePlugin.UPxr_GetCurrentVolumeNumber();
        }

        /// <summary>
        /// Increases the volume. Call `InitAudioDevice` to initialize the audio device before using this API.
        /// </summary>
        /// <returns>Whether the volume has been increased:
        /// * `true`: success
        /// * `false`: failure
        /// </returns>
        public static bool VolumeUp()
        {
            return PXR_EnterprisePlugin.UPxr_VolumeUp();
        }

        /// <summary>
        /// Decreases the volume. Call `InitAudioDevice` to initialize the audio device before using this API.
        /// </summary>
        /// <returns>Whether the volume has been decreased:
        /// * `true`: success
        /// * `false`: failure
        /// </returns>
        public static bool VolumeDown()
        {
            return PXR_EnterprisePlugin.UPxr_VolumeDown();
        }

        /// <summary>
        /// Sets a volume. Call `InitAudioDevice` to initialize the audio device before using this API.
        /// </summary>
        /// <param name="volume">The target volume. Value range: [0,15].</param>
        /// <returns>Whether the target volume has been set:
        /// * `true`: success
        /// * `false`: failure
        /// </returns>
        public static bool SetVolumeNum(int volume)
        {
            return PXR_EnterprisePlugin.UPxr_SetVolumeNum(volume);
        }

        /// <summary>
        /// Gets the specified type of device information.
        /// </summary>
        /// <param name="type">The target information type. Enumerations:
        /// * `ELECTRIC_QUANTITY`: battery
        /// * `PUI_VERSION`: system version
        /// * `EQUIPMENT_MODEL`: device model
        /// * `EQUIPMENT_SN`: device SN code
        /// * `CUSTOMER_SN`: customer SN code
        /// * `INTERNAL_STORAGE_SPACE_OF_THE_DEVICE`: device storage
        /// * `DEVICE_BLUETOOTH_STATUS`: bluetooth status
        /// * `BLUETOOTH_NAME_CONNECTED`: bluetooth name
        /// * `BLUETOOTH_MAC_ADDRESS`: bluetooth MAC address
        /// * `DEVICE_WIFI_STATUS`: Wi-Fi connection status
        /// * `WIFI_NAME_CONNECTED`: connected Wi-Fi name
        /// * `WLAN_MAC_ADDRESS`: WLAN MAC address
        /// * `DEVICE_IP`: device IP address
        /// * `CHARGING_STATUS`: device charging status
        /// </param>
        /// <returns>The specified type of device information. For `CHARGING_STATUS`, an int value will be returned: `2`-charging; `3`-not charging.</returns>
        public static string StateGetDeviceInfo(SystemInfoEnum type)
        {
            return PXR_EnterprisePlugin.UPxr_StateGetDeviceInfo(type);
        }

        /// <summary>
        /// Shuts down or reboots the device.
        /// @note This is a protected API. You need to add `<meta-data android:name="pico_advance_interface" android:value="0"/>`
        /// to the app's AndroidManifest.xml file for calling this API, after which the app is unable to be published on the PICO Store.
        /// </summary>
        /// <param name="deviceControl">Device action. Enumerations:
        /// * `DEVICE_CONTROL_REBOOT`
        /// * `DEVICE_CONTROL_SHUTDOWN`
        /// </param>
        /// <param name="callback">Callback:
        /// * `1`: failed to shut down or reboot the device
        /// * `2`: no permission to perform this operation
        /// </param>
        public static void ControlSetDeviceAction(DeviceControlEnum deviceControl, Action<int> callback)
        {
            PXR_EnterprisePlugin.UPxr_ControlSetDeviceAction(deviceControl, callback);
        }

        /// <summary>
        /// Installs or uninstalls app silently.
        /// @note This is a protected API. You need to add `<meta-data android:name="pico_advance_interface" android:value="0"/>`
        /// to the app's AndroidManifest.xml file for calling this API, after which the app is unable to be published on the PICO Store.
        /// </summary>
        /// <param name="packageControl">The action. Enumerations:
        /// * `PACKAGE_SILENCE_INSTALL`: silent installation
        /// * `PACKAGE_SILENCE_UNINSTALL`: silent uninstallation
        /// </param>
        /// <param name="path">The path to the app package for silent installation or the name of the app package for silent uninstallation.</param>
        /// <param name="callback">Callback:
        /// * `0`: success
        /// * `1`: failure
        /// * `2`: no permission to perform this operation
        /// </param>
        public static void ControlAPPManager(PackageControlEnum packageControl, string path, Action<int> callback)
        {
            PXR_EnterprisePlugin.UPxr_ControlAPPManager(packageControl, path, callback);
        }

        /// <summary>
        /// Sets a Wi-Fi that the device is automatically connected to.
        /// </summary>
        /// <param name="ssid">Wi-Fi name.</param>
        /// <param name="pwd">Wi-Fi password.</param>
        /// <param name="callback">Callback:
        /// * `true`: connected
        /// * `false`: failed to connect
        /// </param>
        public static void ControlSetAutoConnectWIFI(string ssid, string pwd, Action<bool> callback)
        {
            PXR_EnterprisePlugin.UPxr_ControlSetAutoConnectWIFI(ssid, pwd, callback);
        }

        /// <summary>
        /// Removes the Wi-Fi that the device is automatically connected to.
        /// </summary>
        /// <param name="callback">Callback:
        /// * `true`: removed
        /// * `false`: failed to remove
        /// </param>
        public static void ControlClearAutoConnectWIFI(Action<bool> callback)
        {
            PXR_EnterprisePlugin.UPxr_ControlClearAutoConnectWIFI(callback);
        }

        /// <summary>
        /// Sets the Home button event.
        /// </summary>
        /// <param name="eventEnum">Target event. Enumerations:
        /// * `SINGLE_CLICK`: single-click
        /// * `DOUBLE_CLICK`: double-click
        /// * `LONG_PRESS`: long press
        /// * `SINGLE_CLICK_RIGHT_CTL`: single-click on the right controller's Home button
        /// * `DOUBLE_CLICK_RIGHT_CTL`: double-click on the right controller's Home button
        /// * `LONG_PRESS_RIGHT_CTL`: long press on the right controller's Home button
        /// * `SINGLE_CLICK_LEFT_CTL`: single-click on the left controller's Home button
        /// * `DOUBLE_CLICK_LEFT_CTL`: double-click on the left controller's Home button
        /// * `LONG_PRESS_LEFT_CTL`: long press on the left controller's Home button
        /// * `SINGLE_CLICK_HMD`: single-click on the HMD's Home button
        /// * `DOUBLE_CLICK_HMD`: double-click on the HMD's Home button
        /// * `LONG_PRESS_HMD`: long press on the HMD's Home button
        /// </param>
        /// <param name="function">The function of the event. Enumerations:
        /// * `VALUE_HOME_GO_TO_SETTING`: go to Settings
        /// * `VALUE_HOME_BACK`: back (only supported by PICO G2 4K)
        /// * `VALUE_HOME_RECENTER`: recenter the screen
        /// * `VALUE_HOME_OPEN_APP`: open a specified app
        /// * `VALUE_HOME_DISABLE`: disable the Home button
        /// * `VALUE_HOME_GO_TO_HOME`: open the launcher
        /// * `VALUE_HOME_SEND_BROADCAST`: send Home-button-click broadcast
        /// * `VALUE_HOME_CLEAN_MEMORY`: clear background apps
        /// * `VALUE_HOME_QUICK_SETTING`: enable quick settings
        /// * `VALUE_HOME_SCREEN_CAP`: enable screen capture
        /// * `VALUE_HOME_SCREEN_RECORD`: enable screen recording
        /// </param>
        /// <param name="callback">Callback:
        /// * `true`: success
        /// * `false`: failure
        /// </param>
        public static void PropertySetHomeKey(HomeEventEnum eventEnum, HomeFunctionEnum function, Action<bool> callback)
        {
            PXR_EnterprisePlugin.UPxr_PropertySetHomeKey(eventEnum, function, callback);
        }

        /// <summary>
        /// Sets extended settings for the Home button.
        /// </summary>
        /// <param name="eventEnum">Target event. Enumerations:
        /// * `SINGLE_CLICK_RIGHT_CTL`: single-click on the right controller's Home button
        /// * `DOUBLE_CLICK_RIGHT_CTL`: double-click on the right controller's Home button
        /// * `LONG_PRESS_RIGHT_CTL`: long press on the right controller's Home button
        /// * `SINGLE_CLICK_LEFT_CTL`: single-click on the left controller's Home button
        /// * `DOUBLE_CLICK_LEFT_CTL`: double-click on the left controller's Home button
        /// * `LONG_PRESS_LEFT_CTL`: long press on the left controller's Home button
        /// * `SINGLE_CLICK_HMD`: single-click on the HMD's Home button
        /// * `DOUBLE_CLICK_HMD`: double-click on the HMD's Home button
        /// * `LONG_PRESS_HMD`: long press on the HMD's Home button
        /// </param>
        /// <param name="function">The function of the event. Enumerations:
        /// * `VALUE_HOME_GO_TO_SETTING`: go to Settings
        /// * `VALUE_HOME_BACK`: back (only supported by PICO G2 4K)
        /// * `VALUE_HOME_RECENTER`: recenter the screen
        /// * `VALUE_HOME_OPEN_APP`: open a specified app
        /// * `VALUE_HOME_DISABLE`: disable the Home button
        /// * `VALUE_HOME_GO_TO_HOME`: open the launcher
        /// * `VALUE_HOME_SEND_BROADCAST`: send Home-key-click broadcast
        /// * `VALUE_HOME_CLEAN_MEMORY`: clear background apps
        /// * `VALUE_HOME_QUICK_SETTING`: enable quick settings
        /// * `VALUE_HOME_SCREEN_CAP`: enable screen capture
        /// * `VALUE_HOME_SCREEN_RECORD`: enable screen recording
        /// </param>
        /// <param name="timesetup">The interval of key pressing is set only if there is the double click event or long pressing event. When shortly pressing the Home button, pass `0`.</param>
        /// <param name="pkg">Pass `null`.</param>
        /// <param name="className">Pass `null`.</param>
        /// <param name="callback">Callback:
        /// * `true`: set
        /// * `false`: failed to set
        /// </param>
        public static void PropertySetHomeKeyAll(HomeEventEnum eventEnum, HomeFunctionEnum function, int timesetup, string pkg, string className, Action<bool> callback)
        {
            PXR_EnterprisePlugin.UPxr_PropertySetHomeKeyAll(eventEnum, function, timesetup, pkg, className, callback);
        }

        /// <summary>
        /// Sets the Power button's event.
        /// </summary>
        /// <param name="isSingleTap">Whether it is a single click event:
        /// * `true`: single-click event
        /// * `false`: long-press event
        /// </param>
        /// <param name="enable">Enable or disable the Power button:
        /// * `true`: enable
        /// * `false`: disable
        /// </param>
        /// <param name="callback">Callback:
        /// * `0`: set
        /// * `1`: failed to set
        /// </param>
        public static void PropertyDisablePowerKey(bool isSingleTap, bool enable, Action<int> callback)
        {
            PXR_EnterprisePlugin.UPxr_PropertyDisablePowerKey(isSingleTap, enable, callback);
        }

        /// <summary>
        /// Sets the time the screen turns off when the device is not in use.
        /// </summary>
        /// <param name="timeEnum">Screen off timeout. Enumerations:
        /// * `Never`: never off
        /// * `THREE`: 3s (only supported by PICO G2 4K)
        /// * `TEN`: 10s (only supported by PICO G2 4K)
        /// * `THIRTY`: 30s
        /// * `SIXTY`: 60s
        /// * `THREE_HUNDRED`: 5 mins
        /// * `SIX_HUNDRED`: 10 mins
        /// </param>
        /// <param name="callback">Callback:
        /// * `0`: set
        /// * `1`: failed to set
        /// * `10`: the screen off timeout should not be longer than the system sleep timeout
        /// </param>
        public static void PropertySetScreenOffDelay(ScreenOffDelayTimeEnum timeEnum, Action<int> callback)
        {
            PXR_EnterprisePlugin.UPxr_PropertySetScreenOffDelay(timeEnum, callback);
        }

        /// <summary>
        /// Sets the time the system sleeps when the device is not in use.
        /// </summary>
        /// <param name="timeEnum">System sleep timeout. Enumerations:
        /// * `Never`: never sleep
        /// * `FIFTEEN`: 15s (only supported by PICO G2 4K)
        /// * `THIRTY`: 30s (only supported by PICO G2 4K)
        /// * `SIXTY`: 60s (only supported by PICO G2 4K)
        /// * `THREE_HUNDRED`: 5 mins
        /// * `SIX_HUNDRED`: 10 mins
        /// * `ONE_THOUSAND_AND_EIGHT_HUNDRED`: 30 mins
        /// </param>
        public static void PropertySetSleepDelay(SleepDelayTimeEnum timeEnum)
        {
            PXR_EnterprisePlugin.UPxr_PropertySetSleepDelay(timeEnum);
        }

        /// <summary>
        /// Switches specified system function on/off.
        /// </summary>
        /// <param name="systemFunction">Function name. Enumerations:
        /// * `SFS_USB`: USB debugging
        /// * `SFS_AUTOSLEEP`: auto sleep
        /// * `SFS_SCREENON_CHARGING`: screen-on charging
        /// * `SFS_OTG_CHARGING`: OTG charging (only supported by G2 and Neo2 devices)
        /// * `SFS_RETURN_MENU_IN_2DMODE`: display the Return icon on the 2D screen (only supported by G2 devices)
        /// * `SFS_COMBINATION_KEY`: combination key
        /// * `SFS_CALIBRATION_WITH_POWER_ON`: calibration with power on (only supported by G2 devices)
        /// * `SFS_SYSTEM_UPDATE`: system update
        /// * `SFS_CAST_SERVICE`: phone casting service (only supported by G2 and Neo3 Pro/Pro Eye devices)
        /// * `SFS_EYE_PROTECTION`: eye-protection mode
        /// * `SFS_SECURITY_ZONE_PERMANENTLY`: permanently disable the 6DoF play area (not supported by G2 devices)
        /// * `SFS_GLOBAL_CALIBRATION`: global calibration (only supported by G2 devices)
        /// * `SFS_Auto_Calibration`: auto calibration (only supported by G2 devices)
        /// * `SFS_USB_BOOT`: USB plug-in boot
        /// * `SFS_VOLUME_UI`: global volume UI (need to restart the device to make the setting take effect)
        /// * `SFS_CONTROLLER_UI`: global controller connected UI
        /// * `SFS_NAVGATION_SWITCH`: navigation bar
        /// * `SFS_SHORTCUT_SHOW_RECORD_UI`: screen recording button UI
        /// * `SFS_SHORTCUT_SHOW_FIT_UI`: PICO fit UI (only supported by Neo devices for Chinese Mainland)
        /// * `SFS_SHORTCUT_SHOW_CAST_UI`: screencast button UI
        /// * `SFS_SHORTCUT_SHOW_CAPTURE_UI`: screenshot button UI
        /// * `SFS_USB_FORCE_HOST`: set the Neo3 Pro/Pro Eye device as the host device (not supported by G2 and Neo2 devices)
        /// * `SFS_SET_DEFAULT_SAFETY_ZONE`: set a default play area for a Neo3 Pro/Pro Eye device (not supported by G2 devices)
        /// * `SFS_ALLOW_RESET_BOUNDARY`: allow to reset customized boundary. Neo3 Pro/Pro Eye devices can re-customize the boundary (not supported by G2 devices)
        /// * `SFS_BOUNDARY_CONFIRMATION_SCREEN`: whether to display the boundary confirmation screen (not supported by G2 devices)
        /// * `SFS_LONG_PRESS_HOME_TO_RECENTER`: long press the Home button to recenter (not supported by G2 devices)
        /// * `SFS_POWER_CTRL_WIFI_ENABLE`: Neo3 Pro/Pro Eye device stays connected to the network when the device sleeps/turns off (not supported by G2 and Neo2 devices)
        /// * `SFS_WIFI_DISABLE`: disable Wi-Fi (not supported by G2 and Neo2 devices)
        /// * `SFS_SIX_DOF_SWITCH`: 6DoF position tracking (not supported by G2 and Neo2 devices)
        /// * `SFS_INVERSE_DISPERSION`: anti-dispersion (need to restart the device to make the setting take effect; not supported by G2 and Neo2 devices)
        /// * `SFS_LOGCAT`: system log switch (/data/logs) (not supported by G2 and Neo2 devices)
        /// * `SFS_PSENSOR`: PSensor switch (need to restart the device to make the setting take effect; not supported by G2 and Neo2 devices)
        /// * `SFS_SYSTEM_UPDATE_OTA`: OTA upgrade (not supported by G2 and Neo2 devices)
        /// * `SFS_SYSTEM_UPDATE_APP`: app upgrade and update (not supported by G2 and Neo2 devices)
        /// * `SFS_SHORTCUT_SHOW_WLAN_UI`: quickly set whether to show the WLAN button (not supported by G2 and Neo2 devices)
        /// * `SFS_SHORTCUT_SHOW_BOUNDARY_UI`: quickly set whether to show the boundary button (not supported by G2 and Neo2 devices)
        /// * `SFS_SHORTCUT_SHOW_BLUETOOTH_UI`: quickly set whether to show the bluetooth button (not supported by G2 and Neo2 devices)
        /// * `SFS_SHORTCUT_SHOW_CLEAN_TASK_UI`: quickly set whether to show the one-click clear button (not supported by G2 and Neo2 devices)
        /// * `SFS_SHORTCUT_SHOW_IPD_ADJUSTMENT_UI`: quickly set whether to show the IPD adjustment button (not supported by G2 and Neo2 devices)
        /// * `SFS_SHORTCUT_SHOW_POWER_UI`: quickly set whether to show the power button (not supported by G2 and Neo2 devices)
        /// * `SFS_SHORTCUT_SHOW_EDIT_UI`: quickly set whether to show the edit button (not supported by G2 and Neo2 devices)
        /// * `SFS_BASIC_SETTING_APP_LIBRARY_UI`: the button for customizing the app library (not supported by G2 and Neo2 devices; only supported by system apps)
        /// * `SFS_BASIC_SETTING_SHORTCUT_UI`: the button for customizing quick settings (not supported by G2 and Neo2 devices; only supported by system apps)
        /// </param>
        /// <param name="switchEnum">Whether to switch the function on/off:
        /// * `S_ON`: switch on
        /// * `S_OFF`: switch off
        /// </param>
        public static void SwitchSystemFunction(SystemFunctionSwitchEnum systemFunction, SwitchEnum switchEnum)
        {
            PXR_EnterprisePlugin.UPxr_SwitchSystemFunction(systemFunction, switchEnum);
        }

        /// <summary>
        /// Sets the USB configuration mode.
        /// </summary>
        /// <param name="uSBConfigModeEnum">USB configuration mode. Enumerations:
        /// * `MTP`: MTP mode
        /// * `CHARGE`: charging mode
        /// </param>
        public static void SwitchSetUsbConfigurationOption(USBConfigModeEnum uSBConfigModeEnum)
        {
            PXR_EnterprisePlugin.UPxr_SwitchSetUsbConfigurationOption(uSBConfigModeEnum);
        }

        /// <summary>
        /// Sets the duration after which the controllers enter the pairing mode.
        /// @note Not supported by G2 4K devices.
        /// </summary>
        /// <param name="timeEnum">Duration enumerations:
        /// * `SIX`: 6 seconds
        /// * `FIFTEEN`: 15 seconds
        /// * `SIXTY`: 60 seconds
        /// * `ONE_HUNDRED_AND_TWENTY`: 120 seconds (2 minutes)
        /// * `SIX_HUNDRED`: 600 seconds (5 minutes)
        /// * `NEVER`: never enter the pairing mode
        /// </param>
        /// <param name="callback">Returns the result:
        /// * `0`: failure
        /// * `1`: success
        /// </param>
        public static void SetControllerPairTime(ControllerPairTimeEnum timeEnum, Action<int> callback)
        {
            PXR_EnterprisePlugin.UPxr_SetControllerPairTime(timeEnum, callback);
        }

        /// <summary>
        /// Gets the duration after which the controllers enter the pairing mode.
        /// @note Not supported by G2 4K devices.
        /// </summary>
        /// <param name="callback">Returns a duration enumeration from the following:
        /// * `SIX`: 6 seconds
        /// * `FIFTEEN`: 15 seconds
        /// * `SIXTY`: 60 seconds
        /// * `ONE_HUNDRED_AND_TWENTY`: 120 seconds (2 minutes)
        /// * `SIX_HUNDRED`: 600 seconds (5 minutes)
        /// * `NEVER`: never enter the pairing mode
        /// </param>
        public static void GetControllerPairTime(Action<int> callback)
        {
            PXR_EnterprisePlugin.UPxr_GetControllerPairTime(callback);
        }

        /// <summary>
        /// Turns the screen on.
        /// @note This is a protected API. You need to add `<meta-data android:name="pico_advance_interface" android:value="0"/>`
        /// to the app's AndroidManifest.xml file for calling this API, after which the app is unable to be published on the PICO Store.
        /// </summary>
        public static void ScreenOn()
        {
            PXR_EnterprisePlugin.UPxr_ScreenOn();
        }

        /// <summary>
        /// Turns the screen off.
        /// @note This is a protected API. You need to add `<meta-data android:name="pico_advance_interface" android:value="0"/>`
        /// to the app's AndroidManifest.xml file for calling this API, after which the app is unable to be published on the PICO Store.
        /// </summary>
        public static void ScreenOff()
        {
            PXR_EnterprisePlugin.UPxr_ScreenOff();
        }

        /// <summary>
        /// Acquires the wake lock.
        /// </summary>
        public static void AcquireWakeLock()
        {
            PXR_EnterprisePlugin.UPxr_AcquireWakeLock();
        }

        /// <summary>
        /// Releases the wake lock.
        /// </summary>
        public static void ReleaseWakeLock()
        {
            PXR_EnterprisePlugin.UPxr_ReleaseWakeLock();
        }

        /// <summary>
        /// Enables the Confirm button.
        /// </summary>
        public static void EnableEnterKey()
        {
            PXR_EnterprisePlugin.UPxr_EnableEnterKey();
        }

        /// <summary>
        /// Disables the Confirm button.
        /// </summary>
        public static void DisableEnterKey()
        {
            PXR_EnterprisePlugin.UPxr_DisableEnterKey();
        }

        /// <summary>
        /// Enables the Volume button.
        /// </summary>
        public static void EnableVolumeKey()
        {
            PXR_EnterprisePlugin.UPxr_EnableVolumeKey();
        }

        /// <summary>
        /// Disables the Volume button.
        /// </summary>
        public static void DisableVolumeKey()
        {
            PXR_EnterprisePlugin.UPxr_DisableVolumeKey();
        }

        /// <summary>
        /// Enables the Back button.
        /// </summary>
        public static void EnableBackKey()
        {
            PXR_EnterprisePlugin.UPxr_EnableBackKey();
        }

        /// <summary>
        /// Disables the Back button.
        /// </summary>
        public static void DisableBackKey()
        {
            PXR_EnterprisePlugin.UPxr_DisableBackKey();
        }

        /// <summary>
        /// Writes the configuration file to the /data/local/tmp/ path.
        /// </summary>
        /// <param name="path">The path to the configuration file, e.g., `/data/local/tmp/config.txt`.</param>
        /// <param name="content">The content of the configuration file.</param>
        /// <param name="callback">Whether the configuration file has been successfully written:
        /// * `true`: written
        /// * `false`: failed to be written
        /// </param>
        public static void WriteConfigFileToDataLocal(string path, string content, Action<bool> callback)
        {
            PXR_EnterprisePlugin.UPxr_WriteConfigFileToDataLocal(path, content, callback);
        }

        /// <summary>
        /// Resets all buttons to default configuration.
        /// </summary>
        /// <param name="callback">Whether all keys have been successfully reset to default configuration:
        /// * `true`: reset
        /// * `false`: failed to reset
        /// </param>
        public static void ResetAllKeyToDefault(Action<bool> callback)
        {
            PXR_EnterprisePlugin.UPxr_ResetAllKeyToDefault(callback);
        }

        /// <summary>
        /// Sets an app as the launcher app.
        /// </summary>
        /// <param name="switchEnum">Switch. Enumerations:
        /// * `S_ON`: set the app as the launcher app
        /// * `S_OFF`: cancel setting the app as the launcher app
        /// </param>
        /// <param name="packageName">The app package name.</param>
        public static void SetAPPAsHome(SwitchEnum switchEnum, string packageName)
        {
            PXR_EnterprisePlugin.UPxr_SetAPPAsHome(switchEnum, packageName);
        }

        /// <summary>
        /// Force quits app(s) by passing app PID or package name.
        /// @note This is a protected API. You need to add `<meta-data android:name="pico_advance_interface" android:value="0"/>`
        /// to the app's AndroidManifest.xml file for calling this API, after which the app is unable to be published on the PICO Store.
        /// </summary>
        /// <param name="pids">An array of app PID(s).</param>
        /// <param name="packageNames">An array of package name(s).</param>
        public static void KillAppsByPidOrPackageName(int[] pids, string[] packageNames)
        {
            PXR_EnterprisePlugin.UPxr_KillAppsByPidOrPackageName(pids, packageNames);
        }

        /// <summary>
        /// Force quits background app(s) expect those in the allowlist.
        /// @note This is a protected API. You need to add `<meta-data android:name="pico_advance_interface" android:value="0"/>`
        /// to the app's AndroidManifest.xml file for calling this API, after which the app is unable to be published on the PICO Store.
        /// </summary>
        /// <param name="packageNames">An array of package name(s) to be added to the allowlist. The corresponding app(s) in the allowlist will not be force quit.</param>
        public static void KillBackgroundAppsWithWhiteList(string[] packageNames)
        {
            PXR_EnterprisePlugin.UPxr_KillBackgroundAppsWithWhiteList(packageNames);
        }

        /// <summary>
        /// Freezes the screen to the front. The screen will turn around with the HMD.
        /// @note Only supported by G2 4K and Neo2 devices.
        /// </summary>
        /// <param name="freeze">Whether to freeze the screen:
        /// * `true`: freeze
        /// * `false`: stop freezing
        /// </param>
        public static void FreezeScreen(bool freeze)
        {
            PXR_EnterprisePlugin.UPxr_FreezeScreen(freeze);
        }

        /// <summary>
        /// Turns on the screencast function.
        /// </summary>
        public static void OpenMiracast()
        {
            PXR_EnterprisePlugin.UPxr_OpenMiracast();
        }

        /// <summary>
        /// Gets the status of the screencast function.
        /// </summary>
        /// <returns>The status of the screencast function:
        /// * `true`: on
        /// * `false`: off
        /// </returns>
        public static bool IsMiracastOn()
        {
            return PXR_EnterprisePlugin.UPxr_IsMiracastOn();
        }

        /// <summary>
        /// Turns off the screencast function.
        /// </summary>
        public static void CloseMiracast()
        {
            PXR_EnterprisePlugin.UPxr_CloseMiracast();
        }

        /// <summary>
        /// Starts looking for devices that can be used for screen casting.
        /// </summary>
        public static void StartScan()
        {
            PXR_EnterprisePlugin.UPxr_StartScan();
        }

        /// <summary>
        /// Stops looking for devices that can be used for screen casting.
        /// </summary>
        public static void StopScan()
        {
            PXR_EnterprisePlugin.UPxr_StopScan();
        }

        /// <summary>
        /// Casts the screen to the specified device.
        /// </summary>
        /// <param name="modelJson">A modelJson structure containing the following fields:
        /// * `deviceAddress`
        /// * `deviceName`
        /// * `isAvailable` (`true`-device available; `false`-device not available)
        /// </param>
        public static void ConnectWifiDisplay(string modelJson)
        {
            PXR_EnterprisePlugin.UPxr_ConnectWifiDisplay(modelJson);
        }

        /// <summary>
        /// Stops casting the screen to the current device.
        /// </summary>
        public static void DisConnectWifiDisplay()
        {
            PXR_EnterprisePlugin.UPxr_DisConnectWifiDisplay();
        }

        /// <summary>
        /// Forgets the device that have been connected for screencast.
        /// </summary>
        /// <param name="address">Device address.</param>
        public static void ForgetWifiDisplay(string address)
        {
            PXR_EnterprisePlugin.UPxr_ForgetWifiDisplay(address);
        }

        /// <summary>
        /// Renames the device connected for screencast. The name is only for local storage.
        /// </summary>
        /// <param name="address">The MAC address of the device.</param>
        /// <param name="newName">The new device name.</param>
        public static void RenameWifiDisplay(string address, string newName)
        {
            PXR_EnterprisePlugin.UPxr_RenameWifiDisplay(address, newName);
        }

        /// <summary>
        /// Sets the callback for the scanning result, which returns `List<PBS_WifiDisplayModel>` that contains the devices previously connected for screencast and the devices currently found for screencast.
        /// </summary>
        public static void SetWDModelsCallback()
        {
            PXR_EnterprisePlugin.UPxr_SetWDModelsCallback();
        }

        /// <summary>
        /// Sets the callback for the scanning result, which returns the JSON string that contains the devices previously connected for screencast and the devices currently found for screencast.
        /// </summary>
        public static void SetWDJsonCallback()
        {
            PXR_EnterprisePlugin.UPxr_SetWDJsonCallback();
        }

        /// <summary>
        /// Manually updates the list of devices for screencast.
        /// </summary>
        /// <param name="callback">The list of devices for screencast.</param>
        public static void UpdateWifiDisplays(Action<string> callback)
        {
            PXR_EnterprisePlugin.UPxr_UpdateWifiDisplays(callback);
        }

        /// <summary>
        /// Gets the information of the currently connected device.
        /// </summary>
        /// <returns>The information of the currently connected device.</returns>
        public static string GetConnectedWD()
        {
            return PXR_EnterprisePlugin.UPxr_GetConnectedWD();
        }

        /// <summary>
        /// Switches the large space scene on.
        /// @note Not supported by G2 4K devices.
        /// </summary>
        /// <param name="open">Whether to switch the large space scene on:
        /// * `true`: switch on
        /// * `false`: not to switch on
        /// </param>
        /// <param name="callback">Callback:
        /// * `true`: success
        /// * `false`: failure
        /// </param>
        public static void SwitchLargeSpaceScene(bool open, Action<bool> callback)
        {
            PXR_EnterprisePlugin.UPxr_SwitchLargeSpaceScene(open, callback);
        }

        /// <summary>
        /// Gets the status of the large space scene.
        /// @note Not supported by G2 4K devices.
        /// </summary>
        /// <param name="callback">Returns the status of large space:
        /// * `0`: switched off
        /// * `1`: switched on
        /// </param>
        public static void GetSwitchLargeSpaceStatus(Action<string> callback)
        {
            PXR_EnterprisePlugin.UPxr_GetSwitchLargeSpaceStatus(callback);
        }

        /// <summary>
        /// Saves the large space map.
        /// @note Not supported by G2 4K devices.
        /// </summary>
        /// <returns>Whether the large space map has been saved:
        /// * `true`: saved
        /// * `false`: failed to save
        /// </returns>
        public static bool SaveLargeSpaceMaps()
        {
            return PXR_EnterprisePlugin.UPxr_SaveLargeSpaceMaps();
        }

        /// <summary>
        /// Exports maps. The exported maps are stored in the /maps/export file.
        /// @note Not supported by G2 4K devices.
        /// </summary>
        /// <param name="callback">Returns the result:
        /// * `true`: exported
        /// * `false`: failed to export
        /// </param>
        public static void ExportMaps(Action<bool> callback)
        {
            PXR_EnterprisePlugin.UPxr_ExportMaps(callback);
        }

        /// <summary>
        /// Imports maps. Need to copy maps to the /maps folder.
        /// @note Not supported by G2 4K devices.
        /// </summary>
        /// <param name="callback">Returns the result:
        /// * `true`: imported
        /// * `false`: failed to import
        /// </param>
        public static void ImportMaps(Action<bool> callback)
        {
            PXR_EnterprisePlugin.UPxr_ImportMaps(callback);
        }

        /// <summary>
        /// Gets each CPU's utilization for the current device.
        /// </summary>
        /// <returns>An array of CPU utilization info.</returns>
        public static float[] GetCpuUsages()
        {
            return PXR_EnterprisePlugin.UPxr_GetCpuUsages();
        }

        /// <summary>
        /// Gets device temperature in Celsius.
        /// </summary>
        /// <param name="type">The requested type of device temperature:
        /// * `DEVICE_TEMPERATURE_CPU`: CPU temperature
        /// * `DEVICE_TEMPERATURE_GPU`: GPU temperature
        /// * `DEVICE_TEMPERATURE_BATTERY`: battery temperature
        /// * `DEVICE_TEMPERATURE_SKIN`: surface temperature
        /// </param>
        /// <param name="source">The requested source of device temperature:
        /// * `TEMPERATURE_CURRENT`: current temperature
        /// * `TEMPERATURE_THROTTLING`: temperature threshold for throttling
        /// * `TEMPERATURE_SHUTDOWN`: temperature threshold for shutdown
        /// * `TEMPERATURE_THROTTLING_BELOW_VR_MIN`: temperature threshold for throttling. If the actual temperature is higher than the threshold, the lowest clock frequency for VR mode will not be met
        /// </param>
        /// <returns>An array of requested float device temperatures in Celsius.</returns>
        public static float[] GetDeviceTemperatures(int type, int source)
        {
            return PXR_EnterprisePlugin.UPxr_GetDeviceTemperatures(type, source);
        }

        /// <summary>
        /// Captures the current screen.
        /// @note Not supported by G2 4K devices.
        /// </summary>
        public static void Capture()
        {
            PXR_EnterprisePlugin.UPxr_Capture();
        }

        /// <summary>
        /// Records the screen. Call this function again to stop recording.
        /// @note Not supported by G2 4K devices.
        /// </summary>
        public static void Record()
        {
            PXR_EnterprisePlugin.UPxr_Record();
        }

        /// <summary>
        /// Connects the device to a specified Wi-Fi.  
        /// </summary>
        /// <param name="ssid">Wi-Fi name.</param>
        /// <param name="pwd">Wi-Fi password.</param>
        /// <param name="ext">Reserved parameter, pass `0` by default.</param>
        /// <param name="callback">The callback for indicating whether the Wi-Fi connection is successful:
        /// * `0`: connected
        /// * `1`: password error
        /// * `2`: unknown error
        /// </param>
        public static void ControlSetAutoConnectWIFIWithErrorCodeCallback(String ssid, String pwd, int ext, Action<int> callback)
        {
            PXR_EnterprisePlugin.UPxr_ControlSetAutoConnectWIFIWithErrorCodeCallback(ssid, pwd, ext, callback);
        }

        /// <summary>
        /// Keeps an app active. In other words, improves the priority of an app, thereby making the system not to force quit the app.
        /// @note Not supported by G2 4K devices. For Neo3 Pro/Pro Eye devices, the device's system version should be 5.0.0 or later.
        /// </summary>
        /// <param name="appPackageName">App package name.</param>
        /// <param name="keepAlive">Whether to keep the app active (i.e., whether to enhance the priority of the app):
        /// * `true`: keep
        /// * `false`: not keep
        /// </param>
        /// <param name="ext">Reserved parameter, pass `0`.</param>
        public static void AppKeepAlive(String appPackageName, bool keepAlive, int ext)
        {
            PXR_EnterprisePlugin.UPxr_AppKeepAlive(appPackageName, keepAlive, ext);
        }

        /// <summary>
        /// Schedules auto startup for the device.
        /// @note Not supported by G2 4K devices.
        /// </summary>
        /// <param name="year">Year, for example, `2022`.</param>
        /// <param name="month">Month, for example, `2`.</param>
        /// <param name="day">Day, for example, `22`.</param>
        /// <param name="hour">Hour, for example, `22`.</param>
        /// <param name="minute">Minute, for example, `22`.</param>
        /// <param name="open">Whether to enable scheduled auto startup for the device:
        /// * `true`: enable
        /// * `false`: disable
        /// </param>
        public static void TimingStartup(int year, int month, int day, int hour, int minute, bool open)
        {
            PXR_EnterprisePlugin.UPxr_TimingStartup(year, month, day, hour, minute, open);
        }

        /// <summary>
        /// Schedules auto shutdown for the device.
        /// @note Not supported by G2 4K devices.
        /// </summary>
        /// <param name="year">Year, for example, `2022`.</param>
        /// <param name="month">Month, for example, `2`.</param>
        /// <param name="day">Day, for example, `22`.</param>
        /// <param name="hour">Hour, for example, `22`.</param>
        /// <param name="minute">Minute, for example, `22`.</param>
        /// <param name="open">Whether to enable scheduled auto shutdown for the device:
        /// * `true`: enable
        /// * `false`: disable
        /// </param>
        public static void TimingShutdown(int year, int month, int day, int hour, int minute, bool open)
        {
            PXR_EnterprisePlugin.UPxr_TimingShutdown(year, month, day, hour, minute, open);
        }

        /// <summary>
        /// Displays a specified settings screen.
        /// @note Not supported by G2 4K devices.
        /// </summary>
        /// <param name="settingsEnum">The enumerations of settings screen:
        /// * `START_VR_SETTINGS_ITEM_WIFI`: the Wi-Fi settings screen;
        /// * `START_VR_SETTINGS_ITEM_BLUETOOTH`: the bluetooth settings screen;
        /// * `START_VR_SETTINGS_ITEM_CONTROLLER`: the controller settings screen;
        /// * `START_VR_SETTINGS_ITEM_LAB`: the lab settings screen;
        /// * `START_VR_SETTINGS_ITEM_BRIGHTNESS`: the brightness settings screen;
        /// * `START_VR_SETTINGS_ITEM_GENERAL)`: the general settings screen;
        /// * `START_VR_SETTINGS_ITEM_NOTIFICATION`: the notification settings screen.
        /// </param>
        /// <param name="hideOtherItem">Whether to display the selected settings screen:
        /// * `true`: display
        /// * `false`: hide
        /// </param>
        /// <param name="ext">Reserved parameter, pass `0`.</param>
        public static void StartVrSettingsItem(StartVRSettingsEnum settingsEnum, bool hideOtherItem, int ext)
        {
            PXR_EnterprisePlugin.UPxr_StartVrSettingsItem(settingsEnum, hideOtherItem, ext);
        }

        /// <summary>
        /// Changes the Volume button's function to that of the Home and Enter button's, or restores the volume adjustment function to the Volume button.
        /// @note Only supported by PICO 4 Enterprise.
        /// </summary>
        /// <param name="switchEnum">Whether to change the Volume button's function:
        /// * `S_ON`: change
        /// * `S_OFF`: do not change
        /// </param>
        /// <param name="ext">Reserved parameter, pass `0`.</param>
        public static void SwitchVolumeToHomeAndEnter(SwitchEnum switchEnum, int ext)
        {
            PXR_EnterprisePlugin.UPxr_SwitchVolumeToHomeAndEnter(switchEnum, ext);
        }

        /// <summary>
        /// Gets whether the Volume button's function has been changed to that of the Home and Enter button's.
        /// @note Only supported by PICO 4 Enterprise.
        /// </summary>
        /// <returns>
        /// * `S_ON`: changed
        /// * `S_OFF`: not changed
        /// </returns>
        public static SwitchEnum IsVolumeChangeToHomeAndEnter()
        {
            return PXR_EnterprisePlugin.UPxr_IsVolumeChangeToHomeAndEnter();
        }

        /// <summary>
        /// Upgrades the OTA.
        /// @note Not supported by G2 4K devices.
        /// </summary>
        /// <param name="otaPackagePath">The location of the OTA package.</param>
        /// <returns>
        /// * `0`: success
        /// * `1`: failure
        /// * `21`: OTA package version too low
        /// </returns>
        public static int InstallOTAPackage(String otaPackagePath)
        {
            return PXR_EnterprisePlugin.UPxr_InstallOTAPackage(otaPackagePath);
        }

        /// <summary>
        /// Gets the configuration of the Wi-Fi network that the device automatically connects to.
        /// </summary>
        /// <returns>The SSID and password of the Wi-Fi network.</returns>
        public static string GetAutoConnectWiFiConfig()
        {
            return PXR_EnterprisePlugin.UPxr_GetAutoConnectWiFiConfig();
        }

        /// <summary>
        /// Gets the scheduled auto startup settings for the device.
        /// @note Not supported by G2 4K devices.
        /// </summary>
        /// <returns>
        /// * `open`: the status of scheduled auto startup:
        ///   * `true`: enabled
        ///   * `false`: disabled
        /// * `time`: the time when the device auto starts up, for example, `1658980380000`. Returned when `open` is `true`.
        /// </returns>
        public static string GetTimingStartupStatus()
        {
            return PXR_EnterprisePlugin.UPxr_GetTimingStartupStatus();
        }

        /// <summary>
        /// Gets the scheduled auto shutdown settings for the device.
        /// @note Not supported by G2 4K devices.
        /// </summary>
        /// <returns>
        /// * `open`: the status of scheduled auto shutdown:
        ///   * `true`: enabled
        ///   * `false`: disabled
        /// * `time`: the time when the device auto shuts down, for example, `1658980380000`. Returned when `open` is `true`.
        /// </returns>
        public static string GetTimingShutdownStatus()
        {
            return PXR_EnterprisePlugin.UPxr_GetTimingShutdownStatus();
        }

        /// <summary>
        /// Gets the status of a specified controller button.
        /// @note Not supported by G2 4K devices.
        /// </summary>
        /// <param name="pxrControllerKey">The enumerations of controller button:
        /// * `CONTROLLER_KEY_JOYSTICK` 
        /// * `CONTROLLER_KEY_MENU`
        /// * `CONTROLLER_KEY_TRIGGER`
        /// * `CONTROLLER_KEY_RIGHT_A`
        /// * `CONTROLLER_KEY_RIGHT_B`
        /// * `CONTROLLER_KEY_LEFT_X`
        /// * `CONTROLLER_KEY_LEFT_Y`
        /// * `CONTROLLER_KEY_LEFT_GRIP`
        /// * `CONTROLLER_KEY_RIGHT_GRIP`
        /// </param>
        /// <returns>The button's status:
        /// * `0`: disabled
        /// * `1`: enabled
        /// </returns>
        public static int GetControllerKeyState(ControllerKeyEnum pxrControllerKey)
        {
            return PXR_EnterprisePlugin.UPxr_GetControllerKeyState(pxrControllerKey);
        }

        /// <summary>
        /// Enables or disables a specified controller button.
        /// @note Not supported by G2 4K devices.
        /// </summary>
        /// <param name="pxrControllerKey">The enumerations of controller button:
        /// * `CONTROLLER_KEY_JOYSTICK` 
        /// * `CONTROLLER_KEY_MENU`
        /// * `CONTROLLER_KEY_TRIGGER`
        /// * `CONTROLLER_KEY_RIGHT_A`
        /// * `CONTROLLER_KEY_RIGHT_B`
        /// * `CONTROLLER_KEY_LEFT_X`
        /// * `CONTROLLER_KEY_LEFT_Y`
        /// * `CONTROLLER_KEY_LEFT_GRIP`
        /// * `CONTROLLER_KEY_RIGHT_GRIP`
        /// </param>
        /// <param name="status">Whether to enable or disable the button:
        /// * `S_ON`: enable
        /// * `S_OFF`: disable
        /// </param>
        /// <returns>
        /// `0` indicates success, other values indicate failure.
        /// </returns>
        public static int SetControllerKeyState(ControllerKeyEnum pxrControllerKey, SwitchEnum status)
        {
            return PXR_EnterprisePlugin.UPxr_SetControllerKeyState(pxrControllerKey, status);
        }

        /// <summary>
        /// Gets the status of the switch which is for powering off the USB cable when the device is shut down.
        /// </summary>
        /// <returns>The switch's status:
        /// * `S_ON`: on
        /// * `S_OFF`: off
        /// </returns>
        public static SwitchEnum GetPowerOffWithUSBCable()
        {
            return PXR_EnterprisePlugin.UPxr_ControlGetPowerOffWithUSBCable();
        }

        /// <summary>
        /// Gets the screen timeout setting for the device.
        /// </summary>
        /// <returns>`PBS_ScreenOffDelayTimeEnum`: the enumerations of screen timeout. </returns>
        public static ScreenOffDelayTimeEnum GetScreenOffDelay()
        {
            return PXR_EnterprisePlugin.UPxr_PropertyGetScreenOffDelay();
        }

        /// <summary>
        /// Gets the sleep timeout settings for the device.
        /// </summary>
        /// <returns>`PBS_SleepDelayTimeEnum`: the enumeration of sleep timeout.</returns>
        public static SleepDelayTimeEnum GetSleepDelay()
        {
            return PXR_EnterprisePlugin.UPxr_PropertyGetSleepDelay();
        }

        /// <summary>
        /// Gets the current settings for the Power button.
        /// </summary>
        /// <returns>
        /// * `null`: not set
        /// * `singleTap`: whether a single-tap event has been set
        /// * `longTap`: whether a long-press event has been set
        /// * `longPressTime`: the time after which the long-press event takes place. Returned when `longTap` is `true`.
        /// </returns>
        public static string GetPowerKeyStatus()
        {
            return PXR_EnterprisePlugin.UPxr_PropertyGetPowerKeyStatus();
        }

        /// <summary>
        /// Get the Enter button's status.
        /// </summary>
        /// <returns>
        /// * `0`: disabled
        /// * `1`: enabled
        /// </returns>
        public static int GetEnterKeyStatus()
        {
            return PXR_EnterprisePlugin.UPxr_GetEnterKeyStatus();
        }

        /// <summary>
        /// Get the Volume button's status.
        /// </summary>
        /// <returns>
        /// * `0`: disabled
        /// * `1`: enabled
        /// </returns>
        public static int GetVolumeKeyStatus()
        {
            return PXR_EnterprisePlugin.UPxr_GetVolumeKeyStatus();
        }

        /// <summary>
        /// Get the Back button's status.
        /// </summary>
        /// <returns>
        /// * `0`: disabled
        /// * `1`: enabled
        /// </returns>
        public static int GetBackKeyStatus()
        {
            return PXR_EnterprisePlugin.UPxr_GetBackKeyStatus();
        }

        /// <summary>
        /// Gets the event settings for the Home button.
        /// </summary>
        /// <param name="homeEvent">The enumerations of event type:
        /// * `SINGLE_CLICK`: single-click event
        /// * `DOUBLE_CLICK`: double-click event
        /// * `LONG_PRESS`: long-press event
        /// </param>
        /// <returns>
        /// * For `SINGLE_CLICK` and `DOUBLE_CLICK`, the event(s) you set will be returned.
        /// * For `LONG_PRESS`, the time and event you set will be returned. If you have not set a time for a long-press event, time will be `null`.
        /// 
        /// * If you have not set any event for the event type you pass in the request, the response will return `null`.
        /// * For event enumerations, see `PropertySetHomeKey` or `PropertySetHomeKeyAll`.
        /// </returns>
        public static string GetHomKeyStatus(HomeEventEnum homeEvent)
        {
            return PXR_EnterprisePlugin.UPxr_PropertyGetHomKeyStatus(homeEvent);
        }

        /// <summary>
        /// Gets the status of a specified system function switch.
        /// </summary>
        /// <param name="systemFunction">The enumerations of system function switch:
        /// * `SFS_USB`: USB debugging
        /// * `SFS_AUTOSLEEP`: auto sleep
        /// * `SFS_SCREENON_CHARGING`: screen-on charging
        /// * `SFS_OTG_CHARGING`: OTG charging (only supported by G2 and Neo2 devices)
        /// * `SFS_RETURN_MENU_IN_2DMODE`: display the Return icon on the 2D screen (only supported by G2 devices)
        /// * `SFS_COMBINATION_KEY`: combination key
        /// * `SFS_CALIBRATION_WITH_POWER_ON`: calibration with power on (only supported by G2 devices)
        /// * `SFS_SYSTEM_UPDATE`: system update
        /// * `SFS_CAST_SERVICE`: phone casting service (only supported by G2 and Neo3 Pro/Pro Eye devices)
        /// * `SFS_EYE_PROTECTION`: eye-protection mode
        /// * `SFS_SECURITY_ZONE_PERMANENTLY`: permanently disable the 6DoF play area (not supported by G2 devices)
        /// * `SFS_GLOBAL_CALIBRATION`: global calibration (only supported by G2 devices)
        /// * `SFS_Auto_Calibration`: auto calibration (only supported by G2 devices)
        /// * `SFS_USB_BOOT`: USB plug-in boot
        /// * `SFS_VOLUME_UI`: global volume UI (need to restart the device to make the setting take effect)
        /// * `SFS_CONTROLLER_UI`: global controller connected UI
        /// * `SFS_NAVGATION_SWITCH`: navigation bar
        /// * `SFS_SHORTCUT_SHOW_RECORD_UI`: screen recording button UI
        /// * `SFS_SHORTCUT_SHOW_FIT_UI`: PICO fit UI (only supported by Neo devices for Chinese Mainland)
        /// * `SFS_SHORTCUT_SHOW_CAST_UI`: screencast button UI
        /// * `SFS_SHORTCUT_SHOW_CAPTURE_UI`: screenshot button UI
        /// * `SFS_USB_FORCE_HOST`: set the Neo3 Pro/Pro Eye device as the host device (not supported by G2 and Neo2 devices)
        /// * `SFS_SET_DEFAULT_SAFETY_ZONE`: set a default play area for a Neo3 Pro/Pro Eye device (not supported by G2 devices)
        /// * `SFS_ALLOW_RESET_BOUNDARY`: allow to reset customized boundary. Neo3 Pro/Pro Eye devices supports recustomizing the boundary (not supported by G2 devices)
        /// * `SFS_BOUNDARY_CONFIRMATION_SCREEN`: whether to display the boundary confirmation screen (not supported by G2 devices)
        /// * `SFS_LONG_PRESS_HOME_TO_RECENTER`: long press the Home button to recenter (not supported by G2 devices)
        /// * `SFS_POWER_CTRL_WIFI_ENABLE`: Neo3 Pro/Pro Eye device stays connected to the network when the device sleeps/turns off (not supported by G2 and Neo2 devices)
        /// * `SFS_WIFI_DISABLE`: disable Wi-Fi (not supported by G2 and Neo2 devices)
        /// * `SFS_SIX_DOF_SWITCH`: 6DoF position tracking (not supported by G2 and Neo2 devices)
        /// * `SFS_INVERSE_DISPERSION`: anti-dispersion (need to restart the device to make the setting take effect; not supported by G2 and Neo2 devices)
        /// * `SFS_LOGCAT`: system log switch (/data/logs) (not supported by G2 and Neo2 devices)
        /// * `SFS_PSENSOR`: PSensor switch (need to restart the device to make the setting take effect; not supported by G2 and Neo2 devices)
        /// * `SFS_SYSTEM_UPDATE_OTA`: OTA upgrade (not supported by G2 and Neo2 devices)
        /// * `SFS_SYSTEM_UPDATE_APP`: app upgrade and update (not supported by G2 and Neo2 devices)
        /// * `SFS_SHORTCUT_SHOW_WLAN_UI`: quickly set whether to show the WLAN button (not supported by G2 and Neo2 devices)
        /// * `SFS_SHORTCUT_SHOW_BOUNDARY_UI`: quickly set whether to show the boundary button (not supported by G2 and Neo2 devices)
        /// * `SFS_SHORTCUT_SHOW_BLUETOOTH_UI`: quickly set whether to show the bluetooth button (not supported by G2 and Neo2 devices)
        /// * `SFS_SHORTCUT_SHOW_CLEAN_TASK_UI`: quickly set whether to show the one-click clear button (not supported by G2 and Neo2 devices)
        /// * `SFS_SHORTCUT_SHOW_IPD_ADJUSTMENT_UI`: quickly set whether to show the IPD adjustment button (not supported by G2 and Neo2 devices)
        /// * `SFS_SHORTCUT_SHOW_POWER_UI`: quickly set whether to show the power button (not supported by G2 and Neo2 devices)
        /// * `SFS_SHORTCUT_SHOW_EDIT_UI`: quickly set whether to show the edit button (not supported by G2 and Neo2 devices)
        /// * `SFS_BASIC_SETTING_APP_LIBRARY_UI`: the button for customizing the app library (not supported by G2 and Neo2 devices; only supported by system apps)
        /// * `SFS_BASIC_SETTING_SHORTCUT_UI`: the button for customizing quick settings (not supported by G2 and Neo2 devices; only supported by system apps)
        /// </param>
        /// <param name="callback">The callback that returns the switch's status:
        /// * `0`: off
        /// * `1`: on
        /// * `2`: not supported by device
        /// For `SFS_SYSTEM_UPDATE`, the returns are as follows:
        /// * `0`: off
        /// * `1`: OTA upgrade on
        /// * `2`: app upgrade on
        /// * `3`: OTA and app upgrade on
        /// </param>
        public static void GetSwitchSystemFunctionStatus(SystemFunctionSwitchEnum systemFunction, Action<int> callback)
        {
            PXR_EnterprisePlugin.UPxr_GetSwitchSystemFunctionStatus(systemFunction, callback);
        }

        /// <summary>
        /// Gets the configured USB mode.
        /// </summary>
        /// <returns>
        /// * `MTP`: MTP mode
        /// * `CHARGE`: charging mode
        /// </returns>
        public static string GetUsbConfigurationOption()
        {
            return PXR_EnterprisePlugin.UPxr_SwitchGetUsbConfigurationOption();
        }

        /// <summary>
        /// Gets the current launcher.
        /// </summary>
        /// <returns>The package name or class name of the launcher.</returns>
        public static string GetCurrentLauncher()
        {
            return PXR_EnterprisePlugin.UPxr_GetCurrentLauncher();
        }

        /// <summary>
        /// Initializes the screencast service.
        /// @note Not supported by G2 4K devices. For Neo3 Pro/Pro Eye devices, the device's system version should be 5.0.0 or later.
        /// </summary>
        /// <param name="callback">The callback:
        /// * `0`: disconnect
        /// * `1`: connect
        /// * `2`: no microphone permission
        /// </param>
        /// <returns>
        /// * `0`: failure
        /// * `1`: success
        /// Returns `0` when there is no microphone permission.
        /// </returns>
        public static int PICOCastInit(Action<int> callback)
        {
            return PXR_EnterprisePlugin.UPxr_PICOCastInit(callback);
        }

        /// <summary>
        /// Sets whether to show the screencast authorization window.
        /// @note Not supported by G2 4K devices. For Neo3 Pro/Pro Eye devices, the device's system version should be 5.0.0 or later.
        /// </summary>
        /// <param name="authZ">
        /// * `0`: ask every time (default)
        /// * `1`: always allow
        /// * `2`: not accepted
        /// </param>
        /// <returns>
        /// * `0`: failure
        /// * `1`: success
        /// </returns>
        public static int PICOCastSetShowAuthorization(int authZ)
        {
            return PXR_EnterprisePlugin.UPxr_PICOCastSetShowAuthorization(authZ);
        }

        /// <summary>
        /// Gets the setting of whether to show the screencast authorization window.
        /// @note Not supported by G2 4K devices. For Neo3 Pro/Pro Eye devices, the device's system version should be 5.0.0 or later.
        /// </summary>
        /// <returns>
        /// * `0`: ask every time (default)
        /// * `1`: always allow
        /// * `2`: not accepted
        /// </returns>
        public static int PICOCastGetShowAuthorization()
        {
            return PXR_EnterprisePlugin.UPxr_PICOCastGetShowAuthorization();
        }

        /// <summary>
        /// Gets the URL for screencast.
        /// @note Not supported by G2 4K devices. For Neo3 Pro/Pro Eye devices, the device's system version should be 5.0.0 or later.
        /// </summary>
        /// <param name="urlType">The enumerations of URL type:
        /// * `NormalURL`: Normal URL. The screencast authorization window will show if it is not set.
        /// * `NoConfirmURL`: Non-confirm URL. The screencast authorization window will not show in the browser. Screencast will start once you enter the URL.
        /// * `RtmpURL`: Returns the RTMP live streaming URL. The screencast authorization window will not appear on the VR headset's screen.
        /// </param>
        /// <returns>The URL for screencast.</returns>
        public static string PICOCastGetUrl(PICOCastUrlTypeEnum urlType)
        {
            return PXR_EnterprisePlugin.UPxr_PICOCastGetUrl(urlType);
        }

        /// <summary>
        ///  Stops screencast.
        /// @note Not supported by G2 4K devices. For Neo3 Pro/Pro Eye devices, the device's system version should be 5.0.0 or later.
        /// </summary>
        /// <returns>
        /// * `0`: failure
        /// * `1`: success
        /// </returns>
        public static int PICOCastStopCast()
        {
            return PXR_EnterprisePlugin.UPxr_PICOCastStopCast();
        }

        /// <summary>
        /// sets screencast options.
        /// @note Not supported by G2 4K devices. For Neo3 Pro/Pro Eye devices, the device's system version should be 5.0.0 or later.
        /// </summary>
        /// <param name="castOptionOrStatus">The enumerations of the property to set:
        /// * `OPTION_RESOLUTION_LEVEL`: resolution level
        /// * `OPTION_BITRATE_LEVEL`: bitrate level
        /// * `OPTION_AUDIO_ENABLE`: whether to enable the audio
        /// </param>
        /// <param name="castOptionValue">The values that can be set for each property:
        /// * For `OPTION_RESOLUTION_LEVEL`:
        ///   * `OPTION_VALUE_RESOLUTION_HIGH`
        ///   * `OPTION_VALUE_RESOLUTION_MIDDLE`
        ///   * `OPTION_VALUE_RESOLUTION_AUTO`
        ///   * `OPTION_VALUE_RESOLUTION_HIGH_2K`
        ///   * `OPTION_VALUE_RESOLUTION_HIGH_4K`
        /// * For `OPTION_BITRATE_LEVEL`:
        ///   * `OPTION_VALUE_BITRATE_HIGH`
        ///   * `OPTION_VALUE_BITRATE_MIDDLE`
        ///   * `OPTION_VALUE_BITRATE_LOW`
        /// * For `OPTION_AUDIO_ENABLE`:
        ///   * `OPTION_VALUE_AUDIO_ON`
        ///   * `OPTION_VALUE_AUDIO_OFF`
        /// </param>
        /// <returns>
        /// * `0`: failure
        /// * `1`: success
        /// </returns>
        public static int PICOCastSetOption(PICOCastOptionOrStatusEnum castOptionOrStatus, PICOCastOptionValueEnum castOptionValue)
        {
            return PXR_EnterprisePlugin.UPxr_PICOCastSetOption(castOptionOrStatus, castOptionValue);
        }

        /// <summary>
        /// Gets the screencast settings for the current device.
        /// @note Not supported by G2 4K devices. For Neo3 Pro/Pro Eye devices, the device's system version should be 5.0.0 or later.
        /// </summary>
        /// <param name="castOptionOrStatus">The enumerations of the screencast property to get setting for:
        /// * `OPTION_RESOLUTION_LEVEL`: resolution level
        /// * `OPTION_BITRATE_LEVEL`: bitrate level
        /// * `OPTION_AUDIO_ENABLE`: whether the audio is enabled
        /// * `PICOCAST_STATUS`: returns the current screencast status
        /// </param>
        /// <returns>The setting of the selected property:
        /// * For `OPTION_RESOLUTION_LEVEL`:
        ///   * `OPTION_VALUE_RESOLUTION_HIGH`
        ///   * `OPTION_VALUE_RESOLUTION_MIDDLE`
        ///   * `OPTION_VALUE_RESOLUTION_AUTO`
        ///   * `OPTION_VALUE_RESOLUTION_HIGH_2K`
        ///   * `OPTION_VALUE_RESOLUTION_HIGH_4K`
        /// * For `OPTION_BITRATE_LEVEL`:
        ///   * `OPTION_VALUE_BITRATE_HIGH`
        ///   * `OPTION_VALUE_BITRATE_MIDDLE`
        ///   * `OPTION_VALUE_BITRATE_LOW`
        /// * For `OPTION_AUDIO_ENABLE`:
        ///   * `OPTION_VALUE_AUDIO_ON`
        ///   * `OPTION_VALUE_AUDIO_OFF`
        /// * `PICOCAST_STATUS` :
        ///   * `STATUS_VALUE_STATE_STARTED`
        ///   * `STATUS_VALUE_STATE_STOPPED`
        ///   * `STATUS_VALUE_ERROR`
        /// </returns>
        public static PICOCastOptionValueEnum PICOCastGetOptionOrStatus(PICOCastOptionOrStatusEnum castOptionOrStatus)
        {
            return PXR_EnterprisePlugin.UPxr_PICOCastGetOptionOrStatus(castOptionOrStatus);
        }

        /// <summary>Sets the system language for the device. 
        /// For a language that is spoken in different countries/regions, the system language is then co-set by the language code and the device's country/region code. 
        /// For example, if the language code is set to `en` and the device's country/region code is `US`, the system language will be set to English (United States).</summary>
        /// @note Not supported by G2 4K devices.
        ///
        /// <param name="language">Supported language codes:
        /// * `cs`: Čeština (Czech)
        /// * `da`: Dansk (Danish)
        /// * `de`: Deutsch (German)
        /// * `el`: Ελληνικά (Greek)
        /// * `en`: English (United States) / English (United Kingdom)
        /// * `es`: Español (España) / Español (Estados Unidos)
        /// * `fi`: Suomi (Finnish)
        /// * `fr`: Français (French)
        /// * `it`: Italiano (Italian)
        /// * `ja`: 日本語 (Japanese)
        /// * `ko`: 한국어 (Korean)
        /// * `ms`: Melayu (Malay)
        /// * `nb`: Norsk bokmål (Norwegian)
        /// * `nl`: Nederlands (Dutch)
        /// * `pl`: Polski (Polish)
        /// * `pt`: Português (Portuguese(Brasil)) / Português (Portuguese(Portugal))
        /// * `ro`: Română (Romanian)
        /// * `ru`: Русский (Russian)
        /// * `sv`: Svenska (Swedish)
        /// * `th`: ไทย (Thai)
        /// * `tr`: Türkçe (Turkish)
        /// * `zh`: 中文 (简体) (Chinese (Simplified)) / 中文 (中国香港) (Chinese (Hong Kong SAR of China)) 中文 (繁體) / (Chinese (Traditional)).
        /// For devices in Mainland China / Taiwan, China / Hong Kong SAR of China / Macao SAR of China, the country/region code has been defined in factory settings.
        /// </param>
        /// <returns>
        /// * `0`: success
        /// * `1`: failure
        /// * `22`: invalid language
        /// </returns>
        public static int SetSystemLanguage(String language)
        {
            return PXR_EnterprisePlugin.UPxr_SetSystemLanguage(language);
        }

        /// <summary>Gets the device's system language.</summary>
        /// @note Not supported by G2 4K devices.
        ///
        /// <returns>The system language set for the device. For details, refer to the 
        /// parameter description for `SetSystemLanguage`.</returns>
        public static String GetSystemLanguage()
        {
            return PXR_EnterprisePlugin.UPxr_GetSystemLanguage();
        }

        /// <summary>Sets a default Wi-Fi network for the device. Once set, the device will automatically connect to the Wi-Fi network if accessible.</summary>
        /// @note Not supported by G2 4K devices.
        /// 
        /// <param name="ssid">The SSID (name) of the Wi-Fi network.</param>
        /// <param name="pwd">The password of the Wi-Fi network.</param>
        /// <returns>
        /// * `0`: success
        /// * `1`: failure
        /// </returns>
        public static int ConfigWifi(String ssid, String pwd)
        {
            return PXR_EnterprisePlugin.UPxr_ConfigWifi(ssid, pwd);
        }

        /// <summary>Gets the device's default Wi-Fi network.</summary>
        /// @note Not supported by G2 4K devices.
        /// 
        /// <returns>The SSID (name) of the Wi-Fi network.</returns>
        public static String[] GetConfiguredWifi()
        {
            return PXR_EnterprisePlugin.UPxr_GetConfiguredWifi();
        }

        /// <summary>Sets a country/region for the device.</summary>
        /// @note Not supported by G2 4K devices. For Neo3 Pro/Pro Eye devices, the device's system version should be 5.0.0 or later. This API works only before initialization is completed.
        /// 
        /// <param name="countryCode">The country/region code co-determines the device's system language with the language code you set via `SetSystemLanguage`.
        /// Below are supported country/region codes:
        /// * `AD`: Andorra
        /// * `AT`: Austria
        /// * `AU`: Australia
        /// * `BE`: Belgium
        /// * `BG`: Bulgaria
        /// * `CA`: Canada
        /// * `CH`: Switzerland
        /// * `CZ`: Czech Republic
        /// * `DE`: Germany
        /// * `DK`: Denmark
        /// * `EE`: Estonia
        /// * `ES`: Spain
        /// * `FI`: Finland
        /// * `FR`: France
        /// * `GB`: the Great Britain
        /// * `GR`: Greece
        /// * `HR`: Croatia
        /// * `HU`: Hungary
        /// * `IE`: Ireland
        /// * `IL`: Israel
        /// * `IS`: Iceland
        /// * `IT`: Italy
        /// * `JP`: Japan
        /// * `KR`: Korea
        /// * `LI`: Liechtenstein
        /// * `LT`: Lithuania
        /// * `LU`: Luxembourg
        /// * `LV`: Latvia
        /// * `MC`: Monaco
        /// * `MT`: Malta
        /// * `MY`: Malaysia
        /// * `NL`: Netherlands
        /// * `NO`: Norway
        /// * `NZ`: New Zealand
        /// * `PL`: Poland
        /// * `PT`: Portugal
        /// * `RO`: Romania
        /// * `SE`: Sweden
        /// * `SG`: Singapore
        /// * `SI`: Slovenia
        /// * `SK`: Slovakia
        /// * `SM`: San Marino
        /// * `TR`: Turkey
        /// * `US`: the United States
        /// * `VA`: Vatican
        /// </param>
        /// <param name="callback">Set the callback to get the result:
        /// * `0`: success
        /// * `1`: failure
        /// </param>
        public static int SetSystemCountryCode(String countryCode, Action<int> callback)
        {
            return PXR_EnterprisePlugin.UPxr_SetSystemCountryCode(countryCode, callback);
        }

        /// <summary>Gets the device's country/region code.</summary>
        /// @note Not supported by G2 4K devices. For Neo3 Pro/Pro Eye devices, the device's system version should be 5.0.0 or later. This API works only before initialization is completed.
        ///
        /// <returns>A string value that indicates the device's current country/region code. 
        /// For supported country/region codes, see the parameter description in `SetSystemCountryCode`.</returns>
        public static string GetSystemCountryCode()
        {
            return PXR_EnterprisePlugin.UPxr_GetSystemCountryCode();
        }

        /// <summary>Sets the page to skip in initialization settings.</summary>
        /// @note Not supported by G2 4K devices. For Neo3 Pro/Pro Eye devices, the device's system version should be 5.0.0 or later. This API works only before initialization is completed.
        ///
        /// <param name="flag">Set the flag.
        /// The first 6 bits are valid, the 7th to 32rd bits are reserved. For each bit, `0` indicates showing and `1` indicates hiding.
        /// * `Constants#INIT_SETTING_HANDLE_CONNECTION_TEACHING`: the controller connection tutorial page
        /// * `Constants#INIT_SETTING_TRIGGER_KEY_TEACHING`: the Trigger button tutorial page
        /// * `Constants#INIT_SETTING_SELECT_LANGUAGE`: the language selection page
        /// * `Constants#INIT_SETTING_SELECT_COUNTRY`: the country/region selection page. Only available for devices in non-Mainland China countries/regions.
        /// * `Constants#INIT_SETTING_WIFI_SETTING`: the Wi-Fi settings page
        /// * `Constants#INIT_SETTING_QUICK_SETTING`: the quick settings page
        /// </param>
        /// Below is an example implementation:
        /// ```csharp
        /// int flag = Constants.INIT_SETTING_HANDLE_CONNECTION_TEACHING | Constants.INIT_SETTING_TRIGGER_KEY_TEACHING;
        /// int result = serviceBinder.pbsSetSkipInitSettingPage(flag,0);
        /// ```
        /// <returns>
        /// * `0`: success
        /// * `1`: failure
        /// </returns>
        public static int SetSkipInitSettingPage(int flag)
        {
            return PXR_EnterprisePlugin.UPxr_SetSkipInitSettingPage(flag);
        }

        /// <summary>Gets the page to skip in initialization settings.</summary>
        /// @note Not supported by G2 4K devices. For Neo3 Pro/Pro Eye devices, the device's system version should be 5.0.0 or later. This API works only before initialization is completed.
        ///
        /// <returns>Returns the flag set in `SetSkipInitSettingPage`.</returns>
        public static int GetSkipInitSettingPage()
        {
            return PXR_EnterprisePlugin.UPxr_GetSkipInitSettingPage();
        }

        /// <summary>Gets whether the initialization settings have been complete.</summary>
        /// @note Not supported by G2 4K devices. For Neo3 Pro/Pro Eye devices, the device's system version should be 5.0.0 or later. This API works only before initialization is completed.
        ///
        /// <returns> 
        /// * `0`: not complete
        /// * `1`: complete
        /// </returns>
        public static int IsInitSettingComplete()
        {
            return PXR_EnterprisePlugin.UPxr_IsInitSettingComplete();
        }

        /// <summary>Starts an activity in another app.</summary>
        /// @note Not supported by G2 4K devices.
        /// <param name="packageName">(Optional) The app's package name.</param>
        /// <param name="className">(Optional) The app's class name.</param>
        /// <param name="action">(Optional) The action to be performed.</param>
        /// <param name="extra">The basic types of standard fields that can be used as extra data. See [here](https://developer.android.com/reference/android/content/Intent#standard-extra-data) for details.</param> 
        /// <param name="categories">Standard categories that can be used to further clarify an Intent. Add a new category to the intent. See [here](https://developer.android.com/reference/android/content/Intent#addCategory(java.lang.String)) for details.</param>
        /// <param name="flags">Add additional flags to the intent. See [here](https://developer.android.com/reference/android/content/Intent#flags) for details.</param>
        /// Below is an example implementation:
        /// ```csharp
        /// // Launches the video player to play a video.
        /// serviceBinder.pbsStartActivity("", "", "picovr.intent.action.player", "{\"uri\":\"/sdcard/test.mp4\",\"videoType\":0,\"videoSource\":1}", new String[]{Intent.CATEGORY_DEFAULT}, new int[]{Intent.FLAG_ACTIVITY_NEW_TASK},0);
        /// ```
        /// <returns>
        /// * `0`: success
        /// * `1`: failure
        /// </returns>
        public static int StartActivity(String packageName, String className, String action, String extra, String[] categories, int[] flags)
        {
            return PXR_EnterprisePlugin.UPxr_StartActivity(packageName, className, action, extra, categories, flags);
        }

        /// <summary>Shows/hides specified app(s) in the library.
        /// @note Not supported by G2 4K devices. For Neo3 Pro/Pro Eye devices, the device's system version should be 5.0.0 or later.
        /// </summary>
        /// <param name="packageNames">Package name(s). If there are multiple names, use commas (,) to separate them.</param>
        /// <param name="switchEnum">Specifies to show/hide the app(s), enums:
        /// * `S_ON`: show
        /// * `S_OFF`: hide
        /// </param>
        /// <returns>
        /// * `0`: success
        /// * `1`: failure
        /// </returns>
        public static int CustomizeAppLibrary(String[] packageNames, SwitchEnum switchEnum)
        {
            return PXR_EnterprisePlugin.UPxr_CustomizeAppLibrary(packageNames, switchEnum);
        }

        /// <summary>
        /// Gets the controller's battery level.
        /// @note Not supported by G2 4K devices. For Neo3 Pro/Pro Eye devices, the device's system version should be 5.0.0 or later.
        /// </summary>
        /// <returns>Returns the following information: 
        /// * array[0]: the left controller's battery level
        /// * array[1]: the right controller's battery level
        /// * an integer from 1 to 5, which indicates the battery level, the bigger the integer, the higher the battery level
        /// </returns>
        public static int[] GetControllerBattery()
        {
            return PXR_EnterprisePlugin.UPxr_GetControllerBattery();
        }

        /// <summary>
        /// Gets the controller's connection status.
        /// @note Not supported by G2 4K devices. For Neo3 Pro/Pro Eye devices, the device's system version should be 5.0.0 or later.
        /// </summary>
        /// <returns>
        /// * `0`: both controllers are disconnected
        /// * `1`: the left controller is connected
        /// * `2`: the right controller is connected
        /// * `3`: both controllers are connected
        /// </returns>
        public static int GetControllerConnectState()
        {
            return PXR_EnterprisePlugin.UPxr_GetControllerConnectState();
        }

        /// <summary>
        /// Gets the apps that are hidden in the library.
        /// @note Not supported by G2 4K devices. For Neo3 Pro/Pro Eye devices, the device's system version should be 5.0.0 or later.
        /// </summary>
        /// <returns>The packages names of hidden apps. Multiple names are separated by commas (,).</returns>
        public static string GetAppLibraryHideList()
        {
            return PXR_EnterprisePlugin.UPxr_GetAppLibraryHideList();
        }

        /// <summary>
        /// Sets the device that outputs audio during screen casting.
        /// @note Only supported by PICO Neo3 Pro/Pro Eye and PICO 4 Enterprise devices.
        /// </summary>
        /// <param name="screencastAudioOutput">Specifies the device that outputs audio. Enumerations:
        /// `AUDIO_SINK`: the HMD
        /// `AUDIO_TARGET`: the receiver
        /// `AUDIO_SINK_TARGET`: both the HMD and the receiver
        /// </param>
        /// <returns>
        /// * `0`: success
        /// * `1`: failure
        /// </returns>
        public static int SetScreenCastAudioOutput(ScreencastAudioOutputEnum screencastAudioOutput)
        {
            return PXR_EnterprisePlugin.UPxr_SetScreenCastAudioOutput(screencastAudioOutput);
        }

        /// <summary>
        /// Gets the device that outputs audio during screen casting.
        /// @note Only supported by PICO Neo3 Pro/Pro Eye and PICO 4 Enterprise devices.
        /// </summary>
        /// <returns>
        /// Enumerations:
        /// `AUDIO_SINK`: the HMD
        /// `AUDIO_TARGET`: the receiver
        /// `AUDIO_SINK_TARGET`: both the HMD and the receiver
        /// </returns>
        public static ScreencastAudioOutputEnum GetScreenCastAudioOutput()
        {
            return PXR_EnterprisePlugin.UPxr_GetScreenCastAudioOutput();
        }

        /// <summary>
        /// Displays or hides the specified tab or option on the Settings pane.
        /// @note Not supported by G2 4K devices. For Neo3 Pro/Pro Eye devices, the device's system version should be 5.0.0 or later.
        /// </summary>
        /// <param name="customizeSettingsTabEnum">Specifies the tab or option to display or hide. Enumerations:
        /// * `CUSTOMIZE_SETTINGS_TAB_WLAN`: the "WLAN" tab
        /// * `CUSTOMIZE_SETTINGS_TAB_CONTROLLER`: the "Controller" tab
        /// * `CUSTOMIZE_SETTINGS_TAB_BLUETOOTH`: the "Bluetooth" tab
        /// * `CUSTOMIZE_SETTINGS_TAB_DISPLAY`: the "Display" tab
        /// * `CUSTOMIZE_SETTINGS_TAB_LAB`: the "LAB" tab
        /// * `CUSTOMIZE_SETTINGS_TAB_GENERAL_FACTORY_RESET`: the "Factory Reset" option on the "General" tab
        /// * `CUSTOMIZE_SETTINGS_TAB_GENERAL_LOCKSCREEN`: the "Lock Screen" option on the "General" tab
        /// </param>
        /// <param name="switchEnum">Sets to display or hide the specified tab or option:
        /// `S_ON`: display
        /// `S_OFF`: hide
        /// </param>
        /// <returns>
        /// * `0`: success
        /// * `1`: failure
        /// </returns>
        public static int UPxr_CustomizeSettingsTabStatus(CustomizeSettingsTabEnum customizeSettingsTabEnum, SwitchEnum switchEnum)
        {
            return PXR_EnterprisePlugin.UPxr_CustomizeSettingsTabStatus(customizeSettingsTabEnum, switchEnum);
        }

        /// <summary>
        /// Gets the status set for the specified tab or option on the Settings pane.
        /// @note Not supported by G2 4K devices. For Neo3 Pro/Pro Eye devices, the device's system version should be 5.0.0 or later.
        /// </summary>
        /// <param name="customizeSettingsTabEnum">Specifies the tab or option to get status for. Enumerations:
        /// * `CUSTOMIZE_SETTINGS_TAB_WLAN`: the "WLAN" tab
        /// * `CUSTOMIZE_SETTINGS_TAB_CONTROLLER`: the "Controller" tab
        /// * `CUSTOMIZE_SETTINGS_TAB_BLUETOOTH`: the "Bluetooth" tab
        /// * `CUSTOMIZE_SETTINGS_TAB_DISPLAY`: the "Display" tab
        /// * `CUSTOMIZE_SETTINGS_TAB_LAB`: the "LAB" tab
        /// * `CUSTOMIZE_SETTINGS_TAB_GENERAL_FACTORY_RESET`: the "Factory Reset" option on the "General" tab
        /// * `CUSTOMIZE_SETTINGS_TAB_GENERAL_LOCKSCREEN`: the "Lock Screen" option on the "General" tab
        /// </param>
        /// <returns>
        /// The status of the specified tab or option:
        /// `S_ON`: displayed
        /// `S_OFF`: hidden
        /// </returns>
        public static SwitchEnum UPxr_GetCustomizeSettingsTabStatus(CustomizeSettingsTabEnum customizeSettingsTabEnum)
        {
            return PXR_EnterprisePlugin.UPxr_GetCustomizeSettingsTabStatus(customizeSettingsTabEnum);
        }
    }
}