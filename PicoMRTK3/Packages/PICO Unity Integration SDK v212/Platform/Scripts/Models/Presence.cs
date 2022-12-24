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
    /// Destination is a position in the app.
    ///
    /// You can configure destinations in the developer center.
    /// </summary>
    public class Destination
    {
        /** @brief The api name is configured in the developer center.*/
        public readonly string ApiName;

        public readonly string DeeplinkMessage;

        /** @brief The display name,it may be shown to users.*/
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

    public class ApplicationInvite
    {
        /** @brief The destination where the recipient is invited to.*/
        public readonly Destination Destination;

        /** @brief The recipient user of the invitation.*/
        public readonly User Recipient;

        /** @brief The id of the invitation.*/
        public readonly UInt64 ID;

        /** @brief If the recipient clicked the inviting message,`IsActive` is true.*/
        public readonly bool IsActive;

        /** @brief The lobby session id.The users in a lobby has the same target.*/
        public readonly string LobbySessionId;

        /** @brief The match session id. It represents the competition id.*/
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

    public class SendInvitesResult
    {
        public readonly ApplicationInviteList Invites;

        public SendInvitesResult(IntPtr o)
        {
            Invites = new ApplicationInviteList(CLIB.ppf_SendInvitesResult_GetInvites(o));
        }
    }
}
#endif