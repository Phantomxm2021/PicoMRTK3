#if PICO_INSTALL

/*******************************************************************************
Copyright © 2015-2022 Pico Technology Co., Ltd.All rights reserved.

NOTICE：All information contained herein is, and remains the property of
Pico Technology Co., Ltd. The intellectual and technical concepts
contained herein are proprietary to Pico Technology Co., Ltd. and may be
covered by patents, patents in process, and are protected by trade secret or
copyright law. Dissemination of this information or reproduction of this
material is strictly forbidden unless prior written permission is obtained from
Pico Technology Co., Ltd.
*******************************************************************************/

using System;
using Pico.Platform.Models;
using UnityEngine;


namespace Pico.Platform
{
    /**
     * \ingroup Platform
     */
    public static class ApplicationService
    {
        /// <summary>
        /// Launches another app by app package name.
        /// @note If the user does not have that app installed, the user will be directed to the app's download page on the PICO Store.
        /// </summary>
        /// <param name="packageName">The package name of the to-be-launched app.</param>
        /// <param name="options">The options for launching the app. Pass `null` or leave this parameter empty.</param>
        /// <returns>If something goes wrong, a description message will be returned.</returns>
        public static Task<string> LaunchApp(string packageName, ApplicationOptions options = null)
        {
            if (!CoreService.Initialized)
            {
                Debug.LogError(CoreService.UninitializedError);
                return null;
            }

            return new Task<string>(CLIB.ppf_Application_LaunchOtherApp(packageName, (IntPtr) options));
        }
        /// <summary>
        /// Launches another app by app ID.
        /// @note If the user does not have that app installed, the user will be directed to the app's download page on the PICO Store.
        /// </summary>
        /// <param name="appId">The ID of the to-be-launched app.</param>
        /// <param name="options">The options for launching the app. Pass `null` or leave this parameter empty.</param>
        /// <returns>If something goes wrong, a description message will be returned.</returns>
        public static Task<string> LaunchAppByAppId(string appId, ApplicationOptions options = null)
        {
            if (!CoreService.Initialized)
            {
                Debug.LogError(CoreService.UninitializedError);
                return null;
            }

            return new Task<string>(CLIB.ppf_Application_LaunchOtherAppByAppID(appId, (IntPtr) options));
        }

        /// <summary>
        /// Gets the details about an app launch event.
        /// </summary>
        /// <returns>App launch details, including `LaunchResult` and `LaunchType`:
        /// * `LaunchResult`:
        ///   * `0`: Unknown
        ///   * `1`: Success 
        ///   * `2`: FailedRoomFull
        ///   * `3`: FailedGameAlreadyStarted
        ///   * `4`: FailedRoomNotFound
        ///   * `5`: FailedUserDeclined
        ///   * `6`: FailedOtherReason
        /// * `LaunchType`:
        ///   * `0`: Unknown
        ///   * `1`: Normal
        ///   * `2`: Invite
        ///   * `3`: Coordinated
        ///   * `4`: Deeplink
        /// </returns>
        public static LaunchDetails GetLaunchDetails()
        {
            return new LaunchDetails(CLIB.ppf_ApplicationLifecycle_GetLaunchDetails());
        }

        /// <summary>
        /// Logs whether a user has been successfully directed to the desired destination via a deep link.
        /// </summary>
        /// <param name="trackId">The tracking ID of the app launch event.</param>
        /// <param name="result">The app launch result:
        /// * `0`: Unknown
        /// * `1`: Success
        /// * `2`: FailedRoomFull
        /// * `3`: FailedGameAlreadyStarted
        /// * `4`: FailedRoomNotFound
        /// * `5`: FailedUserDeclined
        /// * `6`: FailedOtherReason
        /// </param>
        public static void LogDeeplinkResult(string trackId, LaunchResult result)
        {
            CLIB.ppf_ApplicationLifecycle_LogDeeplinkResult(trackId, result);
        }
    }


    public class ApplicationOptions
    {
        public ApplicationOptions()
        {
            Handle = CLIB.ppf_ApplicationOptions_Create();
        }


        public void SetDeeplinkMessage(string value)
        {
            CLIB.ppf_ApplicationOptions_SetDeeplinkMessage(Handle, value);
        }

        /// For passing to native C
        public static explicit operator IntPtr(ApplicationOptions options)
        {
            return options?.Handle ?? IntPtr.Zero;
        }

        ~ApplicationOptions()
        {
            CLIB.ppf_ApplicationOptions_Destroy(Handle);
        }

        readonly IntPtr Handle;
    }
}
#endif