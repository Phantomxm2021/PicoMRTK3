#if PICO_INSTALL

/*******************************************************************************
Copyright © 2015-2022 PICO Technology Co., Ltd.All rights reserved.  

NOTICE：All information contained herein is, and remains the property of 
PICO Technology Co., Ltd. The intellectual and technical concepts 
contained hererin are proprietary to PICO Technology Co., Ltd. and may be 
covered by patents, patents in process, and are protected by trade secret or 
copyright law. Dissemination of this information or reproduction of this 
material is strictly forbidden unless prior written permission is obtained from
PICO Technology Co., Ltd. 
*******************************************************************************/

using System;
using UnityEngine;
using UnityEngine.XR.Management;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Unity.XR.PXR
{
    [Serializable]
    [XRConfigurationData("PICO", "Unity.XR.PXR.Settings")]
    public class PXR_Settings : ScriptableObject
    {
        public enum StereoRenderingModeAndroid
        {
            MultiPass,
            Multiview
        }

        public enum SystemDisplayFrequency
        {
            Default,
            RefreshRate72,
            RefreshRate90,
            RefreshRate120,
        }

        [SerializeField, Tooltip("Set the Stereo Rendering Method")]
        public StereoRenderingModeAndroid stereoRenderingModeAndroid;

        [SerializeField, Tooltip("Set the system display frequency")]
        public SystemDisplayFrequency systemDisplayFrequency;

        public ushort GetStereoRenderingMode()
        {
            return (ushort)stereoRenderingModeAndroid;
        }
        public ushort GetSystemDisplayFrequency()
        {
            return (ushort)systemDisplayFrequency;
        }

#if UNITY_ANDROID && !UNITY_EDITOR
		public static PXR_Settings settings;
		public void Awake()
		{
            settings = this;
		}
#endif
    }
}

#endif