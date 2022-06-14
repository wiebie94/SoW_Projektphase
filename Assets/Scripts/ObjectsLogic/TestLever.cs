using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TestLever : MonoBehaviour
{
    public bool isOpen = false;
    [SerializeField] UnityEvent Up;
    [SerializeField] UnityEvent Down;

    void Update()
    {
        if(isOpen) Up.Invoke();
        else Down.Invoke();
    }
}
