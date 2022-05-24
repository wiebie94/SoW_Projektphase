using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class MenuScript : MonoBehaviour
{
    private ActionBasedController controllerLeft;
    private ActionBasedController controllerRight;
    private float range = 15f;
    private GameObject menuButton;
    public float rayOffset = 1f;

    // Start is called before the first frame update
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
        menuButton = null;
    }

    private void activateAction_performed(InputAction.CallbackContext obj)
    {
        //Hier muss acion ausgefhrt
        Debug.Log("menuButton" + menuButton.name);
        menuButton.GetComponent<Button>().onClick.Invoke();

    }

    // Update is called once per frame
    void Update()
    {
        
            RaycastHit hit;
            Debug.DrawRay(transform.position * rayOffset, transform.forward* range, Color.red);


            if (Physics.Raycast(transform.position * rayOffset, transform.forward* range, out hit))
            {
               menuButton = hit.collider.gameObject;
               
               
            }
           
        }
}
