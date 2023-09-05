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
using MixedReality.Toolkit;

namespace PicoMRTK3Support.Runtime
{
    public class HandsUtils
    {
        /// <summary>
        /// Converts an MRTK joint id into an index, for use when indexing into the joint pose array.
        /// </summary>
        internal static int ConvertToIndex(TrackedHandJoint joint)
        {
            return (int)joint;
        }
    }
}
#endif