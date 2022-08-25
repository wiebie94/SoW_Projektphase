using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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

    [SerializeField] float handDistanceCheckInterval = 0.3f;
    [SerializeField] float handToBodyDistanceLimit = 3f;
    [SerializeField] Transform playerTransform;

    private bool shouldTeleport = false;

    private bool isInFingerTrigger = false;
    void Start()
    {
        //Animation
        animator = GetComponentInChildren<Animator>();
        mesh = GetComponentInChildren<SkinnedMeshRenderer>();

        //Physics Move
        followTarget = controller.gameObject.transform;
        rb = hand.GetComponent<Rigidbody>();
        rb.maxAngularVelocity = 20;

        TeleportHandToController();

        StartCoroutine(HandToBodyDistanceCheck());
    }

    private void OnEnable()
    {
        //Input Setup
        controller.selectAction.action.started += Grab;
        controller.selectAction.action.canceled += Released;
    }

    private void OnDisable()
    {
        //Input Setup
        controller.selectAction.action.started -= Grab;
        controller.selectAction.action.canceled -= Released;
    }

    private IEnumerator HandToBodyDistanceCheck()
    {
        if (playerTransform == null)
        {
            Debug.LogError("Player Transform is null!");
            yield break;
        }   

        while(true)
        {
            Vector3 handPosition;

            if (isGrabbing && heldObject != null)
                handPosition = heldObject.transform.position;
            else
                handPosition = rb.position;

            if (Vector3.Distance(handPosition, playerTransform.position) > handToBodyDistanceLimit)
                TeleportHandToController();

            yield return new WaitForSeconds(handDistanceCheckInterval);
        }
    }

    public void TeleportHandToController()
    {
        if (isGrabbing && heldObject != null)
        {
            heldObject.TeleportToController(followTarget);
        }    
        else
        {
            rb.position = followTarget.position;
            rb.rotation = followTarget.rotation;
        }
    }

    private void Grab(InputAction.CallbackContext obj)
    {
        if (isGrabbing || heldObject)
            return;

        Collider[] grabbableColliders = Physics.OverlapSphere(palm.position, reachDistance, grabbableLayer);
        if (grabbableColliders.Length < 1)
            return;

        var objectToGrab = GetNearestCollider(grabbableColliders).gameObject;

        Equip(objectToGrab);
    }

    public void Equip(GameObject grabObject)
    {
        if (grabObject == null)
            return;

        Released(new InputAction.CallbackContext());

        heldObject = grabObject.GetComponentInParent<GrabInteractable>();

        if (heldObject == null)
        {
            Debug.LogError("GrabInteractable script on parent not found");
        }

        if (heldObject.GrabBegin(hand.transform.rotation, handType))
        {
            isGrabbing = true;

            ResetHandAnimation();
            Invoke(nameof(DisableHand), 0.05f);
            
            heldObject.onObjectLost += ObjectLostInHand;
        }
        else
        {
            heldObject = null;
        }
    }

    private Collider GetNearestCollider(Collider[] list)
    {
        if (list == null || list.Length == 0)
            return null;

        if (list.Length == 1)
            return list[0];

        Collider nearestCollider = null;
        float nearestDistance = 100000;

        foreach (Collider c in list)
        {
            float distanceToHand = Vector3.Distance(palm.position, c.gameObject.transform.position);
            if (distanceToHand < nearestDistance)
            {
                nearestDistance = distanceToHand;
                nearestCollider = c;
            }
        }

        return nearestCollider;
    }

    public void Released(InputAction.CallbackContext obj)
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

    void FixedUpdate()
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

        if (shouldTeleport)
        {
            rb.position = positionWithOffset;
        }
        else
        {
            var distance = Vector3.Distance(positionWithOffset, rb.position);
            rb.velocity = (positionWithOffset - rb.position).normalized * (followSpeed * distance);
        }


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

    private void DisableHand()
    {
        hand.SetActive(false);
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
        SetTrigger(controller.activateActionValue.action.ReadValue<float>());

        if (isInFingerTrigger)
            SetGrip(1);
        else
            SetGrip(controller.selectActionValue.action.ReadValue<float>());

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

    void ResetHandAnimation()
    {
        animator.SetFloat(animatorGripParam, 0);
        animator.SetFloat(animatorTriggerParam, 0);
    }

    public void SetShouldTeleport(bool value)
    {
        shouldTeleport = value;
    }

    public void VisibilityTurnOff()
    {
        mesh.enabled = false;
    }

    public void FingerTriggerEnter()
    {
        isInFingerTrigger = true;
    }

    public void FingerTriggerExit()
    {
        isInFingerTrigger = false;
    }

    public void VisibilityTurnOn()
    {
        mesh.enabled = true;
    }
}
