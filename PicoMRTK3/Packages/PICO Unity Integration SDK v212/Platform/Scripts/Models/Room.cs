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
using System.Collections.Generic;

namespace Pico.Platform.Models
{
    public class Room
    {
        public readonly Dictionary<string, string> DataStore;
        public readonly string Description;
        public readonly UInt64 RoomId;
        public readonly bool IsMembershipLocked;
        public readonly RoomJoinPolicy RoomJoinPolicy;
        public readonly RoomJoinability RoomJoinability;

        public readonly uint MaxUsers;

        // Check not null before using.
        public readonly User OwnerOptional;

        public readonly RoomType RoomType;

        // Check not null before using.
        public readonly UserList UsersOptional;

        public Room(IntPtr o)
        {
            DataStore = CLIB.DataStoreFromNative(CLIB.ppf_Room_GetDataStore(o));
            Description = CLIB.ppf_Room_GetDescription(o);
            RoomId = CLIB.ppf_Room_GetID(o);
            IsMembershipLocked = CLIB.ppf_Room_GetIsMembershipLocked(o);
            RoomJoinPolicy = (RoomJoinPolicy) CLIB.ppf_Room_GetJoinPolicy(o);
            RoomJoinability = (RoomJoinability) CLIB.ppf_Room_GetJoinability(o);
            MaxUsers = CLIB.ppf_Room_GetMaxUsers(o);
            RoomType = (RoomType) CLIB.ppf_Room_GetType(o);
            {
                var ptr = CLIB.ppf_Room_GetOwner(o);
                if (ptr == IntPtr.Zero)
                {
                    OwnerOptional = null;
                }
                else
                {
                    OwnerOptional = new User(ptr);
                }
            }

            {
                var ptr = CLIB.ppf_Room_GetUsers(o);
                if (ptr == IntPtr.Zero)
                {
                    UsersOptional = null;
                }
                else
                {
                    UsersOptional = new UserList(ptr);
                }
            }
        }
    }

    public class RoomList : MessageArray<Room>
    {
        public int CurIndex;
        public int PageSize;

        public RoomList(IntPtr a)
        {
            CurIndex = CLIB.ppf_RoomArray_GetPageIndex(a);
            PageSize = CLIB.ppf_RoomArray_GetPageSize(a);
            NextPageParam = CLIB.ppf_RoomArray_HasNextPage(a) ? "true" : string.Empty;
            int count = (int) CLIB.ppf_RoomArray_GetSize(a);
            this.Capacity = count;
            for (uint i = 0; i < count; i++)
            {
                this.Add(new Room(CLIB.ppf_RoomArray_GetElement(a, (UIntPtr)i)));
            }
        }
    }
}
#endif