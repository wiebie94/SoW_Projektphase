using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class FireballScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private ShowSkillMenu skillMenu;
    public GameObject leftHandRef;
    public GameObject rightHandRef;
    public GameObject fireBall;
    private GameObject g1;
    private Vector3 initialPos;
    private ActionBasedController controllerLeft;
    private ActionBasedController controllerRight;
    [SerializeField] private float waitTillFireAgain;
    [SerializeField] private float waitTillDespawn;
    private bool fired = false;
    private bool _isDone = false;
    private float timestamp;

    void Start()
    {
        skillMenu = GameObject.Find("Player").GetComponent<ShowSkillMenu>();
        

    }

  

    private void OnEnable()
    {
        ActionBasedController[] controllerArray = ActionBasedController.FindObjectsOfType<ActionBasedController>();
        if (controllerArray[0].name.Equals("LeftHand Controller"))
        {
            controllerLeft = controllerArray[0];
            controllerRight = controllerArray[1];
        }
        else
        {
            controllerLeft = controllerArray[1];
            controllerRight = controllerArray[0];
        }
        ShowSkillMenu.onFireballSkillTriggered += FireballSkillTriggered;
        ShowSkillMenu.onFireballSkillUntriggered += FireballSkillUntriggered;

    }

    private void OnDisable()
    {
        ShowSkillMenu.onFireballSkillTriggered -= FireballSkillTriggered;
        ShowSkillMenu.onFireballSkillUntriggered -= FireballSkillUntriggered;
    }

    private void FireballSkillTriggered()
    {
        if (skillMenu.menuHandLeft)
        {
            controllerRight.activateAction.action.performed += activateAction_performed;
        }
        else
        {
            controllerLeft.activateAction.action.performed += activateAction_performed;

        }
    }

    private void FireballSkillUntriggered()
    {
        controllerLeft.activateAction.action.performed -= activateAction_performed;
        controllerRight.activateAction.action.performed -= activateAction_performed;

    }
    
    private void activateAction_cancelled(InputAction.CallbackContext obj)
    {
    }

    private void activateAction_performed(InputAction.CallbackContext obj)
    {
        if (skillMenu._isFireBallActive) { 

            
                if (skillMenu.menuHandLeft)
            {
                    if (Time.time >= timestamp)
                    {
                        g1 = Instantiate(fireBall, rightHandRef.transform.position + rightHandRef.transform.forward, Quaternion.identity);
                        //TODO:JAN-> Hier weiterer Code f�r den Fireball einf�gen
                        g1.GetComponent<Rigidbody>().AddForce(rightHandRef.transform.forward * 100, ForceMode.Impulse);
                         Destroy(g1, waitTillDespawn);
                        fired = true;
                        timestamp = Time.time + waitTillFireAgain;
                    }
            }
            else
            {
                if(Time.time >= timestamp)
                    {
                        g1 = Instantiate(fireBall, leftHandRef.transform.position + leftHandRef.transform.forward, Quaternion.identity);
                        //TODO:JAN-> Hier weiterer Code f�r den Fireball einf�gen
                        g1.GetComponent<Rigidbody>().AddForce(leftHandRef.transform.forward * 100, ForceMode.Impulse);
                        Destroy(g1, waitTillDespawn);
                        fired = true;
                        timestamp = Time.time + waitTillFireAgain;
                    }
               
                }
            }
    }
}