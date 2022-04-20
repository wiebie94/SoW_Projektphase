using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class TelekineseScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject rightHand;
    ActionBasedController controller;
    InputDevice LeftControllerDevice;
    InputDevice RightControllerDevice;
    Vector3 LeftControllerVelocity;
    Vector3 RightControllerVelocity;
    public GameObject highlightedObj;
    private Material normalMaterial;
    [SerializeField] Material highLightMaterial;

    public Vector3 Velocity { get; private set; } = Vector3.zero;
    public Vector3 Acceleration { get; private set; } = Vector3.zero;
    public float range = 15.0f;
    void Start()
    {
        ActionBasedController[] controllerArray = ActionBasedController.FindObjectsOfType<ActionBasedController>();
        ActionBasedController controller = controllerArray[0];
        controller.selectAction.action.performed += Action_performed;
        controller.selectAction.action.canceled += Action_canceled;
        LeftControllerDevice = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        RightControllerDevice = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

    }



    private void Action_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        
    }

    private void Action_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Debug.Log("Action performed" + obj.performed);
        Debug.Log("Action performed" + obj.action.name);

    }


    void HighLightObject(GameObject gObj)
    {

        //    
        //    normalMaterial = gObj.GetComponent<MeshRenderer>().sharedMaterial;
        //    if(normalMaterial != null)
        //    {
        //        //highLightMaterial.SetTexture("_BaseMap", normalMaterial.mainTexture);
        //        gObj.GetComponent<MeshRenderer>().sharedMaterial = highLightMaterial;

        //        
        //    }

        //}
        if (highlightedObj != gObj)
        {
            ClearHighLight();


            Material[] materials = new Material[2];
            materials[0] = gObj.GetComponent<Renderer>().materials[0];
            
            materials[1] = highLightMaterial;
            gObj.GetComponent<Renderer>().materials = materials;
            highlightedObj = gObj;
        }
    }

    void ClearHighLight()
    {
        if (highlightedObj != null)
        {
            //highLightMaterial.SetTexture("_BaseMap", null);
            Material[] materials = new Material[1];
            materials[0] = highlightedObj.GetComponent<Renderer>().materials[0];
            highlightedObj.GetComponent<Renderer>().materials = materials;
            highlightedObj = null;
            Debug.Log("HIGHLIGHT" + highLightMaterial);
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
        RightControllerDevice.SendHapticImpulse(0u, 0.7f, 2f);

        if (Physics.Raycast(rightHand.transform.position, rightHand.transform.forward, out hit, range))
        {
            if (hit.collider.gameObject.tag == "PullableObject")
            {

               HighLightObject(hit.collider.gameObject);

            }
            else
            {
               ClearHighLight();
            }


        };

    }


}

