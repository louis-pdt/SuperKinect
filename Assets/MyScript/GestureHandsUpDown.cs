using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureHandsUpDown : AbstractGesture
{
    private Vector3 memoryPosition2;
    void Start()
    {
        timestamp = 0.5f;
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
                }
                state++;

                break;
            case 1:
                //detect position to the right
                if (Time.time - previousStateTime > timestamp)
                    state = 0;
                else if (jointPosQuater[(int)JointType.HandLeft].position.y - memoryPosition.y > 0.3f
                    && jointPosQuater[(int)JointType.HandRight].position.y - memoryPosition2.y > 0.3f)
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
                else if (jointPosQuater[(int)JointType.HandLeft].position.y - memoryPosition.y > -0.3f
                    && jointPosQuater[(int)JointType.HandRight].position.y - memoryPosition2.y > -0.3f)
                {
                    state++;
                    previousStateTime = Time.time;
                }
                break;
            case 3:
                Debug.Log("hands up and down !");
                state = 0;
                break;
        }
    }
}
