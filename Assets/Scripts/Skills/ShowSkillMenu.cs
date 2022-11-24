using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ShowSkillMenu : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject leftHandRef;
    public GameObject rightHandRef;
    private bool isParallel =false;
    private bool isSkillMenuOpen = false;
    public bool isGripPressed = false;
    public bool menuHandLeft = false;
    public bool isCoroutineFinished = false;
    [SerializeField] private GameObject SkillMenu;
    public bool _isFireBallActive = false;
    public bool _isTelekinesisActive = false;
    public bool _isTeleportActive = false;
    public GameObject leftHandSkinMesh;
    private bool _isHandSkinSet = false;
    public GameObject rightHandSkinMesh;
    public Material handMaterialFireball;
    public Material handMaterialTeleport;
    public Material handMaterialTelekinesis;
    private Material defaultHandSkin;
    public bool _isDone = false;
    public bool _oneTime = false;
    public Coroutine showMenuCoroutine;
    public Coroutine deactivateMenuCoroutine;
    [SerializeField] private GameObject teleportManager;
    private TeleportKugelManager teleportManagerScript;
    private GameObject AimHelpLeft;
    private GameObject AimHelpRight;
    public Material AimHelpFireBallMaterial;
    public Material AimHelpTelekineseMaterial;
    public Material AimHelpFireBallParticlesMaterial;
    public Material AimHelpTelekineseParticlesMaterial;
    [SerializeField] Load_Spells lCircle;
    [SerializeField] int AimHelpLengthFireball;
    [SerializeField] int AimHelpLengthTelekinese;

    [SerializeField] float loadingTime = 1.5f;
    [SerializeField] float fadeOut = 0.5f;

    [SerializeField] float equipDelay = 0.1f;

    public delegate void OnTelekineseSkillTriggered(HandType hand);
    public static event OnTelekineseSkillTriggered onTelekineseSkillTriggered;

    public delegate void OnTelekineseSkillUntriggered();
    public static event OnTelekineseSkillUntriggered onTelekineseSkillUntriggered;

    public delegate void OnFireballSkillTriggered();
    public static event OnFireballSkillTriggered onFireballSkillTriggered;

    public delegate void OnFireballSkillUntriggered();
    public static event OnFireballSkillUntriggered onFireballSkillUntriggered;




    void Start()
    {
        this.teleportManagerScript = this.teleportManager.GetComponent<TeleportKugelManager>();
        AimHelpLeft = leftHandRef.transform.Find("AimHelpParent").gameObject;
        AimHelpRight = rightHandRef.transform.Find("AimHelpParent").gameObject;
        AimHelpLeft.SetActive(false);
        AimHelpRight.SetActive(false);
        defaultHandSkin = leftHandSkinMesh.GetComponent<SkinnedMeshRenderer>().material;
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
        if (!_isFireBallActive && !_isTeleportActive && !_isTelekinesisActive)
        {
            CalculateParalelVector();
        }

        //Wenn es Paralell ist
        if (isParallel)
        {

            //Wenn das Skill Men� geschlossen ist und die 2 Sekunden noch nicht abgelaufen sind
            if (!isSkillMenuOpen && !isCoroutineFinished && !_isFireBallActive && !_isTeleportActive && !_isTelekinesisActive)
            {
                //Debug.Log("showMenuCoroutine:");

                showMenuCoroutine = StartCoroutine(waitForMenuShow());
                //Starte die zwei Sekunden und zeige das Skillmenu
                //TODO: �ffnen des Men�s zulassen bei aktivem Zauber auf der anderen Hand !?
                isCoroutineFinished = true;

            }

        }

        //Wenn es nicht paralell ist und das Skillmen� aber gerade angezeigt wird, dann schlie�e es
        if (!isParallel && !_isDone)
        {
            if (showMenuCoroutine != null)
            {

                StopCoroutine(showMenuCoroutine);
                lCircle.StopLoading();
                deactivateMenu();
                _isDone = true;
            }


        }


    }


    private void deactivateMenu()
    {
        if (deactivateMenuCoroutine != null)
        {
            StopCoroutine(deactivateMenuCoroutine);
        }
        deactivateMenuCoroutine = StartCoroutine(closeSkillMenu());
    }

    public IEnumerator waitForMenuShow()
    {
        if (!lCircle.isActiveAndEnabled)
        {
            Debug.Log("lCircle is not active!");
            lCircle.enabled = true;
        }

        _isDone = false;
        if (menuHandLeft)
        {
            lCircle.StartLoading(loadingTime);
            lCircle.transform.SetParent(leftHandRef.transform);
            lCircle.transform.localPosition = new Vector3(0, -0.1f, 0.1f);

        }
        else
        {
            lCircle.StartLoading(loadingTime);
            lCircle.transform.SetParent(rightHandRef.transform);
            lCircle.transform.localPosition = new Vector3(0, -0.1f, 0.1f);


        }
        yield return new WaitForSeconds(loadingTime);
        lCircle.StopLoading();
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
        if (Vector3.Angle(upVektor, menuHandUpLeft) < 30f)
        {
            menuHandLeft = true;
            isParallel = true;
            _oneTime = false;
        }
        //Andernfalls ist es die rechte Hand auf der das Men� sein soll 
        else if (Vector3.Angle(upVektor, menuHandUpRight) < 30f)
        {
            menuHandLeft = false;
            isParallel = true;
            _oneTime = false;
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
        for (int i = 0; i < SkillMenu.transform.childCount; i++)
        {
            SkillMenu.transform.GetChild(i).GetComponent<ParticleController>().activateParticles();
        }
        isSkillMenuOpen = true;


    }

    IEnumerator closeSkillMenu()
    {
        for (int i = 0; i < SkillMenu.transform.childCount; i++)
        {
            SkillMenu.transform.GetChild(i).GetComponent<ParticleController>().deactivateParticles();
        }
        yield return new WaitForSeconds(fadeOut);
        //Debug.Log("Setting Skillmenu Active false");
        SkillMenu.SetActive(false);
        //Debug.Log("Setting Skillmenu Active false2");

        isSkillMenuOpen = false;
        isCoroutineFinished = false;
    }

    private void Grip_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        isGripPressed = true;
    }

    private void Grip_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        isGripPressed = false;


    }


    public void SelectedSkill(string name)
    {
        lCircle.StopLoading();
        deactivateMenu();
        if (name.Equals("FireOrb"))
        {
            AimHelpLeft.transform.GetChild(0).GetComponent<LineRenderer>().material = AimHelpFireBallMaterial;
            AimHelpRight.transform.GetChild(0).GetComponent<LineRenderer>().material = AimHelpFireBallMaterial;
            AimHelpLeft.transform.GetChild(0).GetComponent<LineRenderer>().SetPosition(1, new Vector3(0, 0, AimHelpLengthFireball));
            AimHelpRight.transform.GetChild(0).GetComponent<LineRenderer>().SetPosition(1, new Vector3(0, 0, AimHelpLengthFireball));
            AimHelpRight.transform.GetChild(1).GetComponent<ParticleSystem>().GetComponent<Renderer>().material = AimHelpFireBallParticlesMaterial;
            AimHelpLeft.transform.GetChild(1).GetComponent<ParticleSystem>().GetComponent<Renderer>().material = AimHelpFireBallParticlesMaterial;
            AimHelpLeft.transform.GetChild(1).GetComponent<ParticleSystemRenderer>().trailMaterial = AimHelpFireBallParticlesMaterial;
            AimHelpRight.transform.GetChild(1).GetComponent<ParticleSystemRenderer>().trailMaterial = AimHelpFireBallParticlesMaterial;

            onFireballSkillTriggered.Invoke();
            _isFireBallActive = true;
            _isTeleportActive = false;
            _isTelekinesisActive = false;

            if (_isHandSkinSet)
            {
                leftHandSkinMesh.GetComponent<SkinnedMeshRenderer>().material = defaultHandSkin;
                rightHandSkinMesh.GetComponent<SkinnedMeshRenderer>().material = defaultHandSkin;
                _isHandSkinSet = false;

            }
            //g1.transform.SetParent(RightHandController.transform);
            if (this.menuHandLeft == true)
            {
                if (AimHelpLeft.activeSelf)
                {
                    AimHelpLeft.SetActive(false);
                }
                AimHelpRight.SetActive(true);
                if (_isHandSkinSet == false)
                {
                    _isHandSkinSet = true;
                    rightHandSkinMesh.GetComponent<SkinnedMeshRenderer>().material = handMaterialFireball;
                }

            }
            else
            {
                if(AimHelpRight.activeSelf)
                {
                    AimHelpRight.SetActive(false);
                }
                AimHelpLeft.SetActive(true);
                if (_isHandSkinSet == false)
                {
                    _isHandSkinSet = true;
                    leftHandSkinMesh.GetComponent<SkinnedMeshRenderer>().material = handMaterialFireball;
                }


            }
        }
        if (name.Equals("TeleportOrb"))
        {
            GameObject teleportOrb = this.teleportManagerScript.creatKugel(this.SkillMenu.transform.Find("TeleportOrb").position);
            if (teleportOrb == null) return;

            _isTeleportActive = true;
            _isFireBallActive = false;
            _isTelekinesisActive = false;

            /*if (_isHandSkinSet)
            {
                leftHandSkinMesh.GetComponent<SkinnedMeshRenderer>().material = defaultHandSkin;
                rightHandSkinMesh.GetComponent<SkinnedMeshRenderer>().material = defaultHandSkin;
                _isHandSkinSet = false;

            }*/
            if (this.menuHandLeft == true)
            {
                /*if (_isHandSkinSet == false)
                {
                    _isHandSkinSet = true;
                    rightHandSkinMesh.GetComponent<SkinnedMeshRenderer>().material = handMaterialTeleport;
                }*/
                EquipItemToHand(HandType.Right, teleportOrb);
            }
            else
            {
                /*if (_isHandSkinSet == false)
                {
                    _isHandSkinSet = true;
                    leftHandSkinMesh.GetComponent<SkinnedMeshRenderer>().material = handMaterialTeleport;
                }*/
                EquipItemToHand(HandType.Left, teleportOrb);
            }

        }

        if (name.Equals("Telekinesis Orb"))
        {
            AimHelpLeft.transform.GetChild(0).GetComponent<LineRenderer>().material = AimHelpTelekineseMaterial;
            AimHelpRight.transform.GetChild(0).GetComponent<LineRenderer>().material = AimHelpTelekineseMaterial;
            AimHelpLeft.transform.GetChild(0).GetComponent<LineRenderer>().SetPosition(1,new Vector3(0, 0, AimHelpLengthTelekinese));
            AimHelpRight.transform.GetChild(0).GetComponent<LineRenderer>().SetPosition(1, new Vector3(0, 0, AimHelpLengthTelekinese));
            AimHelpRight.transform.GetChild(1).GetComponent<ParticleSystem>().GetComponent<Renderer>().material = AimHelpTelekineseParticlesMaterial;
            AimHelpLeft.transform.GetChild(1).GetComponent<ParticleSystem>().GetComponent<Renderer>().material = AimHelpTelekineseParticlesMaterial;
            AimHelpLeft.transform.GetChild(1).GetComponent<ParticleSystemRenderer>().trailMaterial = AimHelpTelekineseParticlesMaterial;
            AimHelpRight.transform.GetChild(1).GetComponent<ParticleSystemRenderer>().trailMaterial = AimHelpTelekineseParticlesMaterial;
            _isTeleportActive = false;
            _isFireBallActive = false;
            _isTelekinesisActive = true;
            
            if (_isHandSkinSet)
            {
                leftHandSkinMesh.GetComponent<SkinnedMeshRenderer>().material = defaultHandSkin;
                rightHandSkinMesh.GetComponent<SkinnedMeshRenderer>().material = defaultHandSkin;
                _isHandSkinSet = false;

            }
            //g1.transform.SetParent(RightHandController.transform);
            if (this.menuHandLeft == true)
            {
                onTelekineseSkillTriggered.Invoke(HandType.Right);
                if (AimHelpLeft.activeSelf)
                {
                    AimHelpLeft.SetActive(false);
                }
                AimHelpRight.SetActive(true);
                if (_isHandSkinSet == false)
                {
                    _isHandSkinSet = true;
                    rightHandSkinMesh.GetComponent<SkinnedMeshRenderer>().material = handMaterialTelekinesis;
                }

            }
            else
            {
                onTelekineseSkillTriggered.Invoke(HandType.Left);
                if (AimHelpRight.activeSelf)
                {
                    AimHelpRight.SetActive(false);
                }
                AimHelpLeft.SetActive(true);
                if (_isHandSkinSet == false)
                {
                    _isHandSkinSet = true;
                    leftHandSkinMesh.GetComponent<SkinnedMeshRenderer>().material = handMaterialTelekinesis;
                }


            }
        }
    }


    private void EquipItemToHand(HandType hand, GameObject item)
    {
        Hand handScript;

        if (hand == HandType.Left)
            handScript = this.leftHandRef.transform.parent.GetComponent<Hand>();
        else
            handScript = this.rightHandRef.transform.parent.GetComponent<Hand>();

        if (handScript == null)
        {
            Debug.LogError("Hand Script is null!");
            return;
        }

        StartCoroutine(EquipCoroutine(handScript, item));
    }

    private IEnumerator EquipCoroutine(Hand handScript, GameObject item)
    {
        yield return new WaitForSeconds(equipDelay);

        handScript.Equip(item);
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
        if (AimHelpLeft.activeSelf)
        {
            AimHelpLeft.SetActive(false);
        }
        if (AimHelpRight.activeSelf)
        {
            AimHelpRight.SetActive(false);
        }
        if (_isTelekinesisActive)
        {
            onTelekineseSkillUntriggered.Invoke();
            _isTelekinesisActive = false;
        }
        if (_isFireBallActive)
        {
            onFireballSkillUntriggered.Invoke();
            _isFireBallActive = false;
        }


        _isTeleportActive = false;
        

        rightHandSkinMesh.GetComponent<SkinnedMeshRenderer>().material = defaultHandSkin;
        leftHandSkinMesh.GetComponent<SkinnedMeshRenderer>().material = defaultHandSkin;

    }




}
