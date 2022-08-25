using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class UIMenu : MonoBehaviour
{
    [SerializeField] PlayerController player;

    [SerializeField] InputActionProperty menuAction;

    [SerializeField] GameObject pauseScreen;
    [SerializeField] GameObject optionsScreen;
    [SerializeField] GameObject exitScreen;
    [SerializeField] GameObject resetScreen;
    [SerializeField] GameObject resetButton;
    [SerializeField] GameObject resetGameButton;

    private GameObject content;
    private ResetPlayer rPlayer;
    List<GameObject> allScreens;

    private UIPLayerFollow followScript;

    private bool isMenuOpened = false;

    private void Start()
    { 
        if (player == null)
        {
            Debug.Log("Player not set!");
        }

        content = transform.GetChild(0).gameObject;
        rPlayer = resetGameButton.GetComponent<ResetPlayer>();
        followScript = GetComponent<UIPLayerFollow>();

        allScreens = new List<GameObject>();

        allScreens.Add(pauseScreen);
        allScreens.Add(resetScreen);
        allScreens.Add(optionsScreen);
        allScreens.Add(exitScreen);

        menuAction.action.performed += MenuButtonPressed;

        Invoke(nameof(OpenMenu), 0.1f);
    }

    private void MenuButtonPressed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
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
