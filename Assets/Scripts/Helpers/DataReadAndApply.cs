using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class DataReadAndApply : MonoBehaviour
{
    [SerializeField] GameSave gameSave;
    [SerializeField] AudioMixer mixer;
    [SerializeField] PlayerController player;
    void Start()
    {
        if (gameSave == null)
        {
            Debug.LogError("gameSave not set!");
        }

        if (player == null)
        {
            Debug.LogError("player not set!");
        }

        Invoke(nameof(LateStart), 0.05f);
    }

    void LateStart()
    {
        mixer.SetFloat("MasterVolume", gameSave.getGameData().volume);
        player.SetPlayerHeight(gameSave.getGameData().playerHeight);
    }
}
