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
using System.Runtime.InteropServices;

namespace Pico.Platform.Models
{
    public class AchievementUpdate
    {
        public readonly bool JustUnlocked;
        public readonly string Name;

        public AchievementUpdate(IntPtr o)
        {
            JustUnlocked = CLIB.ppf_AchievementUpdate_GetJustUnlocked(o);
            Name = CLIB.ppf_AchievementUpdate_GetName(o);
        }
    }

    public class AchievementDefinition
    {
        public readonly AchievementType Type;
        public readonly string Name;
        public readonly uint BitfieldLength;
        public readonly long Target;
        public readonly string Description;
        public readonly string Title;
        public readonly bool IsArchived;
        public readonly bool IsSecret;
        public readonly ulong ID;
        public readonly string UnlockedDescription;
        public readonly AchievementWritePolicy WritePolicy;
        public readonly string LockedImageURL;
        public readonly string UnlockedImageURL;

        public AchievementDefinition(IntPtr o)
        {
            Type = CLIB.ppf_AchievementDefinition_GetType(o);
            Name = CLIB.ppf_AchievementDefinition_GetName(o);
            BitfieldLength = CLIB.ppf_AchievementDefinition_GetBitfieldLength(o);
            Target = CLIB.ppf_AchievementDefinition_GetTarget(o);
            Description = CLIB.ppf_AchievementDefinition_GetDescription(o);
            Title = CLIB.ppf_AchievementDefinition_GetTitle(o);
            IsArchived = CLIB.ppf_AchievementDefinition_IsArchived(o);
            IsSecret = CLIB.ppf_AchievementDefinition_IsSecret(o);
            ID = CLIB.ppf_AchievementDefinition_GetID(o);
            UnlockedDescription = CLIB.ppf_AchievementDefinition_GetUnlockedDescription(o);
            WritePolicy = CLIB.ppf_AchievementDefinition_GetWritePolicy(o);
            LockedImageURL = CLIB.ppf_AchievementDefinition_GetLockedImageURL(o);
            UnlockedImageURL = CLIB.ppf_AchievementDefinition_GetUnlockedImageURL(o);
        }
    }

    public class AchievementDefinitionList : MessageArray<AchievementDefinition>
    {
        public readonly ulong TotalSize;

        public AchievementDefinitionList(IntPtr a)
        {
            TotalSize = (ulong) CLIB.ppf_AchievementDefinitionArray_GetTotalSize(a);
            var count = (int) CLIB.ppf_AchievementDefinitionArray_GetSize(a);
            this.Capacity = count;
            for (uint i = 0; i < count; i++)
            {
                this.Add(new AchievementDefinition(CLIB.ppf_AchievementDefinitionArray_GetElement(a, (UIntPtr) i)));
            }

            NextPageParam = CLIB.ppf_AchievementDefinitionArray_HasNextPage(a) ? "true" : string.Empty;
        }
    }

    public class AchievementProgress
    {
        public readonly ulong ID;
        public readonly string Bitfield;
        public readonly long Count;
        public readonly bool IsUnlocked;
        public readonly string Name;
        public readonly DateTime UnlockTime;
        public readonly byte[] ExtraData;


        public AchievementProgress(IntPtr o)
        {
            ID = CLIB.ppf_AchievementProgress_GetID(o);
            Bitfield = CLIB.ppf_AchievementProgress_GetBitfield(o);
            Count = CLIB.ppf_AchievementProgress_GetCount(o);
            IsUnlocked = CLIB.ppf_AchievementProgress_GetIsUnlocked(o);
            Name = CLIB.ppf_AchievementProgress_GetName(o);

            uint size = CLIB.ppf_AchievementProgress_GetExtraDataLength(o);
            ExtraData = new byte[size];
            Marshal.Copy(CLIB.ppf_AchievementProgress_GetExtraData(o), ExtraData, 0, (int) size);
            var unlockTime = CLIB.ppf_AchievementProgress_GetUnlockTime(o);
            if (unlockTime != 0)
            {
                UnlockTime = Util.SecondsToDateTime((long) unlockTime);
            }
        }
    }

    public class AchievementProgressList : MessageArray<AchievementProgress>
    {
        public readonly ulong TotalSize;

        public AchievementProgressList(IntPtr a)
        {
            TotalSize = (ulong) CLIB.ppf_AchievementProgressArray_GetTotalSize(a);
            var count = (int) CLIB.ppf_AchievementProgressArray_GetSize(a);
            this.Capacity = count;
            for (uint i = 0; i < count; i++)
            {
                this.Add(new AchievementProgress(CLIB.ppf_AchievementProgressArray_GetElement(a, (UIntPtr) i)));
            }

            NextPageParam = CLIB.ppf_AchievementProgressArray_HasNextPage(a) ? "true" : string.Empty;
        }
    }
}
#endif