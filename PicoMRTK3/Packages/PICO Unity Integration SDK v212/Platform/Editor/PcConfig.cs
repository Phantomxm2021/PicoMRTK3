#if PICO_INSTALL
using System;
using System.IO;
using LitJson;
using Unity.XR.PXR;
using UnityEditor;
using UnityEngine;

namespace Pico.Platform.Editor
{
    /// <summary>
    /// Unity Setting Getter and Setter
    /// </summary>
    class Gs
    {
        public static string appId
        {
            get { return PXR_PlatformSetting.Instance.appID; }
            set { PXR_PlatformSetting.Instance.appID = value; }
        }

        public static string productName
        {
            get { return PlayerSettings.productName; }
            set { PlayerSettings.productName = value; }
        }

        public static string packageName
        {
            get { return PlayerSettings.GetApplicationIdentifier(EditorUserBuildSettings.selectedBuildTargetGroup); }
            set { PlayerSettings.SetApplicationIdentifier(EditorUserBuildSettings.selectedBuildTargetGroup, value); }
        }

        public static string bundleVersion
        {
            get { return PlayerSettings.bundleVersion; }
            set { PlayerSettings.bundleVersion = value; }
        }

        public static int bundleVersionCode
        {
            get { return PlayerSettings.Android.bundleVersionCode; }
            set { PlayerSettings.Android.bundleVersionCode = value; }
        }
    }

    public enum Region
    {
        cn = 0,
        i18n = 1,
    }

    public class PcConfig : ScriptableObject
    {
        public Region region = Region.cn;
        public string accessToken = "";
        internal bool hasError = false;
    }

    [CustomEditor(typeof(PcConfig))]
    public class PcConfigEditor : UnityEditor.Editor
    {
        static string filepath = "Assets/Resources/PicoSdkPCConfig.json";

        public override void OnInspectorGUI()
        {
            var x = Selection.activeObject as PcConfig;
            if (x.hasError)
            {
                EditorGUILayout.LabelField("Config file error,please check the file");
                return;
            }

            base.OnInspectorGUI();
            this.save();
        }

        public static PcConfig load()
        {
            var obj = CreateInstance<PcConfig>();
            obj.hasError = false;
            try
            {
                if (File.Exists(filepath))
                {
                    var jsonContent = File.ReadAllText(filepath);
                    var jsonConf = JsonMapper.ToObject(jsonContent);
                    obj.accessToken = jsonConf["account"]["access_token"].ToString();
                    if (!Region.TryParse(jsonConf["general"]["region"].ToString() ?? "", out obj.region))
                    {
                        obj.region = Region.cn;
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                obj.hasError = true;
            }

            return obj;
        }

        public void save()
        {
            var obj = Selection.activeObject as PcConfig;
            if (obj.hasError)
            {
                return;
            }

            var conf = new JsonData();
            conf["general"] = new JsonData();
            conf["account"] = new JsonData();
            conf["package"] = new JsonData();
            conf["general"]["region"] = obj.region.ToString();
            conf["account"]["access_token"] = obj.accessToken.Trim();
            conf["package"]["package_name"] = Gs.packageName.Trim();
            conf["package"]["package_version_code"] = Gs.bundleVersionCode;
            conf["package"]["package_version_name"] = Gs.bundleVersion;
            File.WriteAllText(filepath, JsonMapper.ToJson(conf));
        }
    }
}
#endif