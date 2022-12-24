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
    /// The User info structure.
    ///
    /// The basic info fields have valid value all the time,such as` DisplayName`,`ImageUrl`.
    /// The presence info is valid when you call Presence related requests.
    /// </summary>
    public class User
    {
        /** @brief User's display name. */
        public readonly string DisplayName;

        /** @brief User's image url which size is 300x300*/
        public readonly string ImageUrl;

        /** @brief User's openID. The same user has different ID in different app.*/
        public readonly string ID;

        /**   @brief User's presence status indicates whether the user is online.*/
        public readonly UserPresenceStatus PresenceStatus;

        /**   @brief The gender of the user.*/
        public readonly Gender Gender;

        /**   @brief The presence name ,it's configured in the developer center.*/
        public readonly string Presence;

        public readonly string PresenceDeeplinkMessage;
        public readonly string PresenceDestinationApiName;
        public readonly string PresenceLobbySessionId;
        public readonly string PresenceMatchSessionId;
        public readonly string PresenceExtra;

        /**   @brief User's small image url which size is 128x128.*/
        public readonly string SmallImageUrl;

        public readonly string InviteToken;

        /** @brief User's register region.It is a country code.*/
        public readonly string StoreRegion;

        public User(IntPtr obj)
        {
            DisplayName = CLIB.ppf_User_GetDisplayName(obj);
            ImageUrl = CLIB.ppf_User_GetImageUrl(obj);
            ID = CLIB.ppf_User_GetID(obj);
            InviteToken = CLIB.ppf_User_GetInviteToken(obj);
            PresenceStatus = CLIB.ppf_User_GetPresenceStatus(obj);
            Gender = CLIB.ppf_User_GetGender(obj);
            Presence = CLIB.ppf_User_GetPresence(obj);
            PresenceDeeplinkMessage = CLIB.ppf_User_GetPresenceDeeplinkMessage(obj);
            PresenceDestinationApiName = CLIB.ppf_User_GetPresenceDestinationApiName(obj);
            PresenceLobbySessionId = CLIB.ppf_User_GetPresenceLobbySessionId(obj);
            PresenceMatchSessionId = CLIB.ppf_User_GetPresenceMatchSessionId(obj);
            PresenceExtra = CLIB.ppf_User_GetPresenceExtra(obj);
            SmallImageUrl = CLIB.ppf_User_GetSmallImageUrl(obj);
            InviteToken = CLIB.ppf_User_GetInviteToken(obj);
            StoreRegion = CLIB.ppf_User_GetStoreRegion(obj);
        }
    }


    public class UserList : MessageArray<User>
    {
        public UserList(IntPtr a)
        {
            var count = (int) CLIB.ppf_UserArray_GetSize(a);
            this.Capacity = count;
            for (int i = 0; i < count; i++)
            {
                this.Add(new User(CLIB.ppf_UserArray_GetElement(a, (UIntPtr) i)));
            }

            NextPageParam = CLIB.ppf_UserArray_GetNextPageParam(a);
        }
    }
    /**
     * \ingroup Models
     */
    public class LaunchFriendResult
    {
        /**@brief whether it's canceled by user.*/
        public readonly bool DidCancel;
        /**@brief whether it's sent successfully.*/
        public readonly bool DidSendRequest;

        public LaunchFriendResult(IntPtr obj)
        {
            DidCancel = CLIB.ppf_LaunchFriendRequestFlowResult_GetDidCancel(obj);
            DidSendRequest = CLIB.ppf_LaunchFriendRequestFlowResult_GetDidSendRequest(obj);
        }
    }


    public class UserRoom
    {
        public readonly User User;
        public readonly Room Room;

        public UserRoom(IntPtr o)
        {
            User = new User(CLIB.ppf_UserAndRoom_GetUser(o));
            var ptr = CLIB.ppf_UserAndRoom_GetRoom(o);
            if (ptr != IntPtr.Zero)
            {
                Room = new Room(ptr);
            }
        }
    }


    public class UserRoomList : MessageArray<UserRoom>
    {
        public UserRoomList(IntPtr a)
        {
            var count = (int) CLIB.ppf_UserAndRoomArray_GetSize(a);
            this.Capacity = count;
            for (int i = 0; i < count; i++)
            {
                this.Add(new UserRoom(CLIB.ppf_UserAndRoomArray_GetElement(a, (UIntPtr) i)));
            }

            NextPageParam = CLIB.ppf_UserAndRoomArray_GetNextPageParam(a);
        }
    }

    public static class Permissions
    {
        public const string UserInfo = "user_info";
        public const string FriendRelation = "friend_relation";
        public const string SportsUserInfo = "sports_userinfo";
        public const string SportsSummaryData = "sports_summarydata";
    }

    public class PermissionResult
    {
        /** @brief The authorized permissions.*/
        public readonly string[] AuthorizedPermissions;
        /** @brief The access token .It has value only after you call RequestPermissions.*/
        public readonly string AccessToken;
        /** @brief The current user's Id.*/
        public readonly string UserID;

        public PermissionResult(IntPtr o)
        {
            {
                int sz = (int) CLIB.ppf_PermissionResult_GetAuthorizedPermissionsSize(o);
                AuthorizedPermissions = new string[sz];
                for (int i = 0; i < sz; i++)
                {
                    AuthorizedPermissions[i] = CLIB.ppf_PermissionResult_GetAuthorizedPermissions(o, (UIntPtr) i);
                }
            }

            AccessToken = CLIB.ppf_PermissionResult_GetAccessToken(o);
            UserID = CLIB.ppf_PermissionResult_GetUserID(o);
        }
    }
}
#endif