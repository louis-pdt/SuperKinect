using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureRun : AbstractGesture
{
    [SerializeField] protected float amplitude;
    [SerializeField] protected float maxXGapShoulder;

    private bool isRunning;


    void Start()
    {
    }

    //private bool InvariantRun ()
    // Use this for initialization
    public override void SearchForGesture()
    {
        if (gestureManager.GetJointPosQuater() == null)
        {
            return;
        }
        jointPosQuater = gestureManager.GetJointPosQuater();

        Vector3 rightHand = jointPosQuater[(int)JointType.HandRight].position;
        Vector3 rightShoulder = jointPosQuater[(int)JointType.ShoulderRight].position;
        Vector3 leftHand = jointPosQuater[(int)JointType.HandLeft].position;
        Vector3 leftShoulder = jointPosQuater[(int)JointType.ShoulderLeft].position;
        Vector3 leftElbow = jointPosQuater[(int)JointType.ElbowLeft].position;
        Vector3 rightElbow = jointPosQuater[(int)JointType.ElbowRight].position;

        if (isRunning)
            activeFeedBack();

        switch (state)
        {
            case 0:
                //detect starting movement : hands in the same x plan as shoulders, left hand up and right down or inverse
                if (Mathf.Abs((leftShoulder.x - leftHand.x)) < maxXGapShoulder
                    && Mathf.Abs((rightShoulder.x - rightHand.x)) < maxXGapShoulder
                    && leftElbow.y < leftShoulder.y
                    && rightElbow.y < rightShoulder.y)
                {
                    if (rightHand.y > rightElbow.y
                        && rightHand.y - leftHand.y > amplitude)
                    {
                        previousStateTime = Time.time;
                        state = 1;
                    }
                    else if (leftHand.y > leftElbow.y
                        && leftHand.y - rightHand.y > amplitude)
                    {
                        previousStateTime = Time.time;
                        state = 2;
                    }
                }
                break;
            case 1:
                //right hand up, check if left hand is up (next position)
                if (Time.time - previousStateTime > timestamp)
                {
                    state = 0;
                    if (isRunning)
                        sphereFeedBack.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
                    isRunning = false;
                }
                else if (Mathf.Abs((leftShoulder.x - leftHand.x)) < maxXGapShoulder
                    && Mathf.Abs((rightShoulder.x - rightHand.x)) < maxXGapShoulder
                    && leftElbow.y < leftShoulder.y
                    && rightElbow.y < rightShoulder.y
                    && leftHand.y > leftElbow.y
                    && leftHand.y - rightHand.y > amplitude)
                {
                    previousStateTime = Time.time;
                    state = 2;
                    isRunning = true;
                }
                break;
            case 2:
                //left hand up, check if right hand is up (next position)
                if (Time.time - previousStateTime > timestamp)
                {
                    state = 0;
                    if (isRunning)
                        sphereFeedBack.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
                    isRunning = false;
                }
                else if (Mathf.Abs((leftShoulder.x - leftHand.x)) < maxXGapShoulder
                    && Mathf.Abs((rightShoulder.x - rightHand.x)) < maxXGapShoulder
                    && leftElbow.y < leftShoulder.y
                    && rightElbow.y < rightShoulder.y
                    && rightHand.y > rightElbow.y
                    && rightHand.y - leftHand.y > amplitude)
                {
                    previousStateTime = Time.time;
                    state = 1;
                    isRunning = true;
                }
                break;
        }
    }
}