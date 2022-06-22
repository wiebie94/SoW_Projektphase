using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TargetLogic : MonoBehaviour
{
    public bool hit = false;

    [SerializeField] UnityEvent hitEvent;

    void Update()
    {
        if(hit) {
            hitEvent.Invoke();
        }
    }

    public void targetHit() {
        hit = true;
    }
}
