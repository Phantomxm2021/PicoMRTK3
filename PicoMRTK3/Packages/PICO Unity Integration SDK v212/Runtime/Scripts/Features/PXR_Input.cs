#if PICO_INSTALL
/*******************************************************************************
Copyright © 2015-2022 PICO Technology Co., Ltd.All rights reserved.  

NOTICE：All information contained herein is, and remains the property of 
PICO Technology Co., Ltd. The intellectual and technical concepts 
contained hererin are proprietary to PICO Technology Co., Ltd. and may be 
covered by patents, patents in process, and are protected by trade secret or 
copyright law. Dissemination of this information or reproduction of this 
material is strictly forbidden unless prior written permission is obtained from
PICO Technology Co., Ltd. 
*******************************************************************************/

using System;
using UnityEngine;
using UnityEngine.XR;

namespace Unity.XR.PXR
{
    public static class PXR_Input
    {
        public enum ControllerDevice
        {
            G2 = 3,
            Neo2,
            Neo3,
            PICO_4,
            NewController = 10
        }

        public enum Controller
        {
            LeftController,
            RightController,
        }

        public enum VibrateController
        {
            No = 0,
            Left = 1,
            Right = 2,
            LeftAndRight = 3,
        }

        public enum ChannelFlip {
            No,
            Yes,
        }

        public enum CacheConfig {
            CacheAndVibrate = 1,
            CacheNoVibrate = 2,
        }

        /// <summary>
        /// Gets the current dominant controller.
        /// </summary>
        /// <returns>The current dominant controller: `LeftController`; `RightController`.</returns>
        public static Controller GetDominantHand()
        {
            return (Controller)PXR_Plugin.Controller.UPxr_GetControllerMainInputHandle();
        }

        /// <summary>
        /// Sets a controller as the dominant controller.
        /// </summary>
        /// <param name="controller">The controller to be set as the dominant controller: `0`-left controller; `1`-right controller.</param>
        public static void SetDominantHand(Controller controller)
        {
            PXR_Plugin.Controller.UPxr_SetControllerMainInputHandle((UInt32)controller);
        }

        /// <summary>
        /// Sets controller vibration, including vibration amplitude and duration.
        /// @note The `SendHapticImpulse` method offered by UnityXR is also supported. Click [here](https://docs.unity3d.com/ScriptReference/XR.InputDevice.SendHapticImpulse.html) for more information.
        /// </summary>
        /// <param name="strength">Vibration amplitude. The valid value ranges from `0` to `1`. The greater the value, the stronger the vibration amplitude. To stop controller vibration, call this function again and set this parameter to `0`.</param>
        /// <param name="time">Vibration duration. The valid value ranges from `0` to `65535` ms.</param>
        /// <param name="controller">The controller to set vibration for:
        /// * `0`: left controller
        /// * `1`: right controller
        /// </param>
        public static void SetControllerVibration(float strength, int time, Controller controller)
        {
            PXR_Plugin.Controller.UPxr_SetControllerVibration((UInt32)controller, strength, time);
        }

        /// <summary>
        /// Gets the device model.
        /// </summary>
        /// <returns>The device model. Enumerations: `G2`, `Neo2`, `Neo3`, `NewController`, `PICO_4`.</returns> 
        public static ControllerDevice GetControllerDeviceType()
        {
            return (ControllerDevice)PXR_Plugin.Controller.UPxr_GetControllerType();
        }

        /// <summary>
        /// Gets the connection status for a specified controller.
        /// </summary>
        /// <param name="controller">The controller to get connection status for:
        /// * `0`: left controller
        /// * `1`: right controller
        /// </param>
        /// <returns>The connection status of the specified controller:
        /// * `true`: connected
        /// * `false`: not connected
        /// </returns>
        public static bool IsControllerConnected(Controller controller)
        {
            var state = false;
            switch (controller)
            {
                case Controller.LeftController:
                    InputDevices.GetDeviceAtXRNode(XRNode.LeftHand).TryGetFeatureValue(PXR_Usages.controllerStatus, out state);
                    return state;
                case Controller.RightController:
                    InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(PXR_Usages.controllerStatus, out state);
                    return state;
            }
            return state;
        }

        /// <summary>
        /// Sets the offset of the controller's display position to its real position.
        /// </summary>
        /// <param name="hand">The controller to set an offset for:
        /// * `0`: left controller
        /// * `1`: right controller
        /// </param>
        /// <param name="offset">The offset (in meters).</param>
        public static void SetControllerOriginOffset(Controller controller, Vector3 offset)
        {
            PXR_Plugin.Controller.UPxr_SetControllerOriginOffset((int)controller, offset);
        }

        /// <summary>
        /// Gets the predicted orientation of a specified controller after a specified time.
        /// </summary>
        /// <param name="hand">The controller to get the predicted rotation for:
        /// * `0`: left controller
        /// * `1`: right controller
        /// </param>
        /// <param name="predictTime">The time for prediction (in milliseconds).</param>
        /// <returns>The predicted orientation.</returns>
        public static Quaternion GetControllerPredictRotation(Controller controller, double predictTime)
        {
            PxrControllerTracking pxrControllerTracking = new PxrControllerTracking();
            float[] headData = new float[7] { 0, 0, 0, 0, 0, 0, 0 };

            PXR_Plugin.Controller.UPxr_GetControllerTrackingState((uint)controller, predictTime,headData, ref pxrControllerTracking);

            return new Quaternion(
                pxrControllerTracking.localControllerPose.pose.orientation.x,
                pxrControllerTracking.localControllerPose.pose.orientation.y, 
                pxrControllerTracking.localControllerPose.pose.orientation.z,
                pxrControllerTracking.localControllerPose.pose.orientation.w);
        }

        /// <summary>
        /// Gets the predicted position of a specified controller after a specified time.
        /// </summary>
        /// <param name="hand">The controller to get the predicted position for:
        /// * `0`: left controller
        /// * `1`: right controller
        /// </param>
        /// <param name="predictTime">The time for prediction (in milliseconds).</param>
        /// <returns>The predicted position.</returns>
        public static Vector3 GetControllerPredictPosition(Controller controller, double predictTime)
        {
            PxrControllerTracking pxrControllerTracking = new PxrControllerTracking();
            float[] headData = new float[7] { 0, 0, 0, 0, 0, 0, 0 };

            PXR_Plugin.Controller.UPxr_GetControllerTrackingState((uint)controller, predictTime, headData, ref pxrControllerTracking);

            return new Vector3(
                pxrControllerTracking.localControllerPose.pose.position.x, 
                pxrControllerTracking.localControllerPose.pose.position.y,
                pxrControllerTracking.localControllerPose.pose.position.z);
        }

        /// <summary>
        /// Sets event-triggered vibration for a specified controller.
        /// </summary>
        /// <param name="hand">The controller to enable vibration for:
        /// * `0`: left controller
        /// * `1`: right controller
        /// </param>
        /// <param name="frequency">Vibration frequency, which ranges from `50` to `500` Hz.</param>
        /// <param name="strength">Vibration amplitude. Its valid value ranges from `0` to `1`. The higher the value, the stronger the vibration amplitude.</param>
        /// <param name="time">Vibration duration, which ranges from `0` to `65535` ms.</param>
        public static int SetControllerVibrationEvent(UInt32 hand, int frequency, float strength, int time) {
            return PXR_Plugin.Controller.UPxr_SetControllerVibrationEvent(hand, frequency, strength, time);
        }

        /// <summary>
        /// Stops audio-triggered vibration.
        /// </summary>
        /// <param name="id">A reserved parameter, set it to the source ID returned by `StartVibrateBySharem` or `SaveVibrateByCache` to stop the corresponding vibration,
        /// or set it to `0` to stop all vibrations.</param>
        public static int StopControllerVCMotor(int sourceId) {
            return PXR_Plugin.Controller.UPxr_StopControllerVCMotor(sourceId);
        }

        /// <summary>
        /// Starts audio-triggered vibration for specified controller(s). The audio data come from an audio file.
        /// </summary>
        /// <param name="file">The path to the audio file.</param>
        /// <param name="vibrateController">The controller(s) to enable vibration for:
        /// * `0`: none
        /// * `1`: left controller
        /// * `2`: right controller
        /// * `3`: left and right controllers
        /// </param>
        public static int StartControllerVCMotor(string file, VibrateController vibrateController) {
            return PXR_Plugin.Controller.UPxr_StartControllerVCMotor(file, (int)vibrateController);
        }

        /// <summary>
        /// Sets the amplitude for audio-triggered vibration. Support changing the vibration amplitude during audio playback.
        /// </summary>
        /// <param name="mode">Vibration amplitude level:
        /// * `0`: no vibration
        /// * `1`: standard amplitude
        /// * `2`: 2×standard amplitude
        /// * `3`: 3×standard amplitude
        /// * `4`: 4×standard amplitude
        /// @note "3×standard amplitude" and "4×standard amplitude" are NOT recommended as they will cause serious loss of vibration details.
        /// </param>
        public static int SetControllerAmp(float mode) {
            return PXR_Plugin.Controller.UPxr_SetControllerAmp(mode);
        }

        /// <summary>
        /// Starts audio-triggered vibration for specified controller(s). The audio data come from an audio clip passed to the Unity Engine.
        /// </summary>
        /// <param name="audioClip">The path to the audio clip.</param>
        /// <param name="vibrateController">The controller(s) to enable vibration for:
        /// * `0`: none
        /// * `1`: left controller
        /// * `2`: right controller
        /// * `3`: left and right controllers
        /// </param>
        /// <param nname="channelFlip">Whether to enable audio channel inversion:
        /// * `Yes`: enable
        /// * `No`: disable
        /// Once audio channel inversion is enabled, the left controller vibrates with the audio data from the right channel, and vice versa.
        /// </param>
        /// <param nname="sourceId">Returns the unique ID for controlling the corresponding vibration,
        /// which will be used in `StartVibrateByCache`, `ClearVibrateByCache` or `StopControllerVCMotor`.</param>
        public static int StartVibrateBySharem(AudioClip audioClip, VibrateController vibrateController, ChannelFlip channelFlip,ref int sourceId) {
            if (audioClip == null)
            {
                return 0;
            }
            float[] data = new float[audioClip.samples * audioClip.channels];
            int buffersize = audioClip.samples * audioClip.channels;
            audioClip.GetData(data, 0);
            int sampleRate = audioClip.frequency;
            int channelMask = audioClip.channels;
            return PXR_Plugin.Controller.UPxr_StartVibrateBySharem(data, (int)vibrateController, buffersize, sampleRate, channelMask, 32, (int)channelFlip,ref sourceId);
        }

        /// <summary>
        /// Starts audio-triggered vibration for specified controller(s). This function is the overloaded version.
        /// </summary>
        /// <param name="data">The PCM data.</param>
        /// <param name="vibrateController">The controller(s) to enable vibration for:
        /// * `0`: none
        /// * `1`: left controller
        /// * `2`: right controller
        /// * `3`: left and right controllers
        /// </param>
        /// <param name="buffersize">The length of PCM data. Formula: (audioClip.samples)×(audioClip.channels).</param>
        /// <param name="frequency">Audio sampling rate.</param>
        /// <param name="channelMask">The number of channels.</param>
        /// <param name="channelFlip">Whether to enable audio channel inversion:
        /// * `Yes`: enable
        /// * `No`: disable
        ///  Once audio channel inversion is enabled, the left controller vibrates with the audio data from the right channel, and vice versa.
        /// </param>
        /// <param name="sourceId">Returns the unique ID for controlling the corresponding vibration,
        /// which will be used in `StartVibrateByCache`, `ClearVibrateByCache` or `StopControllerVCMotor`.</param>
        public static int StartVibrateBySharem(float[] data, VibrateController vibrateController, int buffersize, int frequency, int channelMask, ChannelFlip channelFlip,ref int sourceId)
        {
            return PXR_Plugin.Controller.UPxr_StartVibrateBySharem(data, (int)vibrateController, buffersize, frequency, channelMask, 32, (int)channelFlip, ref sourceId);
        }
        
        /// <summary>
        /// Caches audio-triggered vibration data for specified controller(s).
        /// @note The cached data can be extracted from the cache directory and then transmitted, which reduces resource consumption and improves service performance.
        /// </summary>
        /// <param name="audioClip">The path to the audio clip.</param>
        /// <param name="vibrateController">The controller(s) to cache data for:
        /// * `0`: none
        /// * `1`: left controller
        /// * `2`: right controller
        /// * `3`: left and right controllers</param>
        /// <param name="channelFlip">Whether to enable audio channel inversion:
        /// * `Yes`: enable
        /// * `No`: disable
        /// Once audio channel inversion is enabled, the left controller vibrates with the audio data from the right channel, and vice versa.
        /// </param>
        /// <param name="cacheConfig">Whether to keep the controller vibrating while caching audio-based vibration data:
        /// * `CacheAndVibrate`: cache and keep vibrating
        /// * `CacheNoVibrate`: cache and stop vibrating
        /// </param>
        /// <param name="sourceId">Returns the unique ID for controlling the corresponding vibration,
        /// which will be used in `StartVibrateByCache`, `ClearVibrateByCache` or `StopControllerVCMotor`.</param>
        /// <returns>
        /// * `0`: success
        /// * `-1`: failure
        /// </returns>
        public static int SaveVibrateByCache(AudioClip audioClip, VibrateController vibrateController, ChannelFlip channelFlip, CacheConfig cacheConfig,ref int sourceId)
        {
            if (audioClip == null)
            {
                return 0;
            }
            float[] data = new float[audioClip.samples * audioClip.channels];
            int buffersize = audioClip.samples * audioClip.channels;
            audioClip.GetData(data, 0);
            int sampleRate = audioClip.frequency;
            int channelMask = audioClip.channels;
            return PXR_Plugin.Controller.UPxr_SaveVibrateByCache(data, (int)vibrateController, buffersize, sampleRate, channelMask, 32, (int)channelFlip, (int)cacheConfig,ref sourceId);
        }

        /// <summary>
        /// Caches audio-triggered vibration data for specified controller(s). This function is the overloaded version.
        /// @note The cached data can be extracted from the cache directory and then transmitted, which reduces resource consumption and improves service performance.
        /// </summary>
        /// <param name="data">The PCM data.</param>
        /// <param name="vibrateController">The controller(s) to cache data for:
        /// * `0`: none
        /// * `1`: left controller
        /// * `2`: right controller
        /// * `3`: left and right controllers
        /// </param>
        /// <param name="buffersize">The length of PCM data. Formula: (audioClip.samples)×(audioClip.channels)</param>
        /// <param name="frequency">Audio sampling rate.</param>
        /// <param name="channelMask">The number of channels.</param>
        /// <param name="channelFlip">Whether to enable audio channel inversion:
        /// * `Yes`: enable
        /// * `No`: disable
        /// Once audio channel inversion is enabled, the left controller vibrates with the audio data from the right channel, and vice versa.
        /// </param>
        /// <param name="cacheConfig">Whether to keep the controller vibrating while caching audio-based vibration data:
        /// * `CacheAndVibrate`: cache and keep vibrating
        /// * `CacheNoVibrate`: cache and stop vibrating
        /// </param>
        /// <param name="sourceId">Returns the unique ID for controlling the corresponding vibration,
        /// which will be used in `StartVibrateByCache`, `ClearVibrateByCache` or `StopControllerVCMotor`.</param>
        /// <returns>
        /// * `0`: success
        /// * `-1`: failure
        /// </returns>
        public static int SaveVibrateByCache(float[] data, VibrateController vibrateController, int buffersize, int frequency, int channelMask, ChannelFlip channelFlip, CacheConfig cacheConfig, ref int sourceId)
        {
            return PXR_Plugin.Controller.UPxr_SaveVibrateByCache(data, (int)vibrateController, buffersize, frequency, channelMask, 32, (int)channelFlip, (int)cacheConfig,ref sourceId);
        }

        /// <summary>
        /// Plays cached audio-triggered vibration data.
        /// </summary>
        /// <param name="sourceId">The source ID returned by `StartVibrateBySharem` or `SaveVibrateByCache`.</param>
        /// <returns>
        /// * `0`: success
        /// * `-1`: failure
        /// </returns>
        public static int StartVibrateByCache(int sourceId) {
            return PXR_Plugin.Controller.UPxr_StartVibrateByCache(sourceId);
        }
        
        /// <summary>
        /// Clears cached audio-triggered vibration data.
        /// </summary>
        /// <param name="sourceId">The source ID returned by `StartVibrateBySharem` or `SaveVibrateByCache`.</param>
        /// <returns>
        /// * `0`: success
        /// * `-1`: failure
        /// </returns>
        public static int ClearVibrateByCache(int sourceId) {
            return PXR_Plugin.Controller.UPxr_ClearVibrateByCache(sourceId);
        }

        public static int SetControllerEnableKey(bool isEnable, PxrControllerKeyMap Key)
        {
            return PXR_Plugin.Controller.UPxr_SetControllerEnableKey(isEnable, Key);
        }

        /// <summary>
        /// Starts PHF-triggered vibration for specified controller(s). PHF stands for PICO haptic file.
        /// </summary>
        /// <param name="phfText">The path to the PHF file.</param>
        /// <param name="sourceId">The source ID returned by `StartVibrateBySharem` or `SaveVibrateByCache`.</param>
        /// <param name="vibrateController">The controller(s) to enable vibration for:
        /// * `0`: none
        /// * `1`: left controller
        /// * `2`: right controller
        /// * `3`: left and right controllers
        /// </param>
        /// <param name="channelFlip">Whether to enable audio channel inversion:
        /// * `Yes`: enable
        /// * `No`: disable
        /// Once audio channel inversion is enabled, the left controller vibrates with the audio data from the right channel, and vice versa.</param>
        /// <param name="amp">The vibration gain, the valid value range from `0` to `2`:
        /// * `0`: no vibration
        /// * `1`: standard amplitude
        /// * `2`: 2×standard amplitude</param>
        /// <returns>
        /// * `0`: success
        /// * `-1`: failure
        /// </returns>
        public static int StartVibrateByPHF(TextAsset phfText,ref int sourceId, VibrateController vibrateController, ChannelFlip channelFlip,float amp) {
            return PXR_Plugin.Controller.UPxr_StartVibrateByPHF(phfText.text, phfText.text.Length, ref sourceId, (int)vibrateController, (int)channelFlip, amp);
        }

        /// <summary>
        /// Pauses PHF-triggered vibration.
        /// </summary>
        /// <param name="sourceId">The source ID returned by `StartVibrateBySharem` or `SaveVibrateByCache`.</param>
        /// <returns>
        /// * `0`: success
        /// * `-1`: failure
        /// </returns>
        public static int PauseVibrate(int sourceId) {
            return PXR_Plugin.Controller.UPxr_PauseVibrate(sourceId);
        }

        /// <summary>
        /// Resumes PHF-triggered vibration.
        /// </summary>
        /// <param name="sourceId">The source ID returned by `StartVibrateBySharem` or `SaveVibrateByCache`.</param>
        /// <returns>
        /// * `0`: success
        /// * `-1`: failure
        /// </returns>
        public static int ResumeVibrate(int sourceId) {
            return PXR_Plugin.Controller.UPxr_ResumeVibrate(sourceId);
        }

        /// <summary>
        /// Dynamically updates PHF and AudioClip vibration data.
        /// </summary>
        /// <param name="sourceId">The source ID returned by `StartVibrateBySharem` or `SaveVibrateByCache`.</param>
        /// <param name="vibrateController">The controller(s) to update PHF and AudioClip vibration data for:
        /// * `0`: none
        /// * `1`: left controller
        /// * `2`: right controller
        /// * `3`: left and right controllers
        /// </param>
        /// <param name="channelFlip">Whether to enable audio channel inversion:
        /// * `Yes`: enable
        /// * `No`: disable
        /// Once audio channel inversion is enabled, the left controller vibrates with the audio data from the right channel, and vice versa.</param>
        /// <param name="amp">The vibration gain, the valid value range from `0` to `2`:
        /// * `0`: no vibration
        /// * `1`: standard amplitude
        /// * `2`: 2×standard amplitude</param>
        /// <returns>
        /// * `0`: success
        /// * `-1`: failure
        /// </returns>
        public static int UpdateVibrateParams(int sourceId, VibrateController vibrateController, ChannelFlip channelFlip, float amp) {
            return PXR_Plugin.Controller.UPxr_UpdateVibrateParams(sourceId, (int)vibrateController, (int)channelFlip, amp);
        }

    }
}
#endif
