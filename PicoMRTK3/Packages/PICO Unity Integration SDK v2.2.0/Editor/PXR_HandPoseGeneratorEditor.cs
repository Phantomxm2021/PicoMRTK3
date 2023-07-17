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

using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

namespace Unity.XR.PXR.Editor
{
    [CustomEditor(typeof(PXR_HandPoseGenerator), true)]
    public class PXR_HandPoseGeneratorEditor : UnityEditor.Editor
    {
        private bool shapesRecognizer = true;
        private bool bonesRecognizer = true;
        private bool transRecognizer = true;
        private bool thumb = true;
        private bool index = true;
        private bool middle = true;
        private bool ring = true;
        private bool pinky = true;

        private PXR_HandPoseGenerator m_Target;
        private PXR_HandPoseConfig config;

        private void OnEnable()
        {
            m_Target = (PXR_HandPoseGenerator)target;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            GUILayout.BeginHorizontal();

            GUILayout.Label("Hand Pose Config");
            m_Target.config = (PXR_HandPoseConfig)EditorGUILayout.ObjectField(m_Target.config, typeof(PXR_HandPoseConfig), false);

            if (GUILayout.Button("New"))
            {
                m_Target.config = CreateInstance<PXR_HandPoseConfig>();
                AssetDatabase.CreateAsset(m_Target.config, string.Format("Assets/{0}.asset", typeof(PXR_HandPoseConfig).Name+"_"+DateTime.Now.ToString("MMddhhmmss")));
            }

            if (GUILayout.Button("Save"))
            {
                try
                {
                    ConfigSave();

                    Debug.Log("HandPose Config Saved.");
                }
                catch (Exception e)
                {
                    Debug.LogError("Save Error "+e.ToString());
                }
                AssetDatabase.Refresh();
            }

            GUILayout.EndHorizontal();

            if (m_Target.config != null)
            {
                if (config != m_Target.config)
                {
                    config = m_Target.config;
                    ConfigRead();
                }

                shapesRecognizer = EditorGUILayout.Foldout(shapesRecognizer, "Shapes Recognizer");
                if (shapesRecognizer)
                {
                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Space(10);
                    using (new GUILayout.VerticalScope())
                    {
                        thumb = EditorGUILayout.Foldout(thumb, "Thumb");
                        if (thumb)
                        {
                            FingerConfig(m_Target.thumb);
                        }
                        index = EditorGUILayout.Foldout(index, "Index");
                        if (index)
                        {
                            FingerConfig(m_Target.index);
                        }
                        middle = EditorGUILayout.Foldout(middle, "Middle");
                        if (middle)
                        {
                            FingerConfig(m_Target.middle);
                        }
                        ring = EditorGUILayout.Foldout(ring, "Ring");
                        if (ring)
                        {
                            FingerConfig(m_Target.ring);
                        }
                        pinky = EditorGUILayout.Foldout(pinky, "Pinky");
                        if (pinky)
                        {
                            FingerConfig(m_Target.pinky);
                        }
                        EditorGUILayout.Space(5);
                        serializedObject.FindProperty("shapesholdDuration").floatValue = EditorGUILayout.FloatField("Hold Duration", Mathf.Max(0, serializedObject.FindProperty("shapesholdDuration").floatValue));
                    }
                    EditorGUILayout.EndHorizontal();

                }

                bonesRecognizer = EditorGUILayout.Foldout(bonesRecognizer, "Bones Recognizer");
                if (bonesRecognizer)
                {
                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Space(10);
                    using (new GUILayout.VerticalScope())
                    {
                        BonesConfig(m_Target.Bones);
                    }
                    EditorGUILayout.EndHorizontal();
                }

                transRecognizer = EditorGUILayout.Foldout(transRecognizer, "Transform Recognizer");
                if (transRecognizer)
                {
                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Space(10);
                    using (new GUILayout.VerticalScope())
                    {
                        TransConfig();
                    }
                    EditorGUILayout.EndHorizontal();
                }

                EditorUtility.SetDirty(m_Target.config);
                serializedObject.ApplyModifiedProperties();
            }
        }

        private void ConfigSave()
        {
            m_Target.config.shapesRecognizer.holdDuration = m_Target.shapesholdDuration;
            m_Target.config.shapesRecognizer.thumb = m_Target.thumb;
            m_Target.config.shapesRecognizer.index = m_Target.index;
            m_Target.config.shapesRecognizer.middle = m_Target.middle;
            m_Target.config.shapesRecognizer.ring = m_Target.ring;
            m_Target.config.shapesRecognizer.pinky = m_Target.pinky;

            m_Target.config.bonesRecognizer.holdDuration = m_Target.bonesHoldDuration;
            m_Target.config.bonesRecognizer.Bones = m_Target.Bones;

            m_Target.config.transRecognizer.holdDuration = m_Target.transHoldDuration;
            m_Target.config.transRecognizer.trackAxis = m_Target.trackAxis;
            m_Target.config.transRecognizer.spaceType = m_Target.spaceType;
            m_Target.config.transRecognizer.trackTarget = m_Target.trackTarget;
            m_Target.config.transRecognizer.angleThreshold = m_Target.angleThreshold;
            m_Target.config.transRecognizer.thresholdWidth = m_Target.thresholdWidth;
        }

        private void ConfigRead()
        {
            m_Target.shapesholdDuration = m_Target.config.shapesRecognizer.holdDuration;
            m_Target.thumb = m_Target.config.shapesRecognizer.thumb;
            m_Target.index = m_Target.config.shapesRecognizer.index;
            m_Target.middle = m_Target.config.shapesRecognizer.middle;
            m_Target.ring = m_Target.config.shapesRecognizer.ring;
            m_Target.pinky = m_Target.config.shapesRecognizer.pinky;

            m_Target.bonesHoldDuration = m_Target.config.bonesRecognizer.holdDuration;
            m_Target.Bones = m_Target.config.bonesRecognizer.Bones;

            m_Target.transHoldDuration = m_Target.config.transRecognizer.holdDuration;
            m_Target.trackAxis = m_Target.config.transRecognizer.trackAxis;
            m_Target.spaceType = m_Target.config.transRecognizer.spaceType;
            m_Target.trackTarget = m_Target.config.transRecognizer.trackTarget;
            m_Target.angleThreshold = m_Target.config.transRecognizer.angleThreshold;
            m_Target.thresholdWidth = m_Target.config.transRecognizer.thresholdWidth;
        }

        private void TransConfig()
        {
            EditorGUILayout.BeginHorizontal("box");
            EditorGUILayout.Space(5);
            using (new GUILayout.VerticalScope())
            {
                EditorGUILayout.Space(5);
                m_Target.trackAxis = (TransRecognizer.TrackAxis)EditorGUILayout.EnumPopup("Track Axis", m_Target.trackAxis);
                m_Target.trackTarget = (TransRecognizer.TrackTarget)EditorGUILayout.EnumPopup("Track Target", m_Target.trackTarget);
                m_Target.angleThreshold = EditorGUILayout.FloatField("Angle Threshold", m_Target.angleThreshold);
                m_Target.thresholdWidth = EditorGUILayout.FloatField("Threshold Width", m_Target.thresholdWidth);
                EditorGUILayout.Space(5);
            }
            EditorGUILayout.Space(5);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space(5);
            m_Target.transHoldDuration = EditorGUILayout.FloatField("Hold Duration", m_Target.transHoldDuration);
        }

        private void BonesConfig(List<BonesRecognizer.BonesGroup> listBones)
        {
            EditorGUILayout.BeginHorizontal("box");
            EditorGUILayout.Space(5);
            using (new GUILayout.VerticalScope())
            {
                EditorGUILayout.Space(5);

                GUI.changed = false;
                int count = EditorGUILayout.IntField("Size", listBones.Count);
                if (GUI.changed)
                {
                    if (count >= 0 && count != listBones.Count)
                    {
                        if (count > listBones.Count)
                        {
                            count = count - listBones.Count;
                            for (int i = 0; i < count; i++)
                            {
                                listBones.Add(new BonesRecognizer.BonesGroup());
                            }
                        }
                        else
                        {
                            count = listBones.Count - count;
                            for (int i = 0; i < count; i++)
                            {
                                listBones.Remove(listBones[listBones.Count-1]);
                            }
                        }
                    }
                }

                foreach (var bones in listBones)
                {
                    EditorGUILayout.LabelField("Element "+listBones.IndexOf(bones));
                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Space(10);
                    using (new GUILayout.VerticalScope())
                    {
                        bones.A_Bone = (BonesRecognizer.HandBones)EditorGUILayout.EnumPopup("Bone1", bones.A_Bone);
                        bones.B_Bone = (BonesRecognizer.HandBones)EditorGUILayout.EnumPopup("Bone2", bones.B_Bone);
                        bones.distance = EditorGUILayout.FloatField("Distance", bones.distance);
                        bones.thresholdWidth = EditorGUILayout.FloatField("Width", bones.thresholdWidth);
                    }
                    EditorGUILayout.EndHorizontal();
                }

                EditorGUILayout.Space(5);
            }
            EditorGUILayout.Space(5);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space(5);
            serializedObject.FindProperty("bonesHoldDuration").floatValue = EditorGUILayout.FloatField("Hold Duration", Mathf.Max(0, serializedObject.FindProperty("bonesHoldDuration").floatValue));
        }

        private void FingerConfig(ShapesRecognizer.Finger finger)
        {
            EditorGUILayout.BeginHorizontal("box");
            EditorGUILayout.Space(5);
            using (new GUILayout.VerticalScope())
            {
                EditorGUILayout.Space(5);
                FlexionConfig(finger, finger.fingerConfigs.flexionConfigs);
                CurlConfig(finger, finger.fingerConfigs.curlConfigs);
                AbductionConfig(finger, finger.fingerConfigs.abductionConfigs);
                EditorGUILayout.Space(5);
            }
            EditorGUILayout.Space(5);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space(5);
        }

        private void FlexionConfig(ShapesRecognizer.Finger finger, ShapesRecognizer.RangeConfigs flexionConfigs)
        {
            finger.flexion = (ShapesRecognizer.Flexion)EditorGUILayout.EnumPopup("Flexion", finger.flexion);
            Vector2 defaultVal = new Vector2();
            switch (finger.flexion)
            {
                case ShapesRecognizer.Flexion.Any:
                    return;
                case ShapesRecognizer.Flexion.Open:
                    defaultVal = GetDefaultShapeVal(finger.handFinger, ShapesRecognizer.ShapeType.flexion, true);
                    flexionConfigs.min = defaultVal.x;
                    flexionConfigs.max = defaultVal.y;
                    break;
                case ShapesRecognizer.Flexion.Close:
                    defaultVal = GetDefaultShapeVal(finger.handFinger, ShapesRecognizer.ShapeType.flexion, false);
                    flexionConfigs.min = defaultVal.x;
                    flexionConfigs.max = defaultVal.y;
                    break;
                case ShapesRecognizer.Flexion.Custom:
                    EditorGUILayout.MinMaxSlider("Custom Range",
                        ref flexionConfigs.min,
                        ref flexionConfigs.max,
                        ShapesRecognizer.flexionMin,
                        ShapesRecognizer.flexionMax);
                    break;
                default:
                    break;
            }
            flexionConfigs.width = EditorGUILayout.Slider("Width", flexionConfigs.width, 0,
                ShapesRecognizer.flexionMax - ShapesRecognizer.flexionMin);
            EditorGUILayout.LabelField(new GUIContent("Flexion Range"),
                new GUIContent($"[{flexionConfigs.min+" - "+flexionConfigs.width}, {flexionConfigs.max+" + "+flexionConfigs.width}]"));
        }

        private void CurlConfig(ShapesRecognizer.Finger finger, ShapesRecognizer.RangeConfigs curlConfigs)
        {
            finger.curl = (ShapesRecognizer.Curl)EditorGUILayout.EnumPopup("Curl", finger.curl);
            Vector2 defaultVal = new Vector2();
            switch (finger.curl)
            {
                case ShapesRecognizer.Curl.Any:
                    return;
                case ShapesRecognizer.Curl.Open:
                    defaultVal = GetDefaultShapeVal(finger.handFinger, ShapesRecognizer.ShapeType.curl, true);
                    curlConfigs.min = defaultVal.x;
                    curlConfigs.max = defaultVal.y;
                    break;
                case ShapesRecognizer.Curl.Close:
                    defaultVal = GetDefaultShapeVal(finger.handFinger, ShapesRecognizer.ShapeType.curl, false);
                    curlConfigs.min = defaultVal.x;
                    curlConfigs.max = defaultVal.y;
                    break;
                case ShapesRecognizer.Curl.Custom:
                    EditorGUILayout.MinMaxSlider("Custom Range",
                        ref curlConfigs.min,
                        ref curlConfigs.max,
                        finger.handFinger == HandFinger.Thumb ? ShapesRecognizer.curlThumbMin : ShapesRecognizer.curlMin,
                        finger.handFinger == HandFinger.Thumb ? ShapesRecognizer.curlThumbMax : ShapesRecognizer.curlMax);
                    break;
                default:
                    break;
            }
            curlConfigs.width = EditorGUILayout.Slider("Width", curlConfigs.width, 0,
                 ShapesRecognizer.curlMax- ShapesRecognizer.curlMin);
            EditorGUILayout.LabelField(new GUIContent("Curl Range"),
                new GUIContent($"[{curlConfigs.min+" - "+curlConfigs.width}, {curlConfigs.max+" + "+curlConfigs.width}]"));
        }

        private void AbductionConfig(ShapesRecognizer.Finger finger, ShapesRecognizer.RangeConfigsAbduction abductionConfigs)
        {
            if (finger.handFinger == HandFinger.Pinky) return;

            finger.abduction = (ShapesRecognizer.Abduction)EditorGUILayout.EnumPopup("Abduction", finger.abduction);
            Vector2 defaultVal = new Vector2();
            defaultVal = GetDefaultShapeVal(finger.handFinger, ShapesRecognizer.ShapeType.abduction);
            abductionConfigs.mid = defaultVal.x;
            switch (finger.abduction)
            {
                case ShapesRecognizer.Abduction.Any:
                    return;
                case ShapesRecognizer.Abduction.Open:
                    break;
                case ShapesRecognizer.Abduction.Close:
                    break;
                default:
                    break;
            }
            abductionConfigs.width = EditorGUILayout.Slider("Width", abductionConfigs.width, 0,
                ShapesRecognizer.abductionMax - ShapesRecognizer.abductionMin);
            EditorGUILayout.LabelField(new GUIContent("Abduction Range"),
                new GUIContent($"[{abductionConfigs.mid+" ± "+abductionConfigs.width+"/2"}]"));
        }

        private Vector2 GetDefaultShapeVal(HandFinger finger, ShapesRecognizer.ShapeType shapeType, bool isOpen = true)
        {
            Vector2 val = new Vector2();
            switch (shapeType)
            {
                case ShapesRecognizer.ShapeType.flexion:
                    val.x = finger == HandFinger.Thumb ? (isOpen ? ShapesRecognizer.flexionThumbOpenMin : ShapesRecognizer.flexionThumbCloseMin) :
                        (isOpen ? ShapesRecognizer.flexionOpenMin : ShapesRecognizer.flexionCloseMin);
                    val.y = finger == HandFinger.Thumb ? (isOpen ? ShapesRecognizer.flexionThumbOpenMax : ShapesRecognizer.flexionThumbCloseMax) :
                        (isOpen ? ShapesRecognizer.flexionOpenMax : ShapesRecognizer.flexionCloseMax);
                    break;
                case ShapesRecognizer.ShapeType.curl:
                    val.x = finger == HandFinger.Thumb ? (isOpen ? ShapesRecognizer.curlThumbOpenMin : ShapesRecognizer.curlThumbCloseMin) :
                        (isOpen ? ShapesRecognizer.curlOpenMin : ShapesRecognizer.curlCloseMin);
                    val.y = finger == HandFinger.Thumb ? (isOpen ? ShapesRecognizer.curlThumbOpenMax : ShapesRecognizer.curlThumbCloseMax) :
                        (isOpen ? ShapesRecognizer.curlOpenMax : ShapesRecognizer.curlCloseMax);
                    break;
                case ShapesRecognizer.ShapeType.abduction:
                    val.x = finger == HandFinger.Thumb ? ShapesRecognizer.abductionThumbMid : ShapesRecognizer.abductionMid;
                    val.y = finger == HandFinger.Thumb ? ShapesRecognizer.abductionThumbWidth : ShapesRecognizer.abductionWidth;
                    break;
            }
            return val;
        }
    }

    [CustomPropertyDrawer(typeof(DisplayOnly))]
    public class DisplayOnlyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = true;
        }
    }
}