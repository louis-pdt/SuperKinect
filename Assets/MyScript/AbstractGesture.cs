using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractGesture : MonoBehaviour {

    protected bool gestureDetected;
    protected int state;
    protected JointPosQuater[] jointPosQuater;
    protected GestureManager gestureManager;
    [SerializeField] protected float timestamp;
    protected float previousStateTime;
    protected Vector3 memoryPosition;
    [SerializeField] protected GameObject sphereFeedBack;
    [SerializeField] protected Color colorFeedback;
    [SerializeField] private GameObject toTriggerWhenDetect;
    protected ButtonManager buttonManager;
//GestureDectected() { get; set }

void Awake () {
        if (toTriggerWhenDetect)
        {
            buttonManager = toTriggerWhenDetect.GetComponent<ButtonManager>();
        }

        //gestureManager = GestureManager.Instance;
        gestureManager = FindObjectOfType<GestureManager>();
        jointPosQuater = new JointPosQuater[25];
        for (int i = 0; i < 25; i++)
        {
            jointPosQuater[i] = new JointPosQuater();
        }
    }

    virtual protected void activeFeedBack()
    {
        sphereFeedBack.GetComponent<Renderer>().material.SetColor("_Color", colorFeedback);
    }

    virtual public void SearchForGesture()
    {

    }


    // Update is called once per frame
    void Update () {
		
	}
}
