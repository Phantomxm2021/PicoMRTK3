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
using Pico.Platform.Models;
using UnityEngine;

namespace Pico.Platform
{
    public static class NotificationService
    {
        public static Task<RoomInviteNotificationList> GetRoomInviteNotifications(int pageIdx, int pageSize)
        {
            if (CoreService.IsInitialized())
            {
                return new Task<RoomInviteNotificationList>(CLIB.ppf_Notification_GetRoomInvites(pageIdx, pageSize));
            }

            Debug.LogError(CoreService.UninitializedError);
            return null;
        }

        public static Task MarkAsRead(UInt64 notificationID)
        {
            if (CoreService.IsInitialized())
            {
                return new Task(CLIB.ppf_Notification_MarkAsRead(notificationID));
            }

            Debug.LogError(CoreService.UninitializedError);
            return null;
        }
    }
}
#endif