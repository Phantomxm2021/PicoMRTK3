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
    /// <summary>
    /// Destination is a location in the app.
    /// You can configure destinations for your app on the PICO Developer Platform.
    /// </summary>
    public class Destination
    {
        /** @brief The destination's API name. */
        public readonly string ApiName;

        /** @brief The destination's deeplink message. */
        public readonly string DeeplinkMessage;

        /** @brief The destination's display name.*/
        public readonly string DisplayName;

        public Destination(IntPtr o)
        {
            ApiName = CLIB.ppf_Destination_GetApiName(o);
            DeeplinkMessage = CLIB.ppf_Destination_GetDeeplinkMessage(o);
            DisplayName = CLIB.ppf_Destination_GetDisplayName(o);
        }
    }


    public class DestinationList : MessageArray<Destination>
    {
        public DestinationList(IntPtr a)
        {
            var count = (int) CLIB.ppf_DestinationArray_GetSize(a);
            this.Capacity = count;
            for (int i = 0; i < count; i++)
            {
                this.Add(new Destination(CLIB.ppf_DestinationArray_GetElement(a, (UIntPtr) i)));
            }

            NextPageParam = CLIB.ppf_DestinationArray_GetNextPageParam(a);
        }
    }

    /**
     * \ingroup Models
     */
    /// <summary>
    /// App's invitation info.
    /// </summary>
    public class ApplicationInvite
    {
        /** @brief The destination where the user is directed to after accepting the invitation. */
        public readonly Destination Destination;

        /** @brief Invited users. */
        public readonly User Recipient;

        /** @brief Invitation ID. */
        public readonly UInt64 ID;

        /** @brief If the user clicks the invitation message, this field will be `true`. */
        public readonly bool IsActive;

        /** @brief The lobby session ID that identifies a group or team. */
        public readonly string LobbySessionId;

        /** @brief The match session ID that identifies a competition. */
        public readonly string MatchSessionId;

        public ApplicationInvite(IntPtr o)
        {
            Destination = new Destination(CLIB.ppf_ApplicationInvite_GetDestination(o));
            Recipient = new User(CLIB.ppf_ApplicationInvite_GetRecipient(o));
            ID = CLIB.ppf_ApplicationInvite_GetID(o);
            IsActive = CLIB.ppf_ApplicationInvite_GetIsActive(o);
            LobbySessionId = CLIB.ppf_ApplicationInvite_GetLobbySessionId(o);
            MatchSessionId = CLIB.ppf_ApplicationInvite_GetMatchSessionId(o);
        }
    }


    public class ApplicationInviteList : MessageArray<ApplicationInvite>
    {
        public ApplicationInviteList(IntPtr a)
        {
            var count = (int) CLIB.ppf_ApplicationInviteArray_GetSize(a);
            this.Capacity = count;
            for (int i = 0; i < count; i++)
            {
                this.Add(new ApplicationInvite(CLIB.ppf_ApplicationInviteArray_GetElement(a, (UIntPtr) i)));
            }

            NextPageParam = CLIB.ppf_ApplicationInviteArray_GetNextPageParam(a);
        }
    }

    /**
     * \ingroup Models
     */
    /// <summary>
    /// The result returned after calling /ref SendInvites.
    /// </summary>
    public class SendInvitesResult
    {
        public readonly ApplicationInviteList Invites;

        public SendInvitesResult(IntPtr o)
        {
            Invites = new ApplicationInviteList(CLIB.ppf_SendInvitesResult_GetInvites(o));
        }
    }

    /// \ingroup Models
    /// <summary>
    /// When user click the invitation message, the app will be launched and you
    /// will receive a message with presence infos.
    /// </summary>
    public class PresenceJoinIntent
    {
        /** @brief The deeplink message of the destination.*/
        public readonly string DeeplinkMessage;

        /** @brief The destination api name of the destination.*/
        public readonly string DestinationApiName;

        /** @brief The lobby session id which is configured by the sender.*/
        public readonly string LobbySessionId;

        /** @brief The match session id which is configured by the sender.*/
        public readonly string MatchSessionId;

        /** @brief The extra info of the presence.*/
        public readonly string Extra;

        public PresenceJoinIntent(IntPtr o)
        {
            DeeplinkMessage = CLIB.ppf_PresenceJoinIntent_GetDeeplinkMessage(o);
            DestinationApiName = CLIB.ppf_PresenceJoinIntent_GetDestinationApiName(o);
            LobbySessionId = CLIB.ppf_PresenceJoinIntent_GetLobbySessionId(o);
            MatchSessionId = CLIB.ppf_PresenceJoinIntent_GetMatchSessionId(o);
            Extra = CLIB.ppf_PresenceJoinIntent_GetExtra(o);
        }
    }
}