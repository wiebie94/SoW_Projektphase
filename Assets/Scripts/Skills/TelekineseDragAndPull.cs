using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class TelekineseDragAndPull : MonoBehaviour
{
    private GameObject followObj;

    [SerializeField]
    Vector3 followObjOffset;

    [SerializeField]
    GameObject leftHandRef;
    [SerializeField]
    GameObject rightHandRef;

    [SerializeField] private ActionBasedController controllerLeft;
    [SerializeField] private ActionBasedController controllerRight;

    private HandType telekineseHand;
    private GameObject telekineseHandRef;
    private GameObject dragHandRef;
    private bool enteredDragCollider = false;

    private bool isSecondaryControllerGripPressed = false;
    private float dragStartValue = 0;

    private bool telekineseActiv = false;
    private bool twoHandedTelekinese = false;

    [SerializeField] TelekineseScript telekineseScriptMainRightRef;
    [SerializeField] TelekineseScript telekineseScriptMainLeftRef;
    TelekineseScript telekineseScriptMain;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void Trigger_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Debug.Log("ControllerLeft" + obj.control.name);
        isSecondaryControllerGripPressed = true;

        dragStartValue = CalculateDragValueBetweenHands();
    }

    private void Trigger_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Debug.Log("ControllerLeft" + obj.control.name);
        isSecondaryControllerGripPressed = false;

        dragStartValue = 0;
    }


    // Update is called once per frame
    void Update()
    {
        if (followObj != null)
            transform.position = followObj.transform.position + followObjOffset;

        if (enteredDragCollider == true && dragHandRef != null && isSecondaryControllerGripPressed && !twoHandedTelekinese)
        {
            telekineseScriptMain.PushAndPullObject(dragStartValue - CalculateDragValueBetweenHands());
        }
    }


    private float CalculateDragValueBetweenHands()
    {
        if (telekineseHandRef == null)
            return dragStartValue;

        Vector3 dragHandPos = telekineseHandRef.transform.localPosition;

        Vector3 dragHandUp = telekineseHandRef.transform.forward;

        Debug.DrawRay(dragHandPos, dragHandUp);
        Vector3 pathBeetween = dragHandRef.transform.position - telekineseHandRef.transform.position;
        Debug.DrawRay(telekineseHandRef.transform.position, pathBeetween);

        return Vector3.Dot(dragHandUp, pathBeetween);
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((telekineseHand == HandType.Left && other.CompareTag("RightHand")) ||
                telekineseHand == HandType.Right && other.CompareTag("LeftHand"))
        {
            enteredDragCollider = true;
            dragHandRef = other.gameObject;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if ((telekineseHand == HandType.Left && other.CompareTag("RightHand")) ||
                telekineseHand == HandType.Right && other.CompareTag("LeftHand"))
        {
            enteredDragCollider = false;
            dragHandRef = null;

        }
    }


    private void OnDisable()
    {
        if(enteredDragCollider == true)
        {
            enteredDragCollider =false;
        }

        followObj = null;
        telekineseHandRef = null;
        dragHandRef = null;

        ClearBindings();
        isSecondaryControllerGripPressed = false;
        dragStartValue = 0;

        telekineseActiv = false;
        twoHandedTelekinese = false;
    }

    public void SetTelekineseHand(HandType hand)
    {
        if (telekineseActiv)
        {
            twoHandedTelekinese = true;
            return;
        }

        telekineseHand = hand;
        telekineseActiv = true;

        if (hand == HandType.Left)
        {
            followObj = leftHandRef;
            telekineseHandRef = leftHandRef;
            dragHandRef = rightHandRef;

            
            controllerRight.activateAction.action.performed += Trigger_performed;
            controllerRight.activateAction.action.canceled += Trigger_canceled;


            telekineseScriptMain = telekineseScriptMainLeftRef;
        }
        else
        {
            followObj = rightHandRef;
            telekineseHandRef = rightHandRef;
            dragHandRef = leftHandRef;

            controllerLeft.activateAction.action.performed += Trigger_performed;
            controllerLeft.activateAction.action.canceled += Trigger_canceled;
           

            telekineseScriptMain = telekineseScriptMainRightRef;
        }
    }

    private void OnDestroy()
    {
        ClearBindings();
    }

    private void ClearBindings()
    {
        controllerRight.selectAction.action.performed -= Trigger_performed;
        controllerRight.selectAction.action.canceled -= Trigger_canceled;
        controllerLeft.selectAction.action.performed -= Trigger_performed;
        controllerLeft.selectAction.action.canceled -= Trigger_canceled;
    }
}
