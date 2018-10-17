using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractGesture : MonoBehaviour {

    protected bool gestureDetected;
    protected int state;
    protected JointPosQuater[] jointPosQuater;
    protected GestureManager gestureManager;
    protected float timestamp;
    protected float previousStateTime;
    protected Vector3 memoryPosition;
    //GestureDectected() { get; set }

void Awake () {
        gestureManager = GestureManager.Instance;
        gestureManager = FindObjectOfType<GestureManager>();
        jointPosQuater = new JointPosQuater[25];
        for (int i = 0; i < 25; i++)
        {
            jointPosQuater[i] = new JointPosQuater();
        }
    }

    virtual public void SearchForGesture()
    {

    }


    // Update is called once per frame
    void Update () {
		
	}
}
