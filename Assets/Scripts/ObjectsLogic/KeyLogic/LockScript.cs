using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockScript : MonoBehaviour
{
    public Color color;
    public Color keyColor;
    private Animator animator;
    public bool test = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Transform keyLock = this.transform.Find("Lock_final_Prefab"); //.GetComponent<Renderer>().material.SetColor("_KeyLockColor", color);
        keyLock.GetChild(0).GetComponent<Renderer>().material.SetColor("_KeyLockColor", color);
  
        Transform key = this.transform.Find("Key_for_animation");
        key.GetComponent<Renderer>().material.SetColor("_KeyLockColor", keyColor);
        key.GetComponent<Renderer>().material.SetFloat("_DissolveAmount", 1);
    }
    private void Update()
    {
        if(test) animator.SetTrigger("UnLock");
    }

    private void OnTriggerEnter(Collider other)
    {
        KeySript keySript = other.GetComponent<KeySript>();
        if (keySript == null) return;
        Color otherColor = keySript.color;
        if (this.keyColor != otherColor) return;

        animator.SetTrigger("UnLock");
        keySript.startDissolve();
    }
}
