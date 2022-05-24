using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityTracking : MonoBehaviour


{
    Vector3 lastPosition = Vector3.zero;

    Transform transform;

    public float multiplier = 1000;

    void Start()
    {
        transform = GetComponent<Transform>();
    }

    private void Update()
    {
       Debug.Log("velocity: " + Vector3.Distance(transform.position, lastPosition) * Time.deltaTime * multiplier);
        lastPosition = transform.position;
        //Debug.Log(transform.position);
    }

}
