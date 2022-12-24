
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
    public static class DownloadStatus
    {
        public const string Downloaded = "downloaded";
        public const string Available = "available";
        public const string InProgress = "in-progress";
    }

    public static class IapStatus
    {
        public const string Entitled = "entitled";
        public const string NotEntitled = "not-entitled";
    }

    public class AssetDetails
    {
        public ulong AssetId;
        public string AssetType;

        /** @brief One of 'downloaded', 'available' and 'in-progress'*/
        public string DownloadStatus;

        /** @brief The filepath of the downloaded file.*/
        public string Filepath;

        public string Metadata;
        public string Filename;
        public int Version;

        /** One of 'entitled','not-entitled'*/
        public string IapStatus;

        public string IapSku;
        public string IapName;
        public string IapPrice;
        public string IapCurrency;
        public string IapDescription;
        public string IapIcon;

        public AssetDetails(IntPtr o)
        {
            AssetId = CLIB.ppf_AssetDetails_GetAssetId(o);
            AssetType = CLIB.ppf_AssetDetails_GetAssetType(o);
            DownloadStatus = CLIB.ppf_AssetDetails_GetDownloadStatus(o);
            IapStatus = CLIB.ppf_AssetDetails_GetIapStatus(o);
            Filepath = CLIB.ppf_AssetDetails_GetFilepath(o);
            Metadata = CLIB.ppf_AssetDetails_GetMetadata(o);
            Filename = CLIB.ppf_AssetDetails_GetFilename(o);
            Version = CLIB.ppf_AssetDetails_GetVersion(o);
            IapSku = CLIB.ppf_AssetDetails_GetIapSku(o);
            IapName = CLIB.ppf_AssetDetails_GetIapName(o);
            IapPrice = CLIB.ppf_AssetDetails_GetIapPrice(o);
            IapCurrency = CLIB.ppf_AssetDetails_GetIapCurrency(o);
            IapDescription = CLIB.ppf_AssetDetails_GetIapDescription(o);
            IapIcon = CLIB.ppf_AssetDetails_GetIapIcon(o);
        }
    }

    public class AssetDetailsList : MessageArray<AssetDetails>
    {
        public AssetDetailsList(IntPtr a)
        {
            var count = (int) CLIB.ppf_AssetDetailsArray_GetSize(a);
            this.Capacity = count;
            for (int i = 0; i < count; i++)
            {
                this.Add(new AssetDetails(CLIB.ppf_AssetDetailsArray_GetElement(a, (UIntPtr) i)));
            }

            NextPageParam = CLIB.ppf_AssetDetailsArray_GetNextPageParam(a);
        }
    }


    public class AssetFileDeleteForSafety
    {
        public readonly ulong AssetId;
        public readonly string Reason;

        public AssetFileDeleteForSafety(IntPtr o)
        {
            AssetId = CLIB.ppf_AssetFileDeleteForSafety_GetAssetId(o);
            Reason = CLIB.ppf_AssetFileDeleteForSafety_GetReason(o);
        }
    }


    public class AssetFileDeleteResult
    {
        public readonly string Filepath;
        public readonly bool Success;
        public readonly ulong AssetId;

        public AssetFileDeleteResult(IntPtr o)
        {
            Filepath = CLIB.ppf_AssetFileDeleteResult_GetFilepath(o);
            Success = CLIB.ppf_AssetFileDeleteResult_GetSuccess(o);
            AssetId = CLIB.ppf_AssetFileDeleteResult_GetAssetId(o);
        }
    }

    public class AssetFileDownloadCancelResult
    {
        public readonly string Filepath;
        public readonly bool Success;
        public readonly ulong AssetId;

        public AssetFileDownloadCancelResult(IntPtr o)
        {
            Filepath = CLIB.ppf_AssetFileDownloadCancelResult_GetFilepath(o);
            Success = CLIB.ppf_AssetFileDownloadCancelResult_GetSuccess(o);
            AssetId = CLIB.ppf_AssetFileDownloadCancelResult_GetAssetId(o);
        }
    }


    public class AssetFileDownloadResult
    {
        public readonly ulong AssetId;
        public readonly string Filepath;

        public AssetFileDownloadResult(IntPtr o)
        {
            AssetId = CLIB.ppf_AssetFileDownloadResult_GetAssetId(o);
            Filepath = CLIB.ppf_AssetFileDownloadResult_GetFilepath(o);
        }
    }

    public class AssetFileDownloadUpdate
    {
        public readonly ulong AssetId;
        public readonly ulong BytesTotal;
        public readonly long BytesTransferred;
        public readonly AssetFileDownloadCompleteStatus CompleteStatus;

        public AssetFileDownloadUpdate(IntPtr o)
        {
            AssetId = CLIB.ppf_AssetFileDownloadUpdate_GetAssetId(o);
            BytesTotal = CLIB.ppf_AssetFileDownloadUpdate_GetBytesTotal(o);
            BytesTransferred = CLIB.ppf_AssetFileDownloadUpdate_GetBytesTransferred(o);
            CompleteStatus = CLIB.ppf_AssetFileDownloadUpdate_GetCompleteStatus(o);
        }
    }

    public class AssetStatus
    {
        public readonly ulong AssetId;
        public readonly string Filename;
        public readonly string Filepath;
        public readonly string DownloadStatus;

        public AssetStatus(IntPtr o)
        {
            AssetId = CLIB.ppf_AssetStatus_GetAssetId(o);
            Filename = CLIB.ppf_AssetStatus_GetFilename(o);
            Filepath = CLIB.ppf_AssetStatus_GetFilepath(o);
            DownloadStatus = CLIB.ppf_AssetStatus_GetDownloadStatus(o);
        }
    }
}
#endif