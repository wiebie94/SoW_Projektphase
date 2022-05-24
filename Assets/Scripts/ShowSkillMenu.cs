using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ShowSkillMenu : MonoBehaviour
{
    // Start is called before the first frame update
    
    public GameObject leftHandRef;
    public GameObject rightHandRef;
    private bool isParallel;
    private bool isSkillMenuOpen =false;
    private bool isGripPressed = false;
    public bool menuHandLeft = false;
    public bool isCoroutineFinished = false;
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
            if (!isSkillMenuOpen && !isCoroutineFinished)
            {
                
                StartCoroutine(waitForMenuShow());
                isCoroutineFinished = true;
            }

        }
        else if (isSkillMenuOpen)
        {
            closeSkillMenu();
            
        }

    }

    public IEnumerator waitForMenuShow()
    {
        yield return new WaitForSeconds(2);
        showSkillMenu();
    }



    public void CalculateParalelVector()
    {
        

        //Vektor zeichnen von der Hand des Spielers nach oben
        Vector3 menuHandPos = leftHandRef.transform.position;
        Vector3 upVektor = Vector3.up;
        //Vektor zeichen von der Hand des Spielers nach 
        Vector3 menuHandUpLeft = -leftHandRef.transform.up;
        Vector3 menuHandUpRight = -rightHandRef.transform.up;
        //Falls der Winkel zwischen der Linken Hand und dem Up Vektor kleiner als 30 Grad sein sollte, dann setz Linke Hand auf True und is Paralell auch
        if(Vector3.Angle(upVektor, menuHandUpLeft) < 30f){
            menuHandLeft = true;
            isParallel = true;
        }
        //Andernfalls ist es die rechte Hand auf der das Menü sein soll 
        else if(Vector3.Angle(upVektor, menuHandUpRight) < 30f)
        {
            menuHandLeft = false;
            isParallel = true;
        }
        //Falls es keins von beiden ist ist nichts paralell und deshalb setzen wir paralell auf falls
        else
        {
            isParallel = false;
        }

    }

    public void showSkillMenu()
    {
        //TODO: Von Alfons kommen hier Particle Spawn und Despawn Scripte, das ich hier nur die Methode des Scripts aufrufen muss
        SkillMenu.SetActive(true);
        isSkillMenuOpen = true;
        

    }

    public void closeSkillMenu()
    {
        //TODO: Von Alfons kommen hier Particle Spawn und Despawn Scripte, das ich hier nur die Methode des Scripts aufrufen muss

        SkillMenu.SetActive(false);
        isSkillMenuOpen = false;
        isCoroutineFinished = false;

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
