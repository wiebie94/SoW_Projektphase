using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabGravity : MonoBehaviour
{
    private Rigidbody rb;
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }
    public void activateGravity() {
        rb.useGravity = true;
    }

}
