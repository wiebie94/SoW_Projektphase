using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelKeyController : MonoBehaviour
{
    public List<GameObject> keysPrefab;
    public GameSave gameSave;

    private Transform[] spawnPointKey;


    void Start()
    {
        initSpawnPont();
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
        /*foreach (int keyIndex in this.gameSave.getGameData().hasKeyIndex) 
        {
            if (keyIndex >= this.keysPrefab.Count) 
            {
                Debug.LogWarning("Achtung den KeyIndex "+ keyIndex + "gibt es nicht");
                continue;
            }
            if (keyIndex >= this.spawnPointKey.Length)
            {
                Debug.LogWarning("Achtung es gibt f�r den KeyIndex " + keyIndex + " keinen KeySpawn");
                continue;
            }
            GameObject tmp = Instantiate(this.keysPrefab[keyIndex], this.spawnPointKey[keyIndex].position, Quaternion.identity);

        }*/
    }
    public bool OnLockKeySave(int indexKey) 
    {
        /*if (indexKey >= this.keysPrefab.Count) return false;
        
        if(this.gameSave.getGameData().hasKeyIndex.IndexOf(indexKey) >= 0) return false;

        this.gameSave.getGameData().hasKeyIndex.Add(indexKey); 
        this.gameSave.SaveData();*/
        return true;
    }
}
