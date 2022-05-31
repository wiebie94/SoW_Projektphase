using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateLogic : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OpenGate()
    {
        if (animator == null)
        {
            Debug.LogError("animator is null!");
            return;
        }

        animator.SetBool("IsOpen", true);
    }

    public void CloseGate()
    {
        if (animator == null)
        {
            Debug.LogError("animator is null!");
            return;
        }

        animator.SetBool("IsOpen", false);
    }
}
