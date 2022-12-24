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

namespace Pico.Platform.Models
{
    /// <summary>
    /// The binary message received in RTC room.
    /// </summary>
    public class RtcBinaryMessageReceived
    {
        /** @brief The sender's userId of the message.*/
        public readonly string UserId;

        /** @brief The binary data of the message.*/
        public readonly byte[] Data;

        /** @brief The room id of the message.*/
        public readonly string RoomId;

        public RtcBinaryMessageReceived(IntPtr o)
        {
            UserId = CLIB.ppf_RtcBinaryMessageReceived_GetUserId(o);
            var ptr = CLIB.ppf_RtcBinaryMessageReceived_GetData(o);
            var sz = CLIB.ppf_RtcBinaryMessageReceived_GetLength(o);
            Data = MarshalUtil.ByteArrayFromNative(ptr, (uint) sz);
            RoomId = CLIB.ppf_RtcBinaryMessageReceived_GetRoomId(o);
        }
    }

    /// <summary>
    /// The message send result indicates whether the message is successfully sent.
    /// </summary>
    public class RtcMessageSendResult
    {
        /**@brief The message id which is increased integer.*/
        public readonly long MessageId;

        /**@brief The error code of the send result. 200 means success.*/
        public readonly int Error;

        /**@brief The roomId of the message. */
        public readonly string RoomId;

        public RtcMessageSendResult(IntPtr o)
        {
            MessageId = CLIB.ppf_RtcMessageSendResult_GetMessageId(o);
            Error = CLIB.ppf_RtcMessageSendResult_GetError(o);
            RoomId = CLIB.ppf_RtcMessageSendResult_GetRoomId(o);
        }
    }

    /// <summary>
    /// The remote user cancel publish his/her stream.
    /// </summary>
    public class RtcUserUnPublishInfo
    {
        /** @brief The id of the user.*/
        public readonly string UserId;

        /** @brief The media stream type.*/
        public readonly RtcMediaStreamType MediaStreamType;

        /** @brief The reason why the remote user cancel publish stream.*/
        public readonly RtcStreamRemoveReason Reason;

        public readonly string RoomId;

        public RtcUserUnPublishInfo(IntPtr o)
        {
            UserId = CLIB.ppf_RtcUserUnPublishInfo_GetUserId(o);
            MediaStreamType = CLIB.ppf_RtcUserUnPublishInfo_GetMediaStreamType(o);
            Reason = CLIB.ppf_RtcUserUnPublishInfo_GetReason(o);
            RoomId = CLIB.ppf_RtcUserUnPublishInfo_GetRoomId(o);
        }
    }

    /// <summary>
    /// The publish stream info.
    /// If the remote user publish his/her stream,you will receive a
    /// notification.
    /// </summary>
    public class RtcUserPublishInfo
    {
        /** @brief The user id of the remote user.*/
        public readonly string UserId;

        /** @brief The media stream type.*/
        public readonly RtcMediaStreamType MediaStreamType;

        /**@brief The room id.*/
        public readonly string RoomId;

        public RtcUserPublishInfo(IntPtr o)
        {
            UserId = CLIB.ppf_RtcUserPublishInfo_GetUserId(o);
            MediaStreamType = CLIB.ppf_RtcUserPublishInfo_GetMediaStreamType(o);
            RoomId = CLIB.ppf_RtcUserPublishInfo_GetRoomId(o);
        }
    }

    /// <summary>
    /// The received room message.
    ///
    /// The remote users can send text message to the room and you will receive
    /// this message.
    /// </summary>
    public class RtcRoomMessageReceived
    {
        /**@brief The sender userId*/
        public readonly string UserId;

        /**@brief The text message.*/
        public readonly string Message;

        public readonly string RoomId;

        public RtcRoomMessageReceived(IntPtr o)
        {
            UserId = CLIB.ppf_RtcRoomMessageReceived_GetUserId(o);
            Message = CLIB.ppf_RtcRoomMessageReceived_GetMessage(o);
            RoomId = CLIB.ppf_RtcRoomMessageReceived_GetRoomId(o);
        }
    }

    /// <summary>
    /// The received user message.
    ///
    /// The remote user can send message to certain user.
    /// </summary>
    public class RtcUserMessageReceived
    {
        public readonly string UserId;
        public readonly string Message;
        public readonly string RoomId;

        public RtcUserMessageReceived(IntPtr o)
        {
            UserId = CLIB.ppf_RtcUserMessageReceived_GetUserId(o);
            Message = CLIB.ppf_RtcUserMessageReceived_GetMessage(o);
            RoomId = CLIB.ppf_RtcUserMessageReceived_GetRoomId(o);
        }
    }

    /// <summary>
    /// The received StreamSyncInfo.
    ///
    /// The remote user can send stream sync info and you will receive this message.
    /// </summary>
    public class RtcStreamSyncInfo
    {
        public readonly RtcRemoteStreamKey StreamKey;
        public readonly RtcSyncInfoStreamType StreamType;
        public readonly byte[] Data;

        public RtcStreamSyncInfo(IntPtr o)
        {
            StreamKey = new RtcRemoteStreamKey(CLIB.ppf_RtcStreamSyncInfo_GetStreamKey(o));
            StreamType = CLIB.ppf_RtcStreamSyncInfo_GetStreamType(o);
            var ptr = CLIB.ppf_RtcStreamSyncInfo_GetData(o);
            var sz = CLIB.ppf_RtcStreamSyncInfo_GetLength(o);
            Data = MarshalUtil.ByteArrayFromNative(ptr, (uint) sz);
        }
    }

    /// <summary>
    /// If you turn on the audio properties report,you will receive the audio property info
    /// periodically.
    /// </summary>
    public class RtcAudioPropertyInfo
    {
        /** @brief The detected volume.It's a value between 0~255*/
        public readonly int Volume;

        public RtcAudioPropertyInfo(IntPtr o)
        {
            Volume = CLIB.ppf_RtcAudioPropertyInfo_GetVolume(o);
        }
    }

    /// <summary>
    /// You will receive this message after you call \ref RtcService.JoinRoom.
    /// </summary>
    public class RtcJoinRoomResult
    {
        public readonly string RoomId;
        public readonly string UserId;

        /**@brief The error code equals to 0 if nothing error.*/
        public readonly int ErrorCode;

        /**@brief The elapsed time since you call `RtcService.JoinRoom`*/
        public readonly int Elapsed;

        /**@brief Whether you are first join the room or reconnected.*/
        public readonly RtcJoinRoomType JoinType;

        public RtcJoinRoomResult(IntPtr o)
        {
            RoomId = CLIB.ppf_RtcJoinRoomResult_GetRoomId(o);
            UserId = CLIB.ppf_RtcJoinRoomResult_GetUserId(o);
            ErrorCode = CLIB.ppf_RtcJoinRoomResult_GetErrorCode(o);
            Elapsed = CLIB.ppf_RtcJoinRoomResult_GetElapsed(o);
            JoinType = CLIB.ppf_RtcJoinRoomResult_GetJoinType(o);
        }
    }

    /// <summary>
    /// You will receive this message after you call \ref RtcService.LeaveRoom().
    /// </summary>
    public class RtcLeaveRoomResult
    {
        public readonly string RoomId;

        public RtcLeaveRoomResult(IntPtr o)
        {
            RoomId = CLIB.ppf_RtcLeaveRoomResult_GetRoomId(o);
        }
    }

    /// <summary>
    /// The local audio properties info.
    ///
    /// You will receive this message periodically after you
    /// call \ref RtcService.EnableAudioPropertiesReport().
    /// </summary>
    public class RtcLocalAudioPropertiesInfo
    {
        /**@brief The stream index info.*/
        public readonly RtcStreamIndex StreamIndex;

        /**@brief The audio property detail.*/
        public readonly RtcAudioPropertyInfo AudioPropertyInfo;

        public RtcLocalAudioPropertiesInfo(IntPtr o)
        {
            StreamIndex = CLIB.ppf_RtcLocalAudioPropertiesInfo_GetStreamIndex(o);
            AudioPropertyInfo = new RtcAudioPropertyInfo(CLIB.ppf_RtcLocalAudioPropertiesInfo_GetAudioPropertyInfo(o));
        }
    }

    /// <summary>
    /// The local audio properties report.
    ///
    /// You will receive this message periodically after you
    /// call \ref RtcService.EnableAudioPropertiesReport().
    /// </summary>
    public class RtcLocalAudioPropertiesReport
    {
        public readonly RtcLocalAudioPropertiesInfo[] AudioPropertiesInfos;

        public RtcLocalAudioPropertiesReport(IntPtr o)
        {
            ulong total = (ulong) CLIB.ppf_RtcLocalAudioPropertiesReport_GetAudioPropertiesInfosSize(o);
            AudioPropertiesInfos = new RtcLocalAudioPropertiesInfo[total];
            for (uint i = 0; i < total; i++)
            {
                AudioPropertiesInfos[i] = new RtcLocalAudioPropertiesInfo(CLIB.ppf_RtcLocalAudioPropertiesReport_GetAudioPropertiesInfos(o, (UIntPtr) i));
            }
        }
    }

    /// <summary>
    /// The media device change info.
    ///
    /// RTC engine will send this message if the media device change is detected.
    /// </summary>
    public class RtcMediaDeviceChangeInfo
    {
        public readonly string DeviceId;
        public readonly RtcMediaDeviceType DeviceType;
        public readonly RtcMediaDeviceState DeviceState;
        public readonly RtcMediaDeviceError DeviceError;

        public RtcMediaDeviceChangeInfo(IntPtr o)
        {
            DeviceId = CLIB.ppf_RtcMediaDeviceChangeInfo_GetDeviceId(o);
            DeviceType = CLIB.ppf_RtcMediaDeviceChangeInfo_GetDeviceType(o);
            DeviceState = CLIB.ppf_RtcMediaDeviceChangeInfo_GetDeviceState(o);
            DeviceError = CLIB.ppf_RtcMediaDeviceChangeInfo_GetDeviceError(o);
        }
    }

    /// <summary>
    /// You will received this notification if the remote user call \ref Rtc.MuteLocalAudio.
    /// </summary>
    public class RtcMuteInfo
    {
        public readonly string UserId;
        public readonly RtcMuteState MuteState;

        public RtcMuteInfo(IntPtr o)
        {
            UserId = CLIB.ppf_RtcMuteInfo_GetUserId(o);
            MuteState = CLIB.ppf_RtcMuteInfo_GetMuteState(o);
        }
    }

    /// <summary>
    /// The remote audio properties info.
    /// You can check who is speaking by this method.
    /// </summary>
    public class RtcRemoteAudioPropertiesInfo
    {
        public readonly RtcRemoteStreamKey StreamKey;
        public readonly RtcAudioPropertyInfo AudioPropertiesInfo;

        public RtcRemoteAudioPropertiesInfo(IntPtr o)
        {
            StreamKey = new RtcRemoteStreamKey(CLIB.ppf_RtcRemoteAudioPropertiesInfo_GetStreamKey(o));
            AudioPropertiesInfo = new RtcAudioPropertyInfo(CLIB.ppf_RtcRemoteAudioPropertiesInfo_GetAudioPropertiesInfo(o));
        }
    }

    /// <summary>
    /// You will receive remote user's audio info if you call \ref RtcService.EnableAudioPropertiesReport.
    /// </summary>
    public class RtcRemoteAudioPropertiesReport
    {
        public readonly RtcRemoteAudioPropertiesInfo[] AudioPropertiesInfos;
        public readonly int TotalRemoteVolume;

        public RtcRemoteAudioPropertiesReport(IntPtr o)
        {
            AudioPropertiesInfos = new RtcRemoteAudioPropertiesInfo[(int) CLIB.ppf_RtcRemoteAudioPropertiesReport_GetAudioPropertiesInfosSize(o)];
            for (uint i = 0; i < AudioPropertiesInfos.Length; i++)
            {
                AudioPropertiesInfos[i] = new RtcRemoteAudioPropertiesInfo(CLIB.ppf_RtcRemoteAudioPropertiesReport_GetAudioPropertiesInfos(o, (UIntPtr) i));
            }

            TotalRemoteVolume = CLIB.ppf_RtcRemoteAudioPropertiesReport_GetTotalRemoteVolume(o);
        }
    }

    /// <summary>
    /// RtcRemoteStreamKey indicates the Stream index of a remote user.
    /// </summary>
    public class RtcRemoteStreamKey
    {
        public readonly string RoomId;
        public readonly string UserId;
        public readonly RtcStreamIndex RtcStreamIndex;

        public RtcRemoteStreamKey(IntPtr o)
        {
            RoomId = CLIB.ppf_RtcRemoteStreamKey_GetRoomId(o);
            UserId = CLIB.ppf_RtcRemoteStreamKey_GetUserId(o);
            RtcStreamIndex = CLIB.ppf_RtcRemoteStreamKey_GetStreamIndex(o);
        }
    }

    /// <summary>
    /// The error notification of the room.
    /// </summary>
    public class RtcRoomError
    {
        public readonly int Code;
        public readonly string RoomId;

        public RtcRoomError(IntPtr o)
        {
            Code = CLIB.ppf_RtcRoomError_GetCode(o);
            RoomId = CLIB.ppf_RtcRoomError_GetRoomId(o);
        }
    }

    /// <summary>
    /// You will receive this message periodically after you join room successfully.
    /// </summary>
    public class RtcRoomStats
    {
        /** @brief The time elapsed since you join room .*/
        public readonly int TotalDuration;

        /**@brief The user count in the room*/
        public readonly int UserCount;

        public readonly string RoomId;

        public RtcRoomStats(IntPtr o)
        {
            TotalDuration = CLIB.ppf_RtcRoomStats_GetTotalDuration(o);
            UserCount = CLIB.ppf_RtcRoomStats_GetUserCount(o);
            RoomId = CLIB.ppf_RtcRoomStats_GetRoomId(o);
        }
    }

    /// <summary>
    /// The warn info of the room.
    /// </summary>
    public class RtcRoomWarn
    {
        public readonly int Code;
        public readonly string RoomId;

        public RtcRoomWarn(IntPtr o)
        {
            Code = CLIB.ppf_RtcRoomWarn_GetCode(o);
            RoomId = CLIB.ppf_RtcRoomWarn_GetRoomId(o);
        }
    }

    /// <summary>
    /// You will receive this message after a remote user join the room.
    /// </summary>
    public class RtcUserJoinInfo
    {
        public readonly string UserId;

        /**@brief If the remote user set the UserExtra when he/she call \ref RtcService.JoinRoom with
         * the extra info.
         */
        public readonly string UserExtra;

        /** @brief The time used of the remote user to join the room.*/
        public readonly int Elapsed;

        public readonly string RoomId;

        public RtcUserJoinInfo(IntPtr o)
        {
            UserId = CLIB.ppf_RtcUserJoinInfo_GetUserId(o);
            UserExtra = CLIB.ppf_RtcUserJoinInfo_GetUserExtra(o);
            Elapsed = CLIB.ppf_RtcUserJoinInfo_GetElapsed(o);
            RoomId = CLIB.ppf_RtcUserJoinInfo_GetRoomId(o);
        }
    }

    /// <summary>
    /// You will receive this message when the remote user leaves the room.
    /// </summary>
    public class RtcUserLeaveInfo
    {
        public readonly string UserId;

        /**@brief The offline reason can be one of network error or he/she quit. */
        public readonly RtcUserLeaveReasonType OfflineReason;

        public readonly string RoomId;

        public RtcUserLeaveInfo(IntPtr o)
        {
            UserId = CLIB.ppf_RtcUserLeaveInfo_GetUserId(o);
            OfflineReason = CLIB.ppf_RtcUserLeaveInfo_GetOfflineReason(o);
            RoomId = CLIB.ppf_RtcUserLeaveInfo_GetRoomId(o);
        }
    }
}
#endif