using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveByExplosion : MonoBehaviour
{
    public int masse = 10;
    public int drag = 0;
    public Transform superParent;
    private Rigidbody rb;
    public void activateRB()
    {
        this.transform.parent = superParent;

        if (this.GetComponent<Rigidbody>() != null) return;

        this.rb = this.gameObject.AddComponent<Rigidbody>();
        if (this.rb != null)
        {
            this.rb.mass = this.masse;
            this.rb.drag = this.drag;
        }
        //this.tag = "GrabInteractable";
    }
}
