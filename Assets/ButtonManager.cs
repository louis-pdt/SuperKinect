using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour {

    [SerializeField] GameObject dummy;

    Animator animator;

    private bool detectionActive = false;
    [SerializeField] GameObject sphereOnAir;
    [SerializeField] TMPro.TextMeshProUGUI textStartButton;
    [SerializeField] GameObject[] activateWhenListening;
    [SerializeField] TMPro.TextMeshProUGUI lastGestureName;


    private void Start()
    {
        animator = dummy.GetComponent<Animator>();
        detectionActive = true;
        ToggleDetection();
    }

    public void TriggerWavingLeft()
    {
        animator.SetTrigger("Show_left_wave");
        lastGestureName.text = ("Left wave");
    }

    public void TriggerWavingRight()
    {
        animator.SetTrigger("Show_right_wave");
        lastGestureName.text = ("Right wave");
    }

    public void TriggerRightPunch()
    {
        animator.SetTrigger("Show_right_punch");
        lastGestureName.text = ("Right Punch");
    }

    public void TriggerRunning()
    {
        animator.SetTrigger("Show_running");
        lastGestureName.text = ("Running");
    }

    public void TriggerHandsUp()
    {
        animator.SetTrigger("Show_hands_up");
        lastGestureName.text = ("Hands up");
    }

    public void ToggleDetection()
    {
        if (detectionActive)
        {
            detectionActive = false;
            sphereOnAir.GetComponent<Renderer>().material.color = Color.red;
            textStartButton.text = "START\nDETECTION";
            foreach (var element in activateWhenListening)
            {
                element.SetActive(detectionActive);
            }
        } else
        {
            detectionActive = true;
            sphereOnAir.GetComponent<Renderer>().material.color = Color.green;
            textStartButton.text = "STOP\nDETECTION";
            foreach (var element in activateWhenListening)
            {
                element.SetActive(detectionActive);
            }
        }
    }
}
