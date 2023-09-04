// // /*===============================================================================
// // Copyright (C) 2022 PhantomsXR Ltd. All Rights Reserved.
// //
// // This file is part of the PIcoMRTK3Support.Runtime.
// //
// // The PicoMRTK3 cannot be copied, distributed, or made available to
// // third-parties for commercial purposes without written permission of PhantomsXR Ltd.
// //
// // Contact info@phantomsxr.com for licensing requests.
// // ===============================================================================*/

#if MRTK3_INSTALL
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.PXR;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

namespace PicoMRTK3Support.Runtime
{
    [Obsolete("Deprecated", true)]
    public class PicoMRTKHandVisualizer : MonoBehaviour
    {
        [SerializeField] [Tooltip("The XRNode on which this hand is located.")]
        private XRNode handNode = XRNode.LeftHand;

        public HandType HandType;

        /// <summary> The XRNode on which this hand is located. </summary>
        public XRNode HandNode
        {
            get => handNode;
            set => handNode = value;
        }

        [SerializeField]
        [Tooltip("When true, this visualizer will render rigged hands even on XR devices " +
                 "with transparent displays. When false, the rigged hands will only render " +
                 "on devices with opaque displays.")]
        private bool showHandsOnTransparentDisplays;

        private HandJointLocations handJointLocations = new HandJointLocations();

        /// <summary>
        /// When true, this visualizer will render rigged hands even on XR devices with transparent displays.
        /// When false, the rigged hands will only render on devices with opaque displays.
        /// Usually, it's recommended not to show hand visualization on transparent displays as it can
        /// distract from the user's real hands, and cause a "double image" effect that can be disconcerting.
        /// </summary>
        public bool ShowHandsOnTransparentDisplays
        {
            get => showHandsOnTransparentDisplays;
            set => showHandsOnTransparentDisplays = value;
        }

        [SerializeField] [Tooltip("The transform of the wrist joint.")]
        private Transform wrist;

        [SerializeField] [Tooltip("Renderer of the hand mesh")]
        private SkinnedMeshRenderer handRenderer = null;

        [SerializeField]
        [Tooltip("Name of the shader property used to drive pinch-amount-based visual effects. " +
                 "Generally, maps to something like a glow or an outline color!")]
        private string pinchAmountMaterialProperty = "_PinchAmount";

        // Automatically calculated over time, based on the accumulated error
        // between the user's actual joint locations and the armature's bones/joints.
        private float handScale = 1.0f;

        // The property block used to modify the pinch amount property on the material
        private MaterialPropertyBlock propertyBlock = null;

        // Caching local references 
        // private HandsAggregatorSubsystem handsSubsystem;

        // Scratch list for checking for the presence of display subsystems.
        private List<XRDisplaySubsystem> displaySubsystems = new List<XRDisplaySubsystem>();

        // The XRController that is used to determine the pinch strength (i.e., select value!)
        private XRBaseController controller;

        // The actual, physical, rigged joints that drive the skinned mesh.
        // Otherwise referred to as "armature". Must be in OpenXR order.
        public Transform[] riggedVisualJointsArray = new Transform[(int) HandJoint.JointMax];

        // The substring used to determine the "leaf joint"
        // at the end of a finger, which is discarded.
        [SerializeField] private string endJointName = "end";
#if MRTK3_INSTALL

        [ContextMenu("Config")]
        protected virtual void Awake()
        {
            propertyBlock = new MaterialPropertyBlock();

            if (handRenderer == null)
            {
                handRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
                if (handRenderer == null)
                {
                    Debug.LogWarning("RiggedHandMeshVisualizer couldn't find your rigged mesh renderer! " +
                                     "You should set it manually.");
                }
            }

            if (wrist == null)
            {
                // "Armature" is the default name that Blender assigns
                // to the root of an armature/rig. Also happens to be the wrist joint!
                wrist = transform.Find(HandType == HandType.HandRight ? "Hand_R/Armature" : "Hand_L/Armature");

                if (wrist == null)
                {
                    Debug.LogWarning("RiggedHandMeshVisualizer couldn't find the wrist joint on your hand mesh. " +
                                     "You should set it manually!");

                    // Abort initialization as we don't even have a wrist joint to go off of.
                    return;
                }
            }

            // if (riggedVisualJointsArray[riggedVisualJointsArray.Length - 1] == null)
            // {
            //     // Start the depth-first-traversal at the wrist index.
            //     int index = (int) HandJoint.JointWrist;
            //
            //     // This performs a depth-first-traversal of the armature. Ensure
            //     // the provided armature's bones/joints are in OpenXR order.
            //     foreach (Transform child in wrist.GetComponentsInChildren<Transform>())
            //     {
            //         // The "leaf joints" are excluded.
            //         if (child.name.Contains(endJointName))
            //         {
            //             continue;
            //         }
            //
            //         riggedVisualJointsArray[index++] = child;
            //     }
            // }
            //transform.Find("Axis").SetParent(wrist.GetChild(1));
        }

        protected void OnEnable()
        {
            // Ensure hand is not visible until we can update position first time.
            handRenderer.enabled = false;

            Debug.Assert(handNode == XRNode.LeftHand || handNode == XRNode.RightHand,
                $"HandVisualizer has an invalid XRNode ({handNode})!");

            //handsSubsystem = XRSubsystemHelpers.GetFirstRunningSubsystem<HandsAggregatorSubsystem>();

            if (PXR_HandTracking.GetSettingState())
            {
                StartCoroutine(EnableWhenSubsystemAvailable());
            }
        }

        protected void OnDisable()
        {
            // Disable the rigged hand renderer when this component is disabled
            handRenderer.enabled = false;
        }

        /// <summary>
        /// Coroutine to wait until subsystem becomes available.
        /// </summary>
        private IEnumerator EnableWhenSubsystemAvailable()
        {
            yield return new WaitUntil(() => !PXR_HandTracking.GetSettingState());
            OnEnable();
        }

        private HandAimState handAimState;

        private void Update()
        {
            //Debug.Log($"[Pico-MRTk]-AimComputed:{(handAimState.aimStatus & HandAimStatus.AimComputed) == 0}");
            if (!ShouldRenderHand() || (handAimState.aimStatus & HandAimStatus.AimComputed) == 0)
            {
                handRenderer.enabled = false;
                return;
            }


            handRenderer.enabled = true;

            if (PXR_HandTracking.GetJointLocations(HandType, ref handJointLocations))
            {
                if (handJointLocations.isActive == 0)
                {
                    handRenderer.enabled = false;

                    return;
                }

                transform.localScale = Vector3.one * handJointLocations.handScale;

                // Set to Wrist
                riggedVisualJointsArray[1].position =
                    handJointLocations.jointLocations[1].pose.Position.ToVector3();
                riggedVisualJointsArray[1].rotation =
                    handJointLocations.jointLocations[1].pose.Orientation.ToQuat();

                for (int i = 2; i < riggedVisualJointsArray.Length; ++i)
                {
                    if (riggedVisualJointsArray[i] == null)
                    {
                        continue;
                    }

                    if (i == (int) HandJoint.JointThumbMetacarpal)
                    {
                        Debug.Log(
                            $"JointThumbMetacarpal Rotation:{handJointLocations.jointLocations[i].pose.Orientation.ToQuat()}");
                    }

                    riggedVisualJointsArray[i].localRotation =
                        handJointLocations.jointLocations[i].pose.Orientation.ToQuat();
                }
            }

            // Update the hand material based on selectedness value
            UpdateHandMaterial();
        }

        // Computes the error between the rig's joint position and
        // the user's joint position along the finger vector.
        private float JointError(Vector3 armatureJointPosition, Vector3 userJointPosition, Vector3 fingerVector)
        {
            // The computed error between the rigged mesh's joints and the user's joints
            // is essentially the distance between the mesh and user joints, projected
            // along the forward axis of the finger itself; i.e., the "length error" of the finger.
            return Vector3.Dot((armatureJointPosition - userJointPosition), fingerVector);
        }

        private bool ShouldRenderHand()
        {
            // If we're missing anything, don't render the hand.
            if (!PXR_HandTracking.GetSettingState() || wrist == null || handRenderer == null)
            {
                return false;
            }

            if (displaySubsystems.Count == 0)
            {
                SubsystemManager.GetSubsystems(displaySubsystems);
            }

            // Are we running on an XR display and it happens to be transparent?
            // Probably shouldn't be showing rigged hands! (Users can
            // specify showHandsOnTransparentDisplays if they disagree.)
            if (displaySubsystems.Count > 0 &&
                displaySubsystems[0].running &&
                !displaySubsystems[0].displayOpaque &&
                !showHandsOnTransparentDisplays)
            {
                return false;
            }

            PXR_HandTracking.GetAimState(HandType, ref handAimState);

            // All checks out!
            return true;
        }

        private void UpdateHandMaterial()
        {
            if (controller == null)
            {
                controller = GetComponentInParent<XRBaseController>();
            }

            if (controller == null || handRenderer == null)
            {
                return;
            }

            // Update the hand material
            float pinchAmount = Mathf.Pow(controller.selectInteractionState.value, 2.0f);
            handRenderer.GetPropertyBlock(propertyBlock);
            propertyBlock.SetFloat(pinchAmountMaterialProperty, pinchAmount);
            handRenderer.SetPropertyBlock(propertyBlock);
        }
    }
}

#endif
#endif