using System;
using UnityEngine;

namespace Unity.XR.PXR
{
    public class StringCallback : AndroidJavaProxy
    {
        public Action<string> mCallback;

        public StringCallback(Action<string> callback) : base("com.picoxr.tobservice.interfaces.StringCallback")
        {
            mCallback = callback;
        }

        public void CallBack(string var1)
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