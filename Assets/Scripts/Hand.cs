using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    Animator animator;
    SkinnedMeshRenderer mesh;

    private float gripCurrent;
    private float gripTarget;
    private float triggerCurrent;
    private float triggerTarget;
    
    [SerializeField] float speed;

    private string animatorGripParam = "Grip";
    private string animatorTriggerParam = "Trigger";

    void Start()
    {
        animator = GetComponent<Animator>();
        mesh = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        AnimateHand();
    }

    public void SetGrip(float value)
    {
        gripTarget = value;
    }

    public void SetTrigger(float value)
    {
        triggerTarget = value;    
    }

    void AnimateHand()
    {
        if (gripCurrent != gripTarget)
        {
            gripCurrent = Mathf.MoveTowards(gripCurrent, gripTarget, speed * Time.deltaTime);
            animator.SetFloat(animatorGripParam, gripCurrent);
        }

        if (triggerCurrent != triggerTarget)
        {
            triggerCurrent = Mathf.MoveTowards(triggerCurrent, triggerTarget, speed * Time.deltaTime);
            animator.SetFloat(animatorTriggerParam, triggerCurrent);
        }
    }

    public void VisibilityTurnOff()
    {
        mesh.enabled = false;
    }

    public void VisibilityTurnOn()
    {
        mesh.enabled = true;
    }
}
