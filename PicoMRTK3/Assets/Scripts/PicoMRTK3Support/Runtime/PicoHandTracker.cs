// // /*===============================================================================
// // Copyright (C) 2022 PhantomsXR Ltd. All Rights Reserved.
// //
// // This file is part of the Pico.Runtime.
// //
// // The ARMOD-SDK cannot be copied, distributed, or made available to
// // third-parties for commercial purposes without written permission of PhantomsXR Ltd.
// //
// // Contact info@phantomsxr.com for licensing requests.
// // ===============================================================================*/
#if MRTK3_INSTALL

using Unity.XR.PXR;
using UnityEngine;

namespace PicoMRTK3Support.Runtime
{
    public class PicoHandTracker
    {
        /// <summary>
        /// The maximum number of hand joints that might be tracked.
        /// </summary>
        public const int JointCount = 26;

        /// <summary>
        /// The user's left hand.
        /// </summary>
        public static PicoHandTracker Left { get; } = new PicoHandTracker(Handedness.Left);

        /// <summary>
        /// The user's right hand.
        /// </summary>
        public static PicoHandTracker Right { get; } = new PicoHandTracker(Handedness.Right);

        private readonly Handedness m_handedness;

        internal PicoHandTracker(Handedness trackerHandedness)
        {
            m_handedness = trackerHandedness;
        }

        HandAimState handAimState;

        /// <summary>
        /// Fills the passed-in array with current hand joint locations, if possible.
        /// </summary>
        /// <param name="frameTime">Specify the <see cref="Microsoft.MixedReality.OpenXR.FrameTime"/> to locate the hand joints.</param>
        /// <param name="_handJointLocations">An array of HandJointLocations, indexed according to the HandJoint enum.</param>
        /// <returns>True if the hand joint poses are valid; false otherwise.</returns>
        public bool TryLocateHandJoints(ref HandJointLocations _handJointLocations)
        {
            if (!PXR_HandTracking.GetSettingState()) return false;
            HandType tmp_HandType = HandType.HandLeft;
            if (m_handedness != Handedness.Left)
            {
                tmp_HandType = HandType.HandRight;
            }

            PXR_HandTracking.GetAimState(tmp_HandType, ref handAimState);

            return (handAimState.aimStatus & HandAimStatus.AimComputed) != 0 &&
                   PXR_HandTracking.GetJointLocations(tmp_HandType, ref _handJointLocations);
        }

        /// <summary>
        /// Describes which hand the current hand tracker represents.
        /// </summary>
        public enum Handedness
        {
            /// <summary>
            /// Represents the user's left hand.
            /// </summary>
            Left = 0,

            /// <summary>
            /// Represents the user's right hand.
            /// </summary>
            Right
        }
    }
}
#endif