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
    public class MatchmakingAdminSnapshot
    {
        public readonly MatchmakingAdminSnapshotCandidateList CandidateList;
        public readonly double MyCurrentThreshold;

        public MatchmakingAdminSnapshot(IntPtr o)
        {
            CandidateList = new MatchmakingAdminSnapshotCandidateList(CLIB.ppf_MatchmakingAdminSnapshot_GetCandidates(o));
            MyCurrentThreshold = CLIB.ppf_MatchmakingAdminSnapshot_GetMyCurrentThreshold(o);
        }
    }

    public class MatchmakingAdminSnapshotCandidate
    {
        public readonly bool CanMatch;
        public readonly double MyTotalScore;
        public readonly double TheirCurrentThreshold;

        public MatchmakingAdminSnapshotCandidate(IntPtr o)
        {
            CanMatch = CLIB.ppf_MatchmakingAdminSnapshotCandidate_GetCanMatch(o);
            MyTotalScore = CLIB.ppf_MatchmakingAdminSnapshotCandidate_GetMyTotalScore(o);
            TheirCurrentThreshold = CLIB.ppf_MatchmakingAdminSnapshotCandidate_GetTheirCurrentThreshold(o);
        }
    }

    public class MatchmakingAdminSnapshotCandidateList : MessageArray<MatchmakingAdminSnapshotCandidate>
    {
        public MatchmakingAdminSnapshotCandidateList(IntPtr a)
        {
            var count = (int) CLIB.ppf_MatchmakingAdminSnapshotCandidateArray_GetSize(a);
            this.Capacity = count;
            for (int i = 0; i < count; i++)
            {
                this.Add(new MatchmakingAdminSnapshotCandidate(CLIB.ppf_MatchmakingAdminSnapshotCandidateArray_GetElement(a, (UIntPtr) i)));
            }
        }
    }

    public class MatchmakingBrowseResult
    {
        public readonly MatchmakingEnqueueResult EnqueueResult;
        public readonly MatchmakingRoomList MatchmakingRooms;

        public MatchmakingBrowseResult(IntPtr o)
        {
            EnqueueResult = new MatchmakingEnqueueResult(CLIB.ppf_MatchmakingBrowseResult_GetEnqueueResult(o));
            MatchmakingRooms = new MatchmakingRoomList(CLIB.ppf_MatchmakingBrowseResult_GetRooms(o));
        }
    }

    public class MatchmakingEnqueueResult
    {
        // May be null. Check before using.
        public readonly MatchmakingAdminSnapshot AdminSnapshotOptional;
        public readonly uint AverageWait;
        public readonly uint MatchesInLastHourCount;
        public readonly uint MaxExpectedWait;
        public readonly string Pool;
        public readonly uint RecentMatchPercentage;


        public MatchmakingEnqueueResult(IntPtr o)
        {
            {
                var pointer = CLIB.ppf_MatchmakingEnqueueResult_GetAdminSnapshot(o);
                if (pointer == IntPtr.Zero)
                {
                    AdminSnapshotOptional = null;
                }
                else
                {
                    AdminSnapshotOptional = new MatchmakingAdminSnapshot(pointer);
                }
            }

            AverageWait = CLIB.ppf_MatchmakingEnqueueResult_GetAverageWait(o);
            MatchesInLastHourCount = CLIB.ppf_MatchmakingEnqueueResult_GetMatchesInLastHourCount(o);
            MaxExpectedWait = CLIB.ppf_MatchmakingEnqueueResult_GetMaxExpectedWait(o);
            Pool = CLIB.ppf_MatchmakingEnqueueResult_GetPool(o);
            RecentMatchPercentage = CLIB.ppf_MatchmakingEnqueueResult_GetRecentMatchPercentage(o);
        }
    }

    public class MatchmakingEnqueueResultAndRoom
    {
        public readonly MatchmakingEnqueueResult MatchmakingEnqueueResult;
        public readonly Room Room;

        public MatchmakingEnqueueResultAndRoom(IntPtr o)
        {
            MatchmakingEnqueueResult = new MatchmakingEnqueueResult(CLIB.ppf_MatchmakingEnqueueResultAndRoom_GetMatchmakingEnqueueResult(o));
            Room = new Room(CLIB.ppf_MatchmakingEnqueueResultAndRoom_GetRoom(o));
        }
    }

    public class MatchmakingRoom
    {
        public readonly Models.Room Room;
        public readonly uint PingTime;
        public readonly bool HasPingTime;


        public MatchmakingRoom(IntPtr o)
        {
            this.PingTime = CLIB.ppf_MatchmakingRoom_GetPingTime(o);
            this.Room = new Models.Room(CLIB.ppf_MatchmakingRoom_GetRoom(o));
            this.HasPingTime = CLIB.ppf_MatchmakingRoom_HasPingTime(o);
        }
    }

    public class MatchmakingRoomList : MessageArray<MatchmakingRoom>
    {
        public MatchmakingRoomList(IntPtr a)
        {
            int count = (int) CLIB.ppf_MatchmakingRoomArray_GetSize(a);
            this.Capacity = count;
            for (uint i = 0; i < count; i++)
            {
                this.Add(new MatchmakingRoom(CLIB.ppf_MatchmakingRoomArray_GetElement(a, (UIntPtr) i)));
            }
        }
    }

    public class MatchmakingStats
    {
        public readonly uint DrawCount;
        public readonly uint LossCount;
        public readonly uint SkillLevel;
        public readonly double SkillMean;
        public readonly double SkillStandardDeviation;
        public readonly uint WinCount;


        public MatchmakingStats(IntPtr o)
        {
            DrawCount = CLIB.ppf_MatchmakingStats_GetDrawCount(o);
            LossCount = CLIB.ppf_MatchmakingStats_GetLossCount(o);
            SkillLevel = CLIB.ppf_MatchmakingStats_GetSkillLevel(o);
            SkillMean = CLIB.ppf_MatchmakingStats_GetSkillMean(o);
            SkillStandardDeviation = CLIB.ppf_MatchmakingStats_GetSkillStandardDeviation(o);
            WinCount = CLIB.ppf_MatchmakingStats_GetWinCount(o);
        }
    }
}
#endif