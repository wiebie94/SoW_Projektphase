using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Level1RightObjective : MonoBehaviour
{
    GameObject light;
    public GameObject altar;
    public GameObject liquid;
    [SerializeField] UnityEvent Complete;
    [SerializeField] UnityEvent Incomplete;

    Renderer rend;

    private void Start() {
        light = this.transform.GetChild(0).gameObject;
        rend = liquid.GetComponent<Renderer>();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.GetComponent<FillAndRelease>() != null) {
            if(other.gameObject.GetComponent<FillAndRelease>().getFilled()){
                light.SetActive(true);
                Complete.Invoke();
                other.gameObject.transform.parent.gameObject.SetActive(false);
                altar.transform.GetChild(0).gameObject.SetActive(true);
                rend.material.SetFloat("_Fill", 0.58f);
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
