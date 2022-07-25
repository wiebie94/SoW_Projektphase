using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private XROrigin xrRig;
    private CapsuleCollider collider;
    private Rigidbody rb;

    [SerializeField] GameObject camObj;

    [SerializeField] InputActionProperty moveAction;
    [SerializeField] float moveSpeed = 1;

    [SerializeField] float dragFactor = 0.1f;

    [SerializeField] float colliderExtraHeight = 1;

    [SerializeField] float cameraToFloorLength;
    [SerializeField] Vector3 cameraToFloorVector;

    private bool isMoving = false;

    void Start()
    {
        xrRig = GetComponent<XROrigin>();
        collider = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        cameraToFloorVector = new Vector3(cameraToFloorVector.x, cameraToFloorLength, cameraToFloorVector.z);

        Vector2 moveVector = moveAction.action.ReadValue<Vector2>();

        if (moveVector != Vector2.zero)
        {
           if (!isMoving && Physics.Raycast(camObj.transform.position, Vector3.down, cameraToFloorLength, LayerMask.GetMask("Default")))
                TeleportBodyToLastColliderPosition();

            isMoving = true;

            Vector3 forward = Vector3.ProjectOnPlane(camObj.transform.forward, Vector3.up).normalized;
            Vector3 right = Vector3.ProjectOnPlane(camObj.transform.right, Vector3.up).normalized;

            Vector3 moveForce = (forward * moveVector.y + right * moveVector.x) * moveSpeed * Time.deltaTime;

            rb.AddForce(moveForce);

            Vector3 center = xrRig.CameraInOriginSpacePos;
            collider.center = new Vector3(center.x, collider.center.y, center.z);
        }
        else
        {
            isMoving = false;
        }
        
        Vector3 playerVel = rb.velocity;

        if (rb.velocity.x != 0)
            playerVel.x *= 1 - dragFactor;

        if (rb.velocity.z != 0)
            playerVel.z *= 1 - dragFactor;

        rb.velocity = playerVel;
        collider.height = xrRig.CameraInOriginSpaceHeight + colliderExtraHeight;
    }

    public void TeleportBodyToLastColliderPosition()
    {
        Vector3 colliderWorldPosition = collider.transform.TransformPoint(collider.center);

        Vector3 cameraColliderDifference = camObj.transform.position - colliderWorldPosition;

        Vector3 newBodyPosition = transform.position - cameraColliderDifference;

        transform.position = new Vector3(newBodyPosition.x, transform.localPosition.y, newBodyPosition.z);
    }
}
