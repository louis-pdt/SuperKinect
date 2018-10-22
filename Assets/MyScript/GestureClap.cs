using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureClap : AbstractGesture
{
    [SerializeField] protected float amplitude = 0.3f;
    [SerializeField] protected float maxYHeight = 0.1f;
    [SerializeField] protected float minDistClap = 0.1f;

    private bool isClapping;
    private int previousState;

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

        Debug.Log(leftHand);
        Debug.Log(leftShoulder);
        Debug.Log("1" + (rightHand.x - leftHand.x > amplitude) + " 2 " + (leftHand.x - leftShoulder.x < 0)
                    + " 3 " + (rightShoulder.x - rightHand.x < 0) +
                    (leftShoulder.y - leftHand.y > maxYHeight) + (rightShoulder.y - rightHand.y > maxYHeight));
        switch (state)
        {
            case 0:
                //detect starting movement : hands in the same x plan as shoulders, left hand up and right down or inverse
                if (leftShoulder.y - leftHand.y > maxYHeight
                    && rightShoulder.y - rightHand.y > maxYHeight
                    && Vector3.Distance(leftHand, rightHand) < minDistClap)
                {
                    state = 1;
                    previousStateTime = Time.time;
                }
                break;
            case 1:
                Debug.Log(state);
                
                
                //both hands outside of the shoulder, with a min amplitude between them (next position)
                if (Time.time - previousStateTime > timestamp)
                {
                    state = 0;
                    if (isClapping)
                        sphereFeedBack.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
                    isClapping = false;
                }
                
                else if (rightHand.x - leftHand.x > amplitude
                    && leftShoulder.y - leftHand.y > maxYHeight
                    && rightShoulder.y - rightHand.y > maxYHeight
                    && leftHand.x - leftShoulder.x < 0
                    && rightShoulder.x - rightHand.x < 0)
                {
                    previousStateTime = Time.time;
                    
                    state = 2;
                    ;
                }
                break;

            case 2:
                Debug.Log(state);
                if (Time.time - previousStateTime > timestamp)
                {
                    state = 0;
                    if (isClapping)
                        sphereFeedBack.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
                    isClapping = false;
                }
                else if (leftShoulder.y - leftHand.y > maxYHeight
                    && rightShoulder.y - rightHand.y > maxYHeight
                    && Vector3.Distance(leftHand, rightHand) < minDistClap)
                {
                    state = 1;
                    previousState = 2;
                    timestamp = Time.time;
                    isClapping = true;
                }
                break;
        }
    }
}