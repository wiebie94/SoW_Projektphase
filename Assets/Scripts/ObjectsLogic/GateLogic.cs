using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateLogic : MonoBehaviour
{
    private Animator animator;
    private AudioSource audio;

    void Start()
    {
        animator = GetComponent<Animator>();
        audio = this.gameObject.GetComponent<AudioSource>();
    }

    public void OpenGate()
    {
        if (animator == null)
        {
            Debug.LogError("animator is null!");
            return;
        }

        animator.SetBool("IsOpen", true);
        audio.Play();
    }

    public void CloseGate()
    {
        if (animator == null)
        {
            Debug.LogError("animator is null!");
            return;
        }

        animator.SetBool("IsOpen", false);
        audio.Play();
    }
}
