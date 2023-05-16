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

using System.Runtime.InteropServices;
using UnityEngine;

namespace Unity.XR.PXR
{
    public enum HandType
    {
        HandLeft = 0,
        HandRight = 1,
    }

    public enum ActiveInputDevice
    {
        HeadActive = 0,
        ControllerActive = 1,
        HandTrackingActive = 2,
    }

    public struct Vector3f
    {
        public float x;
        public float y;
        public float z;

        public Vector3 ToVector3()
        {
            return new Vector3() { x = x, y = y, z = z };
        }
    }

    public struct Quatf
    {
        public float x;
        public float y;
        public float z;
        public float w;

        public Quaternion ToQuat()
        {
            return new Quaternion() { x = x, y = y, z = z, w = w };
        }
    }


    public struct Posef
    {
        public Quatf Orientation;
        public Vector3f Position;
        public override string ToString()
        {
            return string.Format("Orientation :{0}, {1}, {2}, {3}  Position: {4}, {5}, {6}",
                Orientation.x, Orientation.y, Orientation.z, Orientation.w,
                Position.x, Position.y, Position.z);
        }

        public void ToHandPosef(HandType hand)
        {
            Vector3 pos = Position.ToVector3();
            Quaternion rot = Orientation.ToQuat();

            if (hand == HandType.HandLeft)
            {
                rot = new Quaternion(rot.x, rot.y, -rot.z, -rot.w) * new Quaternion(0.5f, -0.5f, 0.5f, -0.5f);
            }
            else
            {
                rot = new Quaternion(rot.x, rot.y, -rot.z, -rot.w) * new Quaternion(-0.5f, -0.5f, -0.5f, -0.5f);
            }

            Position.x = pos.x;
            Position.y = pos.y;
            Position.z = -pos.z;
            Orientation.x = rot.x;
            Orientation.y = rot.y;
            Orientation.z = rot.z;
            Orientation.w = rot.w;
        }

        public void ToJointPosef(HandType hand)
        {
            Vector3 pos = Position.ToVector3();
            Quaternion rot = Orientation.ToQuat();

            if (hand == HandType.HandLeft)
            {
                Orientation.x = -rot.y;
                Orientation.y = rot.z;
                Orientation.z = rot.x;
                Orientation.w = -rot.w;
            }
            else
            {
                Orientation.x = rot.y;
                Orientation.y = -rot.z;
                Orientation.z = rot.x;
                Orientation.w = -rot.w;
            }

            Position.x = pos.x;
            Position.y = pos.y;
            Position.z = -pos.z;
        }
    }

    public enum HandAimStatus : ulong
    {
        AimComputed = 0x00000001,
        AimRayValid = 0x00000002,
        AimIndexPinching = 0x00000004,
        AimMiddlePinching = 0x00000008,
        AimRingPinching = 0x00000010,
        AimLittlePinching = 0x00000020,
        AimRayTouched = 0x00000200
    }

    public struct HandAimState
    {
        public HandAimStatus aimStatus;
        public Posef aimRayPose;
        public float pinchStrengthIndex;
        public float pinchStrengthMiddle;
        public float pinchStrengthRing;
        public float pinchStrengthLittle;
        public float touchStrengthRay;
    }

    public enum HandLocationStatus : ulong
    {
        OrientationValid = 0x00000001,
        PositionValid = 0x00000002,
        OrientationTracked = 0x00000004,
        PositionTracked = 0x00000008
    }

    public enum HandJoint
    {
        JointPalm = 0,
        JointWrist = 1,

        JointThumbMetacarpal = 2,
        JointThumbProximal = 3,
        JointThumbDistal = 4,
        JointThumbTip = 5,

        JointIndexMetacarpal = 6,
        JointIndexProximal = 7,
        JointIndexIntermediate = 8,
        JointIndexDistal = 9,
        JointIndexTip = 10,

        JointMiddleMetacarpal = 11,
        JointMiddleProximal = 12,
        JointMiddleIntermediate = 13,
        JointMiddleDistal = 14,
        JointMiddleTip = 15,

        JointRingMetacarpal = 16,
        JointRingProximal = 17,
        JointRingIntermediate = 18,
        JointRingDistal = 19,
        JointRingTip = 20,

        JointLittleMetacarpal = 21,
        JointLittleProximal = 22,
        JointLittleIntermediate = 23,
        JointLittleDistal = 24,
        JointLittleTip = 25,

        JointMax = 26
    }

    public struct HandJointLocation
    {
        public HandLocationStatus locationStatus;
        public Posef pose;
        public float radius;
    }

    public struct HandJointLocations
    {
        public uint isActive;
        public uint jointCount;
        public float handScale;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)HandJoint.JointMax)]
        public HandJointLocation[] jointLocations;
    }

    public enum HandFinger
    {
        Thumb = 0,
        Index = 1,
        Middle = 2,
        Ring = 3,
        Pinky = 4
    }

    public static class PXR_HandTracking
    {
        /// <summary>Gets the status of hand tracking.</summary>
        /// <returns>
        /// * `true`: enabled
        /// * `false`: disabled
        /// </returns>
        public static bool GetSettingState()
        {
            return PXR_Plugin.HandTracking.UPxr_GetHandTrackerSettingState();
        }

        /// <summary>Gets the current input control device.</summary>
        /// <returns>
        /// * `0`(`HeadActive`): HMD 
        /// * `1`(`ControllerActive`): controller
        /// * `2`(`HandTrackingActive`): hand
        /// </returns>
        public static ActiveInputDevice GetActiveInputDevice()
        {
            return PXR_Plugin.HandTracking.UPxr_GetHandTrackerActiveInputType();
        }

        /// <summary>Gets the interaction status of a specified hand.</summary>
        /// <param name="hand">The hand to get the interaction status for:
        /// * `0`(`HandLeft`): left hand
        /// * `1`(`HandRight`): right hand
        /// </param>
        /// <param name="aimState">Returns the data about the interaction status of the specified hand.</param>
        /// <returns>The `HandAimState` struct that contains the following data about hand interaction status:
        /// * `aimStatus`: HandAimStatus, the status of hand tracking, including:
        ///   * `0x00000001`(`AimComputed`): whether the data is valid
        ///   * `0x00000002`(`AimRayValid`): whether the ray was displayed
        ///   * `0x00000004`(`AimIndexPinching`): whether the index finger pinched
        ///   * `0x00000008`(`AimMiddlePinching`): whether the middle finger pinched
        ///   * `0x00000010`(`AimRingPinching`): whether the ring finger pinched
        ///   * `0x00000020`(`AimLittlePinching`): whether the little finger pinched
        ///   * `0x00000200`(`AimRayTouched`): whether the ray touched
        /// * `aimRayPose`: Posef, ray pose
        /// * `pinchStrengthIndex`: float, the strength of index finger pinch
        /// * `pinchStrengthMiddle`: float, the strength of middle finger pinch
        /// * `pinchStrengthRing`: float, the strength of ring finger pinch
        /// * `pinchStrengthLittle`: float, the strength of little finger pinch
        /// * `touchStrengthRay`: float, the strength of ray touch
        /// If you use the hand prefabs without changing any of their default settings, 
        /// such as hand joints, you can get the following data:
        /// * `Computed`: bool, whether the data is valid
        /// * `RayPose`: Posef, ray pose
        /// * `RayValid`: bool, whether the ray was displayed
        /// * `RayTouched`: bool, whether the ray touched
        /// * `TouchStrengthRay`: float, the strength of ray touch
        /// * `IndexPinching`: bool, whether the index finger pinched
        /// * `MiddlePinching`: bool, whether the middle finger pinched
        /// * `RingPinching`: bool, whether the ring finger pinched
        /// * `LittlePinching`: bool, whether the little finger pinched
        /// * `PinchStrengthIndex`: float, the strength of index finger pinch
        /// * `PinchStrengthMiddle`: float, the strength of middle finger pinch
        /// * `PinchStrengthRing`: float, the strength of ring finger pinch
        /// * `PinchStrengthLittle`: float, the strength of little finger pinch
        /// </returns>
        public static bool GetAimState(HandType hand, ref HandAimState aimState)
        {
            if (!PXR_ProjectSetting.GetProjectConfig().handTracking) return false;
            return PXR_Plugin.HandTracking.UPxr_GetHandTrackerAimState(hand, ref aimState);
        }

        /// <summary>Gets the pose data for a hand.</summary>
        /// <param name="hand">The hand to get pose data for:
        /// * `0`(`HandLeft`): left hand
        /// * `1`(`HandRight`): right hand
        /// </param>
        /// <param name="jointLocations">Returns the pose data of the specified hand.</param>
        /// <returns>The `HandJointLocations` struct that contains the following pose data:
        /// * `isActive`: uint, hand tracking quality (`0`: low; `1`: high)
        /// * `jointCount`: uint, the number of joints
        /// * `handScale`: float, the scale of the hand
        /// * `jointLocations`: HandJointLocation[], the locations of joints, including:
        ///   * `locationStatus`: the status of joints, including the following enumerations:
        ///     * `0x00000001`(`OrientationValid`): whether the hand's orientation is valid
        ///     * `0x00000002`(`PositionValid`): whether the hand's position is valid
        ///     * `0x00000004`(`OrientationTracked`): whether the hand's orientation is tracked
        ///     * `0x00000008`(`PositionTracked`): whether the hand's position is tracked
        ///   * `pose`: the poses of joints, including:
        ///     * `Orientation`: hand orientation
        ///     * `Position`: hand position
        ///   * `radius`: the radius of joints
        /// </returns>
        public static bool GetJointLocations(HandType hand, ref HandJointLocations jointLocations)
        {
            if (!PXR_ProjectSetting.GetProjectConfig().handTracking) return false;
            return PXR_Plugin.HandTracking.UPxr_GetHandTrackerJointLocations(hand, ref jointLocations);
        }

    }
}

