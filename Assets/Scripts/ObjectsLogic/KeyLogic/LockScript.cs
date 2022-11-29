using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockScript : MonoBehaviour
{
    public Color color;
    public Color keyColor;
    private Animator animator;
    public bool test = false;

    private AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Transform keyLock = this.transform.Find("Lock_final_Prefab"); //.GetComponent<Renderer>().material.SetColor("_KeyLockColor", color);
        keyLock.GetChild(0).GetComponent<Renderer>().material.SetColor("_KeyLockColor", color);
  
        Transform key = this.transform.Find("Key_for_animation");
        key.GetComponent<Renderer>().material.SetColor("_KeyLockColor", keyColor);
        key.GetComponent<Renderer>().material.SetFloat("_DissolveAmount", 1);

        audio = this.GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (test)
        {
            animator.SetTrigger("UnLock");
            audio.time = 0.75f;
            audio.Play();
            test = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("LockScript triggerEnter");
        KeySript keySript = other.GetComponent<KeySript>();
        if (keySript == null) return;
        Color otherColor = keySript.color;
        if (this.keyColor != otherColor) return;
        Debug.Log("glkh");
        animator.SetTrigger("UnLock");
        audio.Play();
        keySript.startDissolve();
    }
}
