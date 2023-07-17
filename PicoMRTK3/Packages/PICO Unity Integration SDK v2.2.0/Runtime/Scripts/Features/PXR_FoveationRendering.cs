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

using UnityEngine;

namespace Unity.XR.PXR
{
    public class PXR_FoveationRendering
    {
        private static PXR_FoveationRendering instance = null;
        public static PXR_FoveationRendering Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PXR_FoveationRendering();
                }

                return instance;
            }
        }

        /// <summary>
        /// Sets foveated rendering level.
        /// </summary>
        /// <param name="level">Foveated rendering level: None (disabled); Low; Med; High; TopHigh.</param>
        public static void SetFoveationLevel(FoveationLevel level)
        {
            PXR_Plugin.Render.UPxr_SetFoveationLevel(level);
        }

        /// <summary>
        /// Gets the current foveated rendering level.
        /// </summary>
        /// <returns>The current foveated rendering level: -1-None (foveated rendering not enabled); Low; Med; High; TopHigh.</returns>
        public static FoveationLevel GetFoveationLevel()
        {
            return PXR_Plugin.Render.UPxr_GetFoveationLevel();
        }

        /// <summary>
        /// Sets foveated rendering parameters.
        /// </summary>
        /// <param name="foveationGainX">The reduction rate of peripheral pixels in the X-axis direction. Value range: [1.0, 10.0], the greater the value, the higher the reduction rate.</param>
        /// <param name="foveationGainY">The reduction rate of peripheral pixels in the Y-axis direction. Value range: [1.0, 10.0], the greater the value, the higher the reduction rate.</param>
        /// <param name="foveationArea">The range of foveated area whose resolution is not to be reduced. Value range: [0.0, 4.0], the higher the value, the bigger the high-quality central area.</param>
        /// <param name="foveationMinimum">The minimum pixel density. Recommended values: 1/32, 1/16, 1/8, 1/4, 1/2. The actual pixel density will be greater than or equal to the value set here.</param>
        public static void SetFoveationParameters(float foveationGainX, float foveationGainY, float foveationArea, float foveationMinimum)
        {
            PXR_Plugin.Render.UPxr_SetFoveationParameters(foveationGainX, foveationGainY, foveationArea, foveationMinimum);
        }
    }
}