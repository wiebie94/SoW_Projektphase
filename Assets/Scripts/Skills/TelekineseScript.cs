using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;
using UnityEngine.InputSystem;

public class TelekineseScript : MonoBehaviour
{
    [SerializeField] Material highLightMaterial;
    [SerializeField] float range = 15.0f;
    [SerializeField] Transform followTarget;

    private GameObject telekineseObj;
    private Rigidbody telekineseRigidbody;
    private string neeededTag = "GrabInteractable";
    private bool isItemGrabbed = false;
    public GameObject telekineseDragObject;
    [SerializeField] Vector3 followPositionOffset;
    [SerializeField] Vector3 followRotationOffset;
    [SerializeField] float followSpeed;
    [SerializeField] float rotateSpeed;
    [SerializeField]
    private Coroutine timerCoroutine;
    private float timeAfterHit = 0;

    [SerializeField] float activationTime = 1;

    [SerializeField] float pullAndPushSpeed = 1;

    [SerializeField] float telekineseMinRange = 1;
    [SerializeField] float telekineseMaxRange = 10;

    [SerializeField] HandType hand;

    Coroutine telekineseCoroutine;

    ActionBasedController controller;

    void Start()
    {
        controller = GetComponent<ActionBasedController>();
        

        //skillMenu.onTelekineseSkillTriggered

        ShowSkillMenu.onTelekineseSkillTriggered += TelekineseSkillTriggered;
        ShowSkillMenu.onTelekineseSkillUntriggered += TelekineseSkillUntriggered;
    }

    private void TelekineseSkillTriggered(HandType activationHand)
    {
        if (hand != activationHand)
            return;

        TelekineseCoroutineStart();

        controller.activateAction.action.performed += Action_performed_left;
        controller.activateAction.action.canceled += Action_canceled_left;
    }

    private void TelekineseSkillUntriggered()
    {
        controller.activateAction.action.performed -= Action_performed_left;
        controller.activateAction.action.canceled -= Action_canceled_left;
        Debug.Log("TelekineseEnd");
        TelekineseEnd();
        TelekineseCoroutineStop();
    }

    private void Action_performed_left(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (telekineseObj != null)
            TelekineseBegin();
    }

    private void Action_canceled_left(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (isItemGrabbed)
            TelekineseEnd();
    }

    
    void TelekineseBegin()
    {
        isItemGrabbed = true;
        telekineseDragObject.SetActive(true);
        telekineseDragObject.GetComponent<TelekineseDragAndPull>().SetTelekineseHand(hand);
        telekineseRigidbody = telekineseObj.GetComponent<Rigidbody>();
        
        if (telekineseRigidbody == null)
        {
            telekineseRigidbody = telekineseObj.transform.parent.GetComponent<Rigidbody>();

            if (telekineseRigidbody == null)
                Debug.LogError("telekineseRigidbody is null!");
        }

        Debug.Log("telekinse obj: " + telekineseRigidbody.gameObject.name);

        followTarget.position = telekineseRigidbody.position;
    }

    void TelekineseEnd()
    {
        if (telekineseDragObject == null)
            return;

        isItemGrabbed = false;
        telekineseDragObject.SetActive(false);
        telekineseRigidbody = null;
        ClearHighLight();
    }

    void ObjectRayHit()
    {
        
    }

    IEnumerator ObjectHitTimer()
    {
        while(true)
        {
            timeAfterHit += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
    }

    void HighLightObject(GameObject gObj)
    {
        telekineseObj = gObj;

        Material[] materials = new Material[2];
        materials[0] = gObj.GetComponent<Renderer>().materials[0];
            
        materials[1] = highLightMaterial;
        gObj.GetComponent<Renderer>().materials = materials;
        telekineseObj = gObj;
    }

    void ClearHighLight()
    {
        if (telekineseObj == null)
            return;

        Material[] materials = new Material[1];
        materials[0] = telekineseObj.GetComponent<Renderer>().materials[0];
        telekineseObj.GetComponent<Renderer>().materials = materials;

        telekineseObj = null;
        ClearTimer();
    }

    void ClearTimer()
    {
        StopCoroutine(timerCoroutine);
        timerCoroutine = null;
        timeAfterHit = 0;
    }

   void TelekineseCoroutineStart()
    {
        if (telekineseCoroutine != null)
            StopCoroutine(telekineseCoroutine);

        telekineseCoroutine = StartCoroutine(TelekineseRayCast(0.1f));
    }

    void TelekineseCoroutineStop()
    {
        if (telekineseCoroutine != null)
            StopCoroutine(telekineseCoroutine);
    }


    // Update is called once per frame
    void Update()
    {
        if (isItemGrabbed)
        {
            // Position
            var positionWithOffset = followTarget.TransformPoint(followPositionOffset);
            var distance = Vector3.Distance(positionWithOffset, telekineseObj.transform.position);
            telekineseRigidbody.velocity = (positionWithOffset - telekineseObj.transform.position).normalized * (followSpeed * distance);

            // Rotation
            var rotationWithOffset = followTarget.rotation * Quaternion.Euler(followRotationOffset);
            var q = rotationWithOffset * Quaternion.Inverse(telekineseRigidbody.rotation);
            q.ToAngleAxis(out float angle, out Vector3 axis);
            if (Mathf.Abs(axis.magnitude) != Mathf.Infinity)
            {
                if (angle > 180.0f) { angle -= 360.0f; }
                telekineseRigidbody.angularVelocity = axis * (angle * Mathf.Deg2Rad * rotateSpeed);
            }
        }
    }

    IEnumerator TelekineseRayCast(float interval)
    {
        
        while(true)
        {
            RaycastHit hit;
            Debug.DrawRay(transform.position, transform.forward * range, Color.red);

            if (Physics.Raycast(transform.position, transform.forward, out hit, range))
            {
                if (telekineseObj == null && hit.collider.CompareTag(neeededTag))
                {
                    if (timeAfterHit > activationTime)
                        HighLightObject(hit.collider.gameObject);
                    else if (timerCoroutine == null)
                        timerCoroutine = StartCoroutine(ObjectHitTimer());
                }
            }
            else if (telekineseObj != null && isItemGrabbed == false)
            {
                ClearHighLight();
            }

            yield return new WaitForSeconds(interval);
        }
    }


    public void PushAndPullObject(float dragForce)
    {
        Vector3 distance = followTarget.transform.localPosition - Vector3.forward * dragForce * pullAndPushSpeed;
        
        followTarget.transform.localPosition = new Vector3(distance.x,distance.y,Mathf.Clamp(distance.z, telekineseMinRange, telekineseMaxRange));
    }
}

