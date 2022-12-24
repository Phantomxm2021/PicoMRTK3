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

using System.IO;
using Unity.XR.PXR;
using UnityEditor;
using UnityEditor.Build;
using UnityEngine;

namespace Unity.XR.PXR.Editor
{
    [InitializeOnLoad]
    public class PXR_SDKSettingEditor : EditorWindow
    {
        public static PXR_SDKSettingEditor window;
        public static string assetPath = "Assets/Resources/";
        GUIContent myTitleContent = new GUIContent();
        static Language language = Language.English;

        const BuildTarget recommendedBuildTarget = BuildTarget.Android;
        const UIOrientation recommendedOrientation = UIOrientation.LandscapeLeft;


        public bool toggleBuildTarget = true;
        public bool toggleOrientation = true;
        public bool toggleSetAppID = true;
        public static bool appIDCheck = false;
        private bool appIdShowError = false;
        public static string AppID = "";
        GUIStyle styleApply;

        static string[] strWindowName = { "PXR SDK Setting", "PXR SDK 设置" };
        string strseparate = "______________________________________________________________________________________________________________________________________________";
        string[] strNoticeText = { "Notice: Recommended project settings for PXR SDK", "注意：PXR SDK 推荐项目配置" };
        string[] strBtnChange = { "切换至中文", "Switch to English" };
        string[] strApplied = { "Applied", "已应用" };

        string[] strInformationText = { "Information:", "信息说明" };

        string[] strInfo1Text =
        {
        "1 Support Unity Version: Unity2019.4 and above",
        "1 支持Unity版本：Unity2019.4及以上版本"
    };
        string[] strInfo2Text =
        {
        "2 Player Setting: " + "  Default Orientation setting Landscape Left",
        "2 Player Setting: " + "  Default Orientation setting Landscape Left"
    };
        private string[] strInfo3Text =
        {
        "3 EntitlementCheck is highly recommended in order to protect the copyright of an app. To enable it upon app start-up,  \n" +
          "    go to \"Menu/PXR_SDK/Platform Settings\" and enter your APPID." ,
        "3 强烈推荐启用应用版权保护，可在\"Menu/PXR_SDK/PlatformSettings\" 配置面板中，勾选“Start-time Entitlement Check”并填入正确的APPID进行开启。\n"
    };
        private string[] strInfo4Text =
    {
        "4 If you need to use the relevant functions of the Platform SDK, APPID is required,  \n" +
          "    go to \"Menu/PXR_SDK/Platform Settings\" and enter your APPID." ,
        "4 如需使用PlatformSDK相关功能，需填写APPID，可在\"Menu/PXR_SDK/PlatformSettings\" 配置面板中，填写APPID。\n"
    };
        string[] strInfo5Text = { "5 Get the lastest version of SDK:", "5 获取最新版本的SDK:" };
        string[] strInfoURL = { "http://us-dev.picovr.com/sdk", "http://dev.picovr.com/sdk" };


        string[] strConfigurationText = { "Configuration:", "配置" };

        string[] strConfiguration1Text =
        {
        "1 current:             Build Target = {0}\n" +
        "   Recommended:  Build Target = {1}\n",
        "1 当前:             Build Target = {0}\n" +
        "   推荐:             Build Target = {1}\n"
    };
        private string[] quizHova =
        {
        "If selected, you will need to enter the APPID that is obtained from PICO Developer Platform after uploading the app for an entitlement check upon the app launch.",
        "如果勾选版权保护选项，并且填入正确的APPID，应用启动时会进行版权验证。可通过开发者平台获取APPID。"
    };

        private string[] strConfiguration2Text =
        {
        "\n2 User Entitlement Check [?]\n",
        "\n2 启动应用程序时进行授权检查[?]\n"
    };

        string strConfiguration2TextAppID = "   App ID ";

        private string[] quizYes =
        {
        "The APPID is required to run an Entitlement Check. Create / Find your APPID Here:",
        "应用版权保护要求填入正确的APPID，可通过网址创建或查看你的APPID：",
        "If you do not need user Entitlement Check, please uncheck it.",
        "如果不需要应用版权保护，请勿勾选"
    };

        private string getAppIDWebSite = "https://developer.pico-interactive.com/developer/overview";

        string[] strConfiguration3Text =
        {
        "3 current:             Orientation = {0}\n" +
        "    Recommended:  Orientation = {1}\n",
        "3 当前:             Orientation = {0}\n" +
        "    推荐:             Orientation = {1}\n"
    };

        string[] strBtnApply = { "Apply", "应用" };
        string[] strBtnClose = { "Close", "关闭" };

        static PXR_SDKSettingEditor()
        {
            EditorApplication.update += Update;
        }

        static void Init()
        {
            IsIgnoreWindow();
            appIDCheck = IsAppIDChecked();
            if (appIDCheck)
            {
                AppID = PXR_PlatformSetting.Instance.appID;
            }
            ShowSettingWindow();
        }

        static void Update()
        {
            appIDCheck = IsAppIDChecked();
            if (appIDCheck)
            {
                AppID = PXR_PlatformSetting.Instance.appID;
            }

            bool allApplied = IsAllApplied();
            bool showWindow = !allApplied;

            if (IsIgnoreWindow())
            {
                showWindow = false;
            }
            if (showWindow)
            {
                ShowSettingWindow();
            }

            EditorApplication.update -= Update;
        }

        public static bool IsIgnoreWindow()
        {
            string path = assetPath + typeof(PXR_SDKSettingAsset).ToString() + ".asset";
            if (File.Exists(path))
            {
                PXR_SDKSettingAsset asset = AssetDatabase.LoadAssetAtPath<PXR_SDKSettingAsset>(path);
                return asset.ignoreSDKSetting;
            }
            return false;
        }

        public void OnDisable()
        {
            PXR_SDKSettingAsset asset;
            string assetPath = PXR_SDKSettingEditor.assetPath + typeof(PXR_SDKSettingAsset).ToString() + ".asset";
            if (File.Exists(assetPath))
            {
                asset = AssetDatabase.LoadAssetAtPath<PXR_SDKSettingAsset>(assetPath);
            }
            else
            {
                asset = new PXR_SDKSettingAsset();
                ScriptableObjectUtility.CreateAsset<PXR_SDKSettingAsset>(asset, PXR_SDKSettingEditor.assetPath);
            }
            PXR_ProjectSetting.GetProjectConfig();
        }

        public static bool IsAppIDChecked()
        {
            string path = PXR_SDKSettingEditor.assetPath + typeof(PXR_SDKSettingAsset).ToString() + ".asset";
            if (File.Exists(path))
            {
                PXR_SDKSettingAsset asset = AssetDatabase.LoadAssetAtPath<PXR_SDKSettingAsset>(path);
                return asset.appIDChecked && PXR_PlatformSetting.Instance.startTimeEntitlementCheck && PXR_PlatformSetting.Instance.appID != null;
            }
            return false;
        }

        static void ShowSettingWindow()
        {
            if (window != null)
                return;
            window = (PXR_SDKSettingEditor)GetWindow(typeof(PXR_SDKSettingEditor), true, strWindowName[(int)language], true);
            window.autoRepaintOnSceneChange = true;
            window.minSize = new Vector2(960, 620);
        }

        string GetResourcePath()
        {
            var ms = MonoScript.FromScriptableObject(this);
            var path = AssetDatabase.GetAssetPath(ms);
            path = Path.GetDirectoryName(path);
            return path.Substring(0, path.Length - "Editor".Length) + "Textures/";
        }

        public void OnGUI()
        {
            myTitleContent.text = strWindowName[(int)language];
            if (window != null)
            {
                window.titleContent = myTitleContent;
            }
            ShowNoticeTextAndChangeBtn();

            GUIStyle styleSlide = new GUIStyle();
            styleSlide.normal.textColor = Color.white;
            GUILayout.Label(strseparate, styleSlide);

            GUILayout.Label(strInformationText[(int)language]);
            GUILayout.Label(strInfo1Text[(int)language]);
            GUILayout.Label(strInfo2Text[(int)language]);
            GUILayout.Label(strInfo3Text[(int)language]);
            GUILayout.Label(strInfo4Text[(int)language]);
            GUILayout.Label(strInfo5Text[(int)language]);
            string strURL = strInfoURL[(int)language];
            GUIStyle style = new GUIStyle();
            style.normal.textColor = new Color(0, 122f / 255f, 204f / 255f);
            if (GUILayout.Button("    " + strURL, style, GUILayout.Width(200)))
            {
                Application.OpenURL(strURL);
            }

            GUILayout.Label(strseparate, styleSlide);

            GUILayout.Label(strConfigurationText[(int)language]);

            string strinfo1 = string.Format(strConfiguration1Text[(int)language], EditorUserBuildSettings.activeBuildTarget, recommendedBuildTarget);
            EditorConfigurations(strinfo1, EditorUserBuildSettings.activeBuildTarget == recommendedBuildTarget, ref toggleBuildTarget);

            string strinfo2 = strConfiguration2Text[(int)language];
            ConfigEntitlementCheck(strinfo2, appIDCheck && AppID != "", ref toggleSetAppID);

            string strinfo3 = string.Format(strConfiguration3Text[(int)language],
                PlayerSettings.defaultInterfaceOrientation, recommendedOrientation);
            EditorConfigurations(strinfo3, PlayerSettings.defaultInterfaceOrientation == recommendedOrientation,
                ref toggleOrientation);

            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("", GUILayout.Width(200));

            if (IsAllApplied())
            {
                styleApply = new GUIStyle("ObjectPickerBackground");
                styleApply.alignment = TextAnchor.MiddleCenter;
            }
            else
            {
                styleApply = new GUIStyle("LargeButton");
                styleApply.alignment = TextAnchor.MiddleCenter;
            }
            if (GUILayout.Button(strBtnApply[(int)language], styleApply, GUILayout.Width(100), GUILayout.Height(30)))
            {
                appIdShowError = false;
                if (AppID == "")
                {
                    appIdShowError = true;
                }
                EditorApplication.delayCall += OnClickApply;
            }
            styleApply = null;

            EditorGUILayout.LabelField("", GUILayout.Width(200));
            if (GUILayout.Button(strBtnClose[(int)language], GUILayout.Width(100), GUILayout.Height(30)))
            {
                OnClickClose();
            }
            EditorGUILayout.EndHorizontal();
        }

        public void OnClickApply()
        {
            if (toggleSetAppID && AppID != "")
            {
                PXR_PlatformSetting.Instance.appID = AppID;
                PXR_PlatformSetting.Instance.startTimeEntitlementCheck = true;
                appIDCheck = true;
                appIdShowError = !appIDCheck;
                SaveAssetAppIDChecked();
            }
            if (toggleSetAppID && AppID == "")
            {
                PXR_PlatformSetting.Instance.startTimeEntitlementCheck = false;
            }
            if (!toggleSetAppID)
            {
                PXR_PlatformSetting.Instance.appID = AppID;
                PXR_PlatformSetting.Instance.startTimeEntitlementCheck = toggleSetAppID;
            }

            if (toggleOrientation && PlayerSettings.defaultInterfaceOrientation != recommendedOrientation)
            {
                PlayerSettings.defaultInterfaceOrientation = recommendedOrientation;
            }

            if (toggleBuildTarget && EditorUserBuildSettings.activeBuildTarget != recommendedBuildTarget)
            {
                EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android, recommendedBuildTarget);
                EditorUserBuildSettings.selectedBuildTargetGroup = BuildTargetGroup.Android;
            }

            PXR_SDKSettingAsset asset;
            string assetPath = PXR_SDKSettingEditor.assetPath + typeof(PXR_SDKSettingAsset).ToString() + ".asset";
            if (File.Exists(assetPath))
            {
                asset = AssetDatabase.LoadAssetAtPath<PXR_SDKSettingAsset>(assetPath);
            }
            else
            {
                asset = new PXR_SDKSettingAsset();
                ScriptableObjectUtility.CreateAsset<PXR_SDKSettingAsset>(asset, PXR_SDKSettingEditor.assetPath);
            }
            PXR_ProjectSetting.GetProjectConfig();
        }

        void OnClickClose()
        {
            bool allApplied = IsAllApplied();
            if (allApplied)
            {
                Close();
            }
            else
            {
                if (AppID == "")
                {
                    PXR_PlatformSetting.Instance.startTimeEntitlementCheck = false;
                }
                PXR_SettingMessageBoxEditor.Init(language);
            }
            PXR_ProjectSetting.GetProjectConfig();
        }

        public static bool IsAllApplied()
        {
            bool notApplied = (EditorUserBuildSettings.activeBuildTarget != recommendedBuildTarget) || !appIDCheck ||
                            (PlayerSettings.defaultInterfaceOrientation != recommendedOrientation);

            if (!notApplied)
                return true;
            else
                return false;
        }

        void EditorConfigurations(string strConfiguration, bool enable, ref bool toggle)
        {
            EditorGUILayout.BeginHorizontal();

            GUILayout.Label(strConfiguration, GUILayout.Width(500));

            GUIStyle styleApplied = new GUIStyle();
            styleApplied.normal.textColor = Color.green;
            if (enable)
            {
                GUILayout.Label(strApplied[(int)language], styleApplied);
            }
            else
            {
                toggle = EditorGUILayout.Toggle(toggle);
            }

            EditorGUILayout.EndHorizontal();
        }

        void ConfigEntitlementCheck(string strConfiguration, bool enable, ref bool toggle)
        {
            EditorGUILayout.BeginHorizontal();
            var startEntitleCheckLabel = new GUIContent(strConfiguration, quizHova[(int)language]);
            EditorGUILayout.LabelField(startEntitleCheckLabel, GUILayout.Width(500));

            GUIStyle styleApplied = new GUIStyle();
            styleApplied.normal.textColor = Color.green;
            if (enable)
            {
                GUILayout.Label(strApplied[(int)language], styleApplied);
            }
            else
            {
                toggle = EditorGUILayout.Toggle(toggle);
            }

            EditorGUILayout.EndHorizontal();
            if (toggle != PXR_PlatformSetting.Instance.startTimeEntitlementCheck)
            {
                PXR_PlatformSetting.Instance.startTimeEntitlementCheck = toggle;
            }
            if (toggle)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label(strConfiguration2TextAppID, GUILayout.Width(100));
                if (!appIDCheck)
                {
                    APPIDFiledEditorConfigurations();
                }

                if (AppID != "" && AppID != PXR_PlatformSetting.Instance.appID)
                {
                    PXR_PlatformSetting.Instance.appID = AppID;
                }

                if (toggle != PXR_PlatformSetting.Instance.startTimeEntitlementCheck)
                {
                    toggle = true;
                    PXR_PlatformSetting.Instance.startTimeEntitlementCheck = true;
                }
                if (AppID != "" && appIDCheck)
                {
                    GUILayout.Label(AppID);
                }
                EditorGUILayout.EndHorizontal();

                if (appIdShowError)
                {

                    EditorGUILayout.BeginHorizontal(GUILayout.Width(500));
                    EditorGUILayout.HelpBox("APPID is required for Entitlement Check", MessageType.Error, true);
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Label(quizYes[(int)language], GUILayout.Width(500));
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.BeginHorizontal();
                    GUIStyle style = new GUIStyle();
                    style.normal.textColor = new Color(0, 122f / 255f, 204f / 255f);
                    if (GUILayout.Button("    " + getAppIDWebSite, style, GUILayout.Width(300)))
                    {
                        Application.OpenURL(getAppIDWebSite);
                    }
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Label(quizYes[(int)language + 2], GUILayout.Width(500));
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.BeginHorizontal();

                    EditorGUILayout.EndHorizontal();
                }
                else
                {

                    Repaint();
                }
            }
        }

        void APPIDFiledEditorConfigurations()
        {
            EditorGUILayout.BeginHorizontal();
            AppID = EditorGUILayout.TextField(AppID, GUILayout.Width(350.0f));
            EditorGUILayout.EndHorizontal();
        }

        void ShowLogo()
        {
            var resourcePath = GetResourcePath();
            var logo = AssetDatabase.LoadAssetAtPath<Texture2D>(resourcePath + "logo.png");
            if (logo)
            {
                var rect = GUILayoutUtility.GetRect(position.width, 150, GUI.skin.box);
                GUI.DrawTexture(rect, logo, ScaleMode.ScaleToFit);
            }
        }

        void ShowNoticeTextAndChangeBtn()
        {
            EditorGUILayout.BeginHorizontal();

            GUIStyle styleNoticeText = new GUIStyle();
            styleNoticeText.alignment = TextAnchor.UpperCenter;
            styleNoticeText.fontSize = 20;
            GUILayout.Label(strNoticeText[(int)language], styleNoticeText);

            if (GUILayout.Button(strBtnChange[(int)language], GUILayout.Width(150), GUILayout.Height(30)))
            {
                SwitchLanguage();
            }

            EditorGUILayout.EndHorizontal();
        }

        void SwitchLanguage()
        {
            if (language == Language.Chinese)
                language = Language.English;
            else if (language == Language.English)
                language = Language.Chinese;
        }

        private void SaveAssetAppIDChecked()
        {
            PXR_SDKSettingAsset asset;
            string assetPath = PXR_SDKSettingEditor.assetPath + typeof(PXR_SDKSettingAsset).ToString() + ".asset";
            if (File.Exists(assetPath))
            {
                asset = AssetDatabase.LoadAssetAtPath<PXR_SDKSettingAsset>(assetPath);
            }
            else
            {
                asset = new PXR_SDKSettingAsset();
                ScriptableObjectUtility.CreateAsset<PXR_SDKSettingAsset>(asset, PXR_SDKSettingEditor.assetPath);
            }
            asset.appIDChecked = true;
            EditorUtility.SetDirty(asset);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();//must Refresh
        }

        void OnDestroy()
        {
            if (AppID == "")
            {
                PXR_PlatformSetting.Instance.startTimeEntitlementCheck = false;
            }
        }
    }

    public enum Language
    {
        English,
        Chinese,
    }

    public class PXR_SettingMessageBoxEditor : EditorWindow
    {
        static PXR_SettingMessageBoxEditor myWindow;
        static Language language = Language.English;
        static string[] strWindowName = { "Ignore the recommended configuration", "忽略推荐配置" };
        string[] strTipInfo = { "                                   No more prompted \n" +
            "             You can get recommended configuration from  \n" +
            "                            Development documentation.",
             "                               点击\"忽略\"后,不再提示！\n"+
            "                       可从开发者文档中获取推荐配置说明  \n"};

        string[] strBtnIgnore = { "Ignore", "忽略" };
        string[] strBtnCancel = { "Cancel", "取消" };

        public static void Init(Language language)
        {
            PXR_SettingMessageBoxEditor.language = language;
            myWindow = (PXR_SettingMessageBoxEditor)GetWindow(typeof(PXR_SettingMessageBoxEditor), true, strWindowName[(int)language], true);
            myWindow.autoRepaintOnSceneChange = true;
            myWindow.minSize = new Vector2(360, 200);
            myWindow.Show(true);
            Rect pos;
            if (PXR_SDKSettingEditor.window != null)
            {
                Rect frect = PXR_SDKSettingEditor.window.position;
                pos = new Rect(frect.x + 300, frect.y + 200, 200, 140);
            }
            else
            {
                pos = new Rect(700, 400, 200, 140);
            }
            myWindow.position = pos;
        }

        void OnGUI()
        {
            for (int i = 0; i < 10; i++)
            {
                EditorGUILayout.Space();
            }
            GUILayout.Label(strTipInfo[(int)language]);

            for (int i = 0; i < 3; i++)
            {
                EditorGUILayout.Space();
            }
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("", GUILayout.Width(20));
            if (GUILayout.Button(strBtnIgnore[(int)language], GUILayout.Width(100), GUILayout.Height(30)))
            {
                OnClickIgnore();
            }
            EditorGUILayout.LabelField("", GUILayout.Width(50));
            if (GUILayout.Button(strBtnCancel[(int)language], GUILayout.Width(130), GUILayout.Height(30)))
            {
                OnClickCancel();
            }
            EditorGUILayout.EndHorizontal();
        }

        void OnClickIgnore()
        {
            if (PXR_SDKSettingEditor.AppID == "")
            {
                PXR_PlatformSetting.Instance.startTimeEntitlementCheck = false;
            }
            SaveAssetDataBase();
            PXR_SDKSettingEditor.window.Close();
            Close();
        }

        private void SaveAssetDataBase()
        {
            PXR_SDKSettingAsset asset;
            string assetPath = PXR_SDKSettingEditor.assetPath + typeof(PXR_SDKSettingAsset).ToString() + ".asset";
            if (File.Exists(assetPath))
            {
                asset = AssetDatabase.LoadAssetAtPath<PXR_SDKSettingAsset>(assetPath);
            }
            else
            {
                asset = new PXR_SDKSettingAsset();
                ScriptableObjectUtility.CreateAsset<PXR_SDKSettingAsset>(asset, PXR_SDKSettingEditor.assetPath);
            }
            asset.ignoreSDKSetting = true;

            EditorUtility.SetDirty(asset);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        void OnClickCancel()
        {
            if (PXR_SDKSettingEditor.AppID == "")
            {
                PXR_PlatformSetting.Instance.startTimeEntitlementCheck = false;
            }
            Close();
        }
    }

    [InitializeOnLoad]
    public class PXR_SDKQualitySetting
    {
        [InitializeOnLoadMethod]
        static void UnitySDKQualitySettings()
        {
            PlayerSettings.defaultInterfaceOrientation = UIOrientation.LandscapeLeft;

            SetAntiAliasingLevel(4);
        }

        static void SetAntiAliasingLevel(int level)
        {
            int currentLevel = QualitySettings.GetQualityLevel();
            for (int i = currentLevel; i >= 1; i--)
            {
                QualitySettings.DecreaseLevel(true);
                QualitySettings.antiAliasing = level;
            }
            QualitySettings.SetQualityLevel(currentLevel, true);
            for (int i = currentLevel; i < 10; i++)
            {
                QualitySettings.IncreaseLevel(true);
                QualitySettings.antiAliasing = level;
            }
        }

    }

    [InitializeOnLoad]
    public class PXR_SDKBuildSetting : IActiveBuildTargetChanged
    {
        void Start()
        {
            PlayerSettings.defaultInterfaceOrientation = UIOrientation.LandscapeLeft;
        }
        public int callbackOrder { get { return 0; } }
        public void OnActiveBuildTargetChanged(BuildTarget previousTarget, BuildTarget newTarget)
        {
            OnChangePlatform();
        }

        static void OnChangePlatform()
        {
            if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android)
            {

            }
        }

    }
    public static class ScriptableObjectUtility
    {
        public static void CreateAsset<T>(T classdata, string path) where T : ScriptableObject
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + typeof(T).ToString() + ".asset");

            AssetDatabase.CreateAsset(classdata, assetPathAndName);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}
