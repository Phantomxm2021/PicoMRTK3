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

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.XR.PXR
{
    [Serializable]
    public class PXR_HandPoseConfig : ScriptableObject
    {
        [DisplayOnly]
        public bool visualize;
        [DisplayOnly]
        public ShapesRecognizer shapesRecognizer;
        [DisplayOnly]
        public BonesRecognizer bonesRecognizer;

        [Serializable]
        public class ShapesRecognizer
        {
            public Finger thumb = new Finger(HandFinger.Thumb);
            public Finger index = new Finger(HandFinger.Index);
            public Finger middle = new Finger(HandFinger.Middle);
            public Finger ring = new Finger(HandFinger.Ring);
            public Finger pinky = new Finger(HandFinger.Pinky);

            [Serializable]
            public class Finger
            {
                [HideInInspector]
                public HandFinger handFinger;
                public Flexion flexion;
                public Curl curl;
                public Abduction abduction;
                public FingerConfigs fingerConfigs;

                public Finger(HandFinger finger)
                {
                    handFinger = finger;
                    flexion = Flexion.Any;
                    curl = Curl.Any;
                    abduction = Abduction.Any;
                    fingerConfigs = new FingerConfigs(finger);
                }
            }

            [Serializable]
            public class FingerConfigs
            {
                public RangeConfigs flexionConfigs;
                public RangeConfigs curlConfigs;
                public RangeConfigs abductionConfigs;

                public FingerConfigs(HandFinger finger)
                {
                    flexionConfigs = new RangeConfigs(flexionOpenMin, flexionOpenMax, defaultFlexionWidth);
                    curlConfigs = new RangeConfigs(curlOpenMin, curlOpenMax, defaultCurlWidth);
                    if (finger == HandFinger.Thumb)
                    {
                        abductionConfigs = new RangeConfigs(abductionCloseMin, abductionThumbCloseMax, defaultAbductionThumbWidth);
                    }
                    else
                    {
                        abductionConfigs = new RangeConfigs(abductionCloseMin, abductionCloseMax, defaultAbductionWidth);
                    }
                }
            }

            public enum Flexion
            {
                Any,
                Open,
                Close,
                Custom
            }

            public enum Curl
            {
                Any,
                Open,
                Close,
                Custom
            }

            public enum Abduction
            {
                Any,
                Open,
                Close,
            }

            public const float defaultFlexionWidth = 10f;
            public const float defaultCurlWidth = 10f;
            public const float defaultAbductionWidth = 2;
            public const float defaultAbductionThumbWidth = 4;

            public const float flexionOpenMin = 180f;
            public const float flexionOpenMax = 220f;
            public const float flexionCloseMin = 230f;
            public const float flexionCloseMax = 260f;

            public const float curlOpenMin = 180f;
            public const float curlOpenMax = 220f;
            public const float curlCloseMin = 230f;
            public const float curlCloseMax = 270f;

            public const float abductionThumbCloseMax = 19f;
            public const float abductionOpenMax = 90f;
            public const float abductionCloseMin = 0f;
            public const float abductionCloseMax = 17.4f;
        }

        [Serializable]
        public class BonesRecognizer
        {
            public List<Bones> listBones;

            [Serializable]
            public class Bones
            {
                public HandJoint bone1;
                public HandJoint bone2;
                public Distance distance;
                public RangeConfigs bonesConfigs;

                public Bones(Distance distance)
                {
                    switch (distance)
                    {
                        case Distance.None:
                            bonesConfigs = new RangeConfigs(0, 0, bonesWidth);
                            break;
                        case Distance.Near:
                            bonesConfigs = new RangeConfigs(bonesNearMin, bonesNearMax, bonesWidth);
                            break;
                        case Distance.Touching:
                            bonesConfigs = new RangeConfigs(bonesTouchingMin, bonesTouchingMax, bonesWidth);
                            break;
                        default:
                            break;
                    }
                }
            }

            public enum Distance
            {
                None,
                Near,
                Touching
            }

            public const float bonesNearMin = 1.5f;
            public const float bonesNearMax = 15f;
            public const float bonesTouchingMin = 0f;
            public const float bonesTouchingMax = 1.5f;
            public const float bonesWidth = 0;
        }

        [Serializable]
        public class RangeConfigs
        {
            public float min;
            public float max;
            public float width;
            public RangeConfigs(float n, float m, float w)
            {
                min = n;
                max = m;
                width =w;
            }
        }
    }

    public class DisplayOnly : PropertyAttribute { }
}


