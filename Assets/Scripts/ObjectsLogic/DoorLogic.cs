using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLogic : MonoBehaviour
{
    private Animator animator;
    private AudioSource audio;

    void Start()
    {
        animator = this.gameObject.GetComponent<Animator>();
        audio = this.gameObject.GetComponent<AudioSource>();
    }

    public void OpenDoor()
    {
        if (animator == null)
        {
            Debug.LogError("animator is null!");
            return;
        }

        animator.SetBool("IsOpen", true);
        audio.Play();
    }

    public void CloseDoor()
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
