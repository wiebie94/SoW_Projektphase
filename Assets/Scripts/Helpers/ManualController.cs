using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ManualController : MonoBehaviour
{
    [SerializeField] InputActionProperty playerHeightUp;
    [SerializeField] InputActionProperty playerHeightDown;
    [SerializeField] InputActionProperty playerHeightReset;

    [SerializeField] InputActionProperty menuToggle;
    [SerializeField] InputActionProperty sceneReset;
    [SerializeField] InputActionProperty progressReset;

    PlayerController player;

    [SerializeField] UIMenu uiMenu;

    private ResetPlayer resetScript;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        resetScript = GetComponent<ResetPlayer>();

        if (resetScript == null)
        {
            Debug.LogError("resetScript not set!");
        }

        if (player == null)
        {
            Debug.LogError("player not set!");
        }

        if (uiMenu == null)
        {
            Debug.LogError("UI Menu not set!");
        }

        playerHeightUp.action.performed += PlayerHeightUp;
        playerHeightDown.action.performed += PlayerHeightDown;
        playerHeightReset.action.performed += PlayerHeightReset;
        menuToggle.action.performed += MenuToggle;
        sceneReset.action.performed += SceneReset;
        progressReset.action.performed += ProgressReset;

        
    }

    void PlayerHeightUp(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        player.AddPlayerHeightOffset(0.05f);
    }

    void PlayerHeightDown(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        player.AddPlayerHeightOffset(-0.05f);
    }

    void PlayerHeightReset(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        player.ResetPlayerHeightOffset();
    }

    void MenuToggle(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (uiMenu == null)
        {
            Debug.LogError("UI Menu not set!");
        }

        uiMenu.MenuToggle();
    }

    void SceneReset(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        resetScript.respawnPlayer();
    }

    void ProgressReset(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        resetScript.respawnPlayerAndResetData();
    }
}
