using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TriggerSkill : MonoBehaviour
{
    [SerializeField]
    private ShowSkillMenu skillMenu;
    private ActionBasedController controllerLeft;
    private ActionBasedController controllerRight;
    private bool isGripPressed = false;
    private ActionBasedController[] controllerArray;
    // Start is called before the first frame update

    private void Awake()
    {
        
    }


    void Start()
    {
        skillMenu = GameObject.Find("Player").GetComponent<ShowSkillMenu>();
       controllerArray = ActionBasedController.FindObjectsOfType<ActionBasedController>();

       // Debug.Log("ON ENABLE SETTING CONTROLLER");

        if (controllerArray[0].name.Equals("LeftHand Controller"))
        {
            //Debug.Log("Setting Left to 0");
            controllerLeft = controllerArray[0];

        }
        if (controllerArray[1].name.Equals("LeftHand Controller"))
        {
            //Debug.Log("Setting Left to 1");

            controllerLeft = controllerArray[1];
        }
        if (controllerArray[0].name.Equals("RightHand Controller"))
        {
            //Debug.Log("Setting Right to 0");

            controllerRight = controllerArray[0];
        }
        if (controllerArray[1].name.Equals("RightHand Controller"))
        {
            //Debug.Log("Setting Right to 1");

            controllerRight = controllerArray[1];
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void OnTriggerEnter(Collider other)
    {
        if(skillMenu.menuHandLeft == true)
        {
            if (other.CompareTag("RightHand"))
            {


                controllerRight.selectAction.action.performed += Grip_performed;


            }
        }
        else
        {
            if (other.CompareTag("LeftHand"))
            {
                //Subscribe mit dem LinkenController zu Performed und Cancelled
                controllerLeft.selectAction.action.performed += Grip_performed;


            }
        }
        
        
       
  
    }
    private void OnTriggerExit(Collider other)
    {
        if (skillMenu.menuHandLeft == true)
        {

            if (other.CompareTag("RightHand"))
            {

                controllerRight.selectAction.action.performed -= Grip_performed;

            }
        }
        else
        {
            if (other.CompareTag("LeftHand"))
            {

                controllerLeft.selectAction.action.performed -= Grip_performed;
            }
        }
    }
    private void Grip_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (skillMenu.menuHandLeft)
        {
            controllerRight.selectAction.action.performed -= Grip_performed;
            controllerRight.selectAction.action.canceled += Grip_canceled;
            controllerLeft.selectAction.action.performed -= Grip_performed;
            controllerLeft.selectAction.action.canceled -= Grip_canceled;


        }
        else
        {
            controllerLeft.selectAction.action.performed -= Grip_performed;
            controllerLeft.selectAction.action.canceled += Grip_canceled;
            controllerRight.selectAction.action.performed -= Grip_performed;
            controllerRight.selectAction.action.canceled -= Grip_canceled;



        }

        skillMenu.isGripPressed = true;
        skillMenu.SendMessage("SelectedSkill", this.name);

    }

    private void Grip_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (skillMenu.menuHandLeft == true)
        {

            controllerRight.selectAction.action.performed -= Grip_performed;
            controllerRight.selectAction.action.canceled -= Grip_canceled;
            
        }
        else
        {
            controllerLeft.selectAction.action.performed -= Grip_performed;
            controllerLeft.selectAction.action.canceled -= Grip_canceled;


        }
       
        skillMenu.isGripPressed = false;
        Debug.Log("Eigentlich sollte hier unsubscribed werden");
        skillMenu.resetHand();
        
    }

}
