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

using System.Collections.Generic;
using UnityEngine;
using Unity.XR.PXR;
using UnityEngine.Events;
using System;

public class PXR_HandPose : MonoBehaviour
{
    public TrackType trackType;
    public PXR_HandPoseConfig config;
    public UnityEvent handPoseStart;
    public UpdateEvent handPoseUpdate;
    public UnityEvent handPoseEnd;

    private float duration;

    private Quaternion leftWirstRot;
    private Quaternion rightWirstRot;
    private Vector3 thumb0, thumb1, thumb2;
    private Vector3 index0, index1, index2, index3, index4;
    private Vector3 middle0, middle1, middle2, middle3, middle4;
    private Vector3 ring0, ring1, ring2, ring3, ring4;
    private Vector3 pinky0, pinky1, pinky2, pinky3, pinky4;

    private bool thumbFlex_l, indexFlex_l, middleFlex_l, ringFlex_l, pinkyFlex_l;
    private bool thumbCurl_l, indexCurl_l, middleCurl_l, ringCurl_l, pinkyCurl_l;
    private bool thumbAbduc_l, indexAbduc_l, middleAbduc_l, ringAbduc_l, pinkyAbduc_l;
    private bool thumbFlex_r, indexFlex_r, middleFlex_r, ringFlex_r, pinkyFlex_r;
    private bool thumbCurl_r, indexCurl_r, middleCurl_r, ringCurl_r, pinkyCurl_r;
    private bool thumbAbduc_r, indexAbduc_r, middleAbduc_r, ringAbduc_r, pinkyAbduc_r;

    private List<Vector3> leftJointPos = new List<Vector3>(new Vector3[(int)HandJoint.JointMax]);
    private List<Vector3> rightJointPos = new List<Vector3>(new Vector3[(int)HandJoint.JointMax]);
    private HandJointLocations leftHandJointLocations = new HandJointLocations();
    private HandJointLocations rightHandJointLocations = new HandJointLocations();

    private bool leftShapesVaild;
    private bool rightShapesVaild;
    private bool leftBonesVaild;
    private bool rightBonesVaild;
    private bool currentVaild;
    private bool resultVaild;

    private void Update()
    {
        if (config == null) return;

        if (trackType == TrackType.Right || trackType == TrackType.Any)
        {
            PXR_HandTracking.GetJointLocations(HandType.HandRight, ref rightHandJointLocations);

            for (int i = 0; i < rightJointPos.Count; ++i)
            {
                if (rightHandJointLocations.jointLocations == null) break;

                rightJointPos[i] = rightHandJointLocations.jointLocations[i].pose.Position.ToVector3();

                if (i == (int)HandJoint.JointWrist)
                {
                    rightWirstRot = rightHandJointLocations.jointLocations[i].pose.Orientation.ToQuat();
                }
            }
            rightShapesVaild = RightHandShapesRecognizerCheck(rightJointPos, rightWirstRot*Vector3.right, rightWirstRot*Vector3.forward);
            BonesRecognizerCheck(rightJointPos, ref rightBonesVaild);

        }

        if (trackType == TrackType.Left || trackType == TrackType.Any)
        {
            PXR_HandTracking.GetJointLocations(HandType.HandLeft, ref leftHandJointLocations);

            for (int i = 0; i < leftJointPos.Count; ++i)
            {
                if (leftHandJointLocations.jointLocations == null) break;

                leftJointPos[i] = leftHandJointLocations.jointLocations[i].pose.Position.ToVector3();

                if (i == (int)HandJoint.JointWrist)
                {
                    leftWirstRot = leftHandJointLocations.jointLocations[i].pose.Orientation.ToQuat();
                }
            }
            leftShapesVaild = LeftHandShapesRecognizerCheck(leftJointPos, leftWirstRot*Vector3.right, leftWirstRot*Vector3.back);
            BonesRecognizerCheck(leftJointPos, ref leftBonesVaild);
        }

        HandPoseEventCheck();
    }

    public enum TrackType
    {
        Any,
        Left,
        Right
    }

    private void HandPoseEventCheck()
    {
        switch (trackType)
        {
            case TrackType.Any:
                resultVaild = (leftShapesVaild & leftBonesVaild) | (rightShapesVaild & rightBonesVaild);
                break;
            case TrackType.Left:
                resultVaild = leftShapesVaild & leftBonesVaild;
                break;
            case TrackType.Right:
                resultVaild = rightShapesVaild & rightBonesVaild;
                break;
            default:
                break;
        }
        if (currentVaild != resultVaild)
        {
            currentVaild = resultVaild;
            if (currentVaild)
            {
                if (handPoseStart != null)
                {
                    handPoseStart.Invoke();
                }
            }
            else
            {
                if (handPoseEnd != null)
                {
                    handPoseEnd.Invoke();
                }
            }
            duration = 0f;
        }
        else
        {
            if (currentVaild)
            {
                duration += Time.deltaTime;
                handPoseUpdate.Invoke(duration);
            }
        }
    }

    private bool LeftHandShapesRecognizerCheck(List<Vector3> jointPos, Vector3 wirstRight, Vector3 wirstForward)
    {
        thumb0 = jointPos[(int)HandJoint.JointThumbTip];
        thumb1 = jointPos[(int)HandJoint.JointThumbDistal];
        thumb2 = jointPos[(int)HandJoint.JointThumbProximal];

        index0 = jointPos[(int)HandJoint.JointIndexTip];
        index1 = jointPos[(int)HandJoint.JointIndexDistal];
        index2 = jointPos[(int)HandJoint.JointIndexIntermediate];
        index3 = jointPos[(int)HandJoint.JointIndexProximal];
        index4 = jointPos[(int)HandJoint.JointIndexMetacarpal];

        middle0 = jointPos[(int)HandJoint.JointMiddleTip];
        middle1 = jointPos[(int)HandJoint.JointMiddleDistal];
        middle2 = jointPos[(int)HandJoint.JointMiddleIntermediate];
        middle3 = jointPos[(int)HandJoint.JointMiddleProximal];
        middle4 = jointPos[(int)HandJoint.JointMiddleMetacarpal];

        ring0 = jointPos[(int)HandJoint.JointRingTip];
        ring1 = jointPos[(int)HandJoint.JointRingDistal];
        ring2 = jointPos[(int)HandJoint.JointRingIntermediate];
        ring3 = jointPos[(int)HandJoint.JointRingProximal];
        ring4 = jointPos[(int)HandJoint.JointRingMetacarpal];

        pinky0 = jointPos[(int)HandJoint.JointLittleTip];
        pinky1 = jointPos[(int)HandJoint.JointLittleDistal];
        pinky2 = jointPos[(int)HandJoint.JointLittleIntermediate];
        pinky3 = jointPos[(int)HandJoint.JointLittleProximal];
        pinky4 = jointPos[(int)HandJoint.JointLittleMetacarpal];

        FlexionCheck(config.shapesRecognizer.thumb, wirstRight, wirstForward, ref thumbFlex_l);
        FlexionCheck(config.shapesRecognizer.index, wirstRight, wirstForward, ref indexFlex_l);
        FlexionCheck(config.shapesRecognizer.middle, wirstRight, wirstForward, ref middleFlex_l);
        FlexionCheck(config.shapesRecognizer.ring, wirstRight, wirstForward, ref ringFlex_l);
        FlexionCheck(config.shapesRecognizer.pinky, wirstRight, wirstForward, ref pinkyFlex_l);

        CurlCheck(config.shapesRecognizer.thumb, ref thumbCurl_l);
        CurlCheck(config.shapesRecognizer.index, ref indexCurl_l);
        CurlCheck(config.shapesRecognizer.middle, ref middleCurl_l);
        CurlCheck(config.shapesRecognizer.ring, ref ringCurl_l);
        CurlCheck(config.shapesRecognizer.pinky, ref pinkyCurl_l);

        AbductionCheck(config.shapesRecognizer.thumb, ref thumbAbduc_l);
        AbductionCheck(config.shapesRecognizer.index, ref indexAbduc_l);
        AbductionCheck(config.shapesRecognizer.middle, ref middleAbduc_l);
        AbductionCheck(config.shapesRecognizer.ring, ref ringAbduc_l);
        AbductionCheck(config.shapesRecognizer.pinky, ref pinkyAbduc_l);

        return thumbFlex_l & indexFlex_l & middleFlex_l & ringFlex_l & pinkyFlex_l
               & thumbCurl_l & indexCurl_l & middleCurl_l & ringCurl_l & pinkyCurl_l
               & thumbAbduc_l & indexAbduc_l & middleAbduc_l & ringAbduc_l & pinkyAbduc_l;
    }

    private bool RightHandShapesRecognizerCheck(List<Vector3> jointPos, Vector3 wirstRight, Vector3 wirstForward)
    {
        thumb0 = jointPos[(int)HandJoint.JointThumbTip];
        thumb1 = jointPos[(int)HandJoint.JointThumbDistal];
        thumb2 = jointPos[(int)HandJoint.JointThumbProximal];

        index0 = jointPos[(int)HandJoint.JointIndexTip];
        index1 = jointPos[(int)HandJoint.JointIndexDistal];
        index2 = jointPos[(int)HandJoint.JointIndexIntermediate];
        index3 = jointPos[(int)HandJoint.JointIndexProximal];
        index4 = jointPos[(int)HandJoint.JointIndexMetacarpal];

        middle0 = jointPos[(int)HandJoint.JointMiddleTip];
        middle1 = jointPos[(int)HandJoint.JointMiddleDistal];
        middle2 = jointPos[(int)HandJoint.JointMiddleIntermediate];
        middle3 = jointPos[(int)HandJoint.JointMiddleProximal];
        middle4 = jointPos[(int)HandJoint.JointMiddleMetacarpal];

        ring0 = jointPos[(int)HandJoint.JointRingTip];
        ring1 = jointPos[(int)HandJoint.JointRingDistal];
        ring2 = jointPos[(int)HandJoint.JointRingIntermediate];
        ring3 = jointPos[(int)HandJoint.JointRingProximal];
        ring4 = jointPos[(int)HandJoint.JointRingMetacarpal];

        pinky0 = jointPos[(int)HandJoint.JointLittleTip];
        pinky1 = jointPos[(int)HandJoint.JointLittleDistal];
        pinky2 = jointPos[(int)HandJoint.JointLittleIntermediate];
        pinky3 = jointPos[(int)HandJoint.JointLittleProximal];
        pinky4 = jointPos[(int)HandJoint.JointLittleMetacarpal];

        FlexionCheck(config.shapesRecognizer.thumb, wirstRight, wirstForward, ref thumbFlex_r);
        FlexionCheck(config.shapesRecognizer.index, wirstRight, wirstForward, ref indexFlex_r);
        FlexionCheck(config.shapesRecognizer.middle, wirstRight, wirstForward, ref middleFlex_r);
        FlexionCheck(config.shapesRecognizer.ring, wirstRight, wirstForward, ref ringFlex_r);
        FlexionCheck(config.shapesRecognizer.pinky, wirstRight, wirstForward, ref pinkyFlex_r);

        CurlCheck(config.shapesRecognizer.thumb, ref thumbCurl_r);
        CurlCheck(config.shapesRecognizer.index, ref indexCurl_r);
        CurlCheck(config.shapesRecognizer.middle, ref middleCurl_r);
        CurlCheck(config.shapesRecognizer.ring, ref ringCurl_r);
        CurlCheck(config.shapesRecognizer.pinky, ref pinkyCurl_r);

        AbductionCheck(config.shapesRecognizer.thumb, ref thumbAbduc_r);
        AbductionCheck(config.shapesRecognizer.index, ref indexAbduc_r);
        AbductionCheck(config.shapesRecognizer.middle, ref middleAbduc_r);
        AbductionCheck(config.shapesRecognizer.ring, ref ringAbduc_r);
        AbductionCheck(config.shapesRecognizer.pinky, ref pinkyAbduc_r);

        return thumbFlex_r & indexFlex_r & middleFlex_r & ringFlex_r & pinkyFlex_r
               & thumbCurl_r & indexCurl_r & middleCurl_r & ringCurl_r & pinkyCurl_r
               & thumbAbduc_r & indexAbduc_r & middleAbduc_r & ringAbduc_r & pinkyAbduc_r;
    }

    private void BonesRecognizerCheck(List<Vector3> jointPos, ref bool result)
    {
        if (config.bonesRecognizer.listBones.Count == 0)
        {
            result = true;
        }
        else
        {
            foreach (var bones in config.bonesRecognizer.listBones)
            {
                BonesCheck(jointPos[(int)bones.bone1], jointPos[(int)bones.bone2], bones, ref result);
            }
        }
    }

    private void FlexionCheck(PXR_HandPoseConfig.ShapesRecognizer.Finger finger, Vector3 wirstRight, Vector3 wirstForward, ref bool result)
    {
        if (finger.flexion == PXR_HandPoseConfig.ShapesRecognizer.Flexion.Any) result = true;
        else
        {
            float flexAngle = 0;
            switch (finger.handFinger)
            {
                case HandFinger.Thumb:
                    Vector3 thumb12 = (thumb1 - thumb2);
                    Vector3 thumb12_project = Vector3.ProjectOnPlane(thumb12, wirstRight);
                    flexAngle = Vector3.Angle(thumb12_project, wirstForward);
                    break;
                case HandFinger.Index:
                    Vector3 index23 = (index2 - index3);
                    Vector3 index43 = (index4 - index3);
                    flexAngle = Vector3.Angle(index23, index43);
                    break;
                case HandFinger.Middle:
                    Vector3 middle23 = (middle2 - middle3);
                    Vector3 middle43 = (middle4 - middle3);
                    flexAngle = Vector3.Angle(middle23, middle43);
                    break;
                case HandFinger.Ring:
                    Vector3 ring23 = (ring2 - ring3);
                    Vector3 ring43 = (ring4 - ring3);
                    flexAngle = Vector3.Angle(ring23, ring43);
                    break;
                case HandFinger.Pinky:
                    Vector3 pinky23 = (pinky2 - pinky3);
                    Vector3 pinky43 = (pinky4 - pinky3);
                    flexAngle = Vector3.Angle(pinky23, pinky43);
                    break;
                default:
                    break;
            }
            AngleCheck(flexAngle, finger.fingerConfigs.flexionConfigs.min, finger.fingerConfigs.flexionConfigs.max, finger.fingerConfigs.flexionConfigs.width, finger.fingerConfigs.flexionConfigs.width, ref result);
        }

    }

    private void CurlCheck(PXR_HandPoseConfig.ShapesRecognizer.Finger finger, ref bool result)
    {
        if (finger.curl == PXR_HandPoseConfig.ShapesRecognizer.Curl.Any) result = true;
        else
        {
            float curlAngle = 0;
            switch (finger.handFinger)
            {
                case HandFinger.Thumb:
                    Vector3 thumb01 = (thumb0 - thumb1);
                    Vector3 thumb21 = (thumb2 - thumb1);
                    curlAngle = Vector3.Angle(thumb01, thumb21);
                    break;
                case HandFinger.Index:
                    Vector3 index01 = (index0 - index1);
                    Vector3 index32 = (index3 - index2);
                    curlAngle = Vector3.Angle(index32, index01);
                    break;
                case HandFinger.Middle:
                    Vector3 middle01 = (middle0 - middle1);
                    Vector3 middle32 = (middle3 - middle2);
                    curlAngle = Vector3.Angle(middle32, middle01);
                    break;
                case HandFinger.Ring:
                    Vector3 ring01 = (ring0 - ring1);
                    Vector3 ring32 = (ring3 - ring2);
                    curlAngle = Vector3.Angle(ring32, ring01);
                    break;
                case HandFinger.Pinky:
                    Vector3 pinky01 = (pinky0 - pinky1);
                    Vector3 pinky32 = (pinky3 - pinky2);
                    curlAngle = Vector3.Angle(pinky32, pinky01);
                    break;
                default:
                    break;
            }
            AngleCheck(curlAngle, finger.fingerConfigs.curlConfigs.min, finger.fingerConfigs.curlConfigs.max, finger.fingerConfigs.curlConfigs.width, finger.fingerConfigs.curlConfigs.width, ref result);
        }
    }

    private void AbductionCheck(PXR_HandPoseConfig.ShapesRecognizer.Finger finger, ref bool result)
    {
        if (finger.abduction == PXR_HandPoseConfig.ShapesRecognizer.Abduction.Any) result = true;
        else
        {
            float abducAngle = 0;
            Vector3 thumb12 = (thumb1 - thumb2);
            Vector3 index23 = (index2 - index3);
            Vector3 middle23 = (middle2 - middle3);
            Vector3 ring23 = (ring2 - ring3);
            Vector3 pinky23 = (pinky2 - pinky3);
            switch (finger.handFinger)
            {
                case HandFinger.Thumb:
                    abducAngle = Vector3.Angle(thumb12, index23);
                    break;
                case HandFinger.Index:
                    abducAngle = Vector3.Angle(index23, middle23);
                    break;
                case HandFinger.Middle:
                    abducAngle = Vector3.Angle(middle23, ring23);
                    break;
                case HandFinger.Ring:
                    abducAngle = Vector3.Angle(ring23, pinky23);
                    break;
                case HandFinger.Pinky:
                    abducAngle = Vector3.Angle(pinky23, ring23);
                    break;
                default:
                    break;
            }
            if (finger.abduction == PXR_HandPoseConfig.ShapesRecognizer.Abduction.Open)
            {
                AbducCheck(abducAngle, finger.fingerConfigs.abductionConfigs.min, finger.fingerConfigs.abductionConfigs.width, ref result);
            }
            else if (finger.abduction == PXR_HandPoseConfig.ShapesRecognizer.Abduction.Close)
            {
                AbducCheck(abducAngle, finger.fingerConfigs.abductionConfigs.max, finger.fingerConfigs.abductionConfigs.width, ref result);
            }
        }
    }

    private void BonesCheck(Vector3 bone1, Vector3 bone2, PXR_HandPoseConfig.BonesRecognizer.Bones bones, ref bool result)
    {
        switch (bones.distance)
        {
            case PXR_HandPoseConfig.BonesRecognizer.Distance.None:
                result = true;
                break;
            case PXR_HandPoseConfig.BonesRecognizer.Distance.Near:
                DistanceCheck(Vector3.Distance(bone1, bone2), bones.bonesConfigs.min, bones.bonesConfigs.width, ref result);
                break;
            case PXR_HandPoseConfig.BonesRecognizer.Distance.Touching:
                DistanceCheck(Vector3.Distance(bone1, bone2), bones.bonesConfigs.max, bones.bonesConfigs.width, ref result);
                break;
            default:
                break;
        }
    }

    private void AngleCheck(float angle, float min, float max, float minWidth, float maxWidth, ref bool angleValid)
    {
        if (angle > (min + minWidth / 2) && angle < (max - maxWidth / 2))
        {
            angleValid = true;
        }
        if (angle < (min - minWidth / 2) && angle > (max + maxWidth / 2))
        {
            angleValid = false;
        }
    }

    private void AbducCheck(float angle, float mid, float width, ref bool isOpen)
    {
        if (angle > mid + width / 2)
        {
            isOpen = true;
        }
        if (angle < mid - width / 2)
        {
            isOpen = false;
        }
    }

    private void DistanceCheck(float distance, float mid, float width, ref bool isTouching)
    {
        if (distance < mid - width / 2)
        {
            isTouching = true;
        }
        if (distance > mid + width / 2)
        {
            isTouching = false;
        }
    }

    [Serializable]
    public class UpdateEvent : UnityEvent<float> { }
}
