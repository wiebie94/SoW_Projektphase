using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    // Animation
    Animator animator;
    SkinnedMeshRenderer mesh;

    private float gripCurrent;
    private float gripTarget;
    private float triggerCurrent;
    private float triggerTarget;
    
    [SerializeField] float animationSpeed;

    private string animatorGripParam = "Grip";
    private string animatorTriggerParam = "Trigger";


    //Physics Movement
    [SerializeField] GameObject followObj;
    [SerializeField] float followSpeed = 30f;
    [SerializeField] float rotateSpeed = 100f;
    private Transform followTarget;
    private Rigidbody rb;

    [SerializeField] Vector3 positionOffset = Vector3.zero;
    [SerializeField] Vector3 rotationOffset = Vector3.zero;

    void Start()
    {
        //Animation
        animator = GetComponentInChildren<Animator>();
        mesh = GetComponentInChildren<SkinnedMeshRenderer>();

        //Physics Move
        followTarget = followObj.transform;
        rb = GetComponent<Rigidbody>();

        //Teleport Hands
        rb.position = followTarget.position;
        rb.rotation = followTarget.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        AnimateHand();

        PhysicsMove();
    }

    private void PhysicsMove()
    {
        // Position
        var positionWithOffset = followTarget.TransformPoint(positionOffset);
        var distance = Vector3.Distance(positionWithOffset, transform.position);
        rb.velocity = (positionWithOffset - transform.position).normalized * (followSpeed * distance);

        // Rotation
        var rotationWithOffset = followTarget.rotation * Quaternion.Euler(rotationOffset);
        var q = rotationWithOffset * Quaternion.Inverse(rb.rotation);
        q.ToAngleAxis(out float angle, out Vector3 axis);
        if (Mathf.Abs(axis.magnitude) != Mathf.Infinity)
        {
            if (angle > 180.0f) { angle -= 360.0f; }
            rb.angularVelocity = axis * (angle * Mathf.Deg2Rad * rotateSpeed);
        }
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
            gripCurrent = Mathf.MoveTowards(gripCurrent, gripTarget, animationSpeed * Time.deltaTime);
            animator.SetFloat(animatorGripParam, gripCurrent);
        }

        if (triggerCurrent != triggerTarget)
        {
            triggerCurrent = Mathf.MoveTowards(triggerCurrent, triggerTarget, animationSpeed * Time.deltaTime);
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
