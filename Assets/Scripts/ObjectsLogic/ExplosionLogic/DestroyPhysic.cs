using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPhysic : MonoBehaviour
{
    public int masse = 10;
    public int drag = 0;
    private Rigidbody rb;
    public Transform superParent;
    public Transform[] activeByExplosionTrigger;
    private void activateRB() 
    {
        if (this.GetComponent<Rigidbody>() != null) return;
        this.rb = this.gameObject.AddComponent<Rigidbody>();
        if (this.rb != null) 
        {
            this.rb.mass = this.masse;
            this.rb.drag = this.drag;
        }
        //this.tag = "GrabInteractable";
    }

    private void OnDestroy() 
    {
        //Kinder meiner Eltern
        foreach (Transform child in activeByExplosionTrigger) {
            ActiveByExplosion ae = null;
            if (child != null) 
            {
                ae = child.GetComponent<ActiveByExplosion>();
            }
            if (ae != null)
                ae.activateRB();
        }

        //Debug.Log("Destroy: " + this.name);
        Transform[] children = new Transform[this.transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            children[i] = transform.GetChild(i);
        }
        // meine Kinder
        foreach (Transform child in children) 
        {
            //Debug.Log("GameObject: " + this.name + "  Child: "+ child.name);
            if (this.superParent.gameObject.active)
                child.parent = this.superParent;

            DestroyPhysic childS = child.GetComponent<DestroyPhysic>();
            if(childS != null)
                childS.activateRB();
        }
        //Debug.Log("Fertig");
    }
}
