using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TriggerSkill : MonoBehaviour
{
    public GameObject LeftHandController;
    public GameObject RightHandController;
    [SerializeField]
    private ShowSkillMenu skillMenu;
    private ActionBasedController controllerLeft;
    private ActionBasedController controllerRight;
    public GameObject fireball;
    public GameObject teleportBall;
    private bool isGripPressed = false;
    
    // Start is called before the first frame update
    void Start()
    {
        skillMenu = LeftHandController.GetComponent<ShowSkillMenu>();
        ActionBasedController[] controllerArray = ActionBasedController.FindObjectsOfType<ActionBasedController>();
        controllerLeft = controllerArray[0];
        controllerRight = controllerArray[1];
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RightHand")){

            controllerRight.selectAction.action.performed += Grip_performed;
            controllerRight.selectAction.action.canceled += Grip_canceled;


        }
        
       
  
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("RightHand")){

            controllerRight.selectAction.action.performed -= Grip_performed;
            controllerRight.selectAction.action.canceled -= Grip_canceled;
            isGripPressed = false;
        }
    }

    private void Grip_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Debug.Log("show menu in");
        isGripPressed = true;
        skillMenu.closeSkillMenu();
        GameObject g1 = Instantiate(fireball);
        //g1.transform.SetParent(RightHandController.transform);
        g1.transform.position = RightHandController.transform.position;
    }

    private void Grip_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Debug.Log("show menu out");
        isGripPressed = false;
    }

}
