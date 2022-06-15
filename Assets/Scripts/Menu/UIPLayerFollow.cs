using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPLayerFollow : MonoBehaviour
{
    [SerializeField] Transform xrRig;

    [SerializeField] Transform playerCam;
    [SerializeField] float cameraYOffset;
    [SerializeField] float yPositionFollowSpeed = 1;

    [SerializeField] float floatingStrength = 1;
    [SerializeField] float floatingFrequence = 2;

    [SerializeField] float rotationOffset = 0;
    [SerializeField] float rotationSpeed = 1;

    private Vector3 positionOffset;

    private bool shouldFollowPlayer = false;

    void Start()
    {
        if (xrRig == null)
        {
            Debug.LogError("xrRig is null!");
        }

        if (playerCam == null)
        {
            Debug.LogError("playerCam is null!");
        }
    }

    private void Update()
    {
        if (shouldFollowPlayer)
        {
            float targetY = playerCam.position.y + cameraYOffset + Mathf.Sin(Time.time * floatingFrequence) * floatingStrength;
            float newY = Mathf.Lerp(transform.position.y, targetY, yPositionFollowSpeed * Time.deltaTime);

            Vector3 newPosition = new Vector3(xrRig.position.x, newY, xrRig.position.z) + positionOffset;

            transform.position = newPosition;
        }
    }

    public void StartFollowingPlayer()
    {
        //Position
        Vector3 positionDifference = playerCam.position - xrRig.position;
        positionOffset = new Vector3(positionDifference.x, 0, positionDifference.z);

        //Rotation
        Vector3 rot = playerCam.eulerAngles;
        transform.rotation = Quaternion.Euler(0, rot.y + rotationOffset, 0);

        shouldFollowPlayer = true;
    }

   public void StopFollowingPlayer()
    {
        shouldFollowPlayer = false;
    }
}
