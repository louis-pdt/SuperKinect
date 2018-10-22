using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureRightHand : AbstractGesture {

    [SerializeField] protected float amplitude;

    void Start() {
    }
    // Use this for initialization
    public override void SearchForGesture()
    {
        jointPosQuater = gestureManager.GetJointPosQuater();
        switch (state)
        {
            case 0:
                //detect starting movement
                if (jointPosQuater[(int)JointType.HandRight].position.y - jointPosQuater[(int)JointType.ElbowRight].position.y > 0)
                {
                    memoryPosition = jointPosQuater[(int)JointType.HandRight].position;
                    previousStateTime = Time.time;
                    state++;
                }

                break;
            case 1:
                //detect position to the right
                if (Time.time - previousStateTime > timestamp)
                    state = 0;
                else if (jointPosQuater[(int)JointType.HandRight].position.x - memoryPosition.x > amplitude
                    && jointPosQuater[(int)JointType.HandRight].position.y - jointPosQuater[(int)JointType.ElbowRight].position.y > 0)
                {
                    state++;
                    memoryPosition = jointPosQuater[(int)JointType.HandRight].position;
                    previousStateTime = Time.time;
                }
                break;
            case 2:
                //detect position to the left
                if (Time.time - previousStateTime > timestamp)
                    state = 0;
                else if (jointPosQuater[(int)JointType.HandRight].position.x - memoryPosition.x < -amplitude)
                {
                    state++;
                    previousStateTime = Time.time;
                }
                break;
            case 3:
                Debug.Log("balayage droite !");
                activeFeedBack();
                buttonManager.TriggerWavingRight();
                state = 0;
                break;
        }
    }
}
