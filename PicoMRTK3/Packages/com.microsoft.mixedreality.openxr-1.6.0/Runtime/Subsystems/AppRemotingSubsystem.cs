// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Microsoft.MixedReality.OpenXR.Remoting;
using UnityEngine;
using UnityEngine.XR.OpenXR;

namespace Microsoft.MixedReality.OpenXR
{
    internal class AppRemotingSubsystem
    {
        private const NativeLibToken nativeLibToken = NativeLibToken.Remoting;
        private static AppRemotingSubsystem m_instance = new AppRemotingSubsystem();
        private static readonly AppRemotingPlugin m_appRemotingPlugin = OpenXRSettings.Instance.GetFeature<AppRemotingPlugin>();
        private static readonly PlayModeRemotingPlugin m_playModeRemotingPlugin = OpenXRSettings.Instance.GetFeature<PlayModeRemotingPlugin>();
        private bool m_runtimeOverrideAttempted = false;

        internal static AppRemotingSubsystem GetCurrent()
        {
            if (m_appRemotingPlugin != null && m_appRemotingPlugin.enabled)
            {
                return m_instance;
            }
            else if (m_playModeRemotingPlugin != null && m_playModeRemotingPlugin.enabled)
            {
                return m_instance;
            }
            else
            {
                return null;
            }
        }

        public bool TryGetConnectionState(out ConnectionState connectionState, out DisconnectReason disconnectReason)
        {
            return NativeLib.TryGetRemotingConnectionState(nativeLibToken, out connectionState, out disconnectReason);
        }

        public bool TryLocateUserReferenceSpace(FrameTime frameTime, out Pose pose)
        {
            return NativeLib.TryLocateUserReferenceSpace(nativeLibToken, frameTime, out pose);
        }

        internal bool TryConvertToRemoteTime(long playerPerformanceCount, out long remotePerformanceCount)
        {
            return NativeLib.TryConvertToRemoteTime(nativeLibToken, playerPerformanceCount, out remotePerformanceCount);
        }

        internal bool TryConvertToPlayerTime(long remotePerformanceCount, out long playerPerformanceCount)
        {
            return NativeLib.TryConvertToPlayerTime(nativeLibToken, remotePerformanceCount, out playerPerformanceCount);
        }

        internal bool TryEnableRemotingOverride()
        {
            if (!m_runtimeOverrideAttempted)
            {
                m_runtimeOverrideAttempted = true;
                if (NativeLib.TryEnableRemotingOverride(nativeLibToken))
                {
                    return true;
                }
            }
            return false;
        }

        internal void ResetRemotingOverride()
        {
            if (m_runtimeOverrideAttempted)
            {
                m_runtimeOverrideAttempted = false;
                NativeLib.ResetRemotingOverride(nativeLibToken);
            }
        }
    }
}