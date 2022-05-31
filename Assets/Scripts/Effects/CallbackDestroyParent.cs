using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallbackDestroyParent : MonoBehaviour
{

    void Start()
    {
        foreach (Transform child in this.transform)
        {
            ParticleSystem ps = child.gameObject.GetComponent<ParticleSystem>();
            if (ps != null) {
                var main = ps.main;
                main.stopAction = ParticleSystemStopAction.Callback; 
            }
                
        }
    }
    public void ChildDestroy()
    {
        Debug.Log("test: " +this.transform.childCount);
        if(this.transform.childCount == 0) Destroy(this.gameObject);
    }
}
