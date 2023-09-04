// // /*===============================================================================
// // Copyright (C) 2022 PhantomsXR Ltd. All Rights Reserved.
// //
// // This file is part of the PIcoMRTK3Support.Editor.
// //
// // The PicoMRTK3 cannot be copied, distributed, or made available to
// // third-parties for commercial purposes without written permission of PhantomsXR Ltd.
// //
// // Contact info@phantomsxr.com for licensing requests.
// // ===============================================================================*/

#if MRTK3_INSTALL
using PicoMRTK3Support.Runtime;
using Unity.XR.PXR;
using UnityEditor;
using UnityEngine;

namespace PicoMRTK3Support.Editor
{
    // [CustomEditor(typeof(PicoMRTKHandVisualizer))]
    // public class PXR_MRTKHandEditor : UnityEditor.Editor
    // {
    //     public override void OnInspectorGUI()
    //     {
    //         DrawDefaultInspector();
    //         serializedObject.ApplyModifiedProperties();
    //
    //         PicoMRTKHandVisualizer hand = (PicoMRTKHandVisualizer) target;
    //
    //         EditorGUILayout.LabelField("Hand Joints", EditorStyles.boldLabel);
    //
    //         for (int i = 0; i < (int) HandJoint.JointMax; i++)
    //         {
    //             string jointName = ((HandJoint) i).ToString();
    //             hand.riggedVisualJointsArray[i] =
    //                 (Transform) EditorGUILayout.ObjectField(jointName, hand.riggedVisualJointsArray[i],
    //                     typeof(Transform), true);
    //         }
    //     }
    // }
}
#endif