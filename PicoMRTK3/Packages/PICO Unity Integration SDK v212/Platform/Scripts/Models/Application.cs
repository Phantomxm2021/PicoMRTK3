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
    public class LaunchDetails
    {
        /** @brief DeeplinkMessage. When you call  \ref ApplicationService.LaunchApp */
        public readonly string DeeplinkMessage;

        /** @brief Destination API Name is configured in the developer center.*/
        public readonly string DestinationApiName;

        /** @brief Indicates the the source where the app is launched. */
        public readonly string LaunchSource;

        /** @brief The lobby means your comrade.*/
        public readonly string LobbySessionID;

        /** @brief Match session means a competition.*/
        public readonly string MatchSessionID;

        /** @brief The customized info in the presence.*/
        public readonly string Extra;

        /** @brief Current roomId.*/
        public readonly UInt64 RoomID;

        public readonly string TrackingID;
        public readonly UserList Users;
        public readonly LaunchType LaunchType;

        public LaunchDetails(IntPtr o)
        {
            DeeplinkMessage = CLIB.ppf_LaunchDetails_GetDeeplinkMessage(o);
            DestinationApiName = CLIB.ppf_LaunchDetails_GetDestinationApiName(o);
            LaunchSource = CLIB.ppf_LaunchDetails_GetLaunchSource(o);
            LobbySessionID = CLIB.ppf_LaunchDetails_GetLobbySessionID(o);
            MatchSessionID = CLIB.ppf_LaunchDetails_GetMatchSessionID(o);
            Extra = CLIB.ppf_LaunchDetails_GetExtra(o);
            RoomID = CLIB.ppf_LaunchDetails_GetRoomID(o);
            TrackingID = CLIB.ppf_LaunchDetails_GetTrackingID(o);
            Users = new UserList(CLIB.ppf_LaunchDetails_GetUsers(o));
            LaunchType = CLIB.ppf_LaunchDetails_GetLaunchType(o);
        }
    }
}
#endif