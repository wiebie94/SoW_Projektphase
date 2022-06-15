using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TestLever : MonoBehaviour
{
    public bool isOpen = false;
    private bool moved = false;
    [SerializeField] UnityEvent Up;
    [SerializeField] UnityEvent Down;

    void Update()
    {
        if(isOpen) {
            if(!moved) {
                moved = true;
                Up.Invoke();
            }
        }
        else {
            if(moved){
                moved = false;
                Down.Invoke();
            }
            
        }
    }
}
