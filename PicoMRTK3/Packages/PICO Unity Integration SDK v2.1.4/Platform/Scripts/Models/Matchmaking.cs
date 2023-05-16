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
    /// <summary>Matchmaking admin snapshot. You will receive this after calling \ref MatchmakingService.GetAdminSnapshot.</summary>
    public class MatchmakingAdminSnapshot
    {
        /** @brief List of matchmaking candidates */
        public readonly MatchmakingAdminSnapshotCandidateList CandidateList;
        /** @brief The current matching threshold. */
        public readonly double MyCurrentThreshold;

        public MatchmakingAdminSnapshot(IntPtr o)
        {
            CandidateList = new MatchmakingAdminSnapshotCandidateList(CLIB.ppf_MatchmakingAdminSnapshot_GetCandidates(o));
            MyCurrentThreshold = CLIB.ppf_MatchmakingAdminSnapshot_GetMyCurrentThreshold(o);
        }
    }
    /**
     * \ingroup Models
     */
    /// <summary>Matchmaking candidate.</summary>
    public class MatchmakingAdminSnapshotCandidate
    {
        /** @brief Whether me and the other user can be matched. */
        public readonly bool CanMatch;
        /** @brief My matching threshold. */
        public readonly double MyTotalScore;
        /** @brief The other user's matching threshold. */
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
    /**
     * \ingroup Models
     */
    /// <summary>Matchmaking browse result. You will receive the result after calling \ref MatchmakingService.Browse2. </summary>
    public class MatchmakingBrowseResult
    {
        /** @brief Matchmaking enqueue result. */
        public readonly MatchmakingEnqueueResult EnqueueResult;
        /** @brief The list of matchmaking rooms. */
        public readonly MatchmakingRoomList MatchmakingRooms;

        public MatchmakingBrowseResult(IntPtr o)
        {
            EnqueueResult = new MatchmakingEnqueueResult(CLIB.ppf_MatchmakingBrowseResult_GetEnqueueResult(o));
            MatchmakingRooms = new MatchmakingRoomList(CLIB.ppf_MatchmakingBrowseResult_GetRooms(o));
        }
    }
    /**
     * \ingroup Models
     */
    /// <summary>Matchmaking enqueue result.</summary>
    public class MatchmakingEnqueueResult
    {
        /** @brief Matchmaking snapshot options. Used for debugging only. */
        public readonly MatchmakingAdminSnapshot AdminSnapshotOptional;
        /** @brief The average waiting time. */
        public readonly uint AverageWait; 
        /** @brief The number of matches made in the last hour. */
        public readonly uint MatchesInLastHourCount;
        /** @brief The expected longest waiting time. */
        public readonly uint MaxExpectedWait;
        /** @brief Matchmaking pool name. */
        public readonly string Pool; 
        /** @brief Match rate. */
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
    /**
     * \ingroup Models
     */
    /// <summary>Matchmaking enqueue result and room info. You will receive this after calling \ref MatchmakingService.CreateAndEnqueueRoom2.</summary>
    public class MatchmakingEnqueueResultAndRoom
    {
        /** @brief Matchmaking enqueue result. */
        public readonly MatchmakingEnqueueResult MatchmakingEnqueueResult;
        /** @brief Matchmaking room info. */
        public readonly Room Room;

        public MatchmakingEnqueueResultAndRoom(IntPtr o)
        {
            MatchmakingEnqueueResult = new MatchmakingEnqueueResult(CLIB.ppf_MatchmakingEnqueueResultAndRoom_GetMatchmakingEnqueueResult(o));
            Room = new Room(CLIB.ppf_MatchmakingEnqueueResultAndRoom_GetRoom(o));
        }
    }
    /**
     * \ingroup Models
     */
    /// <summary>Matchmaking room.</summary>
    public class MatchmakingRoom
    {
        /** @brief Room info. */
        public readonly Models.Room Room;
        /** @brief Currently, always `0`. */
        public readonly uint PingTime;
        /** @brief Currently, always `false`. */
        public readonly bool HasPingTime;


        public MatchmakingRoom(IntPtr o)
        {
            this.PingTime = CLIB.ppf_MatchmakingRoom_GetPingTime(o);
            this.Room = new Models.Room(CLIB.ppf_MatchmakingRoom_GetRoom(o));
            this.HasPingTime = CLIB.ppf_MatchmakingRoom_HasPingTime(o);
        }
    }
    /**
     * \ingroup Models
     */
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
    /**
     * \ingroup Models
     */
    /// <summary>Matchmaking statistics. Will receive this after calling \ref MatchmakingService.GetStats.</summary>
    public class MatchmakingStats
    {
        /** @brief The current user's number of draws. */
        public readonly uint DrawCount;
        /** @brief The current user's number of losses. */
        public readonly uint LossCount;
        /** @brief The current user's skill level for the current matchmaking pool. */
        public readonly uint SkillLevel;
        /** @brief The average of all skill levels for the current matchmaking pool. */
        public readonly double SkillMean;
        /** @brief The standard deviation of all skill levels for the current matchmaking pool */
        public readonly double SkillStandardDeviation;
        /** @brief The current user's number of wins. */
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