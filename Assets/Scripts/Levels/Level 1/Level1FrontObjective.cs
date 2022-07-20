using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Level1FrontObjective : MonoBehaviour
{
    GameObject light;
    [SerializeField] UnityEvent Complete;
    [SerializeField] UnityEvent Incomplete;

    private void Start() {
        light = this.transform.GetChild(0).gameObject;    
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log(other.gameObject.name);
        if(other.gameObject.transform.parent.gameObject.name == "Wood" && other.gameObject.transform.parent.gameObject.transform.GetChild(0).gameObject.activeSelf) {
            light.SetActive(true);
            Debug.Log("Wood Complete");
            Complete.Invoke();
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.name == "Wood") {
            light.SetActive(false);
            Incomplete.Invoke();
        }
    }
}
