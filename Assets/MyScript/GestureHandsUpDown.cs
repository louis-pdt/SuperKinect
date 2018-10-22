using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureHandsUpDown : AbstractGesture
{
    private Vector3 memoryPosition2;
    [SerializeField] protected float amplitudeHands;
    [SerializeField] protected float amplitudeAboveHead;


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

        switch (state)
        {
            case 0:
                //detect starting movement
                if (jointPosQuater[(int)JointType.HandLeft].position.y - jointPosQuater[(int)JointType.ElbowLeft].position.y > 0
                    && jointPosQuater[(int)JointType.HandRight].position.y - jointPosQuater[(int)JointType.ElbowRight].position.y > 0)
                {
                    memoryPosition = jointPosQuater[(int)JointType.HandLeft].position;
                    memoryPosition2 = jointPosQuater[(int)JointType.HandRight].position;
                    previousStateTime = Time.time;
                    state++;
                }

                break;
            case 1:
                if (Time.time - previousStateTime > timestamp)
                    state = 0;
                else if (jointPosQuater[(int)JointType.HandLeft].position.y - memoryPosition.y > amplitudeHands
                    && jointPosQuater[(int)JointType.HandRight].position.y - memoryPosition2.y > amplitudeHands
                    && jointPosQuater[(int)JointType.HandRight].position.y - jointPosQuater[(int)JointType.Head].position.y > amplitudeAboveHead
                    && jointPosQuater[(int)JointType.HandLeft].position.y - jointPosQuater[(int)JointType.Head].position.y > amplitudeAboveHead)
                {
                    state++;
                    memoryPosition = jointPosQuater[(int)JointType.HandLeft].position;
                    memoryPosition2 = jointPosQuater[(int)JointType.HandRight].position;

                    previousStateTime = Time.time;
                }
                break;
            case 2:
                //detect position to the left
                if (Time.time - previousStateTime > timestamp)
                    state = 0;
                else if (jointPosQuater[(int)JointType.HandLeft].position.y - memoryPosition.y < -amplitudeHands
                    && jointPosQuater[(int)JointType.HandRight].position.y - memoryPosition2.y < -amplitudeHands)
                {
                    state++;
                    previousStateTime = Time.time;
                }
                break;
            case 3:
                activeFeedBack();
                Debug.Log("hands up and down !");
                buttonManager.TriggerHandsUp();
                state = 0;
                break;
        }
    }
}
