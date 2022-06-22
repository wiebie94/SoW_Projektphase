using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySript : MonoBehaviour
{
    public int keyIndex;

    private LevelKeyController keyController;
    private void Start()
    {
        this.keyController = GameObject.Find("KeyManager").GetComponent<LevelKeyController>();
    }

    public void grabKey() 
    {
        this.keyController.OnLockKeySave(keyIndex);
    }
}
