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
using System.IO;
using System.Runtime.CompilerServices;
using Unity.XR.PXR;
using UnityEngine;

[assembly: InternalsVisibleTo("Assembly-CSharp-Editor")]

namespace Pico.Platform
{
    /**
     * \defgroup Platform PICO Platform Services
     */
    /**
     * \ingroup Platform
     */
    public static class CoreService
    {
        public static bool Initialized = false;
        public static string UninitializedError = "Platform SDK has not been initialized!";
        public static string UserIdEmptyError = "User ID is null or empty!";

        /// <summary>Gets whether the Platform SDK has been initialized.</summary>
        /// <returns>
        /// * `true`: initialized
        /// * `false`: not initialized
        /// </returns>
        public static bool IsInitialized()
        {
            return Initialized;
        }

        /// <summary>
        /// Gets the app ID for the current app.
        /// </summary>
        /// <returns>The app ID.</returns>
        /// <exception cref="UnityException">If the app ID cannot be found, this exception will be thrown.</exception>
        private static string GetAppID(string appId = null)
        {
            string configAppID = PXR_PlatformSetting.Instance.appID.Trim();
            if (String.IsNullOrWhiteSpace(appId))
            {
                if (String.IsNullOrWhiteSpace(configAppID))
                {
                    throw new UnityException("Cannot find appId");
                }

                appId = configAppID;
            }
            else
            {
                if (!String.IsNullOrWhiteSpace(configAppID))
                {
                    Debug.LogWarning($"Using {appId} as appId rather than {configAppID} which is configured in Unity Editor");
                }
            }

            return appId;
        }

        /// <summary>
        /// Initializes the Platform SDK asynchronously. 
        /// </summary>
        /// <param name="appId">The app ID for the Platform SDK. If not provided, Unity editor configuration will be applied.</param>
        /// <returns>The initialization result.</returns>
        /// <exception cref="UnityException">If the input app ID is null or empty or if the initialization fails, this exception will be thrown.</exception>
        /// <exception cref="NotImplementedException">If the current platform is not supported, this exception will be thrown.</exception>
        public static Task<PlatformInitializeResult> AsyncInitialize(string appId = null)
        {
            appId = GetAppID(appId);
            if (String.IsNullOrWhiteSpace(appId))
            {
                throw new UnityException("AppID cannot be null or empty");
            }

            Task<PlatformInitializeResult> task;
            if (Application.platform == RuntimePlatform.Android)
            {
                var requestId = CLIB.ppf_UnityInitAsynchronousWrapper(appId);
                if (requestId == 0)
                {
                    throw new UnityException("Pico Platform failed to initialize.");
                }

                task = new Task<PlatformInitializeResult>(requestId);
            }
            else if ((Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.OSXEditor)
                     || (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor))
            {
                var configPath = Path.GetFullPath("Assets/Resources/PicoSdkPCConfig.json");
                var logDirectory = Path.GetFullPath("Logs");
                if (!File.Exists(configPath))
                {
                    throw new UnityException($"cannot find PC config file {configPath}");
                }

                if (!Directory.Exists(logDirectory))
                {
                    Directory.CreateDirectory(logDirectory);
                }

                var requestId = CLIB.ppf_PcInitAsynchronousWrapper(appId, File.ReadAllText(configPath), logDirectory);
                if (requestId == 0)
                {
                    throw new UnityException("Pico Platform failed to initialize.");
                }

                task = new Task<PlatformInitializeResult>(requestId);
            }
            else
            {
                throw new NotImplementedException("Pico platform is not implemented on this platform yet.");
            }

            Initialized = true;
            Runner.RegisterGameObject();
            return task;
        }

        /// <summary>
        /// Initializes the Platform SDK synchronously.
        /// </summary>
        /// <param name="appId">The app ID for the Platform SDK. If not provided, Unity editor configuration will be applied.</param>
        /// <exception cref="NotImplementedException">If the current platform is not supported, this exception will be thrown.</exception>
        /// <exception cref="UnityException">If the initialization fails, this exception will be thrown.</exception>
        public static void Initialize(string appId = null)
        {
            if (Initialized)
            {
                return;
            }

            appId = GetAppID(appId);
            if (String.IsNullOrWhiteSpace(appId))
            {
                throw new UnityException("AppID must not be null or empty");
            }

            PlatformInitializeResult initializeResult;
            if (Application.platform == RuntimePlatform.Android)
            {
                initializeResult = CLIB.ppf_UnityInitWrapper(appId);

                if (initializeResult == PlatformInitializeResult.Success ||
                    initializeResult == PlatformInitializeResult.AlreadyInitialized)
                {
                    Initialized = true;
                }
            }
            else if ((Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.OSXEditor)
                     || (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor))
            {
                var configPath = Path.GetFullPath("Assets/Resources/PicoSdkPCConfig.json");
                var logDirectory = Path.GetFullPath("Logs");
                if (!File.Exists(configPath))
                {
                    throw new UnityException($"cannot find PC config file {configPath}");
                }

                if (!Directory.Exists(logDirectory))
                {
                    Directory.CreateDirectory(logDirectory);
                }

                initializeResult = CLIB.ppf_PcInitWrapper(appId, File.ReadAllText(configPath), logDirectory);
                if (initializeResult == PlatformInitializeResult.Success ||
                    initializeResult == PlatformInitializeResult.AlreadyInitialized)
                {
                    Initialized = true;
                }
            }
            else
            {
                throw new NotImplementedException("Pico platform is not implemented on this platform yet.");
            }

            if (!Initialized)
            {
                throw new UnityException($"Pico Platform failed to initialize：{initializeResult}.");
            }

            Runner.RegisterGameObject();
        }

        /// <summary>
        /// Initializes game-related modules, such as room, matchmaking, and network.
        /// </summary>
        /// <param name="accessToken">The access token of Platform SDK. You can get the access token by calling `UserService.GetAccessToken()`.</param>
        public static Task<GameInitializeResult> GameInitialize(string accessToken)
        {
            if (Initialized)
            {
                return new Task<GameInitializeResult>(CLIB.ppf_Game_InitializeWithToken(accessToken));
            }

            Debug.LogError(UninitializedError);
            return null;
        }

        /// <summary>
        /// Initializes modules without token related with game, such as room, matchmaking, and net.
        /// </summary>
        public static Task<GameInitializeResult> GameInitialize()
        {
            if (Initialized)
            {
                return new Task<GameInitializeResult>(CLIB.ppf_Game_InitializeAuto());
            }

            Debug.LogError(UninitializedError);
            return null;
        }

        /// <summary>
        /// Uninitializes game-related modules, such as room, matchmaking, and network.
        /// </summary>
        /// <returns>
        /// * `true`: success
        /// * `false`: failure
        /// </returns>
        public static bool GameUninitialize()
        {
            if (Initialized)
            {
                return CLIB.ppf_Game_UnInitialize();
            }

            return false;
        }
    }
}
#endif