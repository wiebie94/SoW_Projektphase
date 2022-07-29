using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockScript : MonoBehaviour
{
    public Color color;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        this.transform.Find("Lock_Prefab").GetComponent<Renderer>().material.SetColor("_KeyLockColor", color);
        Transform key = this.transform.Find("Key_for_animation");
        key.GetComponent<Renderer>().material.SetColor("_KeyLockColor", color);
        key.GetComponent<Renderer>().material.SetFloat("_DissolveAmount", 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        KeySript keySript = other.GetComponent<KeySript>();
        if (keySript == null) return;
        Color otherColor = keySript.color;
        if (this.color != otherColor) return;

        animator.SetTrigger("UnLock");
        keySript.startDissolve();
    }
}
