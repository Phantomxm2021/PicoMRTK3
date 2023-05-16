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

using UnityEditor;
using UnityEngine;


namespace Unity.XR.PXR.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(PXR_OverLay))]
    public class PXR_OverLayEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var guiContent = new GUIContent();
            foreach (PXR_OverLay overlayTarget in targets)
            {
                EditorGUILayout.LabelField("Overlay Settings", EditorStyles.boldLabel);

                EditorGUILayout.BeginVertical("frameBox");
                guiContent.text = "Type";
                overlayTarget.overlayType = (PXR_OverLay.OverlayType)EditorGUILayout.EnumPopup(guiContent, overlayTarget.overlayType);
                guiContent.text = "Shape";
                overlayTarget.overlayShape = (PXR_OverLay.OverlayShape)EditorGUILayout.EnumPopup(guiContent, overlayTarget.overlayShape);
                guiContent.text = "Depth";
                overlayTarget.layerDepth = EditorGUILayout.IntField(guiContent, overlayTarget.layerDepth);

                EditorGUILayout.EndVertical();

                EditorGUILayout.Separator();
                EditorGUILayout.LabelField("Overlay Textures", EditorStyles.boldLabel);
                guiContent.text = "Texture Type";
                overlayTarget.textureType = (PXR_OverLay.TextureType)EditorGUILayout.EnumPopup(guiContent, overlayTarget.textureType);
                EditorGUILayout.Separator();

                if (overlayTarget.textureType == PXR_OverLay.TextureType.ExternalSurface)
                {
                    overlayTarget.isExternalAndroidSurface = true;
                    overlayTarget.isDynamic = false;
                }
                else if (overlayTarget.textureType == PXR_OverLay.TextureType.DynamicTexture)
                {
                    overlayTarget.isExternalAndroidSurface = false;
                    overlayTarget.isDynamic = true;
                }
                else
                {
                    overlayTarget.isExternalAndroidSurface = false;
                    overlayTarget.isDynamic = false;
                }

                if (overlayTarget.isExternalAndroidSurface)
                {
                    EditorGUILayout.BeginVertical("frameBox");
                    guiContent.text = "DRM";
                    overlayTarget.isExternalAndroidSurfaceDRM = EditorGUILayout.Toggle(guiContent, overlayTarget.isExternalAndroidSurfaceDRM);

                    guiContent.text = "3D Surface Type";
                    overlayTarget.externalAndroidSurface3DType = (PXR_OverLay.Surface3DType)EditorGUILayout.EnumPopup(guiContent, overlayTarget.externalAndroidSurface3DType);
                    EditorGUILayout.EndVertical();
                }
                else
                {
                    EditorGUILayout.LabelField("Texture");
                    EditorGUILayout.BeginVertical("frameBox");

                    var labelControlRect = EditorGUILayout.GetControlRect();
                    EditorGUI.LabelField(new Rect(labelControlRect.x, labelControlRect.y, labelControlRect.width / 2, labelControlRect.height), new GUIContent("Left", "Texture used for the left eye"));
                    EditorGUI.LabelField(new Rect(labelControlRect.x + labelControlRect.width / 2, labelControlRect.y, labelControlRect.width / 2, labelControlRect.height), new GUIContent("Right", "Texture used for the right eye"));

                    var textureControlRect = EditorGUILayout.GetControlRect(GUILayout.Height(64));
                    overlayTarget.layerTextures[0] = (Texture)EditorGUI.ObjectField(new Rect(textureControlRect.x, textureControlRect.y, 64, textureControlRect.height), overlayTarget.layerTextures[0], typeof(Texture), false);
                    overlayTarget.layerTextures[1] = (Texture)EditorGUI.ObjectField(new Rect(textureControlRect.x + textureControlRect.width / 2, textureControlRect.y, 64, textureControlRect.height), overlayTarget.layerTextures[1] != null ? overlayTarget.layerTextures[1] : overlayTarget.layerTextures[0], typeof(Texture), false);

                    EditorGUILayout.EndVertical();

                }
                EditorGUILayout.Separator();

                if (overlayTarget.overlayShape == PXR_OverLay.OverlayShape.Equirect)
                {
                    guiContent.text = "Radius";
                    overlayTarget.radius = EditorGUILayout.FloatField(guiContent, Mathf.Abs(overlayTarget.radius));
                }

                if (overlayTarget.overlayShape == PXR_OverLay.OverlayShape.Quad || overlayTarget.overlayShape == PXR_OverLay.OverlayShape.Cylinder || overlayTarget.overlayShape == PXR_OverLay.OverlayShape.Equirect)
                {
                    guiContent.text = "Texture Rects";
                    overlayTarget.useImageRect = EditorGUILayout.Toggle(guiContent, overlayTarget.useImageRect);
                    if (overlayTarget.useImageRect)
                    {
                        guiContent.text = "Source Rects";
                        overlayTarget.textureRect = (PXR_OverLay.TextureRect)EditorGUILayout.EnumPopup(guiContent, overlayTarget.textureRect);

                        if (overlayTarget.textureRect == PXR_OverLay.TextureRect.Custom)
                        {
                            EditorGUILayout.BeginVertical("frameBox");

                            EditorGUILayout.BeginHorizontal();
                            EditorGUILayout.LabelField("Left Rect");
                            EditorGUILayout.LabelField("Right Rect");
                            EditorGUILayout.EndHorizontal();

                            EditorGUILayout.BeginHorizontal();
                            overlayTarget.srcRectLeft = ClampRect(EditorGUILayout.RectField(overlayTarget.srcRectLeft));
                            EditorGUILayout.Space(15);
                            guiContent.text = "Right";
                            overlayTarget.srcRectRight = ClampRect(EditorGUILayout.RectField(overlayTarget.srcRectRight));
                            EditorGUILayout.EndHorizontal();

                            EditorGUILayout.EndVertical();
                            EditorGUILayout.Space();
                        }
                        else if (overlayTarget.textureRect == PXR_OverLay.TextureRect.MonoScopic)
                        {
                            overlayTarget.srcRectLeft = new Rect(0, 0, 1, 1);
                            overlayTarget.srcRectRight = new Rect(0, 0, 1, 1);
                        }
                        else if (overlayTarget.textureRect == PXR_OverLay.TextureRect.StereoScopic)
                        {
                            overlayTarget.srcRectLeft = new Rect(0, 0, 0.5f, 1);
                            overlayTarget.srcRectRight = new Rect(0.5f, 0, 0.5f, 1);
                        }

                        if (overlayTarget.overlayShape == PXR_OverLay.OverlayShape.Quad || overlayTarget.overlayShape == PXR_OverLay.OverlayShape.Equirect)
                        {
                            guiContent.text = "Destination Rects";
                            overlayTarget.destinationRect = (PXR_OverLay.DestinationRect)EditorGUILayout.EnumPopup(guiContent, overlayTarget.destinationRect);

                            if (overlayTarget.destinationRect == PXR_OverLay.DestinationRect.Custom)
                            {
                                EditorGUILayout.BeginVertical("frameBox");

                                EditorGUILayout.BeginHorizontal();
                                EditorGUILayout.LabelField("Left Rect");
                                EditorGUILayout.LabelField("Right Rect");
                                EditorGUILayout.EndHorizontal();

                                EditorGUILayout.BeginHorizontal();
                                overlayTarget.dstRectLeft = ClampRect(EditorGUILayout.RectField(overlayTarget.dstRectLeft));
                                EditorGUILayout.Space(15);
                                guiContent.text = "Right";
                                overlayTarget.dstRectRight = ClampRect(EditorGUILayout.RectField(overlayTarget.dstRectRight));
                                EditorGUILayout.EndHorizontal();

                                EditorGUILayout.EndVertical();
                                EditorGUILayout.Space();
                            }
                            else
                            {
                                overlayTarget.dstRectLeft = new Rect(0, 0, 1, 1);
                                overlayTarget.dstRectRight = new Rect(0, 0, 1, 1);
                            }
                        }
                    }
                }

                guiContent.text = "Layer Blend";
                overlayTarget.useLayerBlend = EditorGUILayout.Toggle(guiContent, overlayTarget.useLayerBlend);
                if (overlayTarget.useLayerBlend)
                {
                    EditorGUILayout.BeginVertical("frameBox");
                    guiContent.text = "Src Color";
                    overlayTarget.srcColor = (PxrBlendFactor)EditorGUILayout.EnumPopup(guiContent, overlayTarget.srcColor);
                    guiContent.text = "Dst Color";
                    overlayTarget.dstColor = (PxrBlendFactor)EditorGUILayout.EnumPopup(guiContent, overlayTarget.dstColor);
                    guiContent.text = "Src Alpha";
                    overlayTarget.srcAlpha = (PxrBlendFactor)EditorGUILayout.EnumPopup(guiContent, overlayTarget.srcAlpha);
                    guiContent.text = "Dst Alpha";
                    overlayTarget.dstAlpha = (PxrBlendFactor)EditorGUILayout.EnumPopup(guiContent, overlayTarget.dstAlpha);

                    EditorGUILayout.EndVertical();
                }

                guiContent.text = "Override Color Scale";
                overlayTarget.overrideColorScaleAndOffset = EditorGUILayout.Toggle(guiContent, overlayTarget.overrideColorScaleAndOffset);
                if (overlayTarget.overrideColorScaleAndOffset)
                {
                    EditorGUILayout.BeginVertical("frameBox");

                    guiContent.text = "Scale";
                    Vector4 colorScale = EditorGUILayout.Vector4Field(guiContent, overlayTarget.colorScale);

                    guiContent.text = "Offset";
                    Vector4 colorOffset = EditorGUILayout.Vector4Field(guiContent, overlayTarget.colorOffset);
                    overlayTarget.SetLayerColorScaleAndOffset(colorScale, colorOffset);

                    EditorGUILayout.EndVertical();
                }
            }

            if (GUI.changed)
            {
                UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(UnityEngine.SceneManagement.SceneManager.GetActiveScene());
            }
        }
        private Rect ClampRect(Rect rect)
        {
            rect.x = Mathf.Clamp01(rect.x);
            rect.y = Mathf.Clamp01(rect.y);
            rect.width = Mathf.Clamp01(rect.width);
            rect.height = Mathf.Clamp01(rect.height);
            return rect;
        }
    }
}
