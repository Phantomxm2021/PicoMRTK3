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

namespace Pico.Platform.Models
{
    /**
     * \ingroup Models
     */
    /// <summary>App launch details.</summary>
    public class LaunchDetails
    {
        /** @brief How the app was launched:
         * `Normal`: launched by clicking the app's icon
         * `RoomInvite`: launched by clicking the room invitation message card
         * `Deeplink`: launched by clicking the presence invitation message card or calling \ref ApplicationService.LaunchApp
         * `ChallengeInvite`: launched by clicking the challenge invitation message card
         */
        public readonly LaunchType LaunchType;

        /** @brief Deeplink message. You can pass a deeplink when you call  \ref ApplicationService.LaunchApp, and the other app will receive the deeplink.
         * This field will have a value only when `LaunchType` is `LaunchApp`.
         */
        public readonly string DeeplinkMessage;

        /** @brief Destination API name configured on the PICO Developer Platform.
         * For a presence invitation, the inviter's presence data will contain this field which will be passed when the invitee clicks on the message card.
         */
        public readonly string DestinationApiName;

        /** @brief The lobby session ID that identifies a group or team.
         * For a presence invitation, the inviter's presence data will contain this field which will be passed when the invitee clicks on the message card.
         */
        public readonly string LobbySessionID;

        /** @brief The match session ID that identifies a competition.
         * For a presence invitation, the inviter's presence data will contain this field which will be passed when the invitee clicks on the message card.
         */
        public readonly string MatchSessionID;

        /** @brief The customized extra presence info.
         * For a presence invitation, the inviter's presence data will contain this field which will be passed when the invitee clicks on the message card.
         * You can use this field to add self-defined presence data. The data size cannot exceed 2MB.
         */
        public readonly string Extra;

        /** @brief Room ID.
         * For a room invitation, after calling \ref RoomService.InviteUser, this field will be passed when the invitee clicks on the message card.
         */
        public readonly UInt64 RoomID;
        
        /** @brief Challenge ID.
         * For a challenge invitation, after calling \ref ChallengeService.Invite, this field will be passed when the invitee clicks on the message card.
         */
        public readonly UInt64 ChallengeID;

        /** @brief Tracking ID. */
        public readonly string TrackingID;
        
        public LaunchDetails(IntPtr o)
        {
            DeeplinkMessage = CLIB.ppf_LaunchDetails_GetDeeplinkMessage(o);
            DestinationApiName = CLIB.ppf_LaunchDetails_GetDestinationApiName(o);
            LobbySessionID = CLIB.ppf_LaunchDetails_GetLobbySessionID(o);
            MatchSessionID = CLIB.ppf_LaunchDetails_GetMatchSessionID(o);
            Extra = CLIB.ppf_LaunchDetails_GetExtra(o);
            RoomID = CLIB.ppf_LaunchDetails_GetRoomID(o);
            ChallengeID = CLIB.ppf_LaunchDetails_GetChallengeID(o);
            TrackingID = CLIB.ppf_LaunchDetails_GetTrackingID(o);
            LaunchType = CLIB.ppf_LaunchDetails_GetLaunchType(o);
        }
    }

    /// <summary>
    /// App's version info.
    /// </summary>
    public class ApplicationVersion
    {
        /** @brief The current version code of the installed app. */
        public readonly long CurrentCode;

        /** @brief The current version name of the installed app. */
        public readonly string CurrentName;

        /** @brief The latest version code of the installed app in the PICO Store. */
        public readonly long LatestCode;

        /** @brief The latest version name of the installed app in the PICO Store. */
        public readonly string LatestName;

        public ApplicationVersion(IntPtr o)
        {
            CurrentCode = CLIB.ppf_ApplicationVersion_GetCurrentCode(o);
            CurrentName = CLIB.ppf_ApplicationVersion_GetCurrentName(o);
            LatestCode = CLIB.ppf_ApplicationVersion_GetLatestCode(o);
            LatestName = CLIB.ppf_ApplicationVersion_GetLatestName(o);
        }
    }
}