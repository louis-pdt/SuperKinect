using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum JointType : int
{
    SpineBase = 0,
    SpineMid = 1,
    Neck = 2,
    Head = 3,
    ShoulderLeft = 4,
    ElbowLeft = 5,
    WristLeft = 6,
    HandLeft = 7,
    ShoulderRight = 8,
    ElbowRight = 9,
    WristRight = 10,
    HandRight = 11,
    HipLeft = 12,
    KneeLeft = 13,
    AnkleLeft = 14,
    FootLeft = 15,
    HipRight = 16,
    KneeRight = 17,
    AnkleRight = 18,
    FootRight = 19,
    SpineShoulder = 20,
    HandTipLeft = 21,
    ThumbLeft = 22,
    HandTipRight = 23,
    ThumbRight = 24
    //Count = 25
}

public class JointPosQuater
{
    public Vector3 position;
    public Quaternion rotation;
}
public class GestureManager : MonoBehaviour {

    private static GestureManager instance = null;
    [SerializeField] GameObject TextNoPlayer;

    private GestureManager()
    {
    }

    public static GestureManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GestureManager();
            }
            return instance;
        }
    }

    private AbstractGesture[] gesturesList;

    public int playerIndex = 0;

    private JointType jointType;

    private JointPosQuater[] jointPosQuater;

    public JointPosQuater[] GetJointPosQuater()
    {
        return jointPosQuater;
    }
    // Use this for initialization
    void Start () {
        KinectManager manager = KinectManager.Instance;
        jointPosQuater = new JointPosQuater[25];
        for (int i =0; i < 25; i++)
        {
            jointPosQuater[i] = new JointPosQuater();
        }
        gesturesList = GetComponents<AbstractGesture>();
    }

	
	// Update is called once per frame
	void Update () {
        KinectManager manager = KinectManager.Instance;

        long userID = manager ? manager.GetUserIdByIndex(playerIndex) : 0;

        foreach (int joint in System.Enum.GetValues(typeof(JointType)))
        {
            if (manager.IsJointTracked(userID, joint))
            {
                jointPosQuater[joint].position = manager.GetJointPosition(userID, joint);
                jointPosQuater[joint].rotation = manager.GetJointOrientation(userID, joint, true);
            }
        }
        if (manager.IsUserTracked(userID)) {
            TextNoPlayer.SetActive(false);
            for (int i = 0; i < gesturesList.Length; i++)
            {
                gesturesList[i].SearchForGesture();
            }
        } else
        {
            TextNoPlayer.SetActive(true);
        }

    }
}
