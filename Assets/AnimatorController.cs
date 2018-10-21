using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour {


    [SerializeField] Animator dummyAnimator;

    // Use this for initialization
    void Start () {
        dummyAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown("a"))
            dummyAnimator.SetTrigger("Show_right_wave");
        if (Input.GetKeyDown("z"))
            dummyAnimator.SetTrigger("Show_left_wave");
        if (Input.GetKeyDown("e"))
            dummyAnimator.SetTrigger("Show_right_punch");
        if (Input.GetKeyDown("r"))
            dummyAnimator.SetTrigger("Show_running");
        if (Input.GetKeyDown("t"))
            dummyAnimator.SetTrigger("Show_hands_up");
    }
}
