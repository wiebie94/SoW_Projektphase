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

    void Start()
    {
        player = FindObjectOfType<PlayerController>();

        if (player == null)
        {
            Debug.LogError("player not set!");
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
        Debug.Log("MenuToggle!");
    }

    void SceneReset(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Debug.Log("SceneReset!");
    }

    void ProgressReset(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Debug.Log("ProgressReset!");
    }
}
