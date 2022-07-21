using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Level1RightObjective : MonoBehaviour
{
    GameObject light;
    [SerializeField] UnityEvent Complete;
    [SerializeField] UnityEvent Incomplete;

    private void Start() {
        light = this.transform.GetChild(0).gameObject;    
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.GetComponent<FillAndRelease>() != null) {
            if(other.gameObject.GetComponent<FillAndRelease>().getFilled()){
                light.SetActive(true);
                Complete.Invoke();
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.GetComponent<FillAndRelease>() != null) {
            if(other.gameObject.GetComponent<FillAndRelease>().getFilled()){
                light.SetActive(false);
                Incomplete.Invoke();
            }
        }
    }
}
