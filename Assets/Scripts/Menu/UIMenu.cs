using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class UIMenu : MonoBehaviour
{
    [SerializeField] InputActionProperty menuAction;

    [SerializeField] GameObject pauseScreen;
    [SerializeField] GameObject optionsScreen;

    private GameObject content;

    List<GameObject> allScreens;

    private UIPLayerFollow followScript;

    private bool isMenuOpened = false;

    private void Start()
    { 
        content = transform.GetChild(0).gameObject;

        followScript = GetComponent<UIPLayerFollow>();

        allScreens = new List<GameObject>();

        allScreens.Add(pauseScreen);
        allScreens.Add(optionsScreen);

        menuAction.action.performed += MenuButtonPressed;
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
    }

    public void OpenMenu()
    {
        if (isMenuOpened)
            return;

        followScript.StartFollowingPlayer();

        isMenuOpened = true;
        content.SetActive(true);

        OpenPauseScreen();
    }

    public void OpenPauseScreen()
    {
        CloseAllScreens();

        pauseScreen.SetActive(true);
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
}
