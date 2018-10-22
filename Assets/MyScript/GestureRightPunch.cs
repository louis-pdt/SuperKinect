using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureRightPunch : AbstractGesture {

    [SerializeField] protected float amplitude = 0.2f;

    [SerializeField] private float maxInitDistance = 0.1f;

    void Start() {
    }
    // Use this for initialization
    public override void SearchForGesture()
    {
        jointPosQuater = gestureManager.GetJointPosQuater();
        Vector3 rightHand = jointPosQuater[(int)JointType.HandRight].position;
        Vector3 rightShoulder = jointPosQuater[(int)JointType.ShoulderRight].position;

        //.Log(state);
        //Debug.Log(rightHand);
        switch (state)
        {
            case 0:
                //detect starting movement
                if (Vector3.Distance(rightShoulder, rightHand) < maxInitDistance)
                {
                    //memoryPosition = jointPosQuater[(int)JointType.HandRight].position;
                    previousStateTime = Time.time;
                    state++;
                }

                break;
            case 1:
                if (Time.time - previousStateTime > timestamp)
                    state = 0;
                else if (rightShoulder.z - rightHand.z > amplitude)
                {
                    state++;
                    //memoryPosition = jointPosQuater[(int)JointType.HandRight].position;
                    previousStateTime = Time.time;
                }
                break;
            case 2:
                Debug.Log("Poing droite !");
                activeFeedBack();
                buttonManager.TriggerRightPunch();
                state = 0;
                break;

        }
    }
}
