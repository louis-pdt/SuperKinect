using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureClap : AbstractGesture
{
    [SerializeField] protected float amplitude = 0.3f;
    [SerializeField] protected float maxYHeight = 0.1f;
    [SerializeField] protected float minDistClap = 0.1f;

    private bool isClapping;


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
        //Vector3 leftElbow = jointPosQuater[(int)JointType.ElbowLeft].position;
        //Vector3 rightElbow = jointPosQuater[(int)JointType.ElbowRight].position;

        if (isClapping)
            activeFeedBack();

        switch (state)
        {
            case 0:
                //detect starting movement : hands in the same x plan as shoulders, left hand up and right down or inverse
                if (Mathf.Abs((leftShoulder.y - leftHand.y)) < maxYHeight
                    && Mathf.Abs((rightShoulder.x - rightHand.x)) < maxYHeight
                    && Vector3.Distance(leftHand, rightHand) < minDistClap)
                {
                    state = 1;
                }
                break;
            case 1:
                //both hands outside of the shoulder, with a min amplitude between them (next position)
                if (Time.time - previousStateTime > timestamp)
                {
                    state = 0;
                    if (isClapping)
                        sphereFeedBack.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
                    isClapping = false;
                }

                else if (leftHand.x - rightHand.x > amplitude
                    && (leftShoulder.y - leftHand.y) < maxYHeight
                    && (rightShoulder.y - rightHand.y) < maxYHeight
                    && leftHand.x - leftShoulder.x < 0
                    && rightShoulder.x - rightHand.x < 0)
                {
                    previousStateTime = Time.time;
                    state = 2;
                    isClapping = true;
                }
                break;

            case 2:
                if (Time.time - previousStateTime > timestamp)
                {
                    state = 0;
                    if (isClapping)
                        sphereFeedBack.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
                    isClapping = false;
                }
                else if (Mathf.Abs((leftShoulder.y - leftHand.y)) < maxYHeight
                    && Mathf.Abs((rightShoulder.x - rightHand.x)) < maxYHeight
                    && Vector3.Distance(leftHand, rightHand) < minDistClap)
                {
                    state = 1;
                }
                break;
        }
    }
}