using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

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
    [SerializeField] ActionBasedController controller;
    [SerializeField] GameObject hand;
    [SerializeField] float followSpeed = 30f;
    [SerializeField] float rotateSpeed = 100f;
    private Transform followTarget;
    private Rigidbody rb;

    [SerializeField] Vector3 positionOffset = Vector3.zero;
    [SerializeField] Vector3 rotationOffset = Vector3.zero;

    [SerializeField] Transform palm;
    [SerializeField] float reachDistance = 0.1f, joinDistace = 0.05f;
    [SerializeField] LayerMask grabbableLayer;

    [SerializeField] HandType handType;

    private bool isGrabbing;
    private GrabInteractable heldObject;
    private Transform grabPoint;
    private FixedJoint joint1, joint2;

    void Start()
    {
        //Animation
        animator = GetComponentInChildren<Animator>();
        mesh = GetComponentInChildren<SkinnedMeshRenderer>();

        //Physics Move
        followTarget = controller.gameObject.transform;
        rb = hand.GetComponent<Rigidbody>();
        rb.maxAngularVelocity = 20;

        //Input Setup
        controller.selectAction.action.started += Grab;
        controller.selectAction.action.canceled += Released;

        //Teleport Hands
        rb.position = followTarget.position;
        rb.rotation = followTarget.rotation;
    }

    private void Grab(InputAction.CallbackContext obj)
    {
        if (isGrabbing || heldObject)
            return;

        Collider[] grabbableColliders = Physics.OverlapSphere(palm.position, reachDistance, grabbableLayer);
        if (grabbableColliders.Length < 1)
            return;


        var objectToGrab = grabbableColliders[0].gameObject;

        Equip(objectToGrab);
    }

    public void Equip(GameObject grabObject)
    {
        Released(new InputAction.CallbackContext());

        heldObject = grabObject.GetComponentInParent<GrabInteractable>();

        if (heldObject == null)
        {
            Debug.LogError("GrabInteractable script on parent not found");
        }

        if (heldObject.GrabBegin(hand.transform.rotation, handType))
        {
            hand.SetActive(false);
            isGrabbing = true;

            heldObject.onObjectLost += ObjectLostInHand;
        }
        else
        {
            heldObject = null;
        }
    }

    private void Released(InputAction.CallbackContext obj)
    {
        if (isGrabbing)
        {
            heldObject.GrabEnd(handType);

            isGrabbing = false;

            heldObject.onObjectLost -= ObjectLostInHand;
            heldObject = null;

            StartCoroutine(HandActivationAndTeleportationAfterGrab());
        }
    }

    private void ObjectLostInHand()
    {
        Released(new InputAction.CallbackContext());
    }

    void Update()
    {
        if (!isGrabbing)
        {
            AnimateHand();
            PhysicsMove();
        }
        else
        {
            heldObject.MoveObjectTowards(followTarget, handType);
        }
    }

    private IEnumerator HandActivationAndTeleportationAfterGrab()
    {
        hand.transform.GetChild(0).gameObject.SetActive(false);
        hand.transform.GetChild(1).gameObject.SetActive(false);
        hand.SetActive(true);

        //Teleport Hands
        rb.position = followTarget.position;
        rb.rotation = followTarget.rotation;

        float defaultMass = rb.mass;
        rb.mass = float.MinValue;

        yield return new WaitForSeconds(0.035f);

        hand.transform.GetChild(0).gameObject.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        hand.transform.GetChild(1).gameObject.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        rb.mass = defaultMass;
    }

    private void PhysicsMove()
    {
        // Position
        var positionWithOffset = followTarget.TransformPoint(positionOffset);
        var distance = Vector3.Distance(positionWithOffset, rb.position);
        rb.velocity = (positionWithOffset - rb.position).normalized * (followSpeed * distance);

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
        SetGrip(controller.selectAction.action.ReadValue<float>());
        SetTrigger(controller.activateAction.action.ReadValue<float>());

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
