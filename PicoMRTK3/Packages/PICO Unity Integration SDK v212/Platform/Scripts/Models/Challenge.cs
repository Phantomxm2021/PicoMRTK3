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
    public class ChallengeOptions
    {
        public ChallengeOptions()
        {
            Handle = CLIB.ppf_ChallengeOptions_Create();
        }

        public void SetEndDate(DateTime value)
        {
            CLIB.ppf_ChallengeOptions_SetEndDate(Handle, Convert.ToUInt64(Util.DateTimeToSeconds(value)));
        }

        public void SetIncludeActiveChallenges(bool value)
        {
            CLIB.ppf_ChallengeOptions_SetIncludeActiveChallenges(Handle, value);
        }

        public void SetIncludeFutureChallenges(bool value)
        {
            CLIB.ppf_ChallengeOptions_SetIncludeFutureChallenges(Handle, value);
        }

        public void SetIncludePastChallenges(bool value)
        {
            CLIB.ppf_ChallengeOptions_SetIncludePastChallenges(Handle, value);
        }

        /// Optional: Only find challenges belonging to this leaderboard.
        public void SetLeaderboardName(string value)
        {
            CLIB.ppf_ChallengeOptions_SetLeaderboardName(Handle, value);
        }

        public void SetStartDate(DateTime value)
        {
            CLIB.ppf_ChallengeOptions_SetStartDate(Handle, Convert.ToUInt64(Util.DateTimeToSeconds(value)));
        }

        public void SetTitle(string value)
        {
            CLIB.ppf_ChallengeOptions_SetTitle(Handle, value);
        }

        public void SetViewerFilter(ChallengeViewerFilter value)
        {
            CLIB.ppf_ChallengeOptions_SetViewerFilter(Handle, value);
        }

        public void SetVisibility(ChallengeVisibility value)
        {
            CLIB.ppf_ChallengeOptions_SetVisibility(Handle, value);
        }


        /// For passing to native C
        public static explicit operator IntPtr(ChallengeOptions options)
        {
            return options != null ? options.Handle : IntPtr.Zero;
        }

        ~ChallengeOptions()
        {
            CLIB.ppf_ChallengeOptions_Destroy(Handle);
        }

        IntPtr Handle;

        public IntPtr GetHandle()
        {
            return Handle;
        }
    }

    public class Challenge
    {
        public readonly ChallengeCreationType CreationType;
        public readonly UInt64 ID;
        public readonly DateTime StartDate;
        public readonly DateTime EndDate;
        public readonly UserList ParticipantsOptional;
        public readonly UserList InvitedUsersOptional;
        public readonly Leaderboard Leaderboard;
        public readonly string Title;
        public readonly ChallengeVisibility Visibility;


        public Challenge(IntPtr o)
        {
            CreationType = CLIB.ppf_Challenge_GetCreationType(o);

            try
            {
                EndDate = Util.SecondsToDateTime((long) CLIB.ppf_Challenge_GetEndDate(o));
            }
            catch (Exception e)
            {
                Debug.LogWarning($"Challenge Set EndDate: ppf_Challenge_GetEndDate(o) = {CLIB.ppf_Challenge_GetEndDate(o)}, Exception: {e}");
            }

            ID = CLIB.ppf_Challenge_GetID(o);
            {
                var pointer = CLIB.ppf_Challenge_GetInvitedUsers(o);
                if (pointer == IntPtr.Zero)
                {
                    InvitedUsersOptional = null;
                }
                else
                {
                    InvitedUsersOptional = new UserList(pointer);
                }
            }
            Leaderboard = new Leaderboard(CLIB.ppf_Challenge_GetLeaderboard(o));
            {
                var pointer = CLIB.ppf_Challenge_GetParticipants(o);
                if (pointer == IntPtr.Zero)
                {
                    ParticipantsOptional = null;
                }
                else
                {
                    ParticipantsOptional = new UserList(pointer);
                }
            }
            try
            {
                StartDate = Util.SecondsToDateTime((long) CLIB.ppf_Challenge_GetStartDate(o));
            }
            catch (Exception e)
            {
                Debug.LogWarning($"Challenge Set StartDate: ppf_Challenge_GetStartDate(o) = {CLIB.ppf_Challenge_GetStartDate(o)}, Exception: {e}");
            }

            Title = CLIB.ppf_Challenge_GetTitle(o);
            Visibility = CLIB.ppf_Challenge_GetVisibility(o);
        }
    }

    public class ChallengeList : MessageArray<Challenge>
    {
        public ChallengeList(IntPtr a)
        {
            TotalCount = CLIB.ppf_ChallengeArray_GetTotalCount(a);
            NextPageParam = CLIB.ppf_ChallengeArray_HasNextPage(a) ? "true" : string.Empty;
            int count = (int) CLIB.ppf_ChallengeArray_GetSize(a);
            this.Capacity = count;
            for (uint i = 0; i < count; i++)
            {
                this.Add(new Challenge(CLIB.ppf_ChallengeArray_GetElement(a, (UIntPtr) i)));
            }
        }

        public readonly ulong TotalCount;
    }

    public class ChallengeEntry
    {
        public readonly string DisplayScore;
        public readonly byte[] ExtraData;
        public readonly UInt64 ID;
        public readonly int Rank;
        public readonly long Score;
        public readonly DateTime Timestamp;
        public readonly User User;


        public ChallengeEntry(IntPtr o)
        {
            DisplayScore = CLIB.ppf_ChallengeEntry_GetDisplayScore(o);
            var extraDataPtr = CLIB.ppf_ChallengeEntry_GetExtraData(o);
            var extraDataSize = CLIB.ppf_ChallengeEntry_GetExtraDataLength(o);
            ExtraData = MarshalUtil.ByteArrayFromNative(extraDataPtr, extraDataSize);
            ID = CLIB.ppf_ChallengeEntry_GetID(o);
            Rank = CLIB.ppf_ChallengeEntry_GetRank(o);
            Score = CLIB.ppf_ChallengeEntry_GetScore(o);

            try
            {
                Timestamp = Util.SecondsToDateTime((long) CLIB.ppf_ChallengeEntry_GetTimestamp(o));
            }
            catch (Exception e)
            {
                Debug.LogWarning($"ChallengeEntry Set Timestamp: ppf_ChallengeEntry_GetTimestamp(o) = {CLIB.ppf_ChallengeEntry_GetTimestamp(o)}, Exception: {e}");
            }

            User = new User(CLIB.ppf_ChallengeEntry_GetUser(o));
        }
    }

    public class ChallengeEntryList : MessageArray<ChallengeEntry>
    {
        public ChallengeEntryList(IntPtr a)
        {
            TotalCount = CLIB.ppf_ChallengeEntryArray_GetTotalCount(a);
            NextPageParam = CLIB.ppf_ChallengeEntryArray_HasNextPage(a) ? "true" : string.Empty;
            int count = (int) CLIB.ppf_ChallengeEntryArray_GetSize(a);
            this.Capacity = count;
            for (uint i = 0; i < count; i++)
            {
                this.Add(new ChallengeEntry(CLIB.ppf_ChallengeEntryArray_GetElement(a, (UIntPtr) i)));
            }
        }

        public readonly ulong TotalCount;
    }
}
#endif