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
using System.Collections.Generic;
using MixedReality.Toolkit;
using MixedReality.Toolkit.Input;
using MixedReality.Toolkit.Subsystems;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Scripting;
using UnityEngine.XR;

namespace PicoMRTK3Support.Runtime
{
    [Preserve]
    [MRTKSubsystem(
        Name = "com.phantom.mixedreality.hands",
        DisplayName = "Pico MRTK Hands Aggregator Subsystem",
        Author = "Phantom",
        ProviderType = typeof(PicoMRTKAggregator),
        SubsystemTypeOverride = typeof(PicoMRTKHandsAggregatorSubsystem),
        ConfigType = typeof(MRTKHandsAggregatorConfig))]
    public class PicoMRTKHandsAggregatorSubsystem : HandsAggregatorSubsystem
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void Register()
        {
            // Fetch subsystem metadata from the attribute.
            var cinfo = XRSubsystemHelpers.ConstructCinfo<PicoMRTKHandsAggregatorSubsystem, MRTKSubsystemCinfo>();

            if (!Register(cinfo))
            {
                Debug.LogError($"Failed to register the {cinfo.Name} subsystem.");
            }

            Debug.Log($"[PICO-MRTK]{cinfo.Name}-PicoMRTKHandsAggregatorSubsystem Register.");
        }

        private class PicoAggregateHandContainer : HandDataContainer
        {
            // List of hands subsystems we will scrape hands data from.
            private List<HandsSubsystem> handsSubsystems = new List<HandsSubsystem>();

            // Cached index finger length.
            private float? indexFingerLength;

            public PicoAggregateHandContainer(XRNode handNode) : base(handNode)
            {
                SubsystemManager.GetSubsystems(handsSubsystems);
            }

            private static readonly ProfilerMarker TryGetEntireHandPerfMarker =
                new ProfilerMarker("[PicoMRTK] PicoMRTKHandsAggregatorSubsystem.TryGetEntireHand");

            /// <inheritdoc/>
            public override bool TryGetEntireHand(out IReadOnlyList<HandJointPose> joints)
            {
                using (TryGetEntireHandPerfMarker.Auto())
                {
                    if (!AlreadyFullQueried)
                    {
                        bool gotPhysicalData = false;
                        foreach (var sys in handsSubsystems)
                        {
                            // Don't scrape subsystems that aren't running.
                            if (sys.running == false)
                            {
                                Debug.Log($"[Pico-MRTK]-{sys.subsystemDescriptor.Name} is not running");
                                continue;
                            }

                            var tmp_EntireHand = sys.TryGetEntireHand(HandNode, out IReadOnlyList<HandJointPose> data);
                            if (tmp_EntireHand)
                            {
                                // If we get valid physical data, we will ignore any subsequent
                                // hands subsystems that are *not* physical data.
                                gotPhysicalData |= sys.subsystemDescriptor.IsPhysicalData;

                                // If we get a valid hand sample from at least one of the
                                // subsystems we're listening to, we can consider ourselves
                                // to have valid data this frame.
                                FullQueryValid |= true;
                                Array.Copy((HandJointPose[]) data, HandJoints, (int) TrackedHandJoint.TotalJoints);
                            }

                            if (gotPhysicalData)
                            {
                                break;
                            }
                        }

                        // Mark this hand as having been fully queried this frame.
                        // If any joint is queried again this frame, we'll reuse the
                        // information to avoid extra work.
                        AlreadyFullQueried = true;
                    }

                    joints = HandJoints;
                    return FullQueryValid;
                }
            }

            private static readonly ProfilerMarker TryGetJointPerfMarker =
                new ProfilerMarker("[PicoMRTK] MRTKHandsAggregatorSubsystem.TryGetJoint");

            /// <inheritdoc/>
            public override bool TryGetJoint(TrackedHandJoint joint, out HandJointPose pose)
            {
                using (TryGetJointPerfMarker.Auto())
                {
                    bool thisQueryValid = false;

                    int jointIndex = (int) joint;

                    // If we happened to have already queried the entire
                    // hand data this frame, we don't need to re-query for
                    // just the joint. If we haven't, we do still need to
                    // query for the single joint.
                    if (!AlreadyFullQueried)
                    {
                        bool gotPhysicalData = false;

                        foreach (var sys in handsSubsystems)
                        {
                            // Don't scrape subsystems that aren't running.
                            if (sys.running == false)
                            {
                                Debug.Log($"[Pico-MRTK]-{sys.subsystemDescriptor.Name} is not running");
                                continue;
                            }

                            var tmp_State = sys.TryGetJoint(joint, HandNode, out HandJointPose data);
                            if (tmp_State)
                            {
                                // If we get valid physical data, we will ignore any subsequent
                                // hands subsystems that are *not* physical data.
                                gotPhysicalData |= sys.subsystemDescriptor.IsPhysicalData;

                                thisQueryValid |= true;
                                HandJoints[jointIndex] = data;
                            }

                            if (gotPhysicalData)
                            {
                                break;
                            }
                        }
                    }
                    else
                    {
                        // If we've already run a full-hand query, this single joint query
                        // is just as valid as the full query.
                        thisQueryValid = FullQueryValid;
                    }

                    // Fix for Pico hand tracking
                    // Because pico hand tracking palm has not data(position and rotation).
                    // if (jointIndex == 0)
                    //     jointIndex = 1;
                    // Fix for Pico hand tracking.
                    // Left-handed and right-handed data are reversed
                    // if (HandNode == XRNode.LeftHand)
                    // {
                    //     var tmp_OldQuaternion = handJoints[jointIndex].Pose.rotation;
                    //     handJoints[jointIndex].Pose = new Pose(handJoints[jointIndex].Pose.position,
                    //         new Quaternion(-tmp_OldQuaternion.x + Quaternion.Euler(90, 0, 0).x,
                    //             tmp_OldQuaternion.y,
                    //             tmp_OldQuaternion.z,
                    //             -tmp_OldQuaternion.w));
                    // }

                    pose = HandJoints[jointIndex];
                    return thisQueryValid;
                }
            }

            /// <summary>
            /// Returns the length of the index finger, as measured when this hand first became visible.
            /// Recomputed each time the hand is lost and found.
            /// </summary>
            internal bool TryGetIndexFingerLength(out float length)
            {
                bool gotData = true;

                gotData &= TryGetJoint(TrackedHandJoint.IndexTip, out HandJointPose indexTip);

                // If our first query fails, we've lost tracking, and we reset the cached finger length
                // to be recomputed when the hand is visible again.
                if (!gotData)
                {
                    indexFingerLength = null;
                    length = 0.0f;
                    return false;
                }

                // If we are tracked and also have a cached finger length, return that.
                if (indexFingerLength.HasValue && indexFingerLength.Value != 0.0f)
                {
                    length = indexFingerLength.Value;
                    return true;
                }
                else
                {
                    // Otherwise, we compute a fresh finger length.
                    gotData &= TryGetJoint(TrackedHandJoint.IndexProximal, out HandJointPose indexKnuckle);
                    gotData &= TryGetJoint(TrackedHandJoint.IndexIntermediate, out HandJointPose indexMiddle);
                    gotData &= TryGetJoint(TrackedHandJoint.IndexDistal, out HandJointPose indexDistal);

                    if (gotData)
                    {
                        indexFingerLength = Vector3.Distance(indexKnuckle.Position, indexMiddle.Position) +
                                            Vector3.Distance(indexMiddle.Position, indexDistal.Position) +
                                            Vector3.Distance(indexDistal.Position, indexTip.Position);

                        length = indexFingerLength.Value;
                        return true;
                    }
                    else
                    {
                        indexFingerLength = null;
                        length = 0;
                        return false;
                    }
                }
            }
        }

        [Preserve]
        protected class PicoMRTKAggregator : Provider
        {
            protected MRTKHandsAggregatorConfig Config { get; private set; }

            private Dictionary<XRNode, PicoAggregateHandContainer> hands = null;

            // Reusable pinch pose structs to reduce allocs.
            private HandJointPose leftPinchPose, rightPinchPose;

            public override void Start()
            {
                base.Start();

                Config = XRSubsystemHelpers
                    .GetConfiguration<MRTKHandsAggregatorConfig, PicoMRTKHandsAggregatorSubsystem>();

                hands ??= new Dictionary<XRNode, PicoAggregateHandContainer>
                {
                    {XRNode.LeftHand, new PicoAggregateHandContainer(XRNode.LeftHand)},
                    {XRNode.RightHand, new PicoAggregateHandContainer(XRNode.RightHand)}
                };
                Debug.Log($"[Pico-MRTK]-PicoMRTKAggregator started");
                InputSystem.onBeforeUpdate += ResetHands;
            }

            public override void Stop()
            {
                ResetHands();
                InputSystem.onBeforeUpdate -= ResetHands;
                base.Stop();
            }

            private void ResetHands()
            {
                hands[XRNode.LeftHand].Reset();
                hands[XRNode.RightHand].Reset();
            }

            #region IHandsSubsystem implementation

            /// <inheritdoc/>
            public override bool TryGetNearInteractionPoint(XRNode handNode, out HandJointPose jointPose)
            {
                return TryGetJoint(TrackedHandJoint.IndexTip, handNode, out jointPose);
            }


            /// <inheritdoc/>
            public override bool TryGetPinchingPoint(XRNode handNode, out HandJointPose jointPose)
            {
                // GetJoint will reuse existing joint data if the hand was already queried this frame.
                bool gotData = TryGetJoint(TrackedHandJoint.ThumbTip, handNode, out HandJointPose thumbPose);
                gotData &= TryGetJoint(TrackedHandJoint.IndexTip, handNode, out HandJointPose indexPose);
                gotData &= TryGetJoint(TrackedHandJoint.Palm, handNode, out HandJointPose palmPose);

                HandJointPose pinchPointPose = handNode == XRNode.LeftHand ? leftPinchPose : rightPinchPose;

                // Stabilize the pinch pose by a weighted average between the thumb and index.
                // A true average (50% thumb, 50% index) is too unstable for precise manipulation.
                pinchPointPose.Position = Vector3.Lerp(thumbPose.Position, indexPose.Position, 0.2f);

                // Stabilize the rotation by sampling the palm joint's rotation instead.
                // Index + thumb rotations move too much for precise manipulation.
                pinchPointPose.Rotation = palmPose.Rotation;

                jointPose = pinchPointPose;
                return gotData;
            }

            /// <inheritdoc/>
            public override bool TryGetPinchProgress(XRNode handNode, out bool isReadyToPinch, out bool isPinching,
                out float pinchAmount)
            {
                bool gotData = TryGetJoint(TrackedHandJoint.Palm, handNode, out HandJointPose palm);

                // Is the hand far enough up/in view to be eligible for pinching?
                bool handIsUp =
                    Vector3.Angle(Camera.main.transform.forward, (palm.Position - Camera.main.transform.position)) <
                    Config.HandRaiseCameraFov;

                gotData &= TryGetJoint(TrackedHandJoint.ThumbTip, handNode, out HandJointPose thumbTip);
                gotData &= TryGetJoint(TrackedHandJoint.IndexTip, handNode, out HandJointPose indexTip);

                // Compute index finger length (cached) for normalizing pinch thresholds to different sized hands.
                gotData &= hands[handNode].TryGetIndexFingerLength(out float indexFingerLength);

                if (!gotData)
                {
                    isReadyToPinch = false;
                    isPinching = false;
                    pinchAmount = 0.0f;

                    return false;
                }

                // Is the hand facing away from the head? Pinching is only allowed when this is true.
                bool handIsFacingAway = IsPalmFacingAway(palm);

                // Possibly sqr magnitude for performance?
                // Would need to adjust thresholds so that everything works in square-norm
                float pinchDistance = Vector3.Distance(indexTip.Position, thumbTip.Position);
                float normalizedPinch = pinchDistance / indexFingerLength;

                // Is the hand in the ready-pose? Clients may choose to ignore pinch progress
                // if the hand is not yet ready to pinch.
                isReadyToPinch = handIsUp && handIsFacingAway;

                // Are we actually fully pinching?
                isPinching = (normalizedPinch < Config.PinchOpenThreshold);

                // Calculate pinch amount as the inverse lerp of the current pinch norm vs the open/closed thresholds. 
                pinchAmount = 1.0f - Mathf.InverseLerp(Config.PinchClosedThreshold, Config.PinchOpenThreshold,
                    normalizedPinch);
                return gotData;
            }

            /// <inheritdoc/>
            public override bool TryGetPalmFacingAway(XRNode hand, out bool palmFacingAway)
            {
                bool gotData = TryGetJoint(TrackedHandJoint.Palm, hand, out HandJointPose palm);

                if (!gotData)
                {
                    palmFacingAway = false;
                    return false;
                }

                palmFacingAway = IsPalmFacingAway(palm);
                return gotData;
            }

            /// <inheritdoc/>
            public override bool TryGetJoint(TrackedHandJoint joint, XRNode handNode, out HandJointPose jointPose)
            {
                Debug.Assert(handNode == XRNode.LeftHand || handNode == XRNode.RightHand,
                    "Non-hand XRNode used in TryGetJoint query");
                return hands[handNode].TryGetJoint(joint, out jointPose);
            }

            ///<inheritdoc/>
            public override bool TryGetEntireHand(XRNode handNode, out IReadOnlyList<HandJointPose> jointPoses)
            {
                Debug.Assert(handNode == XRNode.LeftHand || handNode == XRNode.RightHand,
                    "Non-hand XRNode used in GetHand query");

                return hands[handNode].TryGetEntireHand(out jointPoses);
            }

            #endregion IHandsAggregatorSubsystem implementation

            #region Helpers

            /// <summary>
            /// Calculates whether the palm is facing away from the user.
            /// </summary>
            private bool IsPalmFacingAway(HandJointPose palmJoint, bool _isLeftHand = false)
            {
                if (Camera.main == null)
                {
                    return false;
                }

                Vector3 palmDown = palmJoint.Rotation * (_isLeftHand ? Vector3.up : -Vector3.up);

                // The original palm orientation is based on a horizontal palm facing down.
                // So, if you bring your hand up and face it away from you, the palm.up is the forward vector.

                var tmp_FacingAway = Mathf.Abs(Vector3.Angle(palmDown, Camera.main.transform.forward)) >
                                     Config.HandFacingAwayToleranceInDegrees;
                return !tmp_FacingAway;
            }

            #endregion Helpers
        }
    }
}
#endif