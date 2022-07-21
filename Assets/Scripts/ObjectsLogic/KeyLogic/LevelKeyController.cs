using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelKeyController : MonoBehaviour
{
    public GameObject keyPrefab;
    public GameSave gameSave;

    private Transform[] spawnPointKey;


    void Start()
    {
        initSpawnPont();
        //this.OnLockKeySave(new Color(1f,0f,0f,1f));
        spawnKeys();
    }

    private void initSpawnPont()
    {
        Transform keySpawn = this.transform.Find("KeySpawn");
        this.spawnPointKey = new Transform[keySpawn.childCount];
        for (int i = 0; i < spawnPointKey.Length; i++) 
        {
            this.spawnPointKey[i] = keySpawn.GetChild(i);
        }
    }

    private void spawnKeys()
    {
        Debug.Log(this.gameSave.getGameData());

        int spawnIndex = 0;

        foreach (Color keyColor in this.gameSave.getGameData().keySave) 
        {

            if (spawnIndex >= this.spawnPointKey.Length)
            {
                Debug.LogWarning("Achtung es gibt fuer den KeyIndex " + spawnIndex + " keinen KeySpawn");
                break;
            }
            GameObject tmp = Instantiate(keyPrefab, this.spawnPointKey[spawnIndex].position, Quaternion.identity);

            Renderer tmpR = tmp.GetComponentInChildren<Renderer>();

            // tmpR.material.SetColor("Key Mat", keyColor);
            tmp.GetComponent<KeyController>().setColor(keyColor);

            spawnIndex++;
        }
    }
    public bool OnLockKeySave(Color keyColor) 
    { 
        if(this.gameSave.getGameData().keySave.IndexOf(keyColor) >= 0) return false;

        this.gameSave.getGameData().keySave.Add(keyColor); 
        this.gameSave.SaveData();
        return true;
    }
}
