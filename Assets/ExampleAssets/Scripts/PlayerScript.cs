using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody rb;
    private List<GameObject> pullableObjects = new List<GameObject>();
    private Collider[] ObjectsInRange;

    private int objectPullRange = 1;

    // Create a public variable for the cameraTarget object
    public GameObject cameraTarget;
    /* 
    You will need to set the cameraTarget object in Unity. 
    The direction this object is facing will be used to determine
    the direction of forces we will apply to our player.
    */
    public float movementIntensity;
    /* 
    Creates a public variable that will be used to set 
    the movement intensity (from within Unity)
    */
    // Start is called before the first frame update
    void Start()
    {
        // make our rb variable equal the rigid body component
        rb = GetComponent<Rigidbody>();
    }



    // Update is called once per frame
    void Update()
    {
        var ForwardDirection = cameraTarget.transform.forward;
        var RightDirection = cameraTarget.transform.right;

        // Move Forwards
        if (Input.GetKey(KeyCode.W))
        {
            
            rb.AddForce(ForwardDirection * movementIntensity);
            /* You may want to try using velocity rather than force.
            This allows for a more responsive control of the movement
            possibly better suited to first person controls, eg: */
            //rb.velocity = ForwardDirection * movementIntensity;
        }
        // Move Backwards
        if (Input.GetKey(KeyCode.S))
        {
            // Adding a negative to the direction reverses it
            rb.AddForce(-ForwardDirection * movementIntensity);
        }
        // Move Rightwards (eg Strafe. *We are using A & D to swivel)
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(RightDirection * movementIntensity);
        }
        // Move Leftwards
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(-RightDirection * movementIntensity);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Space pressed");
        }

    }


}