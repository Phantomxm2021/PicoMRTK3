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
    public class Leaderboard
    {
        public readonly string ApiName;

        public readonly ulong ID;

        // May be null. Check before using.
        public readonly Destination DestinationOptional;

        public Leaderboard(IntPtr o)
        {
            ApiName = CLIB.ppf_Leaderboard_GetApiName(o);
            ID = CLIB.ppf_Leaderboard_GetID(o);
            var pointer = CLIB.ppf_Leaderboard_GetDestination(o);
            if (pointer == IntPtr.Zero)
                DestinationOptional = null;
            else
                DestinationOptional = new Destination(pointer);
        }
    }

    public class LeaderboardList : MessageArray<Leaderboard>
    {
        public LeaderboardList(IntPtr a)
        {
            NextPageParam = CLIB.ppf_LeaderboardArray_HasNextPage(a) ? "true" : string.Empty;
            var count = (int) CLIB.ppf_LeaderboardArray_GetSize(a);
            this.Capacity = count;
            for (var i = 0; i < count; i++)
            {
                Add(new Leaderboard(CLIB.ppf_LeaderboardArray_GetElement(a, (UIntPtr) i)));
            }
        }
    }

    public class SupplementaryMetric
    {
        public readonly UInt64 ID;
        public readonly long Metric;


        public SupplementaryMetric(IntPtr o)
        {
            ID = CLIB.ppf_SupplementaryMetric_GetID(o);
            Metric = CLIB.ppf_SupplementaryMetric_GetMetric(o);
        }
    }

    public class LeaderboardEntry
    {
        public readonly string DisplayScore;
        public readonly byte[] ExtraData;
        public readonly UInt64 ID;
        public readonly int Rank;

        public readonly long Score;

        // May be null. Check before using.
        public readonly SupplementaryMetric SupplementaryMetricOptional;
        public readonly DateTime Timestamp;
        public readonly User User;


        public LeaderboardEntry(IntPtr o)
        {
            DisplayScore = CLIB.ppf_LeaderboardEntry_GetDisplayScore(o);
            var extraDataPtr = CLIB.ppf_LeaderboardEntry_GetExtraData(o);
            var extraDataSize = CLIB.ppf_LeaderboardEntry_GetExtraDataLength(o);
            ExtraData = MarshalUtil.ByteArrayFromNative(extraDataPtr, extraDataSize);
            ID = CLIB.ppf_LeaderboardEntry_GetID(o);
            Rank = CLIB.ppf_LeaderboardEntry_GetRank(o);
            Score = CLIB.ppf_LeaderboardEntry_GetScore(o);
            Timestamp = Util.SecondsToDateTime((long) CLIB.ppf_LeaderboardEntry_GetTimestamp(o));
            User = new User(CLIB.ppf_LeaderboardEntry_GetUser(o));
            {
                var pointer = CLIB.ppf_LeaderboardEntry_GetSupplementaryMetric(o);
                if (pointer == IntPtr.Zero)
                {
                    SupplementaryMetricOptional = null;
                }
                else
                {
                    SupplementaryMetricOptional = new SupplementaryMetric(pointer);
                }
            }
        }
    }

    public class LeaderboardEntryList : MessageArray<LeaderboardEntry>
    {
        public readonly ulong TotalCount;

        public LeaderboardEntryList(IntPtr a)
        {
            NextPageParam = CLIB.ppf_LeaderboardEntryArray_HasNextPage(a) ? "true" : string.Empty;
            var count = (int) CLIB.ppf_LeaderboardEntryArray_GetSize(a);
            this.Capacity = count;
            for (uint i = 0; i < count; i++)
            {
                this.Add(new LeaderboardEntry(CLIB.ppf_LeaderboardEntryArray_GetElement(a, (UIntPtr) i)));
            }

            TotalCount = CLIB.ppf_LeaderboardEntryArray_GetTotalCount(a);
        }
    }
}
#endif