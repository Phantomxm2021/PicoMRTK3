// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using System.Runtime.InteropServices;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.XR.Management;
using UnityEngine.XR.OpenXR;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.XR.OpenXR.Features;
#endif

namespace Microsoft.MixedReality.OpenXR.Remoting
{
#if UNITY_EDITOR
    [OpenXRFeature(UiName = featureName,
        BuildTargetGroups = new[] { BuildTargetGroup.Standalone, BuildTargetGroup.WSA },
        Company = "Microsoft",
        Desc = "Feature to enable " + featureName + ".",
        DocumentationLink = "https://aka.ms/openxr-unity-app-remoting",
        OpenxrExtensionStrings = requestedExtensions,
        Category = FeatureCategory.Feature,
        Required = false,
        Priority = -100,    // hookup before other plugins so it affects json before GetProcAddr.
        FeatureId = featureId,
        Version = "1.6.0")]
#endif
    [NativeLibToken(NativeLibToken = NativeLibToken.Remoting)]
    internal class AppRemotingPlugin : OpenXRFeaturePlugin<AppRemotingPlugin>
    {
        private enum RemotingState
        {
            Idle = 0,
            Connect = 1,
            Listen = 2,
            Disconnecting = 3
        }

        private static RemotingState s_remotingState;
        private RemotingConfiguration m_remotingConnectConfiguration;
        private RemotingListenConfiguration m_remotingListenConfiguration;

        private bool m_secureConnect = false;
        private static SecureRemotingConnectConfiguration s_secureRemotingConnectConfiguration = default;
        private static InternalValidateServerCertificateDelegate s_internalValidateServerCertificateCallback = null;

        private bool m_secureListen = false;
        private static SecureRemotingListenConfiguration s_secureRemotingListenConfiguration = default;
        private static SecureRemotingValidateAuthenticationTokenDelegate s_validateAuthenticationTokenCallback = null;

        internal const string featureId = "com.microsoft.openxr.feature.appremoting";
        internal const string featureName = "Holographic Remoting remote app";
        private const string requestedExtensions = "XR_MSFT_holographic_remoting XR_MSFT_holographic_remoting_speech";

        private OpenXRRuntimeRestartHandler m_restartHandler = null;

        protected override IntPtr HookGetInstanceProcAddr(IntPtr func)
        {
            if (enabled && AppRemotingSubsystem.GetCurrent().TryEnableRemotingOverride())
            {
                return NativeLib.HookGetInstanceProcAddr(nativeLibToken, func);
            }
            else
            {
                return func;
            }
        }

        protected override void OnSubsystemCreate()
        {
            base.OnSubsystemCreate();

            if (enabled && m_restartHandler == null)
            {
                m_restartHandler = new OpenXRRuntimeRestartHandler(this, skipRestart: true, skipQuitApp: true);
            }
            else if (!enabled && m_restartHandler != null)
            {
                m_restartHandler.Dispose();
                m_restartHandler = null;
            }
        }

        protected override void OnInstanceDestroy(ulong instance)
        {
            if (enabled)
            {
                AppRemotingSubsystem.GetCurrent().ResetRemotingOverride();
            }

            Debug.Log($"[AppRemotingPlugin] OnInstanceDestroy, remotingState was {s_remotingState}.");
            if (s_remotingState != RemotingState.Listen)
            {
                s_remotingState = RemotingState.Idle;
            }
            base.OnInstanceDestroy(instance);
        }

        protected unsafe override void OnSystemChange(ulong systemId)
        {
            base.OnSystemChange(systemId);

            if (systemId != 0)
            {
                Debug.Log($"[AppRemotingPlugin] OnSystemChange, systemId = {systemId}, remotingState = {s_remotingState}.");
                NativeLib.SetRemoteSpeechCulture(nativeLibToken, CultureInfo.CurrentCulture.Name);

                if (s_remotingState == RemotingState.Connect)
                {
                    NativeLib.ConnectRemoting(nativeLibToken, m_remotingConnectConfiguration, m_secureConnect,
                    s_secureRemotingConnectConfiguration.AuthenticationToken, s_secureRemotingConnectConfiguration.PerformSystemValidation,
                    s_internalValidateServerCertificateCallback);
                }
                else if (s_remotingState == RemotingState.Listen)
                {
                    // The following method is used for both secure and non-secure Listen in native layer.
                    // The secure mode parameters are used in native layer only when m_secureListen
                    // is set to true, otherwise they are disregarded. 
                    NativeLib.ListenRemoting(nativeLibToken,
                                            m_remotingListenConfiguration,
                                            m_secureListen,
                                            NativeArrayUnsafeUtility.GetUnsafePtr(s_secureRemotingListenConfiguration.Certificate),
                                            (uint)(s_secureRemotingListenConfiguration.Certificate.Length),
                                            s_secureRemotingListenConfiguration.SubjectName,
                                            s_secureRemotingListenConfiguration.KeyPassphrase,
                                            s_validateAuthenticationTokenCallback);
                }
            }
        }

        protected override void OnSessionStateChange(int oldState, int newState)
        {
            if ((XrSessionState)newState == XrSessionState.LossPending)
            {
                if (s_remotingState == RemotingState.Connect)
                {
                    Debug.LogError($"[AppRemotingPlugin] Cannot establish a connection to Holographic Remoting Player " +
                        $"on the target with IP Address {m_remotingConnectConfiguration.RemoteHostName}:{m_remotingConnectConfiguration.RemotePort}.");
                }
                else if (s_remotingState == RemotingState.Listen)
                {
                    Debug.Log("[AppRemotingPlugin] Listening to incoming Holographic Remoting connection is interrupted.");
                }
            }
        }

        public System.Collections.IEnumerator Connect(RemotingConfiguration configuration, SecureRemotingConnectConfiguration? secureRemotingConnectConfiguration = null, Action onRemotingConnectCompleted = null)
        {
            if (s_remotingState == RemotingState.Idle)
            {
                m_remotingConnectConfiguration = configuration;
                m_remotingListenConfiguration = default;
                s_remotingState = RemotingState.Connect;
                if (secureRemotingConnectConfiguration != null)
                {
                    m_secureConnect = true;
                    s_secureRemotingConnectConfiguration = secureRemotingConnectConfiguration.Value;
                    if (s_secureRemotingConnectConfiguration.ValidateServerCertificateCallback != null)
                    {
                        s_internalValidateServerCertificateCallback = new InternalValidateServerCertificateDelegate(ImplementValidateServerCertificate);
                    }
                }

                if (XRGeneralSettings.Instance.Manager.activeLoader == null)
                {
                    Debug.Log("[AppRemotingPlugin] Connect InitializeLoader");
                    yield return XRGeneralSettings.Instance.Manager.InitializeLoader();
                }

                if (XRGeneralSettings.Instance.Manager.activeLoader != null)
                {
                    Debug.Log("[AppRemotingPlugin] Connect StartSubsystems");
                    XRGeneralSettings.Instance.Manager.StartSubsystems();
                }

                yield return new WaitUntil(() => XRGeneralSettings.Instance.Manager.activeLoader == null);
            }
            else
            {
                Debug.LogError("Cannot connect when previous connection is still in progress");
            }

            if (onRemotingConnectCompleted != null)
            {
                onRemotingConnectCompleted.Invoke();
            }
        }

        // IL2CPP does not support marshaling delegates that point to instance methods to native code.
        // Using a static method that handles the callback and redirect accordingly. Note that 
        // certificate handling is also done in the following method and hence the signature is a little different than ValidateServerCertificateCallback.
        [MonoPInvokeCallback]
        private static SecureRemotingCertificateValidationResult ImplementValidateServerCertificate(string hostName, SecureRemotingCertificateValidationResult systemValidationResult)
        {
            X509Certificate2Collection certChain = GetCertificateChain();
            SecureRemotingCertificateValidationResult? systemValidationResultPassed = s_secureRemotingConnectConfiguration.PerformSystemValidation ? systemValidationResult : (SecureRemotingCertificateValidationResult?)null;
            return s_secureRemotingConnectConfiguration.ValidateServerCertificateCallback(hostName, certChain, systemValidationResultPassed);
        }

        // Intended to use only as part of secure connect
        private static X509Certificate2Collection GetCertificateChain()
        {
            X509Certificate2Collection certChain = new X509Certificate2Collection();
            uint certChainLength = NativeLib.GetNumCertificates(nativeLibToken);
            for (uint certIndex = 0; certIndex < certChainLength; certIndex++)
            {
                IntPtr certificate = NativeLib.GetCertificate(nativeLibToken, certIndex, out int size);
                byte[] certByteArray = new byte[size];
                Marshal.Copy(certificate, certByteArray, 0, size);
                X509Certificate2 cert = new X509Certificate2(certByteArray);
                certChain.Add(cert);
            }
            return certChain;
        }

        public System.Collections.IEnumerator Listen(RemotingListenConfiguration configuration, SecureRemotingListenConfiguration? secureRemotingListenConfiguration = null, Action onRemotingListenCompleted = null)
        {
            var defaultWait = new WaitForSeconds(0.5f);
            AppRemotingSubsystem subsystem = AppRemotingSubsystem.GetCurrent();

            if (s_remotingState == RemotingState.Idle)
            {
                m_remotingListenConfiguration = configuration;
                m_remotingConnectConfiguration = default;
                if (secureRemotingListenConfiguration != null)
                {
                    m_secureListen = true;
                    s_secureRemotingListenConfiguration = (SecureRemotingListenConfiguration)secureRemotingListenConfiguration;
                    s_validateAuthenticationTokenCallback = new SecureRemotingValidateAuthenticationTokenDelegate(ImplementValidateAuthenticationToken);
                }
                s_remotingState = RemotingState.Listen;

                while (s_remotingState == RemotingState.Listen)
                {
                    if (XRGeneralSettings.Instance.Manager.activeLoader == null)
                    {
                        Debug.Log("[AppRemotingPlugin] Listen, InitializeLoader");
                        yield return XRGeneralSettings.Instance.Manager.InitializeLoader();
                    }

                    if (XRGeneralSettings.Instance.Manager.activeLoader != null)
                    {
                        Debug.Log("[AppRemotingPlugin] Listen, StartSubsystems");
                        XRGeneralSettings.Instance.Manager.StartSubsystems();
                        yield return defaultWait;
                    }

                    while (true)
                    {
                        if (!subsystem.TryGetConnectionState(out ConnectionState connectionState, out _) ||
                            connectionState == ConnectionState.Disconnected)
                        {
                            Debug.Log("[AppRemotingPlugin] Listen, After disconnection, Stop XR Loader.");
                            StopXrLoader();
                            break;  // If disconnected, stop XR session and try to restart.
                        }
                        if (XRGeneralSettings.Instance.Manager.activeLoader == null)
                        {
                            break;  // if XR loader is already stopped, try to restart.
                        }

                        yield return defaultWait;
                    }

                    Debug.Log("[AppRemotingPlugin] Listen, Try restart XR session");
                    yield return defaultWait;
                }
            }
            else
            {
                Debug.LogError("[AppRemotingPlugin] Cannot listen when previous connection is still in progress");
            }

            if (onRemotingListenCompleted != null)
            {
                onRemotingListenCompleted.Invoke();
            }
        }

        // IL2CPP does not support marshaling delegates that point to instance methods to native code.
        // Using a static method that handles the callback and redirect accordingly.
        [MonoPInvokeCallback]
        private static bool ImplementValidateAuthenticationToken(string authenticationTokenToCheck)
        {
            return s_secureRemotingListenConfiguration.ValidateAuthenticationTokenCallback(authenticationTokenToCheck);
        }

        private class AppRemotingDisconnectHelper : MonoBehaviour
        {
            private void Start()
            {
                StartCoroutine(NotifySubsystemDestroyAndDisconnect());
            }
            public static System.Collections.IEnumerator NotifySubsystemDestroyAndDisconnect()
            {
                if (OpenXRContext.Current.Instance != 0)
                {
                    // Notify the AR Foundation subsystems before the subsystem destroy and 
                    // allow some time for cleaning up
                    NativeLib.DestroyAnchorSubsystemPending(NativeLibToken.HoloLens);

                    // wait for one frame to make sure the Anchor changes are notified to Unity on GetAnchorChanges() callback
                    yield return null;

                    NativeLib.RemoveAllAnchors(NativeLibToken.HoloLens);

                    // wait for one frame to make sure removed anchors are notified
                    yield return null;

                    NativeLib.DisconnectRemoting(NativeLibToken.Remoting);
                }

                AppRemotingPlugin.StopXrLoader();
                AppRemotingPlugin.s_remotingState = RemotingState.Idle;
            }
        }

        public void Disconnect()
        {
            if (s_remotingState != RemotingState.Disconnecting)
            {
                s_remotingState = RemotingState.Disconnecting;
                _ = new GameObject("AppRemotingDisconnectHelper", typeof(AppRemotingDisconnectHelper))
                {
                    hideFlags = HideFlags.HideAndDontSave
                };
            }
        }

        private static void StopXrLoader()
        {
            if (XRGeneralSettings.Instance.Manager.activeLoader != null)
            {
                XRGeneralSettings.Instance.Manager.StopSubsystems();
                Debug.Log("[AppRemotingPlugin] Disconnect StopSubsystems");

                if (XRGeneralSettings.Instance.Manager.isInitializationComplete)
                {
                    XRGeneralSettings.Instance.Manager.DeinitializeLoader();
                    Debug.Log("[AppRemotingPlugin] Disconnect DeinitializeLoader");
                }
            }
        }

#if UNITY_EDITOR
        protected override void GetValidationChecks(System.Collections.Generic.List<ValidationRule> results, BuildTargetGroup targetGroup)
        {
            AppRemotingValidator.GetValidationChecks(this, results, targetGroup);
        }
#endif
    }

    public delegate SecureRemotingCertificateValidationResult InternalValidateServerCertificateDelegate(string hostName, SecureRemotingCertificateValidationResult systemValidationResult);

    // Temporary placeholder for securemode API definitions. Will be moved from here soon.
    public struct SecureRemotingConnectConfiguration
    {
        public string AuthenticationToken;
        public bool PerformSystemValidation;
        public SecureRemotingValidateServerCertificateDelegate ValidateServerCertificateCallback;
    }

    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct SecureRemotingCertificateValidationResult
    {
        public bool TrustedRoot;
        public bool Revoked;
        public bool Expired;
        public bool WrongUsage;
        public bool RevocationCheckFailed;
        public bool InvalidCertOrChain;
        public SecureRemotingNameValidationResult NameValidationResult;
    }

    public enum SecureRemotingNameValidationResult
    {
        ResultNotChecked = 0,
        ResultMatch = 1,
        ResultMismatch = 2,
    }

    public delegate SecureRemotingCertificateValidationResult SecureRemotingValidateServerCertificateDelegate(string hostName, X509Certificate2Collection serverCertificateChain, SecureRemotingCertificateValidationResult? systemValidationResult = null);

    public struct SecureRemotingListenConfiguration
    {
        public NativeArray<byte> Certificate;
        public string SubjectName;
        public string KeyPassphrase;
        public SecureRemotingValidateAuthenticationTokenDelegate ValidateAuthenticationTokenCallback;
    }

    public delegate bool SecureRemotingValidateAuthenticationTokenDelegate(string authenticationTokenToCheck);
}
