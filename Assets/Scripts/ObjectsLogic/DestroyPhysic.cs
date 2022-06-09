using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPhysic : MonoBehaviour
{
    private Rigidbody rb;
    private void activateRB() 
    {
        this.rb = this.gameObject.AddComponent<Rigidbody>();
        if (this.rb != null) 
        {
            this.rb.mass = 10;
        }

    }

    private void OnDestroy() 
    {
        Debug.Log("Destroy: " + this.name);
        Transform[] children = new Transform[this.transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            children[i] = transform.GetChild(i);
        }

        foreach (Transform child in children) 
        {
            Debug.Log("GameObject: " + this.name + "  Child: "+ child.name);
            child.parent = this.transform.parent;

            DestroyPhysic childS = child.GetComponent<DestroyPhysic>();
            if(childS != null)
                childS.activateRB();
        }
        Debug.Log("Fertig");
    }
}
