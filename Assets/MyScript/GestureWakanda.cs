using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureWakanda : AbstractGesture
{
    [SerializeField] protected float distanceInitShoulder;
    [SerializeField] protected float distanceFinishShoulder;


    void Start()
    {
    }
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
        Debug.Log(state);
        switch (state)
        {
            case 0:
                //detect starting movement
                if (Vector3.Distance(rightHand, leftShoulder) < distanceInitShoulder
                    && Vector3.Distance(leftHand, rightShoulder) < distanceInitShoulder)
                {
                    previousStateTime = Time.time;
                    state++;
                }
                break;
            case 1:
                if (Time.time - previousStateTime > timestamp)
                    state = 0;
                else if (Vector3.Distance(leftShoulder, leftHand) > distanceFinishShoulder
                    && Vector3.Distance(rightShoulder, rightHand) > distanceFinishShoulder)
                {
                    state++;
                }
                break;
            case 2:
                activeFeedBack();
                Debug.Log("wakanda !");
                state = 0;
                break;
        }
    }
}
