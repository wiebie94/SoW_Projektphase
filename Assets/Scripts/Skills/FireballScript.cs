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
    private GameObject AimHelpLeft;
    private GameObject AimHelpRight;
    private bool fired = false;
    private bool _isDone = false;
    private float timestamp;

    void Start()
    {
        skillMenu = GameObject.Find("Player").GetComponent<ShowSkillMenu>();
        AimHelpLeft = leftHandRef.transform.Find("AimHelpParent").gameObject;
        AimHelpRight = rightHandRef.transform.Find("AimHelpParent").gameObject;

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

    private bool checkForFireEnable()
    {


        RaycastHit hit;
        Ray ray;
        int layerMask = 1 << 14;


        if (skillMenu.menuHandLeft)
        {
            if (Physics.Raycast(rightHandRef.transform.position, rightHandRef.transform.forward,1.5f, layerMask))  //layerforraycast anpassen
            {
                Debug.Log("Fire.False");
                return false;
            }
        }
        else
        {
            if (Physics.Raycast(leftHandRef.transform.position, leftHandRef.transform.forward,1.5f, layerMask))  //layerforraycast anpassen
            {
                Debug.Log("Fire.False");
                return false;
            }

        }
        Debug.DrawRay(rightHandRef.transform.position, rightHandRef.transform.forward * 1.5f, Color.green);

        return true;

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
        bool fireOk = checkForFireEnable();
        Debug.Log(fireOk);

        if (fireOk)
        {
            Debug.Log("FireOK");
            if (skillMenu._isFireBallActive)
            {


                if (skillMenu.menuHandLeft)
                {
                    if (Time.time >= timestamp)
                    {


                        StartCoroutine(fadeAimHelp());
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
                    if (Time.time >= timestamp)
                    {

                        StartCoroutine(fadeAimHelp());
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
        else
        {
            Debug.Log("OnlyTelekineseAllowed");

        }
    }
    IEnumerator fadeAimHelp()
    {

        AimHelpLeft.transform.GetChild(1).transform.gameObject.SetActive(false);
        AimHelpRight.transform.GetChild(1).transform.gameObject.SetActive(false);
        AimHelpLeft.transform.GetChild(0).GetComponent<LineRenderer>().SetPosition(1, new Vector3(0, 0, 0));
        AimHelpRight.transform.GetChild(0).GetComponent<LineRenderer>().SetPosition(1, new Vector3(0, 0, 0));

        for (float x = 0; x < waitTillFireAgain; x += Time.deltaTime)
        {


            float clamValue = +(((1 / waitTillFireAgain) / 10) * x) * 10;
            Debug.Log(clamValue);
            if (clamValue >= 0.1f)
            {
                AimHelpLeft.transform.GetChild(1).transform.gameObject.SetActive(true);
                AimHelpRight.transform.GetChild(1).transform.gameObject.SetActive(true);
            }
            AimHelpLeft.transform.GetChild(0).GetComponent<LineRenderer>().SetPosition(1, new Vector3(0, 0, clamValue));
            AimHelpRight.transform.GetChild(0).GetComponent<LineRenderer>().SetPosition(1, new Vector3(0, 0, clamValue));
            yield return new WaitForSeconds(0.00005f);
        }

    }
}