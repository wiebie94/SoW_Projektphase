using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlateLogic : MonoBehaviour
{
    private Animator animator;
    [SerializeField] UnityEvent eventDown;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other) {
        if (animator == null)
        {
            Debug.LogError("animator is null!");
            return;
        }
        Debug.Log("down");
        animator.SetBool("IsDown", true);
        eventDown.Invoke();
    }
}
