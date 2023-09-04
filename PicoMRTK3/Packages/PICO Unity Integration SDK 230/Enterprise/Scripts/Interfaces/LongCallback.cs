using System;
using UnityEngine;

namespace Unity.XR.PXR
{
    public class LongCallback : AndroidJavaProxy
    {
        public Action<long> mCallback;

        public LongCallback(Action<long> callback) : base("com.picoxr.tobservice.interfaces.LongCallback")
        {
            mCallback = callback;
        }

        public void CallBack(long var1)
        {
            Debug.Log("ToBService ILongCallback 回调:" + var1);
            PXR_EnterpriseTools.Instance.QueueOnMainThread(() =>
            {
                if (mCallback != null)
                {
                    mCallback(var1);
                }
            });
        }
    }
}