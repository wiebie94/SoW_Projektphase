using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private XROrigin xrRig;
    private CapsuleCollider collider;
    private Rigidbody rb;

    void Start()
    {
        xrRig = GetComponent<XROrigin>();
        collider = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 center = xrRig.CameraInOriginSpacePos;
        collider.center = new Vector3(center.x, collider.center.y, center.z);
        collider.height = xrRig.CameraInOriginSpaceHeight;
    }
}
