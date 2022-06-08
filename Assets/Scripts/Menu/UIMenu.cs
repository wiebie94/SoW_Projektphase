using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseScreen;
    [SerializeField] GameObject optionsScreen;

    private GameObject content;

    List<GameObject> allScreens;

    private void Start()
    {
        content = transform.GetChild(0).gameObject;

        allScreens = new List<GameObject>();

        allScreens.Add(pauseScreen);
        allScreens.Add(optionsScreen);
    }

    public void CloseMenu()
    {
        CloseAllScreens();

        content.SetActive(false);
    }

    public void OpenMenu()
    {
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
