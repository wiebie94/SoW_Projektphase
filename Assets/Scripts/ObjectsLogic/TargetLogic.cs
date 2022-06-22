using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TargetLogic : MonoBehaviour
{
    [SerializeField] UnityEvent hitEvent;

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Fireball") {
            Debug.Log("Target hit");
            hitEvent.Invoke();
        }
    }
}
