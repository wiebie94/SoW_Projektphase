using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Simulation;

public class UIMenu : MonoBehaviour
{
    [SerializeField] PlayerController player;

    [SerializeField] InputActionProperty menuAction;

    [SerializeField] GameObject pauseScreen;
    [SerializeField] GameObject optionsScreen;
    [SerializeField] GameObject exitScreen;
    [SerializeField] GameObject resetScreen;

    [SerializeField] GameObject TreadmillButton;
    [SerializeField] GameObject ControllerButton;
    [SerializeField] private Texture greenButtonTex;
    [SerializeField] private Texture redButtonTex;

    [SerializeField] GameObject XROriginGO;
    [SerializeField] GameObject XRLocoMotion;

    private GameObject content;
    private ResetPlayer rPlayer;
    List<GameObject> allScreens;

    private UIPLayerFollow followScript;

    private bool isMenuOpened = false;

    private bool treadmillOn = false;
    private bool controllerOn = true;

    [SerializeField] GameSave gameSave;

    private void Start()
    { 
        if (player == null)
        {
            Debug.Log("Player not set!");
        }

        if (gameSave == null)
        {
            Debug.Log("gameSave not set!");
        }

        content = transform.GetChild(0).gameObject;
        followScript = GetComponent<UIPLayerFollow>();

        allScreens = new List<GameObject>();

        allScreens.Add(pauseScreen);
        allScreens.Add(resetScreen); 
        allScreens.Add(optionsScreen);
        allScreens.Add(exitScreen);

        menuAction.action.performed += MenuButtonPressed;

        TreadmillButton.GetComponent<Renderer>().material.SetTexture("_BaseMap", redButtonTex);
        ControllerButton.GetComponent<Renderer>().material.SetTexture("_BaseMap", greenButtonTex);
    }

    private void MenuButtonPressed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        MenuToggle();
    }

    public void MenuToggle()
    {
        if (!isMenuOpened)
            OpenMenu();
        else
            CloseMenu();
    }

    public void CloseMenu()
    {


        isMenuOpened = false;
        CloseAllScreens();

        content.SetActive(false);
        followScript.StopFollowingPlayer();

        player.DisableFingerTrigger();
        gameSave.SaveData();
    }

    public void OpenMenu()
    {
        if (isMenuOpened)
            return;

        followScript.StartFollowingPlayer();

        isMenuOpened = true;
        content.SetActive(true);
        //resetButton.SetActive(true);

        OpenPauseScreen();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ResetGame()
    {
        Debug.Log("RESETTING GAME");
        rPlayer.respawnPlayerAndResetData();
    }

    public void OpenPauseScreen()
    {
        CloseAllScreens();

        pauseScreen.SetActive(true);
        //ResetButtons(pauseScreen.transform);
    }

    public void OpenResetScreen()
    {
        CloseAllScreens();

        resetScreen.SetActive(true);
        //ResetButtons(pauseScreen.transform);
    }

    public void OpenExitScreen()
    {
        CloseAllScreens();

        exitScreen.SetActive(true);
        //ResetButtons(pauseScreen.transform);
    }

    public void OpenOptionsScreen()
    {
        CloseAllScreens();

        optionsScreen.SetActive(true);
        //ResetButtons(optionsScreen.transform);
    }

    //Treadmill Steuerung an und aus stellen
    public void TreadmillButtonPressed()
    {
        if (!treadmillOn)
        {
            treadmillOn = true;
            TreadmillButton.GetComponent<Renderer>().material.SetTexture("_BaseMap", greenButtonTex);
            this.GetComponentInParent<KATDevice>().setTreadmillOn();
        }
        else
        {
            treadmillOn = false;
            TreadmillButton.GetComponent<Renderer>().material.SetTexture("_BaseMap", redButtonTex);
            this.GetComponentInParent<KATDevice>().setTreadmillOff();
        }
    } 

    //Controller Steuerung an und aus stellen
    public void ControllerButtonPressed()
    {
        if (!controllerOn)
        {
            
            controllerOn = true;
            ControllerButton.GetComponent<Renderer>().material.SetTexture("_BaseMap", greenButtonTex);
            XROriginGO.GetComponent<PlayerController>().setControllerOn();
            XRLocoMotion.GetComponent<SnapTurnProviderBase>().enabled = true;
        }
        else
        {
            controllerOn = false;
            ControllerButton.GetComponent<Renderer>().material.SetTexture("_BaseMap", redButtonTex);
            XROriginGO.GetComponent<PlayerController>().setControllerOff();
            XRLocoMotion.GetComponent<SnapTurnProviderBase>().enabled = false;
        }
    }

    private void ResetButtons(Transform parent)
    {
        int children = transform.childCount;

        for (int i = 0; i < children; i++)
        {
            ButtonLogic button = parent.GetChild(i).GetComponent<ButtonLogic>();

            if (button != null)
                button.ResetButton();
        }
    }

    private void CloseAllScreens()
    {
        foreach(GameObject screen in allScreens)
        {
            screen.SetActive(false);
        }
    }

    private void OnEnable()
    {
        menuAction.action.performed += MenuButtonPressed;
    }

    private void OnDisable()
    {
        menuAction.action.performed -= MenuButtonPressed;
    }
}
