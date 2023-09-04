using System;
using UnityEngine;

namespace Unity.XR.PXR
{
    public class IntCallback : AndroidJavaProxy
    {
        public Action<int> mCallback;

        public IntCallback(Action<int> callback) : base("com.picoxr.tobservice.interfaces.IntCallback")
        {
            mCallback = callback;
        }

        public void CallBack(int var1)
        {
            Debug.Log("ToBService IIntCallback 回调:" + var1);
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