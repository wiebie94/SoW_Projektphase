using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class GrabInteractable : MonoBehaviour
{
    private bool isTwoHandedGrabPossible = false;
    [SerializeField] bool shouldObjectBeReplaced = false;
    [SerializeField] bool isXAndZAxisSwapped;
    [SerializeField] bool isOnHingeJoint;
    [SerializeField] Transform leftHand;
    [SerializeField] Transform rightHand;

    private Rigidbody rb;
    private float followSpeed = 30;
    private float rotateSpeed = 100;
    private float defaultMass;

    private Vector3 positionOffset;
    private Vector3 rotationOffset;

    private bool isGrabbedByLeftHand;
    private bool isGrabbedByRightHand;

    private Vector3 leftHandLastPos;
    private Vector3 rightHandLastPos;

    private Quaternion leftHandLastRotation;
    private Quaternion rightHandLastRotation;

    private Transform leftHandTransform;
    private Transform rightHandTransform;

    [SerializeField] UnityEvent onObjectGrabbed;
    [SerializeField] UnityEvent onObjectReleased;
    
    public delegate void OnObjectLost();
    public event OnObjectLost onObjectLost;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = 20;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

        if (isXAndZAxisSwapped)
        {
            rotationOffset = new Vector3(0, -90, 0);
        }

        defaultMass = rb.mass;
    }

    public void TeleportToController(Transform followObj)
    {
        rb.position = followObj.TransformPoint(positionOffset);
        rb.rotation = followObj.rotation;
    }

    public void MoveObjectTowards(Transform followObj, HandType handType)
    {
        if (isGrabbedByLeftHand && isGrabbedByRightHand)
        {
            SaveTransforms(followObj, handType);

            if (leftHandTransform != null && rightHandTransform != null)
            {
                followObj.position = Vector3.Lerp(leftHandTransform.position, rightHandTransform.position, 0.5f);
                followObj.rotation = Quaternion.Lerp(leftHandTransform.rotation, rightHandTransform.rotation, 0.5f);
            } 
        }

        // Position
        var positionWithOffset = followObj.TransformPoint(positionOffset);
        var distance = Vector3.Distance(positionWithOffset, rb.position);
        rb.velocity = (positionWithOffset - rb.position).normalized * (followSpeed * distance);

        if (isOnHingeJoint)
            return;

        // Rotation
        var rotationWithOffset = followObj.rotation * Quaternion.Euler(rotationOffset);
        var q = rotationWithOffset * Quaternion.Inverse(rb.rotation);
        q.ToAngleAxis(out float angle, out Vector3 axis);
        if (Mathf.Abs(axis.magnitude) != Mathf.Infinity)
        {
            if (angle > 180.0f) { angle -= 360.0f; }
            rb.angularVelocity = axis * (angle * Mathf.Deg2Rad * rotateSpeed);
        }
    }

    private void SaveTransforms(Transform followObj, HandType handType)
    {
        if (leftHandTransform == null && handType == HandType.Left)
        {
            leftHandTransform = followObj;
        }
        else if (rightHandTransform == null && handType == HandType.Right)
        {
            rightHandTransform = followObj;
        }
    }

    public bool GrabBegin(Quaternion handRotation, HandType hand)
    {
        if (leftHand == null || rightHand == null)
        {
            Debug.LogError("Left or right hand not set for grabInteractable!");
            return false;
        }

        if (isGrabbedByLeftHand || isGrabbedByRightHand)
        //if (!isTwoHandedGrabPossible && (isGrabbedByLeftHand || isGrabbedByRightHand))
        {
            onObjectLost.Invoke();
            //return false;
        }

        onObjectGrabbed.Invoke();

        rb.interpolation = RigidbodyInterpolation.Interpolate;

        if (shouldObjectBeReplaced)
            transform.GetChild(0).gameObject.SetActive(false);

        if (hand == HandType.Left)
        {
            isGrabbedByLeftHand = true;
            leftHand.gameObject.SetActive(true);

            if (isGrabbedByRightHand || isOnHingeJoint)
                positionOffset = Vector3.zero;
            else if (isXAndZAxisSwapped)
                positionOffset = new Vector3(leftHand.localPosition.z, -leftHand.localPosition.y, 0);
            else
                positionOffset = new Vector3(-leftHand.localPosition.x, -leftHand.localPosition.y, 0);
        }
        else
        {
            isGrabbedByRightHand = true;
            rightHand.gameObject.SetActive(true);

            if (isGrabbedByLeftHand || isOnHingeJoint)
                positionOffset = Vector3.zero;
            else if (isXAndZAxisSwapped)
                positionOffset = new Vector3(rightHand.localPosition.z, -rightHand.localPosition.y, 0);
            else
                positionOffset = new Vector3(-rightHand.localPosition.x, -rightHand.localPosition.y, 0);
        }

        return true;
    }

    public void GrabEnd(HandType hand)
    {
        onObjectReleased.Invoke();

        if (shouldObjectBeReplaced)
            transform.GetChild(0).gameObject.SetActive(true);

        if (leftHand == null || rightHand == null)
        {
            Debug.LogError("left or right hand not set for grabInteractable!");
            return;
        }

        rb.interpolation = RigidbodyInterpolation.None;

        if (hand == HandType.Left)
        {
            isGrabbedByLeftHand = false;
            leftHand.gameObject.SetActive(false);
        }
        else
        {
            isGrabbedByRightHand = false;
            rightHand.gameObject.SetActive(false);
        }
        
    }

}
