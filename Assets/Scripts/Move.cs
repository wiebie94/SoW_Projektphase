using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    Vector3 movement;
    public float speed = 1;
    bool forward = true; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(forward) {
            movement.x += Time.deltaTime * speed;
        } else {
            movement.x -= Time.deltaTime * speed;
        }
        
        transform.position = movement;
        if(transform.position.x >= 3) forward = false;
        if(transform.position.x <= -3) forward = true;
    }
}
