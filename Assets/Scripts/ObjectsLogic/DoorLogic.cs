using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLogic : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OpenDoor()
    {
        if (animator == null)
        {
            Debug.LogError("animator is null!");
            return;
        }

        animator.SetBool("IsOpen", true);
    }

    public void CloseDoor()
    {
        if (animator == null)
        {
            Debug.LogError("animator is null!");
            return;
        }

        animator.SetBool("IsOpen", false);
    }
}
