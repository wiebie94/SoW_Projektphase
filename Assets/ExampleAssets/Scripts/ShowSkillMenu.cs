using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ShowSkillMenu : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject leftHandRef;
    [SerializeField]
    GameObject rightHandRef;
    private bool isParallel;
    private bool isSkillMenuOpen =false;
    private bool isGripPressed = false;
    [SerializeField] private GameObject SkillMenu;
    



    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        CalculateParalelVector();
        if (isParallel)
        {
            if (!isSkillMenuOpen)
            {
                StartCoroutine(waitForMenu());
            }

        }
        else if (isSkillMenuOpen)
        {
            closeSkillMenu();
        }

    }

    public IEnumerator waitForMenu()
    {
        yield return new WaitForSeconds(5);
        showSkillMenu();
    }

    public void CalculateParalelVector()
    {
        //Vektor zeichnen von der Hand des Spielers nach oben
        Vector3 menuHandPos = leftHandRef.transform.position;
        Vector3 upVektor = Vector3.up;
        Debug.DrawRay(menuHandPos, upVektor);
        //Vektor zeichen von der Hand des Spielers nach 
        Vector3 menuHandUp = -leftHandRef.transform.up;
        Debug.DrawRay(menuHandPos, menuHandUp);
        float AngleBetween  =Vector3.Angle(upVektor, menuHandUp);
        if(AngleBetween < 15f)
        {
            isParallel = true;
        }
        else
        {
            isParallel = false;
        }

    }

    public void showSkillMenu()
    {
        isSkillMenuOpen = true;
        SkillMenu.SetActive(true);

    }

    public void closeSkillMenu()
    {
       
            isSkillMenuOpen = false;
            SkillMenu.SetActive(false);
        
    }

    private void Grip_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Debug.Log("ControllerLeft" + obj.control.name);
        isGripPressed = true;
    }

    private void Grip_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Debug.Log("ControllerLeft" + obj.control.name);
        isGripPressed = false;
    }


   

    private void OnDestroy()
    {
        ClearBindings();
    }

    private void ClearBindings()
    {
   
    }

    public bool getGripPressed()
    {
        return isGripPressed;
    }




}
