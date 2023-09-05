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

using System.Collections.Generic;
using MixedReality.Toolkit;
using MixedReality.Toolkit.Input;
using MixedReality.Toolkit.Subsystems;
using Unity.Profiling;
using Unity.XR.PXR;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Scripting;
using UnityEngine.XR;
using HandJoint = Unity.XR.PXR.HandJoint;
using HandJointLocation = Unity.XR.PXR.HandJointLocation;

namespace PicoMRTK3Support.Runtime
{
#if PICO_INSTALL && (UNITY_EDITOR_WIN || UNITY_WSA || UNITY_STANDALONE_WIN || UNITY_ANDROID)
    [Preserve]
    [MRTKSubsystem(
        Name = "com.phantom.mixedreality.picohands",
        DisplayName = "Subsystem for Pico Hands API",
        Author = "Phantom",
        ProviderType = typeof(PicoProvider),
        SubsystemTypeOverride = typeof(PicoMRTKHandsSubsystem),
        ConfigType = typeof(BaseSubsystemConfig))]
#endif // MROPENXR_PRESENT
    public class PicoMRTKHandsSubsystem: HandsSubsystem
    {
#if PICO_INSTALL && (UNITY_EDITOR_WIN || UNITY_WSA || UNITY_STANDALONE_WIN || UNITY_ANDROID)
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void Register()
        {
            // Fetch subsystem metadata from the attribute.
            var cinfo = XRSubsystemHelpers.ConstructCinfo<PicoMRTKHandsSubsystem, HandsSubsystemCinfo>();

            // Populate remaining cinfo field.
            cinfo.IsPhysicalData = true;

            if (!Register(cinfo))
            {
                Debug.LogError($"Failed to register the {cinfo.Name} subsystem.");
            }
        }
#endif
        private class PicoHandContainer : HandDataContainer
        {
            public PicoHandContainer(XRNode handNode) : base(handNode)
            {
                picoHandTracker = handNode == XRNode.LeftHand ? PicoHandTracker.Left : PicoHandTracker.Right;
            }

            private readonly PicoHandTracker picoHandTracker;

            // Scratchpad for reading out hand data, to reduce allocs.
            private HandJointLocations HandJointLocations = new()
            {
                jointLocations = new HandJointLocation[PicoHandTracker.JointCount]
            };

            private static readonly ProfilerMarker TryGetEntireHandPerfMarker =
                new ProfilerMarker("[MRTK] OpenXRHandsSubsystem.TryGetEntireHand");

            /// <inheritdoc/>
            public override bool TryGetEntireHand(out IReadOnlyList<HandJointPose> result)
            {
                using (TryGetEntireHandPerfMarker.Auto())
                {
                    if (!AlreadyFullQueried)
                    {
                        TryCalculateEntireHand();
                    }

                    result = HandJoints;
                    return FullQueryValid;
                }
            }

            private static readonly ProfilerMarker TryGetJointPerfMarker =
                new ProfilerMarker("[MRTK] OpenXRHandsSubsystem.TryGetJoint");

            /// <inheritdoc/>
            public override bool TryGetJoint(TrackedHandJoint joint, out HandJointPose pose)
            {
                using (TryGetJointPerfMarker.Auto())
                {
                    bool thisQueryValid = false;
                    int index = HandsUtils.ConvertToIndex(joint);

                    // If we happened to have already queried the entire
                    // hand data this frame, we don't need to re-query for
                    // just the joint. If we haven't, we do still need to
                    // query for the single joint.
                    if (!AlreadyFullQueried)
                    {
                        if (!picoHandTracker.TryLocateHandJoints(ref HandJointLocations))
                        {
                            pose = HandJoints[index];
                            return false;
                        }

                        // Joints are relative to the camera floor offset object.
                        Transform origin = PlayspaceUtilities.XROrigin.CameraFloorOffsetObject.transform;
                        if (origin == null)
                        {
                            pose = HandJoints[index];
                            return false;
                        }


                        var jointLocation =
                            HandJointLocations.jointLocations[HandJointIndexFromTrackedHandJointIndex[index]];
                        UpdateJoint(index, jointLocation, origin);
                        thisQueryValid = true;
                    }
                    else
                    {
                        // If we've already run a full-hand query, this single joint query
                        // is just as valid as the full query.
                        thisQueryValid = FullQueryValid;
                    }

                    // Fix for Pico hand tracking
                    // Because pico hand tracking palm has not data(position and rotation).
                    // if (index == 0)
                    //     index = 1;
                    // // Fix for Pico hand tracking.
                    // // Left-handed and right-handed data are reversed
                    // if (HandNode == XRNode.LeftHand)
                    // {
                    //     var tmp_OldQuaternion = handJoints[index].Pose.rotation;
                    //     handJoints[index].Pose = new Pose(handJoints[index].Pose.position,
                    //         new Quaternion((-tmp_OldQuaternion.x) + Quaternion.Euler(180, 0, 0).x, tmp_OldQuaternion.y,
                    //             -tmp_OldQuaternion.z,
                    //             -tmp_OldQuaternion.w));
                    //     // Debug.Log(
                    //     //     $"[Pico-MRTK]-OldQ:{tmp_OldQuaternion},new:{handJoints[index].Pose.rotation},Old Up*:{tmp_OldQuaternion * Vector3.up},New UP*:{handJoints[index].Pose.rotation * Vector3.up},PoseUP:{handJoints[index].Pose.up}");
                    // }

                    pose = HandJoints[index];

                    return thisQueryValid;
                }
            }

            private static readonly ProfilerMarker TryCalculateEntireHandPerfMarker =
                new ProfilerMarker("[MRTK] OpenXRHandsSubsystem.TryCalculateEntireHand");

            /// <summary/>
            /// For a certain hand, query every Bone in the hand, and write all results to the
            /// handJoints collection. This will also mark handsQueriedThisFrame[handNode] = true.
            /// </summary>
            private void TryCalculateEntireHand()
            {
                using (TryCalculateEntireHandPerfMarker.Auto())
                {
                    if (!picoHandTracker.TryLocateHandJoints(ref HandJointLocations))
                    {
                        // No articulated hand data available this frame.
                        FullQueryValid = false;
                        AlreadyFullQueried = true;
                        return;
                    }

                    // Null checks against Unity objects can be expensive, especially when you do
                    // it 52 times per frame (26 hand joints across 2 hands). Instead, we manage
                    // the playspace transformation internally for hand joints.
                    // Joints are relative to the camera floor offset object.
                    Transform origin = PlayspaceUtilities.XROrigin.CameraFloorOffsetObject.transform;
                    if (origin == null)
                    {
                        return;
                    }

                    for (int i = 0; i < PicoHandTracker.JointCount; i++)
                    {
                        UpdateJoint(TrackedHandJointIndexFromHandJointIndex[i], HandJointLocations.jointLocations[i],
                            origin);
                    }

                    // Mark this hand as having been fully queried this frame.
                    // If any joint is queried again this frame, we'll reuse the
                    // information to avoid extra work.
                    FullQueryValid = true;
                    AlreadyFullQueried = true;
                }
            }

            private static readonly ProfilerMarker UpdateJointPerfMarker =
                new ProfilerMarker("[MRTK] OpenXRHandsSubsystem.UpdateJoint");

            /// <summary/>
            /// Given a destination jointID, apply the Bone info to the correct struct
            /// in the handJoints collection.
            /// </summary>
            private void UpdateJoint(int jointIndex, HandJointLocation handJointLocation,
                Transform playspaceTransform)
            {
                using (UpdateJointPerfMarker.Auto())
                {
                    HandJoints[jointIndex] = new HandJointPose(
                        playspaceTransform.TransformPoint(handJointLocation.pose.Position.ToVector3()),
                        playspaceTransform.rotation * handJointLocation.pose.Orientation.ToQuat(),
                        handJointLocation.radius);
                }
            }

            private static readonly int[] TrackedHandJointIndexFromHandJointIndex = new int[]
            {
                HandsUtils.ConvertToIndex(TrackedHandJoint.Palm),
                HandsUtils.ConvertToIndex(TrackedHandJoint.Wrist),

                HandsUtils.ConvertToIndex(TrackedHandJoint.ThumbMetacarpal),
                HandsUtils.ConvertToIndex(TrackedHandJoint.ThumbProximal),
                HandsUtils.ConvertToIndex(TrackedHandJoint.ThumbDistal),
                HandsUtils.ConvertToIndex(TrackedHandJoint.ThumbTip),

                HandsUtils.ConvertToIndex(TrackedHandJoint.IndexMetacarpal),
                HandsUtils.ConvertToIndex(TrackedHandJoint.IndexProximal),
                HandsUtils.ConvertToIndex(TrackedHandJoint.IndexIntermediate),
                HandsUtils.ConvertToIndex(TrackedHandJoint.IndexDistal),
                HandsUtils.ConvertToIndex(TrackedHandJoint.IndexTip),

                HandsUtils.ConvertToIndex(TrackedHandJoint.MiddleMetacarpal),
                HandsUtils.ConvertToIndex(TrackedHandJoint.MiddleProximal),
                HandsUtils.ConvertToIndex(TrackedHandJoint.MiddleIntermediate),
                HandsUtils.ConvertToIndex(TrackedHandJoint.MiddleDistal),
                HandsUtils.ConvertToIndex(TrackedHandJoint.MiddleTip),

                HandsUtils.ConvertToIndex(TrackedHandJoint.RingMetacarpal),
                HandsUtils.ConvertToIndex(TrackedHandJoint.RingProximal),
                HandsUtils.ConvertToIndex(TrackedHandJoint.RingIntermediate),
                HandsUtils.ConvertToIndex(TrackedHandJoint.RingDistal),
                HandsUtils.ConvertToIndex(TrackedHandJoint.RingTip),

                HandsUtils.ConvertToIndex(TrackedHandJoint.LittleMetacarpal),
                HandsUtils.ConvertToIndex(TrackedHandJoint.LittleProximal),
                HandsUtils.ConvertToIndex(TrackedHandJoint.LittleIntermediate),
                HandsUtils.ConvertToIndex(TrackedHandJoint.LittleDistal),
                HandsUtils.ConvertToIndex(TrackedHandJoint.LittleTip),
            };

            private static readonly int[] HandJointIndexFromTrackedHandJointIndex = new int[]
            {
                (int) HandJoint.JointPalm,
                (int) HandJoint.JointWrist,

                (int) HandJoint.JointThumbMetacarpal,
                (int) HandJoint.JointThumbProximal,
                (int) HandJoint.JointThumbDistal,
                (int) HandJoint.JointThumbTip,

                (int) HandJoint.JointIndexMetacarpal,
                (int) HandJoint.JointIndexProximal,
                (int) HandJoint.JointIndexIntermediate,
                (int) HandJoint.JointIndexDistal,
                (int) HandJoint.JointIndexTip,

                (int) HandJoint.JointMiddleMetacarpal,
                (int) HandJoint.JointMiddleProximal,
                (int) HandJoint.JointMiddleIntermediate,
                (int) HandJoint.JointMiddleDistal,
                (int) HandJoint.JointMiddleTip,

                (int) HandJoint.JointRingMetacarpal,
                (int) HandJoint.JointRingProximal,
                (int) HandJoint.JointRingIntermediate,
                (int) HandJoint.JointRingDistal,
                (int) HandJoint.JointRingTip,

                (int) HandJoint.JointLittleMetacarpal,
                (int) HandJoint.JointLittleProximal,
                (int) HandJoint.JointLittleIntermediate,
                (int) HandJoint.JointLittleDistal,
                (int) HandJoint.JointLittleTip,
            };
        }

        [Preserve]
        private class PicoProvider : Provider, IHandsSubsystem
        {
            private Dictionary<XRNode, PicoHandContainer> hands = null;

            public override void Start()
            {
                base.Start();

                hands ??= new Dictionary<XRNode, PicoHandContainer>
                {
                    {XRNode.LeftHand, new PicoHandContainer(XRNode.LeftHand)},
                    {XRNode.RightHand, new PicoHandContainer(XRNode.RightHand)}
                };

                InputSystem.onBeforeUpdate += ResetHands;
            }

            public override void Stop()
            {
                ResetHands();
                InputSystem.onBeforeUpdate -= ResetHands;
                base.Stop();
            }

            public override bool TryGetEntireHand(XRNode handNode, out IReadOnlyList<HandJointPose> jointPoses)
            {
                Debug.Assert(handNode == XRNode.LeftHand || handNode == XRNode.RightHand,
                    "Non-hand XRNode used in TryGetEntireHand query");

                return hands[handNode].TryGetEntireHand(out jointPoses);
            }

            public override bool TryGetJoint(TrackedHandJoint joint, XRNode handNode, out HandJointPose jointPose)
            {
                Debug.Assert(handNode == XRNode.LeftHand || handNode == XRNode.RightHand,
                    "Non-hand XRNode used in TryGetJoint query");

                return hands[handNode].TryGetJoint(joint, out jointPose);
            }

            private void ResetHands()
            {
                hands[XRNode.LeftHand].Reset();
                hands[XRNode.RightHand].Reset();
            }
        }
    }
}
#endif