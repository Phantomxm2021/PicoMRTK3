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
using System.Collections.Generic;

namespace Pico.Platform.Models
{
    /**
     * \ingroup Models
     */
    /// <summary>
    /// The User info structure.
    /// The basic info fields, such as `DisplayName` and `ImageUrl`, are always valid. 
    /// The presence info is valid only when you call presence-related APIs.
    /// See \ref UserService.GetLoggedInUser 
    /// </summary>
    public class User
    {
        /** @brief User's display name.*/
        public readonly string DisplayName;

        /** @brief The URL of user's profile photo. The image size is 300x300.*/
        public readonly string ImageUrl;

        /**  @brief The URL of the user's small profile photo. The image size is 128x128. */
        public readonly string SmallImageUrl;

        /** @brief User's openID. The same user has different openIDs in different apps.*/
        public readonly string ID;

        /** @brief User's presence status which indicates whether the user is online.*/
        public readonly UserPresenceStatus PresenceStatus;

        /** @brief User's gender.*/
        public readonly Gender Gender;

        /** @brief User's presence info which is configured on the PICO Developer Platform.*/
        public readonly string Presence;

        /** @brief The deeplink message. */
        public readonly string PresenceDeeplinkMessage;

        /** @brief The destination's API name. */
        public readonly string PresenceDestinationApiName;

        /** @brief The lobby session ID which identifies a group or team. */
        public readonly string PresenceLobbySessionId;

        /** @brief The match session ID which identifies a competition. */
        public readonly string PresenceMatchSessionId;

        /** @brief User's extra presence data. */
        public readonly string PresenceExtra;

        /** @brief The invite token. */
        public readonly string InviteToken;

        /** @brief User's registration country/region. It is a country/region code. */
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
    /// <summary>
    /// Indicates whether the friend request is canceled or successfully sent.
    /// </summary>
    public class LaunchFriendResult
    {
        /**@brief Whether the request is canceled by the user.*/
        public readonly bool DidCancel;

        /**@brief Whether the request is successfully sent. */
        public readonly bool DidSendRequest;

        public LaunchFriendResult(IntPtr obj)
        {
            DidCancel = CLIB.ppf_LaunchFriendRequestFlowResult_GetDidCancel(obj);
            DidSendRequest = CLIB.ppf_LaunchFriendRequestFlowResult_GetDidSendRequest(obj);
        }
    }
    

    /**
     * \ingroup Models
     */
    /// <summary>
    /// The info returned after calling \ref UserService.GetFriendsAndRooms.
    /// </summary>
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

    /**
     * \ingroup Models
     */
    public static class Permissions
    {
        public const string UserInfo = "user_info";
        public const string FriendRelation = "friend_relation";
        public const string SportsUserInfo = "sports_userinfo";
        public const string SportsSummaryData = "sports_summarydata";
    }

    /**
     * \ingroup Models
     */
    /// <summary>
    /// The result returned after calling \ref UserService.RequestUserPermissions or \ref UserService.GetAuthorizedPermissions.
    /// </summary>
    public class PermissionResult
    {
        /** @brief The authorized permissions.*/
        public readonly string[] AuthorizedPermissions;

        /** @brief The access token. It has a value only after you call \ref UserService.RequestUserPermissions.*/
        public readonly string AccessToken;

        /** @brief The current user's ID.*/
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
    
    /**
     * \ingroup Models
     */
    /// <summary>
    /// The result returned after calling \ref UserService.GetUserRelations.
    /// </summary>
    public class UserRelationResult: Dictionary<string, UserRelationType>
    {
        public UserRelationResult(IntPtr o)
        {
            {
                int sz = (int) CLIB.ppf_UserRelationResult_GetRelationsSize(o);
                for (int i = 0; i < sz; i++)
                {
                    string userId = CLIB.ppf_UserRelationResult_GetRelationsKey(o, i);
                    UserRelationType relation = CLIB.ppf_UserRelationResult_GetRelationsValue(o, i);
                    Add(userId, relation);
                }
            }
        }
    }
}