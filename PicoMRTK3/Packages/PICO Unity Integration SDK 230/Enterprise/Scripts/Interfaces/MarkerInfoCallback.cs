using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;
using UnityEngine.XR;

namespace Unity.XR.PXR
{
    public class MarkerInfoCallback:AndroidJavaProxy
    {
        public Action<List<MarkerInfo>> mCallback;
        private List<MarkerInfo> mlist = new List<MarkerInfo>();
        private TrackingOriginModeFlags TrackingMode;
        private float YOffset;
        public MarkerInfoCallback(TrackingOriginModeFlags trackingMode,float cameraYOffset,Action<List<MarkerInfo>> callback) : base("com.picoxr.tobservice.interfaces.StringCallback")
        {
            TrackingMode = trackingMode;
            YOffset = cameraYOffset;
            mCallback = callback;
            mlist.Clear();
        }

        public void CallBack(string var1)
        {
            Debug.Log("ToBService MarkerInfo Callback 回调:" + var1);
            List<MarkerInfo> tmp = JsonToMarkerInfos(var1);
            PXR_EnterpriseTools.Instance.QueueOnMainThread(() =>
            {
                if (mCallback != null)
                {
                    mCallback(tmp);
                }
            });
        }
        public List<MarkerInfo> JsonToMarkerInfos(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                return null;
            }

            List<MarkerInfo> ModelList = new List<MarkerInfo>();
            JsonData jsonData = JsonMapper.ToObject(json);
            IDictionary dictionary = jsonData as IDictionary;
            for (int i = 0; i < dictionary.Count; i++)
            {
                Debug.Log("TOB TestDemo---- MarkerInfo Callback 回调:1" );
                if (TrackingMode != TrackingOriginModeFlags.Device)
                {
                    YOffset = 0;
                }
                float OriginHeight = PXR_Plugin.System.UPxr_GetConfigFloat(ConfigType.ToDelaSensorY);
                //Debug.Log("TOB TestDemo---- MarkerInfo Callback 回调:OriginHeight："+OriginHeight );
                // float OriginHeight = Mathf.Abs(PXR_Plugin.System.UPxr_GetConfigFloat(ConfigType.ToDelaSensorY));
                MarkerInfo model = new MarkerInfo();
                model.posX = double.Parse(jsonData[i]["posX"].ToString());
                model.posY = double.Parse(jsonData[i]["posY"].ToString())+OriginHeight+YOffset;
                model.posZ = -double.Parse(jsonData[i]["posZ"].ToString());
                
                model.rotationX = -double.Parse(jsonData[i]["rotationX"].ToString());
                model.rotationY = -double.Parse(jsonData[i]["rotationY"].ToString());
                model.rotationZ = double.Parse(jsonData[i]["rotationZ"].ToString());
                model.rotationW = double.Parse(jsonData[i]["rotationW"].ToString());
                
                model.validFlag = int.Parse(jsonData[i]["validFlag"].ToString());
                model.markerType = int.Parse(jsonData[i]["markerType"].ToString());
                model.iMarkerId = int.Parse(jsonData[i]["iMarkerId"].ToString());
                model.dTimestamp = double.Parse(jsonData[i]["dTimestamp"].ToString());
            
                IDictionary dictionaryReserve = jsonData[i]["reserve"] as IDictionary;
                model.reserve = new int[dictionaryReserve.Count];
                for (int j = 0; j < dictionaryReserve.Count; j++)
                {
                    model.reserve[j]=int.Parse(jsonData[i]["reserve"][j].ToString());
                }
                ModelList.Add(model);
            }

            return ModelList;
        }
    }
}