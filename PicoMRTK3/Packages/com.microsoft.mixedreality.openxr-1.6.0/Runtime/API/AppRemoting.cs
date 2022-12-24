// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using Unity.Collections;
using UnityEngine.XR.OpenXR;

namespace Microsoft.MixedReality.OpenXR.Remoting
{
    /// <summary>
    /// Provides information and configuration for creating a Holographic Remoting remote app.
    /// </summary>
    /// <remarks>
    /// Please note that client/server and player/remote are orthogonal concepts in remoting.
    /// Holographic Remoting remote app can act as either a
    /// Server - when listening to incoming connection from a Holographic Remoting player app (Client)
    /// (or)
    /// Client - when connecting to Holographic Remoting player app (Server) that is listening to incoming connections.
    /// For more details, please reference the <see href="https://docs.microsoft.com/windows/mixed-reality/develop/native/holographic-remoting-terminology">Holographic Remoting Terminology</see>.
    /// </remarks>
    public static class AppRemoting
    {
        /// <summary>
        /// Starts connect with given Holographic Remoting remote app (Client) configuration and initializes XR.
        /// </summary>
        /// <remarks>
        /// The remote app (Client) will try and connect to remote player (Server) listening for incoming connections, 
        /// and after a successful connection, XR experience starts. If the connection fails for any reason, try to connect by calling the coroutine again.
        /// This method must be run as a coroutine itself, as initializing XR has to happen in a coroutine.
        /// </remarks>
        /// <param name="configuration">The set of parameters to use for remoting.</param>
        public static System.Collections.IEnumerator Connect(RemotingConfiguration configuration)
        {
            if (m_appRemotingPlugin != null && m_appRemotingPlugin.enabled)
            {
                yield return m_appRemotingPlugin.Connect(configuration, null, null);
            }
            else
            {
                Debug.LogError($"App Remoting feature is not enabled.");
            }
        }

        /// <summary>
        /// Starts listen with given Holographic Remoting remote app (Server) configuration and initializes XR.
        /// </summary>
        /// <remarks>
        /// The remote app (Server) will be waiting for remote player (Client) to connect, and after a successful connection, XR experience starts. 
        /// If the connection fails for any reason, it will retry listening for incoming connection until some other method is called.
        /// This method must be run as a coroutine itself, as initializing XR has to happen in a coroutine.
        /// </remarks>
        /// <param name="listenConfiguration">The set of parameters to use for remoting.</param>
        /// <param name="onRemotingListenCompleted"> Action callback to signal listen complete. A new Connect or Listen coroutine can safely be started after this callback.</param>
        /// <remarks>
        /// During a Listen coroutine, if the remote app calls <see cref="Listen"/> or <see cref="Connect"/> function, 
        /// the second call will fail, because there can only be a single outstanding remoting connection. The Listen coroutine will wait indefinitely for new connections 
        /// until the remote app calls <see cref="Disconnect"/> function to stop listening.  During this coroutine, there could be multiple remoting connections 
        /// and the <see cref="ConnectionState"/> may change multiple times. If the remote app wants to know the completion of above listening session, 
        /// it can use the `onRemotingListenCompleted` callback here. After the callback, the Listen coroutine will complete, and the application can safely call the `Connect` or `Listen` functions again. 
        /// </remarks>
        public static System.Collections.IEnumerator Listen(RemotingListenConfiguration listenConfiguration, Action onRemotingListenCompleted = null)
        {
            if (m_appRemotingPlugin != null && m_appRemotingPlugin.enabled)
            {
                yield return m_appRemotingPlugin.Listen(listenConfiguration, null, onRemotingListenCompleted);
            }
            else
            {
                Debug.LogError($"App Remoting feature is not enabled.");
                if (onRemotingListenCompleted != null)
                {
                    onRemotingListenCompleted.Invoke();
                }
            }
        }

        /// <summary>
        /// Disconnects from the remote player (Client/Server) and stops the active XR session.
        /// </summary>
        public static void Disconnect()
        {
            if (m_appRemotingPlugin != null && m_appRemotingPlugin.enabled)
            {
                m_appRemotingPlugin.Disconnect();
            }
            else
            {
                Debug.LogError($"App Remoting feature is not enabled.");
            }
        }

        /// <summary>
        /// Provides information on the current remoting session, if one exists.
        /// </summary>
        /// <param name="connectionState">The current connection state of the remote session.</param>
        /// <param name="disconnectReason">If the connection state is disconnected, this helps explain why.</param>
        /// <returns>Whether the information was successfully retrieved.</returns>
        public static bool TryGetConnectionState(out ConnectionState connectionState, out DisconnectReason disconnectReason)
        {
            connectionState = ConnectionState.Disconnected;
            disconnectReason = DisconnectReason.None;
            AppRemotingSubsystem subsystem = AppRemotingSubsystem.GetCurrent();
            return subsystem != null && subsystem.TryGetConnectionState(out connectionState, out disconnectReason);
        }

        /// <summary>
        ///  To locate the `XR_REMOTING_REFERENCE_SPACE_TYPE_USER_MSFT` reference space in Unity's scene origin space in the remote app. For more details, reference the
        ///  <see href="https://docs.microsoft.com/windows/mixed-reality/develop/native/holographic-remoting-coordinate-system-synchronization-openxr">Coordinate System Synchronization with Holographic Remoting</see>.
        /// </summary>
        /// <param name="frameTime">Specify the <see cref="FrameTime"/> to locate the user reference space.</param>
        /// <param name="pose">Output the pose of the user reference space in the Unity's scene origin space.</param>
        /// <returns>Returns true when the user reference space is tracking and output pose is valid to be used.
        ///  Returns false when the user reference space lost tracking or it's not properly set up.</returns>
        public static bool TryLocateUserReferenceSpace(FrameTime frameTime, out Pose pose)
        {
            pose = Pose.identity;
            AppRemotingSubsystem subsystem = AppRemotingSubsystem.GetCurrent();
            return subsystem != null && subsystem.TryLocateUserReferenceSpace(frameTime, out pose);
        }

        /// <summary>
        /// Convert the time from a player app QPC time to the synchronized remote app QPC time.
        /// </summary>
        /// <param name="playerPerformanceCount">The performance count obtained in the player app using QueryPerformanceCounter.</param>
        /// <param name="remotePerformanceCount">Output the synchronized performance count as if using QueryPerformanceCounter in the remote app at the same time.
        ///     The output will be 0, indicating invalid time, if the function returns false.</param>
        /// <returns>Returns true when the time are successfully converted. 
        ///  Returns false indicates the time synchronization between remote and player app is not yet established.
        /// </returns>
        internal static bool TryConvertToRemoteTime(long playerPerformanceCount, out long remotePerformanceCount)
        {
            remotePerformanceCount = 0; // default to invalid timestamp
            AppRemotingSubsystem subsystem = AppRemotingSubsystem.GetCurrent();
            return subsystem != null && subsystem.TryConvertToRemoteTime(playerPerformanceCount, out remotePerformanceCount);
        }

        /// <summary>
        /// Convert the time from a remote app QPC time to the synchronized player app QPC time.
        /// </summary>
        /// <param name="remotePerformanceCount">The performance count obtained in the remote app using QueryPerformanceCounter.</param>
        /// <param name="playerPerformanceCount">Output the synchronized performance count as if using QueryPerformanceCounter in the player app at the same time.
        ///     The output will be 0, indicating invalid time, if the function returns false.</param>
        /// <returns>Returns true when the time are successfully converted. 
        ///  Returns false indicates the time synchronization between remote and player app is not yet established.
        /// </returns>
        internal static bool TryConvertToPlayerTime(long remotePerformanceCount, out long playerPerformanceCount)
        {
            playerPerformanceCount = 0; // default to invalid timestamp
            AppRemotingSubsystem subsystem = AppRemotingSubsystem.GetCurrent();
            return subsystem != null && subsystem.TryConvertToPlayerTime(remotePerformanceCount, out playerPerformanceCount);
        }

        private static readonly AppRemotingPlugin m_appRemotingPlugin = OpenXRSettings.Instance.GetFeature<AppRemotingPlugin>();
    }

    /// <summary>
    /// Describes the preferred video codec to use for the connection.
    /// </summary>
    public enum RemotingVideoCodec
    {
        /// <summary>
        /// Represents HEVC video codec preferred, fall back to H264 if HEVC is not supported by all participants.
        /// </summary>
        Auto = 0,
        /// <summary>
        /// Represents HEVC video codec.
        /// </summary>
        H265,
        /// <summary>
        /// Represents H264 video codec.
        /// </summary>
        H264,
    }

    /// <summary>
    /// Specifies the configuration for the remote app (Client) to initiate a remoting connection.
    /// </summary>
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct RemotingConfiguration
    {
        /// <summary>
        /// The host name or IP address of the player running in network server mode to connect to.
        /// </summary>
        public string RemoteHostName;

        /// <summary>
        /// The port number of the server's handshake port.
        /// </summary>
        public ushort RemotePort;

        /// <summary>
        /// The max bitrate in Kbps to use for the connection.
        /// </summary>
        public uint MaxBitrateKbps;

        /// <summary>
        /// The video codec to use for the connection.
        /// </summary>
        public RemotingVideoCodec VideoCodec;

        /// <summary>
        /// Enable/disable audio remoting.
        /// </summary>
        public bool EnableAudio;
    }

    /// <summary>
    /// Specifies the configuration for the remote app (Server) to initiate a remoting connection in listen mode.
    /// </summary>
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct RemotingListenConfiguration
    {
        /// <summary>
        /// The host name or IP address of the player running in network server mode to connect to.
        /// </summary>
        public string ListenInterface;

        /// <summary>
        /// The port number of the server's handshake port.
        /// </summary>
        public ushort HandshakeListenPort;

        /// <summary>
        /// The port number of the server's transport port.
        /// </summary>
        public ushort TransportListenPort;

        /// <summary>
        /// The max bitrate in Kbps to use for the connection.
        /// </summary>
        public uint MaxBitrateKbps;

        /// <summary>
        /// The video codec to use for the connection.
        /// </summary>
        public RemotingVideoCodec VideoCodec;

        /// <summary>
        /// Enable/disable audio remoting.
        /// </summary>
        public bool EnableAudio;
    }

    /// <summary>
    /// Describes the current connection state.
    /// </summary>
    public enum ConnectionState
    {
        /// <summary>
        /// Represents that the state is not connected, and no connection attempt is
        /// in progress (Client), or not listening for incoming connections (Server).
        /// </summary>
        Disconnected = 0,
        /// <summary>
        /// Represents connecting to server (Client), listening for incoming
        /// connections (Server), or performing connection handshake (Client/Server).
        /// </summary>
        Connecting = 1,
        /// <summary>
        /// Represents fully connected, all communication channels established (Client/Server).
        /// </summary>
        Connected = 2,
    }

    /// <summary>
    /// Describes the reason for why the connection disconnected.
    /// </summary>
    public enum DisconnectReason
    {
        /// <summary>
        /// The connection succeeded and there was no connection failure.
        /// </summary>
        None = 0,
        /// <summary>
        /// The connection failed for an unknown reason.
        /// </summary>
        Unknown = 1,
        /// <summary>
        /// The secure connection was enabled, but certificate was missing, invalid, or not usable (Server).
        /// </summary>
        NoServerCertificate = 2,
        /// <summary>
        /// The handshake port could not be opened for accepting connections (Server).
        /// </summary>
        HandshakePortBusy = 3,
        /// <summary>
        /// The handshake server is unreachable (Client).
        /// </summary>
        HandshakeUnreachable = 4,
        /// <summary>
        /// The handshake server closed the connection prematurely; likely due to TLS/Plain mismatch or invalid certificate (Client).
        /// </summary>
        HandshakeConnectionFailed = 5,
        /// <summary>
        /// The authentication with the handshake server failed (Client).
        /// </summary>
        AuthenticationFailed = 6,
        /// <summary>
        /// No common compatible remoting version could be determined during handshake (Client).
        /// </summary>
        RemotingVersionMismatch = 7,
        /// <summary>
        /// No common transport protocol could be determined during handshake (Client).
        /// </summary>
        IncompatibleTransportProtocols = 8,
        /// <summary>
        /// The handshake failed for any other reason (Client).
        /// </summary>
        HandshakeFailed = 9,
        /// <summary>
        /// The transport port could not be opened for accepting connections (Server).
        /// </summary>
        TransportPortBusy = 10,
        /// <summary>
        /// The transport server is unreachable (Client).
        /// </summary>
        TransportUnreachable = 11,
        /// <summary>
        /// The transport connection was closed before all communication channels had been set up (Client/Server).
        /// </summary>
        TransportConnectionFailed = 12,
        /// <summary>
        /// The transport connection was closed due to protocol version mismatch (Client/Server).
        /// </summary>
        ProtocolVersionMismatch = 13,
        /// <summary>
        /// A protocol error occurred that was severe enough to invalidate the current connection or connection attempt (Client/Server).
        /// </summary>
        ProtocolError = 14,
        /// <summary>
        /// The transport connection was closed due to the requested video codec not being available (Client/Server).
        /// </summary>
        VideoCodecNotAvailable = 15,
        /// <summary>
        /// The connection attempt has been canceled (Client/Server).
        /// </summary>
        Canceled = 16,
        /// <summary>
        /// The connection has been closed by peer (Client/Server).
        /// </summary>
        ConnectionLost = 17,
        /// <summary>
        /// The connection has been closed due to graphics device loss (Client/Server).
        /// </summary>
        DeviceLost = 18,
        /// <summary>
        /// The connection has been closed by request (Client/Server).
        /// </summary>
        DisconnectRequest = 19,
        /// <summary>
        /// The network is unreachable. This usually means the client knows no route to reach the remote host (Client).
        /// </summary>
        HandshakeNetworkUnreachable = 20,
        /// <summary>
        /// No connection could be made because the remote side actively refused it. Usually this means that no host application is running (Client).
        /// </summary>
        HandshakeConnectionRefused = 21,
        /// <summary>
        /// The transport connection was closed due to the requested video format not being available (Client/Server).
        /// </summary>
        VideoFormatNotAvailable = 22,
        /// <summary>
        /// Disconnected after receiving a disconnect request from the peer (Client/Server).
        /// </summary>
        PeerDisconnectRequest = 23,
        /// <summary>
        /// Timed out while waiting for peer to close connection (Client/Server).
        /// </summary>
        PeerDisconnectTimeout = 24,
        /// <summary>
        /// Timed out while waiting for transport session to be opened (Client/Server).
        /// </summary>
        SessionOpenTimeout = 25,
        /// <summary>
        /// Timed out while waiting for the remoting handshake to complete (Client/Server).
        /// </summary>
        RemotingHandshakeTimeout = 26,
        /// <summary>
        /// The connection failed due to an internal error (Client/Server).
        /// </summary>
        InternalError = 27,
        /// <summary>
        /// The handshake could not be opened due to insufficient permissions (Client).
        /// </summary>
        HandshakePermissionDenied = 28,
    }
}
