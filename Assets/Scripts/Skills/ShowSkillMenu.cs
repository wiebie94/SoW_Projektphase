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
    public bool isGripPressed = false;
    public bool menuHandLeft = false;
    public bool isCoroutineFinished = false;
    [SerializeField] private GameObject SkillMenu;
    public bool _isFireBallActive = false;
    public bool _isTeleportActive = false;
    public GameObject leftHandSkinMesh;
    private bool _isHandSkinSet = false;
    public GameObject rightHandSkinMesh;
    public Material handMaterialFireball;
    public Material handMaterialTeleport;
    public Material defaultHandSkin;
    public bool _isDone = false;
    public GameObject loadingCircle;
    public IEnumerator iEnem;
    public GameObject lCircleInstantiated;

    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {

        if (menuHandLeft == true)
        {
            SkillMenu.transform.position = leftHandRef.transform.position;
            SkillMenu.transform.rotation = leftHandRef.transform.rotation;
        }
        else
        {
            SkillMenu.transform.position = rightHandRef.transform.position;
            SkillMenu.transform.rotation = rightHandRef.transform.rotation;
        }

        CalculateParalelVector();
        //Wenn es Paralell ist
        if (isParallel)
        {

            //Wenn das Skill Menü geschlossen ist und die 2 Sekunden noch nicht abgelaufen sind
            if (!isSkillMenuOpen && !isCoroutineFinished && !_isFireBallActive && !_isTeleportActive)
            {
                iEnem = waitForMenuShow();
                Debug.Log("STARTING COROUTINE");

                //Starte die zwei Sekunden und zeige das Skillmenu
                //TODO: Öffnen des Menüs zulassen bei aktivem Zauber auf der anderen Hand !?
                StartCoroutine(iEnem);
                isCoroutineFinished = true;
                
            }

        }
        
        //Wenn es nicht paralell ist und das Skillmenü aber gerade angezeigt wird, dann schließe es
        if (!isParallel && isSkillMenuOpen)
        {
            if (lCircleInstantiated)
            {
                Destroy(lCircleInstantiated);
            }
            closeSkillMenu();
            
        }
        
        //Wenn es nicht paralell ist und die Coroutine noch nicht abgeschlossen wurde
        if (!isParallel && !_isDone)
        {

            if (iEnem.Current != null)
            {
                Debug.Log("STOPPING COROUTINE");

                StopCoroutine(iEnem);
                if (lCircleInstantiated)
                {
                    Destroy(lCircleInstantiated);
                }
                closeSkillMenu();
                _isDone = true;
            }
           
            

        }
        

    }

    public IEnumerator waitForMenuShow()
    {
       
        _isDone = false;
        if (menuHandLeft)
        {
            lCircleInstantiated =  Instantiate(loadingCircle, leftHandRef.transform.position, Quaternion.identity);
            lCircleInstantiated.transform.SetParent(leftHandRef.transform);
            lCircleInstantiated.transform.localPosition = new Vector3(0, -0.1f, 0.1f);

        }
        else
        {
            lCircleInstantiated =  Instantiate(loadingCircle, rightHandRef.transform.position, Quaternion.identity);
            lCircleInstantiated.transform.SetParent(rightHandRef.transform);
            lCircleInstantiated.transform.localPosition = new Vector3(0, -0.1f, 0.1f);


        }
        yield return new WaitForSeconds(2);
        if (lCircleInstantiated)
        {
            Destroy(lCircleInstantiated);
        }
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


    public void SelectedSkill(string name)
    {
        if (name.Equals("FireOrb"))
        {

            _isFireBallActive = true;
            _isTeleportActive = false;
            if (_isHandSkinSet)
            {
                leftHandSkinMesh.GetComponent<SkinnedMeshRenderer>().material = defaultHandSkin;
                rightHandSkinMesh.GetComponent<SkinnedMeshRenderer>().material = defaultHandSkin;
                _isHandSkinSet = false;

            }
            //g1.transform.SetParent(RightHandController.transform);
            if (this.menuHandLeft == true)
            {
                if (_isHandSkinSet == false)
                {
                    _isHandSkinSet = true;
                    rightHandSkinMesh.GetComponent<SkinnedMeshRenderer>().material = handMaterialFireball;
                }

            }
            else
            {
                if (_isHandSkinSet == false)
                {
                    _isHandSkinSet = true;
                    leftHandSkinMesh.GetComponent<SkinnedMeshRenderer>().material = handMaterialFireball;
                }


            }
        }
        if (name.Equals("TeleportOrb"))
        {

            _isTeleportActive = true;
            _isFireBallActive = false;
            if (_isHandSkinSet)
            {
                leftHandSkinMesh.GetComponent<SkinnedMeshRenderer>().material = defaultHandSkin;
                rightHandSkinMesh.GetComponent<SkinnedMeshRenderer>().material = defaultHandSkin;
                _isHandSkinSet = false;

            }
            //g1.transform.SetParent(RightHandController.transform);
            if (this.menuHandLeft == true)
            {
                if (_isHandSkinSet == false)
                {
                    _isHandSkinSet = true;
                    rightHandSkinMesh.GetComponent<SkinnedMeshRenderer>().material = handMaterialTeleport;
                }

            }
            else
            {
                if (_isHandSkinSet == false)
                {
                    _isHandSkinSet = true;
                    leftHandSkinMesh.GetComponent<SkinnedMeshRenderer>().material = handMaterialTeleport;
                }


            }
        }
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


    public void resetHand()
    {
        _isFireBallActive = false;
        _isTeleportActive = false;
        rightHandSkinMesh.GetComponent<SkinnedMeshRenderer>().material = defaultHandSkin;
        leftHandSkinMesh.GetComponent<SkinnedMeshRenderer>().material = defaultHandSkin;

    }




}
