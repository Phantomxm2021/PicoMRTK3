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

using UnityEngine;
using UnityEditor;
using System;
using Unity.XR.PXR;
using System.Collections.Generic;

[CustomEditor(typeof(PXR_HandPoseEditor))]
public class PXR_HandPoseEditors : Editor
{
    bool shapesRecognizer = true;
    bool bonesRecognizer = true;
    bool thumb = true, index = true, middle = true, ring = true, pinky = true;
    public override void OnInspectorGUI()
    {
        PXR_HandPoseEditor m_Target = (PXR_HandPoseEditor)target;

        GUILayout.BeginHorizontal();
        GUILayout.Label("Hand Pose Config");
        m_Target.config = EditorGUILayout.ObjectField(m_Target.config, typeof(PXR_HandPoseConfig), true) as PXR_HandPoseConfig;
        if (GUILayout.Button("New"))
        {
            m_Target.config = CreateInstance<PXR_HandPoseConfig>();
            AssetDatabase.CreateAsset(m_Target.config, string.Format("Assets/{0}.asset", typeof(PXR_HandPoseConfig).Name+"_"+DateTime.Now.ToString("MMddhhmmss")));
        }
        if (GUILayout.Button("Save"))
        {
            if (m_Target.config != null) m_Target.config.bonesRecognizer = m_Target.bonesRecognizer;
            AssetDatabase.Refresh();
        }
        GUILayout.EndHorizontal();

        if (m_Target.config != null)
        {
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
                        FingerConfig(ref m_Target.config.shapesRecognizer.thumb);
                    }
                    index = EditorGUILayout.Foldout(index, "Index");
                    if (index)
                    {
                        FingerConfig(ref m_Target.config.shapesRecognizer.index);
                    }
                    middle = EditorGUILayout.Foldout(middle, "Middle");
                    if (middle)
                    {
                        FingerConfig(ref m_Target.config.shapesRecognizer.middle);
                    }
                    ring = EditorGUILayout.Foldout(ring, "Ring");
                    if (ring)
                    {
                        FingerConfig(ref m_Target.config.shapesRecognizer.ring);
                    }
                    pinky = EditorGUILayout.Foldout(pinky, "Pinky");
                    if (pinky)
                    {
                        FingerConfig(ref m_Target.config.shapesRecognizer.pinky);
                    }
                }
                EditorGUILayout.EndHorizontal();

            }

            bonesRecognizer = EditorGUILayout.Foldout(bonesRecognizer, "Bones Recognizer");
            if (bonesRecognizer)
            {
                EditorGUILayout.LabelField("List Bones");
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(10);
                using (new GUILayout.VerticalScope())
                {
                    BonesConfig(ref m_Target.bonesRecognizer.listBones);
                }
                EditorGUILayout.EndHorizontal();
            }

            EditorUtility.SetDirty(m_Target.config);
        }
    }

    private void BonesConfig(ref List<PXR_HandPoseConfig.BonesRecognizer.Bones> listBones)
    {
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
                        listBones.Add(new PXR_HandPoseConfig.BonesRecognizer.Bones(PXR_HandPoseConfig.BonesRecognizer.Distance.None));
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
                bones.bone1 = (HandJoint)EditorGUILayout.EnumPopup("Bone1", bones.bone1);
                bones.bone2 = (HandJoint)EditorGUILayout.EnumPopup("Bone2", bones.bone2);
                bones.distance = (PXR_HandPoseConfig.BonesRecognizer.Distance)EditorGUILayout.EnumPopup("Distance", bones.distance);
                switch (bones.distance)
                {
                    case PXR_HandPoseConfig.BonesRecognizer.Distance.None:
                        bones.bonesConfigs.min = 0;
                        bones.bonesConfigs.max = 0;
                        break;
                    case PXR_HandPoseConfig.BonesRecognizer.Distance.Near:
                        bones.bonesConfigs.min = PXR_HandPoseConfig.BonesRecognizer.bonesNearMin;
                        bones.bonesConfigs.max = PXR_HandPoseConfig.BonesRecognizer.bonesNearMax;
                        break;
                    case PXR_HandPoseConfig.BonesRecognizer.Distance.Touching:
                        bones.bonesConfigs.min = PXR_HandPoseConfig.BonesRecognizer.bonesTouchingMin;
                        bones.bonesConfigs.max = PXR_HandPoseConfig.BonesRecognizer.bonesTouchingMax;
                        break;
                    default:
                        break;
                }
                bones.bonesConfigs.width = EditorGUILayout.Slider("Width", bones.bonesConfigs.width, 0,
                    WidthRange(bones.bonesConfigs.min, bones.bonesConfigs.max, PXR_HandPoseConfig.BonesRecognizer.bonesNearMax));

                EditorGUILayout.LabelField(new GUIContent("Distance Range"),
                    new GUIContent(RangeView(bones.bonesConfigs.width, bones.bonesConfigs.max, bones.bonesConfigs.min,
                    PXR_HandPoseConfig.BonesRecognizer.bonesNearMax, PXR_HandPoseConfig.BonesRecognizer.bonesTouchingMin)));
            }
            EditorGUILayout.EndHorizontal();
        }
    }

    private float WidthRange(float min, float max, float limit)
    {
        float widthMax;

        widthMax = max - min;

        return widthMax;
    }

    private string RangeView(float width, float max, float min, float rangeMax, float rangeMin)
    {
        string flexionRangeMin;
        if (min - (width / 2) >= rangeMin)
        {
            flexionRangeMin = $"{min - (width /2)}";
        }
        else
        {
            flexionRangeMin = $"{rangeMin}";
        }

        string flexionRangeMax;
        if (max + (width / 2) <= rangeMax)
        {
            flexionRangeMax = $"{max + (width /2)}";
        }
        else
        {
            flexionRangeMax = $"{rangeMax}";
        }
        return $"[{min+(width / 2)}~{max-(width/2)}],[{flexionRangeMin}~{flexionRangeMax}]";
    }

    private void FingerConfig(ref PXR_HandPoseConfig.ShapesRecognizer.Finger finger)
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(10);
        using (new GUILayout.VerticalScope())
        {
            FlexionConfig(ref finger.flexion, ref finger.fingerConfigs.flexionConfigs);
            CurlConfig(ref finger.curl, ref finger.fingerConfigs.curlConfigs);
            AbductionConfig(finger.handFinger, ref finger.abduction, ref finger.fingerConfigs.abductionConfigs);
        }
        EditorGUILayout.EndHorizontal();
    }

    private void FlexionConfig(ref PXR_HandPoseConfig.ShapesRecognizer.Flexion flexion, ref PXR_HandPoseConfig.RangeConfigs flexionConfigs)
    {
        flexion = (PXR_HandPoseConfig.ShapesRecognizer.Flexion)EditorGUILayout.EnumPopup("Flexion", flexion);
        switch (flexion)
        {
            case PXR_HandPoseConfig.ShapesRecognizer.Flexion.Any:
                return;
            case PXR_HandPoseConfig.ShapesRecognizer.Flexion.Open:
                flexionConfigs.min = PXR_HandPoseConfig.ShapesRecognizer.flexionOpenMin;
                flexionConfigs.max = PXR_HandPoseConfig.ShapesRecognizer.flexionOpenMax;
                break;
            case PXR_HandPoseConfig.ShapesRecognizer.Flexion.Close:
                flexionConfigs.min = PXR_HandPoseConfig.ShapesRecognizer.flexionCloseMin;
                flexionConfigs.max = PXR_HandPoseConfig.ShapesRecognizer.flexionCloseMax;
                break;
            case PXR_HandPoseConfig.ShapesRecognizer.Flexion.Custom:
                EditorGUILayout.MinMaxSlider("Custom Range",
                    ref flexionConfigs.min,
                    ref flexionConfigs.max,
                    PXR_HandPoseConfig.ShapesRecognizer.flexionOpenMin,
                    PXR_HandPoseConfig.ShapesRecognizer.flexionCloseMax);
                break;
            default:
                break;
        }
        flexionConfigs.width = EditorGUILayout.Slider("Width", flexionConfigs.width, 0,
            WidthRange(flexionConfigs.min, flexionConfigs.max, PXR_HandPoseConfig.ShapesRecognizer.flexionCloseMax));

        flexionConfigs.min = Convert.ToInt16(flexionConfigs.min);
        flexionConfigs.max = Convert.ToInt16(flexionConfigs.max);
        flexionConfigs.width = Convert.ToInt16(flexionConfigs.width);

        EditorGUILayout.LabelField(new GUIContent("Flexion Range"),
            new GUIContent(RangeView(flexionConfigs.width, flexionConfigs.max, flexionConfigs.min,
            PXR_HandPoseConfig.ShapesRecognizer.flexionCloseMax, PXR_HandPoseConfig.ShapesRecognizer.flexionOpenMin)));
    }

    private void CurlConfig(ref PXR_HandPoseConfig.ShapesRecognizer.Curl curl, ref PXR_HandPoseConfig.RangeConfigs curlConfigs)
    {
        curl = (PXR_HandPoseConfig.ShapesRecognizer.Curl)EditorGUILayout.EnumPopup("Curl", curl);
        switch (curl)
        {
            case PXR_HandPoseConfig.ShapesRecognizer.Curl.Any:
                return;
            case PXR_HandPoseConfig.ShapesRecognizer.Curl.Open:
                curlConfigs.min = PXR_HandPoseConfig.ShapesRecognizer.curlOpenMin;
                curlConfigs.max = PXR_HandPoseConfig.ShapesRecognizer.curlOpenMax;
                break;
            case PXR_HandPoseConfig.ShapesRecognizer.Curl.Close:
                curlConfigs.min = PXR_HandPoseConfig.ShapesRecognizer.curlCloseMin;
                curlConfigs.max = PXR_HandPoseConfig.ShapesRecognizer.curlCloseMax;
                break;
            case PXR_HandPoseConfig.ShapesRecognizer.Curl.Custom:
                EditorGUILayout.MinMaxSlider("Custom Range",
                    ref curlConfigs.min,
                    ref curlConfigs.max,
                    PXR_HandPoseConfig.ShapesRecognizer.curlOpenMin,
                    PXR_HandPoseConfig.ShapesRecognizer.curlCloseMax);
                break;
            default:
                break;
        }
        curlConfigs.width = EditorGUILayout.Slider("Width", curlConfigs.width, 0,
            WidthRange(curlConfigs.min, curlConfigs.max, PXR_HandPoseConfig.ShapesRecognizer.curlCloseMax));

        curlConfigs.min = Convert.ToInt16(curlConfigs.min);
        curlConfigs.max = Convert.ToInt16(curlConfigs.max);
        curlConfigs.width = Convert.ToInt16(curlConfigs.width);

        EditorGUILayout.LabelField(new GUIContent("Curl Range"),
            new GUIContent(RangeView(curlConfigs.width, curlConfigs.max, curlConfigs.min,
            PXR_HandPoseConfig.ShapesRecognizer.curlCloseMax, PXR_HandPoseConfig.ShapesRecognizer.curlOpenMin)));
    }

    private void AbductionConfig(HandFinger finger, ref PXR_HandPoseConfig.ShapesRecognizer.Abduction abduction, ref PXR_HandPoseConfig.RangeConfigs abductionConfigs)
    {
        if (finger == HandFinger.Pinky) return;

        abduction = (PXR_HandPoseConfig.ShapesRecognizer.Abduction)EditorGUILayout.EnumPopup("Abduction", abduction);
        switch (abduction)
        {
            case PXR_HandPoseConfig.ShapesRecognizer.Abduction.Any:
                return;
            case PXR_HandPoseConfig.ShapesRecognizer.Abduction.Open:
                abductionConfigs.min = finger == HandFinger.Thumb ?
                    PXR_HandPoseConfig.ShapesRecognizer.abductionThumbCloseMax : PXR_HandPoseConfig.ShapesRecognizer.abductionCloseMax;
                abductionConfigs.max = PXR_HandPoseConfig.ShapesRecognizer.abductionOpenMax;
                break;
            case PXR_HandPoseConfig.ShapesRecognizer.Abduction.Close:
                abductionConfigs.min = PXR_HandPoseConfig.ShapesRecognizer.abductionCloseMin;
                abductionConfigs.max = finger == HandFinger.Thumb ?
                    PXR_HandPoseConfig.ShapesRecognizer.abductionThumbCloseMax : PXR_HandPoseConfig.ShapesRecognizer.abductionCloseMax;
                break;
            default:
                break;
        }
        abductionConfigs.width = EditorGUILayout.Slider("Width", abductionConfigs.width, 0,
            WidthRange(abductionConfigs.min, abductionConfigs.max, PXR_HandPoseConfig.ShapesRecognizer.abductionOpenMax));

        abductionConfigs.width = Convert.ToInt16(abductionConfigs.width);

        EditorGUILayout.LabelField(new GUIContent("Abduction Range"),
            new GUIContent(RangeView(abductionConfigs.width, abductionConfigs.max, abductionConfigs.min,
            PXR_HandPoseConfig.ShapesRecognizer.abductionOpenMax, PXR_HandPoseConfig.ShapesRecognizer.abductionCloseMin)));
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
