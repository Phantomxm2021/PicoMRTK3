﻿/*******************************************************************************
Copyright © 2015-2022 PICO Technology Co., Ltd.All rights reserved.  

NOTICE：All information contained herein is, and remains the property of 
PICO Technology Co., Ltd. The intellectual and technical concepts 
contained herein are proprietary to PICO Technology Co., Ltd. and may be 
covered by patents, patents in process, and are protected by trade secret or 
copyright law. Dissemination of this information or reproduction of this 
material is strictly forbidden unless prior written permission is obtained from
PICO Technology Co., Ltd. 
*******************************************************************************/

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Unity.XR.PXR;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

namespace Unity.XR.PXR.Editor
{
    [CustomEditor(typeof(PXR_Manager))]
    public class PXR_ManagerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            GUI.changed = false;
            DrawDefaultInspector();

            PXR_Manager manager = (PXR_Manager)target;
            PXR_ProjectSetting projectConfig = PXR_ProjectSetting.GetProjectConfig();

            //Screen Fade
            manager.screenFade = EditorGUILayout.Toggle("Open Screen Fade", manager.screenFade);
            if (Camera.main != null)
            {
                var head = Camera.main.transform;
                if (head)
                {
                    var fade = head.GetComponent<PXR_ScreenFade>();
                    if (manager.screenFade)
                    {
                        if (!fade)
                        {
                            head.gameObject.AddComponent<PXR_ScreenFade>();
                            Selection.activeObject = head;
                        }
                    }
                    else
                    {
                        if (fade) DestroyImmediate(fade);
                    }
                }
            }
            //ffr
            manager.foveationLevel = (FoveationLevel)EditorGUILayout.EnumPopup("Foveation Level", manager.foveationLevel);

            if (FoveationLevel.None != manager.foveationLevel)
            {
                projectConfig.enableSubsampled = EditorGUILayout.Toggle("  Enable Subsampled", projectConfig.enableSubsampled);
            }

            //eye tracking
            GUIStyle firstLevelStyle = new GUIStyle(GUI.skin.label);
            firstLevelStyle.alignment = TextAnchor.UpperLeft;
            firstLevelStyle.fontStyle = FontStyle.Bold;
            firstLevelStyle.fontSize = 12;
            firstLevelStyle.wordWrap = true;
            var guiContent = new GUIContent();
            guiContent.text = "Eye Tracking";
            guiContent.tooltip = "Before calling EyeTracking API, enable this option first, only for Neo3 Pro Eye , PICO 4 Pro device.";
            projectConfig.eyeTracking = EditorGUILayout.Toggle(guiContent, projectConfig.eyeTracking);
            manager.eyeTracking = projectConfig.eyeTracking;
            if (manager.eyeTracking)
            {
                projectConfig.eyetrackingCalibration = EditorGUILayout.Toggle(new GUIContent("Eye Tracking Calibration"), projectConfig.eyetrackingCalibration);
                EditorGUILayout.BeginVertical("box");
                EditorGUILayout.LabelField("Note:", firstLevelStyle);
                EditorGUILayout.LabelField("Eye Tracking is supported only on Neo 3 Pro Eye , PICO 4 Pro");
                EditorGUILayout.EndVertical();
            }

            //face tracking
            var FaceContent = new GUIContent();
            FaceContent.text = "Face Tracking Mode";
            manager.trackingMode = (FaceTrackingMode)EditorGUILayout.EnumPopup(FaceContent, manager.trackingMode);
            if (manager.trackingMode == FaceTrackingMode.None) {
                projectConfig.faceTracking = false;
                projectConfig.lipsyncTracking = false;
            }else if (manager.trackingMode == FaceTrackingMode.Hybrid)
            {
                projectConfig.faceTracking = true;
                projectConfig.lipsyncTracking = true;
            }
            else if(manager.trackingMode == FaceTrackingMode.FaceOnly) {
                projectConfig.faceTracking = true;
                projectConfig.lipsyncTracking = false;
            }else if (manager.trackingMode == FaceTrackingMode.LipsyncOnly)
            {
                projectConfig.faceTracking = false;
                projectConfig.lipsyncTracking = true;
            }
            manager.faceTracking = projectConfig.faceTracking;
            manager.lipsyncTracking = projectConfig.lipsyncTracking;

            //hand tracking
            var handContent = new GUIContent();
            handContent.text = "Hand Tracking";
            projectConfig.handTracking = EditorGUILayout.Toggle(handContent, projectConfig.handTracking);

            //body tracking
            var bodyContent = new GUIContent();
            bodyContent.text = "Body Tracking";
            projectConfig.bodyTracking = EditorGUILayout.Toggle(bodyContent, projectConfig.bodyTracking);
            manager.bodyTracking = projectConfig.bodyTracking;

            // content protect
            projectConfig.useContentProtect = EditorGUILayout.Toggle("Use Content Protect", projectConfig.useContentProtect);

            //MRC
            var mrcContent = new GUIContent();
            mrcContent.text = "MRC";
            projectConfig.openMRC = EditorGUILayout.Toggle(mrcContent, projectConfig.openMRC);
            manager.openMRC = projectConfig.openMRC;
            if (manager.openMRC == true)
            {
                EditorGUILayout.BeginVertical("frameBox");
                string[] layerNames = new string[32];
                for (int i = 0; i < 32; i++)
                {
                    layerNames[i] = LayerMask.LayerToName(i);
                    if (layerNames[i].Length == 0)
                    {
                        layerNames[i] = "LayerName " + i.ToString();
                    }
                }
                manager.foregroundLayerMask = EditorGUILayout.MaskField("foreground Layer Masks", manager.foregroundLayerMask, layerNames);
                manager.backLayerMask = EditorGUILayout.MaskField("back Layer Masks", manager.backLayerMask, layerNames);
                EditorGUILayout.EndVertical();
            }
            //Late Latching
            projectConfig.latelatching = EditorGUILayout.Toggle("Use Late Latching", projectConfig.latelatching);
            manager.lateLatching = projectConfig.latelatching;
            if (manager.lateLatching)
            {
                projectConfig.latelatchingDebug = EditorGUILayout.Toggle("  Late Latching Debug", projectConfig.latelatchingDebug);
                manager.latelatchingDebug = projectConfig.latelatchingDebug;
            }

            if (Camera.main != null)
            {
                var head = Camera.main.transform;
                if (head)
                {
                    var fade = head.GetComponent<PXR_LateLatching>();
                    if (manager.lateLatching)
                    {
                        if (!fade)
                        {
                            head.gameObject.AddComponent<PXR_LateLatching>();
                            Selection.activeObject = head;
                        }
                    }
                    else
                    {
                        if (fade) DestroyImmediate(fade);
                    }
                }
            }

            // msaa
            if (QualitySettings.renderPipeline != null)
            {
                EditorGUI.BeginDisabledGroup(true);
                manager.useRecommendedAntiAliasingLevel = EditorGUILayout.Toggle("Use Recommended MSAA", manager.useRecommendedAntiAliasingLevel);
                EditorGUI.EndDisabledGroup();
                EditorGUILayout.HelpBox("A Scriptable Render Pipeline is in use,the 'Use Recommended MSAA' will not be used. ", MessageType.Info,true);
            }
            else
            {
                manager.useRecommendedAntiAliasingLevel = EditorGUILayout.Toggle("Use Recommended MSAA", manager.useRecommendedAntiAliasingLevel);
            }

            if (GUI.changed)
            {
                EditorUtility.SetDirty(projectConfig);
                EditorUtility.SetDirty(manager);
            }
            serializedObject.ApplyModifiedProperties();
        }
        
    }
}


