using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(ActionBasedController))]
public class HandController : MonoBehaviour
{
    ActionBasedController controller;
    public Hand hand;

    void Start()
    {
        controller = GetComponent<ActionBasedController>();

        controller.selectAction.action.performed += SelectWas;
        controller.selectAction.action.canceled += SelectCancel;
    }

    private void SelectCancel(InputAction.CallbackContext obj)
    {
        //Debug.Log("Select pressd");
    }

    private void SelectWas(InputAction.CallbackContext obj)
    {
        //Debug.Log("Select pressed");
    }

    private void Update()
    {
        hand.SetGrip(controller.selectAction.action.ReadValue<float>());
        hand.SetTrigger(controller.activateAction.action.ReadValue<float>());
    }
}
