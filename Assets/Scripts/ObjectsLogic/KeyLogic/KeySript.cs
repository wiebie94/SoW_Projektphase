using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySript : MonoBehaviour
{
    public Color color;
    private LevelKeyController keyController;
    private DissolveController dissolveController;
    private void Start()
    {
        this.dissolveController = this.GetComponent<DissolveController>();
        this.keyController = GameObject.Find("KeyManager").GetComponent<LevelKeyController>();
        if (this.keyController == null)
            Debug.LogWarning("kein KeyManager in der Scene");
        this.GetComponentInChildren<Renderer>().material.SetColor("_KeyLockColor", color);

    }
    public void setColor(Color color) { 
        this.color = color;
        this.GetComponentInChildren<Renderer>().material.SetColor("_KeyLockColor", color);
    }
    public void grabKey() 
    {
        this.keyController.OnLockKeySave(this.color);
    }
    public void startDissolve() 
    {
        this.dissolveController.dissolveDown();
        Invoke("destroyKey", this.dissolveController.duration);

    }
    private void destroyKey()
    {

        //funktion das der schlüssel los gelassen wird
        //Destroy(this.transform.parent.gameObject);
    }
}
