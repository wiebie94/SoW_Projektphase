using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class TelekineseScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject rightHand;
    public GameObject clearHighLightObject;
    public Material normalMaterial;
    ActionBasedController controller;
    public Material highLightMaterial;
    InputDevice LeftControllerDevice;
    InputDevice RightControllerDevice;
    Vector3 LeftControllerVelocity;
    Vector3 RightControllerVelocity;


    public Vector3 Velocity { get; private set; } = Vector3.zero;
    public Vector3 Acceleration { get; private set; } = Vector3.zero;
    public float range = 15.0f;
    void Start()
    {
        controller = GetComponent<ActionBasedController>();
        controller.selectAction.action.performed += Action_performed;
        controller.selectAction.action.canceled += Action_canceled;
        LeftControllerDevice = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        RightControllerDevice = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);


    }



    private void Action_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Debug.Log("GripCancelled");
    }

    private void Action_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Debug.Log("GripAction");

    }

    void HightLightObject(GameObject gObj)
    {
        if (clearHighLightObject != gObj)
        {
            ClearHighLight();
            normalMaterial = gObj.GetComponent<MeshRenderer>().sharedMaterial;
            Debug.Log("Normal Material" + normalMaterial);
            gObj.GetComponent<MeshRenderer>().sharedMaterial = highLightMaterial;
            clearHighLightObject = gObj;
        }
    }

    void ClearHighLight()
    {
        if (clearHighLightObject != null)
        {
            Debug.Log("Setting Normal Material");
            clearHighLightObject.GetComponent<MeshRenderer>().sharedMaterial = normalMaterial;
            clearHighLightObject = null;
        }
    }




    // Update is called once per frame
    void Update()
    {

        RaycastHit hit;
        Debug.DrawRay(rightHand.transform.position, rightHand.transform.forward * range, Color.red);
        RightControllerDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.devicePosition, out RightControllerVelocity);
        Quaternion leftRotation;
        RightControllerDevice.TryGetFeatureValue(CommonUsages.deviceRotation, out leftRotation);
        Debug.Log("Rotation" + leftRotation);
        Debug.Log("Velo" + RightControllerVelocity);
        RightControllerDevice.SendHapticImpulse(0u, 0.7f, 2f);

        if (Physics.Raycast(rightHand.transform.position, rightHand.transform.forward, out hit, range))
        {
            if (hit.collider.gameObject.tag == "PullableObject")
            {
                // hit.collider.gameObject.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

                HightLightObject(hit.collider.gameObject);

            }
            else
            {
                ClearHighLight();
            }


        };

    }


}

