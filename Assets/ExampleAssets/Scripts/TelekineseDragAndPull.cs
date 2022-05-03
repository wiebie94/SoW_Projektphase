using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class TelekineseDragAndPull : MonoBehaviour
{
    [SerializeField]
    GameObject followObj;

    [SerializeField]
    Vector3 followObjOffset;

    [SerializeField]
    GameObject leftHandRef;
    [SerializeField]
    GameObject interactionObjectRef;
    public bool enteredDragCollider = false;

    [SerializeField] TelekineseScript telekineseScript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

  

    // Update is called once per frame
    void Update()
    {
        transform.position = followObj.transform.position + followObjOffset;

        if(enteredDragCollider == true && interactionObjectRef != null && telekineseScript.getIsItemPullAndPush())
        {
            Vector3 leftHandPos = leftHandRef.transform.localPosition;

            Vector3 leftHandUp = leftHandRef.transform.forward;

            

            //Vector3 upVector = new Vector3(0, leftHandRef.transform.localPosition.y, 0);
            Debug.DrawRay(leftHandPos, leftHandUp);
            Vector3 pathBeetween = interactionObjectRef.transform.position - leftHandRef.transform.position;
            Debug.DrawRay(leftHandRef.transform.position, pathBeetween);
            telekineseScript.PushAndPullObject(Vector3.Dot(leftHandUp, pathBeetween));
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RightHand"))
        {
            enteredDragCollider = true;
            interactionObjectRef = other.gameObject;
            Debug.Log("HandBool" + enteredDragCollider);
        }

    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("RightHand"))
        {
            enteredDragCollider = false;
            interactionObjectRef = null;
            Debug.Log("HandBool" + enteredDragCollider);

        }
    }


    private void OnDisable()
    {
        if(enteredDragCollider == true)
        {
            enteredDragCollider =false;
            Debug.Log("HandBool" + enteredDragCollider);
        }
    }


}
