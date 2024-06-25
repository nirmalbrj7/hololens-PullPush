using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Microsoft;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit.Input;

public class HandTracking : MonoBehaviour
{

    public GameObject sphereMarker;
    public GameObject TriggerJenga;

    //right hand
    GameObject thumbObject_right;
    GameObject indexObject_right;
    GameObject middleObject_right;
    GameObject ringObject_right;
    GameObject pinkyObject_right;
    //left hand
    GameObject thumbObject_left;
    GameObject indexObject_left;
    GameObject middleObject_left;
    GameObject ringObject_left;
    GameObject pinkyObject_left;

    MixedRealityPose pose_right;
    MixedRealityPose pose_left;
    void Start()
    {
        TriggerJenga.SetActive(false);
        thumbObject_right = Instantiate(sphereMarker, this.transform);
        indexObject_right = Instantiate(sphereMarker, this.transform);
        middleObject_right = Instantiate(sphereMarker, this.transform);
        ringObject_right = Instantiate(sphereMarker, this.transform);
        pinkyObject_right = Instantiate(sphereMarker, this.transform);


        thumbObject_left = Instantiate(sphereMarker, this.transform);
        indexObject_left = Instantiate(sphereMarker, this.transform);
        middleObject_left = Instantiate(sphereMarker, this.transform);
        ringObject_left = Instantiate(sphereMarker, this.transform);
        pinkyObject_left = Instantiate(sphereMarker, this.transform);
        
    }

    void Update()
    {
        thumbObject_right.GetComponent<Renderer>().enabled = false;
        indexObject_right.GetComponent<Renderer>().enabled = false;
        middleObject_right.GetComponent<Renderer>().enabled = false;
        ringObject_right.GetComponent<Renderer>().enabled = false;
        pinkyObject_right.GetComponent<Renderer>().enabled = false;

        thumbObject_left.GetComponent<Renderer>().enabled = false;
        indexObject_left.GetComponent<Renderer>().enabled = false;
        middleObject_left.GetComponent<Renderer>().enabled = false;
        ringObject_left.GetComponent<Renderer>().enabled = false;
        pinkyObject_left.GetComponent<Renderer>().enabled = false;

        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.ThumbTip, Handedness.Right, out pose_right))
        {
            thumbObject_right.GetComponent<Renderer>().enabled = true;
            thumbObject_right.transform.position = pose_right.Position;
        }

        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexTip, Handedness.Right, out pose_right))
        {
            indexObject_right.GetComponent<Renderer>().enabled = true;
            indexObject_right.transform.position = pose_right.Position;
        }

        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.MiddleTip, Handedness.Right, out pose_right))
        {
            middleObject_right.GetComponent<Renderer>().enabled = true;
            middleObject_right.transform.position = pose_right.Position;
        }

        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.RingTip, Handedness.Right, out pose_right))
        {
            ringObject_right.GetComponent<Renderer>().enabled = true;
            ringObject_right.transform.position = pose_right.Position;
        }

        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.PinkyTip, Handedness.Right, out pose_right))
        {
            pinkyObject_right.GetComponent<Renderer>().enabled = true;
            pinkyObject_right.transform.position = pose_right.Position;
        }


        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.ThumbTip, Handedness.Left, out pose_left))
        {
            thumbObject_left.GetComponent<Renderer>().enabled = true;
            thumbObject_left.transform.position = pose_left.Position;
        }

        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexTip, Handedness.Left, out pose_left))
        {
            indexObject_left.GetComponent<Renderer>().enabled = true;
            indexObject_left.transform.position = pose_left.Position;
        }

        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.MiddleTip, Handedness.Left, out pose_left))
        {
            middleObject_left.GetComponent<Renderer>().enabled = true;
            middleObject_left.transform.position = pose_left.Position;
        }

        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.RingTip, Handedness.Left, out pose_left))
        {
            ringObject_left.GetComponent<Renderer>().enabled = true;
            ringObject_left.transform.position = pose_left.Position;
        }

        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.PinkyTip, Handedness.Left, out pose_left))
        {
            pinkyObject_left.GetComponent<Renderer>().enabled = true;
            pinkyObject_left.transform.position = pose_left.Position;
        }

        CheckForHandPosition();
    }

    public void CheckForHandPosition(){
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.ThumbTip, Handedness.Right, out MixedRealityPose thumbTipPose) &&
            HandJointUtils.TryGetJointPose(TrackedHandJoint.ThumbMetacarpalJoint, Handedness.Right, out MixedRealityPose thumbMetacarpalPose) &&
            HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexTip, Handedness.Right, out MixedRealityPose indexTipPose) &&
            HandJointUtils.TryGetJointPose(TrackedHandJoint.MiddleTip, Handedness.Right, out MixedRealityPose middleTipPose) &&
            HandJointUtils.TryGetJointPose(TrackedHandJoint.RingTip, Handedness.Right, out MixedRealityPose ringTipPose) &&
            HandJointUtils.TryGetJointPose(TrackedHandJoint.PinkyTip, Handedness.Right, out MixedRealityPose pinkyTipPose))
        {
            Vector3 thumbDirection = (thumbTipPose.Position - thumbMetacarpalPose.Position).normalized;
            bool isThumbUp = thumbDirection.y > 0.8f; // Adjust threshold as needed

            bool areOtherFingersDown = 
                (indexTipPose.Position.y < thumbMetacarpalPose.Position.y) &&
                (middleTipPose.Position.y < thumbMetacarpalPose.Position.y) &&
                (ringTipPose.Position.y < thumbMetacarpalPose.Position.y) &&
                (pinkyTipPose.Position.y < thumbMetacarpalPose.Position.y);

            if (isThumbUp && areOtherFingersDown)
            {
                TriggerJenga.SetActive(true);
            }
            else
            {
                TriggerJenga.SetActive(false);
            }
        }
    }
}
    