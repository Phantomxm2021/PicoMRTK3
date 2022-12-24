// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using System.Globalization;
using System.IO;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.XR.OpenXR.Features;
using UnityEngine.XR.Management;
using UnityEngine.XR.OpenXR;
using UnityEngine.XR.OpenXR.Features;
#endif

namespace Microsoft.MixedReality.OpenXR.Remoting
{
#if UNITY_EDITOR
    [OpenXRFeature(UiName = featureName,
        BuildTargetGroups = new[] { BuildTargetGroup.Standalone },
        Company = "Microsoft",
        Desc = featureName + " in Unity editor.",
        DocumentationLink = "https://aka.ms/openxr-unity-editor-remoting",
        OpenxrExtensionStrings = requestedExtensions,
        Category = FeatureCategory.Feature,
        Required = false,
        Priority = -100,    // hookup before other plugins so it affects json before GetProcAddr.
        FeatureId = featureId,
        Version = "1.6.0")]
#endif
    [NativeLibToken(NativeLibToken = NativeLibToken.Remoting)]
    internal class PlayModeRemotingPlugin : OpenXRFeaturePlugin<PlayModeRemotingPlugin>
#if UNITY_EDITOR
        , ISerializationCallbackReceiver
#endif
    {
        internal const string featureId = "com.microsoft.openxr.feature.playmoderemoting";
        internal const string featureName = "Holographic Remoting for Play Mode";
        private const string requestedExtensions = "XR_MSFT_holographic_remoting XR_MSFT_holographic_remoting_speech";
        private const string SettingsFileName = "MixedRealityOpenXRRemotingSettings.asset";
        private static string UserSettingsFolder => Path.Combine(Application.dataPath, "..", "UserSettings");
        private static string SettingsAssetPath => Path.Combine(UserSettingsFolder, SettingsFileName);

        [SerializeField, Tooltip("The host name or IP address of the player running in network server mode to connect to."), Obsolete("Use the remotingSettings values instead")]
        private string m_remoteHostName = string.Empty;

        [SerializeField, Tooltip("The port number of the server's handshake port."), Obsolete("Use the remotingSettings values instead")]
        private ushort m_remoteHostPort = 8265;

        [SerializeField, Tooltip("The max bitrate in Kbps to use for the connection."), Obsolete("Use the remotingSettings values instead")]
        private uint m_maxBitrate = 20000;

        [SerializeField, Tooltip("The video codec to use for the connection."), Obsolete("Use the remotingSettings values instead")]
        private RemotingVideoCodec m_videoCodec = RemotingVideoCodec.Auto;

        [SerializeField, Tooltip("Enable/disable audio remoting."), Obsolete("Use the remotingSettings values instead")]
        private bool m_enableAudio = false;

        private readonly bool m_playModeRemotingIsActive =
#if UNITY_EDITOR
            true;
#else
            false;
#endif

        protected override IntPtr HookGetInstanceProcAddr(IntPtr func)
        {
            if (enabled && m_playModeRemotingIsActive)
            {
                if (AppRemotingSubsystem.GetCurrent().TryEnableRemotingOverride())
                {
                    return NativeLib.HookGetInstanceProcAddr(nativeLibToken, func);
                }
            }
            return func;
        }

        protected override void OnInstanceDestroy(ulong instance)
        {
            if (enabled && m_playModeRemotingIsActive)
            {
                AppRemotingSubsystem.GetCurrent().ResetRemotingOverride();
            }
            base.OnInstanceDestroy(instance);
        }

        protected override void OnSystemChange(ulong systemId)
        {
            base.OnSystemChange(systemId);

            if (systemId != 0 && m_playModeRemotingIsActive)
            {
                RemotingSettings remotingSettings = GetOrLoadRemotingSettings();
                NativeLib.SetRemoteSpeechCulture(nativeLibToken, CultureInfo.CurrentCulture.Name);

                NativeLib.ConnectRemoting(nativeLibToken, new RemotingConfiguration
                {
                    RemoteHostName = remotingSettings.RemoteHostName,
                    RemotePort = remotingSettings.RemoteHostPort,
                    MaxBitrateKbps = remotingSettings.MaxBitrate,
                    VideoCodec = remotingSettings.VideoCodec,
                    EnableAudio = remotingSettings.EnableAudio
                }, secureConnect: false, authenticationToken: "", performSystemValidation: false, validateServerCertificateCallback: null);
            }
        }

        protected override void OnSessionStateChange(int oldState, int newState)
        {
            if (m_playModeRemotingIsActive && (XrSessionState)newState == XrSessionState.LossPending)
            {
                _ = NativeLib.TryGetRemotingConnectionState(NativeLibToken.Remoting, out ConnectionState connectionState, out DisconnectReason disconnectReason);

                RemotingSettings remotingSettings = GetOrLoadRemotingSettings();

                if (disconnectReason == DisconnectReason.RemotingVersionMismatch)
                {
                    Debug.LogError($"The Holographic Remoting Player app has a mismatched version " +
                        $"on the remote host {remotingSettings.RemoteHostName}:{remotingSettings.RemoteHostPort}. " +
                        $"Please update the Player app on your headset and try again.");
                }
                else
                {
                    Debug.LogError($"Play to Holographic Remoting is disconnected unexpectedly. " +
                        $"Host address = {remotingSettings.RemoteHostName}:{remotingSettings.RemoteHostPort}. " +
                        $"ConnectionState = {connectionState}, DisconnectReason = {disconnectReason}. ");
                }

#if UNITY_EDITOR
                EditorApplication.ExitPlaymode();
#endif
            }
        }

        private RemotingSettings m_remotingSettings;

        internal RemotingSettings GetOrLoadRemotingSettings()
        {
            if (m_remotingSettings == null)
            {
                // If this file doesn't yet exist, create it and port from the old values.
                m_remotingSettings = CreateInstance<RemotingSettings>();

                if (File.Exists(SettingsAssetPath))
                {
                    using (StreamReader settingsReader = new StreamReader(SettingsAssetPath))
                    {
                        JsonUtility.FromJsonOverwrite(settingsReader.ReadToEnd(), m_remotingSettings);
                    }
                }
                else
                {
#pragma warning disable CS0618 // to use the obsolete fields to port to the new asset file
                    m_remotingSettings.RemoteHostName = m_remoteHostName;
                    m_remotingSettings.RemoteHostPort = m_remoteHostPort;
                    m_remotingSettings.MaxBitrate = m_maxBitrate;
                    m_remotingSettings.VideoCodec = m_videoCodec;
                    m_remotingSettings.EnableAudio = m_enableAudio;
#pragma warning restore CS0618
                }
            }

            return m_remotingSettings;
        }

#if UNITY_EDITOR
        internal bool HasValidSettings() => !string.IsNullOrEmpty(GetOrLoadRemotingSettings().RemoteHostName);

        private void SaveSettings()
        {
            // Don't try to load the settings here. If this is null, then there's
            // no need to do extra work to load and save the same file.
            // When remoting is used, this is guaranteed to be non-null.
            if (m_remotingSettings == null)
            {
                return;
            }

            if (!Directory.Exists(UserSettingsFolder))
            {
                Directory.CreateDirectory(UserSettingsFolder);
            }

            using (StreamWriter settingsWriter = new StreamWriter(SettingsAssetPath))
            {
                settingsWriter.Write(JsonUtility.ToJson(m_remotingSettings, true));
            }
        }

        protected override void GetValidationChecks(System.Collections.Generic.List<ValidationRule> results, BuildTargetGroup targetGroup)
        {
            PlayModeRemotingValidator.GetValidationChecks(this, results);
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize() => SaveSettings();

        void ISerializationCallbackReceiver.OnAfterDeserialize() { } // Can't call EnsureSettingsLoaded() here, since Application.dataPath can't be accessed during deserialization

        private class PlayModeRemotingHelper : MonoBehaviour
        {
            private void Start()
            {
                StartCoroutine(EnsureInitialization());
            }

            public static System.Collections.IEnumerator EnsureInitialization()
            {
                if (XRGeneralSettings.Instance.Manager.activeLoader == null)
                {
                    Debug.Log("[PlayModeRemotingPlugin] InitializeLoader");
                    yield return XRGeneralSettings.Instance.Manager.InitializeLoader();
                }

                if (XRGeneralSettings.Instance.Manager.activeLoader != null)
                {
                    Debug.Log("[PlayModeRemotingPlugin] StartSubsystems");
                    XRGeneralSettings.Instance.Manager.StartSubsystems();
                }
            }

            public static void StopXrLoader()
            {
                if (XRGeneralSettings.Instance.Manager.activeLoader != null)
                {
                    XRGeneralSettings.Instance.Manager.StopSubsystems();
                    Debug.Log("[PlayModeRemotingPlugin] StopSubsystems");

                    if (XRGeneralSettings.Instance.Manager.isInitializationComplete)
                    {
                        XRGeneralSettings.Instance.Manager.DeinitializeLoader();
                        Debug.Log("[PlayModeRemotingPlugin] DeinitializeLoader");
                    }
                }
            }

            public static void InitializePlayModeStateChanged()
            {
                EditorApplication.playModeStateChanged += PlayModeStateChangeObserved;
            }

            private static void PlayModeStateChangeObserved(PlayModeStateChange state)
            {
                // If PlayModeRemotingPlugin isn't enabled or InitManagerOnStart is enabled, we don't need the helper.
                PlayModeRemotingPlugin feature = OpenXRSettings.Instance.GetFeature<PlayModeRemotingPlugin>();
                XRGeneralSettings standaloneGeneralSettings = XRSettingsHelpers.GetOrCreateXRGeneralSettings(BuildTargetGroup.Standalone);
                if (feature == null || !feature.enabled || standaloneGeneralSettings == null || standaloneGeneralSettings.InitManagerOnStart)
                {
                    EditorApplication.playModeStateChanged -= PlayModeStateChangeObserved;
                    return;
                }

                if (state == PlayModeStateChange.EnteredPlayMode)
                {
                    _ = new GameObject("PlayModeRemotingHelper", typeof(PlayModeRemotingHelper))
                    {
                        hideFlags = HideFlags.HideAndDontSave
                    };
                }
                else if (state == PlayModeStateChange.ExitingPlayMode)
                {
                    StopXrLoader();
                    EditorApplication.playModeStateChanged -= PlayModeStateChangeObserved;
                }
            }
        }

        [InitializeOnEnterPlayMode]
        private static void OnEnterPlaymodeInEditor(EnterPlayModeOptions options)
        {
            PlayModeRemotingHelper.InitializePlayModeStateChanged();
        }
#endif
    }

    internal class RemotingSettings : ScriptableObject
    {
        [field: SerializeField, Tooltip("The host name or IP address of the player running in network server mode to connect to.")]
        public string RemoteHostName { get; set; } = string.Empty;

        [field: SerializeField, Tooltip("The port number of the server's handshake port.")]
        public ushort RemoteHostPort { get; set; } = 8265;

        [field: SerializeField, Tooltip("The max bitrate in Kbps to use for the connection.")]
        public uint MaxBitrate { get; set; } = 20000;

        [field: SerializeField, Tooltip("The video codec to use for the connection.")]
        public RemotingVideoCodec VideoCodec { get; set; } = RemotingVideoCodec.Auto;

        [field: SerializeField, Tooltip("Enable/disable audio remoting.")]
        public bool EnableAudio { get; set; } = false;
    }
}
