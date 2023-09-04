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

using Unity.Collections;
using UnityEngine;
using UnityEngine.Scripting;
using System.Runtime.CompilerServices;


#if XR_HANDS
using UnityEngine.XR.Hands;
using UnityEngine.XR.Hands.ProviderImplementation;


namespace Unity.XR.PXR
{
    [Preserve]
    /// <summary>
    /// Implement Unity XRHandSubSystem 
    /// Reference: https://docs.unity3d.com/Packages/com.unity.xr.hands@1.1/manual/implement-a-provider.html
    /// </summary>
    public class PXR_HandSubSystem : XRHandSubsystem
    {
        XRHandProviderUtility.SubsystemUpdater m_Updater;

        // This method registers the subsystem descriptor with the SubsystemManager
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        static void RegisterDescriptor()
        {
            var handsSubsystemCinfo = new XRHandSubsystemDescriptor.Cinfo
            {
                id = "PICO Hands",
                providerType = typeof(PXRHandSubsystemProvider),
                subsystemTypeOverride = typeof(PXR_HandSubSystem)
            };
            XRHandSubsystemDescriptor.Register(handsSubsystemCinfo);
        }

        protected override void OnCreate()
        {
            base.OnCreate();
            m_Updater = new XRHandProviderUtility.SubsystemUpdater(this);
        }

        protected override void OnStart()
        {
            Debug.Log("PXR_HandSubSystem Start");
            m_Updater.Start();
            base.OnStart();
        }

        protected override void OnStop()
        {
            m_Updater.Stop();
            base.OnStop();
        }

        protected override void OnDestroy()
        {
            m_Updater.Destroy();
            m_Updater = null;
            base.OnDestroy();
        }


        class PXRHandSubsystemProvider : XRHandSubsystemProvider
        {

            HandJointLocations jointLocations = new HandJointLocations();
            readonly HandLocationStatus AllStatus = HandLocationStatus.PositionTracked | HandLocationStatus.PositionValid |
                          HandLocationStatus.OrientationTracked | HandLocationStatus.OrientationValid;

            bool isValid = false;

            public override void Start()
            {

            }

            public override void Stop()
            {

            }

            public override void Destroy()
            {

            }

            /// <summary>
            /// Mapping the PICO Joint Index To Unity Joint Index
            /// </summary>
            static int[] pxrJointIndexToUnityJointIndexMapping;

            static void Initialize()
            {
                if (pxrJointIndexToUnityJointIndexMapping == null)
                {
                    pxrJointIndexToUnityJointIndexMapping = new int[(int)HandJoint.JointMax];
                    pxrJointIndexToUnityJointIndexMapping[(int)HandJoint.JointPalm] = XRHandJointID.Palm.ToIndex();
                    pxrJointIndexToUnityJointIndexMapping[(int)HandJoint.JointWrist] = XRHandJointID.Wrist.ToIndex();
                    pxrJointIndexToUnityJointIndexMapping[(int)HandJoint.JointThumbMetacarpal] = XRHandJointID.ThumbMetacarpal.ToIndex();
                    pxrJointIndexToUnityJointIndexMapping[(int)HandJoint.JointThumbProximal] = XRHandJointID.ThumbProximal.ToIndex();
                    pxrJointIndexToUnityJointIndexMapping[(int)HandJoint.JointThumbDistal] = XRHandJointID.ThumbDistal.ToIndex();
                    pxrJointIndexToUnityJointIndexMapping[(int)HandJoint.JointThumbTip] = XRHandJointID.ThumbTip.ToIndex();

                    pxrJointIndexToUnityJointIndexMapping[(int)HandJoint.JointIndexMetacarpal] = XRHandJointID.IndexMetacarpal.ToIndex();
                    pxrJointIndexToUnityJointIndexMapping[(int)HandJoint.JointIndexProximal] = XRHandJointID.IndexProximal.ToIndex();
                    pxrJointIndexToUnityJointIndexMapping[(int)HandJoint.JointIndexIntermediate] = XRHandJointID.IndexIntermediate.ToIndex();
                    pxrJointIndexToUnityJointIndexMapping[(int)HandJoint.JointIndexDistal] = XRHandJointID.IndexDistal.ToIndex();
                    pxrJointIndexToUnityJointIndexMapping[(int)HandJoint.JointIndexTip] = XRHandJointID.IndexTip.ToIndex();


                    pxrJointIndexToUnityJointIndexMapping[(int)HandJoint.JointMiddleMetacarpal] = XRHandJointID.MiddleMetacarpal.ToIndex();
                    pxrJointIndexToUnityJointIndexMapping[(int)HandJoint.JointMiddleProximal] = XRHandJointID.MiddleProximal.ToIndex();
                    pxrJointIndexToUnityJointIndexMapping[(int)HandJoint.JointMiddleIntermediate] = XRHandJointID.MiddleIntermediate.ToIndex();
                    pxrJointIndexToUnityJointIndexMapping[(int)HandJoint.JointMiddleDistal] = XRHandJointID.MiddleDistal.ToIndex();
                    pxrJointIndexToUnityJointIndexMapping[(int)HandJoint.JointMiddleTip] = XRHandJointID.MiddleTip.ToIndex();

                    pxrJointIndexToUnityJointIndexMapping[(int)HandJoint.JointRingMetacarpal] = XRHandJointID.RingMetacarpal.ToIndex();
                    pxrJointIndexToUnityJointIndexMapping[(int)HandJoint.JointRingProximal] = XRHandJointID.RingProximal.ToIndex();
                    pxrJointIndexToUnityJointIndexMapping[(int)HandJoint.JointRingIntermediate] = XRHandJointID.RingIntermediate.ToIndex();
                    pxrJointIndexToUnityJointIndexMapping[(int)HandJoint.JointRingDistal] = XRHandJointID.RingDistal.ToIndex();
                    pxrJointIndexToUnityJointIndexMapping[(int)HandJoint.JointRingTip] = XRHandJointID.RingTip.ToIndex();

                    pxrJointIndexToUnityJointIndexMapping[(int)HandJoint.JointLittleMetacarpal] = XRHandJointID.LittleMetacarpal.ToIndex();
                    pxrJointIndexToUnityJointIndexMapping[(int)HandJoint.JointLittleProximal] = XRHandJointID.LittleProximal.ToIndex();
                    pxrJointIndexToUnityJointIndexMapping[(int)HandJoint.JointLittleIntermediate] = XRHandJointID.LittleIntermediate.ToIndex();
                    pxrJointIndexToUnityJointIndexMapping[(int)HandJoint.JointLittleDistal] = XRHandJointID.LittleDistal.ToIndex();
                    pxrJointIndexToUnityJointIndexMapping[(int)HandJoint.JointLittleTip] = XRHandJointID.LittleTip.ToIndex();
                }
            }

            /// <summary>
            /// Gets the layout of hand joints for this provider, by having the
            /// provider mark each index corresponding to a <see cref="XRHandJointID"/>
            /// get marked as <see langword="true"/> if the provider attempts to track
            /// that joint.
            /// </summary>
            /// <remarks>
            /// Called once on creation so that before the subsystem is even started,
            /// so the user can immediately create a valid hierarchical structure as
            /// soon as they get a reference to the subsystem without even needing to
            /// start it.
            /// </remarks>
            /// <param name="handJointsInLayout">
            /// Each index corresponds to a <see cref="XRHandJointID"/>. For each
            /// joint that the provider will attempt to track, mark that spot as
            /// <see langword="true"/> by calling <c>.ToIndex()</c> on that ID.
            /// </param>
            public override void GetHandLayout(NativeArray<bool> handJointsInLayout)
            {

                Initialize();
                handJointsInLayout[XRHandJointID.Palm.ToIndex()] = true;
                handJointsInLayout[XRHandJointID.Wrist.ToIndex()] = true;

                handJointsInLayout[XRHandJointID.ThumbMetacarpal.ToIndex()] = true;
                handJointsInLayout[XRHandJointID.ThumbProximal.ToIndex()] = true;
                handJointsInLayout[XRHandJointID.ThumbDistal.ToIndex()] = true;
                handJointsInLayout[XRHandJointID.ThumbTip.ToIndex()] = true;

                handJointsInLayout[XRHandJointID.IndexMetacarpal.ToIndex()] = true;
                handJointsInLayout[XRHandJointID.IndexProximal.ToIndex()] = true;
                handJointsInLayout[XRHandJointID.IndexIntermediate.ToIndex()] = true;
                handJointsInLayout[XRHandJointID.IndexDistal.ToIndex()] = true;
                handJointsInLayout[XRHandJointID.IndexTip.ToIndex()] = true;

                handJointsInLayout[XRHandJointID.MiddleMetacarpal.ToIndex()] = true;
                handJointsInLayout[XRHandJointID.MiddleProximal.ToIndex()] = true;
                handJointsInLayout[XRHandJointID.MiddleIntermediate.ToIndex()] = true;
                handJointsInLayout[XRHandJointID.MiddleDistal.ToIndex()] = true;
                handJointsInLayout[XRHandJointID.MiddleTip.ToIndex()] = true;

                handJointsInLayout[XRHandJointID.RingMetacarpal.ToIndex()] = true;
                handJointsInLayout[XRHandJointID.RingProximal.ToIndex()] = true;
                handJointsInLayout[XRHandJointID.RingIntermediate.ToIndex()] = true;
                handJointsInLayout[XRHandJointID.RingDistal.ToIndex()] = true;
                handJointsInLayout[XRHandJointID.RingTip.ToIndex()] = true;

                handJointsInLayout[XRHandJointID.LittleMetacarpal.ToIndex()] = true;
                handJointsInLayout[XRHandJointID.LittleProximal.ToIndex()] = true;
                handJointsInLayout[XRHandJointID.LittleIntermediate.ToIndex()] = true;
                handJointsInLayout[XRHandJointID.LittleDistal.ToIndex()] = true;
                handJointsInLayout[XRHandJointID.LittleTip.ToIndex()] = true;

                isValid = true;
            }

           


            /// <summary>
            /// Attempts to retrieve current hand-tracking data from the provider.
            /// </summary>
            public override XRHandSubsystem.UpdateSuccessFlags TryUpdateHands(
                XRHandSubsystem.UpdateType updateType,
                ref UnityEngine.Pose leftHandRootPose,
                NativeArray<XRHandJoint> leftHandJoints,
                ref UnityEngine.Pose rightHandRootPose,
                NativeArray<XRHandJoint> rightHandJoints)
            {
                if (!isValid)
                    return XRHandSubsystem.UpdateSuccessFlags.None;

                XRHandSubsystem.UpdateSuccessFlags ret = UpdateSuccessFlags.None;

                const int handRootIndex = (int)HandJoint.JointWrist;

                if (PXR_HandTracking.GetJointLocations(HandType.HandLeft, ref jointLocations))
                {
                    if (jointLocations.isActive != 0U)
                    {
                        for (int index = 0, jointCount = (int)jointLocations.jointCount; index < jointCount; ++index)
                        {
                            ref HandJointLocation joint = ref jointLocations.jointLocations[index];
                            int unityHandJointIndex = pxrJointIndexToUnityJointIndexMapping[index];

                            leftHandJoints[unityHandJointIndex] = CreateXRHandJoint(Handedness.Left, unityHandJointIndex, joint);

                            if (index == handRootIndex)
                            {
                                leftHandRootPose = PXRPosefToUnityPose(joint.pose);
                                ret |= UpdateSuccessFlags.LeftHandRootPose;
                            }
                        }

                        ret |= UpdateSuccessFlags.LeftHandJoints;
                    }
                }

                if (PXR_HandTracking.GetJointLocations(HandType.HandRight, ref jointLocations))
                {
                    if (jointLocations.isActive != 0U)
                    {
                        for (int index = 0, jointCount = (int)jointLocations.jointCount; index < jointCount; ++index)
                        {
                            ref HandJointLocation joint = ref jointLocations.jointLocations[index];
                            int unityHandJointIndex = pxrJointIndexToUnityJointIndexMapping[index];
                            rightHandJoints[unityHandJointIndex] = CreateXRHandJoint(Handedness.Right, unityHandJointIndex, joint);

                            if (index == handRootIndex)
                            {
                                rightHandRootPose = PXRPosefToUnityPose(joint.pose);
                                ret |= UpdateSuccessFlags.RightHandRootPose;
                            }

                        }
                        ret |= UpdateSuccessFlags.RightHandJoints;
                    }
                }

                return ret;
            }


            /// <summary>
            /// Create Unity XRHandJoint From PXR HandJointLocation
            /// </summary>
            /// <param name="handedness"></param>
            /// <param name="unityHandJointIndex"></param>
            /// <param name="joint"></param>
            /// <returns></returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            XRHandJoint CreateXRHandJoint(Handedness handedness, int unityHandJointIndex, in HandJointLocation joint)
            {

                UnityEngine.Pose pose = UnityEngine.Pose.identity;
                XRHandJointTrackingState state = XRHandJointTrackingState.None;
                if ((joint.locationStatus & AllStatus) == AllStatus)
                {
                    state = (XRHandJointTrackingState.Pose | XRHandJointTrackingState.Radius);
                    pose = PXRPosefToUnityPose(joint.pose);
                }
                return XRHandProviderUtility.CreateJoint(handedness,
                                        state,
                                        XRHandJointIDUtility.FromIndex(unityHandJointIndex),
                                        pose, joint.radius
                                        );
            }



            /// <summary>
            /// PXR's Posef to Unity'Pose
            /// </summary>
            /// <param name="pxrPose"></param>
            /// <returns></returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            UnityEngine.Pose PXRPosefToUnityPose(in Unity.XR.PXR.Posef pxrPose)
            {
                Vector3 position = pxrPose.Position.ToVector3();
                Quaternion orientation = pxrPose.Orientation.ToQuat();
                return new UnityEngine.Pose(position, orientation);
            }

        }
    }
}


#endif //XR_HANDS