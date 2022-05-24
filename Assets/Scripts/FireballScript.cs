using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class FireballScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject leftHandRef;
    public GameObject rightHandRef;
    public GameObject fireBall;
    private ActionBasedController controllerLeft;
    private ActionBasedController controllerRight;
    public bool fired = false;

    void Start()
    {
        ActionBasedController[] controllerArray = ActionBasedController.FindObjectsOfType<ActionBasedController>();
        controllerLeft = controllerArray[0];
        controllerRight = controllerArray[1];
        controllerRight.activateAction.action.performed += activateAction_performed;
        controllerRight.activateAction.action.canceled += activateAction_cancelled;

    }

    private void activateAction_cancelled(InputAction.CallbackContext obj)
    {
        Debug.Log("FireballScript" + obj.action);
    }

    private void activateAction_performed(InputAction.CallbackContext obj)
    {
        Rigidbody rb = fireBall.GetComponent<Rigidbody>();
        fired = true;
        Vector3 test = rightHandRef.transform.position - rightHandRef.transform.forward;
        rb.AddForce(test * 1, ForceMode.VelocityChange);

    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(rightHandRef.transform.position, rightHandRef.transform.forward * 15, Color.red);
        if (!fired)
        {
            fireBall.transform.localPosition = rightHandRef.transform.position;

        }


    }
}
