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
using UnityEngine;

namespace Pico.Platform.Models
{
    public class RoomInviteNotification
    {
        public readonly UInt64 ID;
        public readonly UInt64 RoomID;
        public readonly string SenderID;
        public readonly DateTime SentTime;


        public RoomInviteNotification(IntPtr o)
        {
            ID = CLIB.ppf_RoomInviteNotification_GetID(o);
            RoomID = CLIB.ppf_RoomInviteNotification_GetRoomID(o);
            SenderID = CLIB.ppf_RoomInviteNotification_GetSenderID(o);
            SentTime = new DateTime();
            try
            {
                SentTime = Util.SecondsToDateTime((long) CLIB.ppf_RoomInviteNotification_GetSentTime(o));
            }
            catch (UnityException ex)
            {
                Debug.LogWarning("RoomInviteNotification get SentTime fail");
                throw;
            }
        }
    }

    public class RoomInviteNotificationList : MessageArray<RoomInviteNotification>
    {
        public RoomInviteNotificationList(IntPtr a)
        {
            NextPageParam = CLIB.ppf_RoomInviteNotificationArray_HasNextPage(a) ? "true" : string.Empty;
            int count = (int) CLIB.ppf_RoomInviteNotificationArray_GetSize(a);
            this.Capacity = count;
            for (uint i = 0; i < count; i++)
            {
                this.Add(new RoomInviteNotification(CLIB.ppf_RoomInviteNotificationArray_GetElement(a, (UIntPtr)i)));
            }
        }
    }
}
#endif