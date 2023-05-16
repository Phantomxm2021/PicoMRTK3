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

using System.IO;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Unity.XR.PXR.Editor
{
    [InitializeOnLoad]
    public static class PXR_SDKBuildCheck
    {
        private static bool doNotShowAgain = false;

        static PXR_SDKBuildCheck()
        {
            ObjectFactory.componentWasAdded += ComponentWasAdded;
            BuildPlayerWindow.RegisterBuildPlayerHandler(OnBuild);
            doNotShowAgain = GetDoNotShowBuildWarning();
            Debug.Log("PXRLog [Build Check]RegisterBuildPlayerHandler,Already Do not show: " + doNotShowAgain);
        }
        static void ComponentWasAdded(Component com)
        {
            if (com.name == "XR Rig")
            {
                if (!com.GetComponent<PXR_Manager>() && com.GetType() != typeof(Transform))
                {
                    com.gameObject.AddComponent<PXR_Manager>();
                }
            }
        }
        static bool GetDoNotShowBuildWarning()
        {
            string path = PXR_SDKSettingEditor.assetPath + typeof(PXR_SDKSettingAsset).ToString() + ".asset";
            if (File.Exists(path))
            {
                PXR_SDKSettingAsset asset = AssetDatabase.LoadAssetAtPath<PXR_SDKSettingAsset>(path);
                if (asset != null)
                {
                    return asset.doNotShowBuildWarning;
                }

            }
            return false;
        }

        public static void SetDoNotShowBuildWarning()
        {
            string path = PXR_SDKSettingEditor.assetPath + typeof(PXR_SDKSettingAsset).ToString() + ".asset";
            PXR_SDKSettingAsset asset = AssetDatabase.LoadAssetAtPath<PXR_SDKSettingAsset>(path);
            if (File.Exists(path))
            {

                asset.doNotShowBuildWarning = true;
            }
            else
            {
                asset = new PXR_SDKSettingAsset();
                ScriptableObjectUtility.CreateAsset<PXR_SDKSettingAsset>(asset, PXR_SDKSettingEditor.assetPath);
            }
            asset.doNotShowBuildWarning = true;
            EditorUtility.SetDirty(asset);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        public static void OnBuild(BuildPlayerOptions options)
        {
            if (!doNotShowAgain)
            {
                if (!PXR_PlatformSetting.Instance.startTimeEntitlementCheck)
                {
                    int result = EditorUtility.DisplayDialogComplex("Start-time Entitlement Check",
                        "EntitlementCheck is highly recommend which can\nprotect the copyright of app. Enable it now?",
                        "OK", "Ignore", "Ignore, Don't remind again");

                    switch (@result)
                    {
                        // ok
                        case 0:
                            PXR_PlatformSettingEditor.Edit();
                            throw new System.OperationCanceledException("Build was canceled by the user.");
                        //cancel
                        case 1:
                            Debug.LogWarning("PXRLog Warning: EntitlementCheck is highly recommended which can protect the copyright of app. You can enable it when App start-up in the Inspector of \"Menu/PXR_SDK/PlatformSettings\" and Enter your APPID. If you want to call the APIs as needed, please refer to the development Document.");
                            Debug.Log("PXRLog [Build Check] Start-time Entitlement Check Cancel The StartTime Entitlement Check status: " + PXR_PlatformSetting.Instance.startTimeEntitlementCheck.ToString());

                            BuildPlayerWindow.DefaultBuildMethods.BuildPlayer(options);
                            break;
                        //alt
                        case 2:
                            doNotShowAgain = true;
                            SetDoNotShowBuildWarning();
                            Debug.LogWarning("PXRLog Warning: EntitlementCheck is highly recommended which can protect the copyright of app. You can enable it when App start-up in the Inspector of \"Menu/PXR_SDK/PlatformSettings\" and Enter your APPID. If you want to call the APIs as needed, please refer to the development Document.");
                            Debug.Log("PXRLog [Build Check] Start-time Entitlement Check Do not show again The StartTime Entitlement Check status: " + PXR_PlatformSetting.Instance.startTimeEntitlementCheck.ToString());

                            BuildPlayerWindow.DefaultBuildMethods.BuildPlayer(options);
                            break;
                    }
                }
                else
                {
                    Debug.Log("PXRLog [Build Check]1 Enable Start-time Entitlement Check:" + PXR_PlatformSetting.Instance.startTimeEntitlementCheck + ", your AppID :" + PXR_PlatformSetting.Instance.appID);
                    BuildPlayerWindow.DefaultBuildMethods.BuildPlayer(options);
                }
            }
            else
            {
                Debug.Log("PXRLog [Build Check]2 Enable Start-time Entitlement Check:" + PXR_PlatformSetting.Instance.startTimeEntitlementCheck + ", your AppID :" + PXR_PlatformSetting.Instance.appID);
                BuildPlayerWindow.DefaultBuildMethods.BuildPlayer(options);
            }
        }

    }
}

