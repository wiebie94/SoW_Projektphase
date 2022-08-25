using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;
using UnityEngine.InputSystem;

public class TelekineseScript : MonoBehaviour
{
    [SerializeField] Material highLightMaterial;
    [SerializeField] Material highLightWoodMat1;
    [SerializeField] Material highLightWoodMat11;
    [SerializeField] Material highLightWoodMat2;
    [SerializeField] Material highLightWoodMat21;
    [SerializeField] float range = 15.0f;
    [SerializeField] Transform followTarget;

    private GameObject telekineseObj;
    private GameObject telekineseObjOld;
    private Rigidbody telekineseRigidbody;
    private string neeededTag = "GrabInteractable";
    private bool isItemGrabbed = false;

    public GameObject telekineseDragObject;
    [SerializeField] Vector3 followPositionOffset;
    [SerializeField] Vector3 followRotationOffset;
    [SerializeField] float followSpeed;
    [SerializeField] float rotateSpeed;
    [SerializeField]
    private Coroutine timerCoroutine;
    private float timeAfterHit = 0;

    private int layerForRaycast;
    private int layerOnlyTelekinese;

    [SerializeField] float activationTime = 1;

    [SerializeField] float pullAndPushSpeed = 1;

    [SerializeField] float telekineseMinRange = 1;
    [SerializeField] float telekineseMaxRange = 10;

    [SerializeField] HandType hand;

    Coroutine telekineseCoroutine;
    public AudioSource audioSource;
    public AudioClip telekineseSoundClip;
    ActionBasedController controller;


    void Start()
    {
        controller = GetComponent<ActionBasedController>();

        layerForRaycast = LayerMask.GetMask("GrabInteractable");
        layerOnlyTelekinese = LayerMask.GetMask("Telekinese");

        //skillMenu.onTelekineseSkillTriggered

        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        ShowSkillMenu.onTelekineseSkillTriggered += TelekineseSkillTriggered;
        ShowSkillMenu.onTelekineseSkillUntriggered += TelekineseSkillUntriggered;
       
    }
    private void OnDisable()
    {
        ShowSkillMenu.onTelekineseSkillTriggered -= TelekineseSkillTriggered;
        ShowSkillMenu.onTelekineseSkillUntriggered -= TelekineseSkillUntriggered;
    }

    private void TelekineseSkillTriggered(HandType activationHand)
    {
        if (hand != activationHand)
            return;

        TelekineseCoroutineStart();

        controller.activateAction.action.performed += Action_performed_left;
        controller.activateAction.action.canceled += Action_canceled_left;

    }

    private void TelekineseSkillUntriggered()
    {
        controller.activateAction.action.performed -= Action_performed_left;
        controller.activateAction.action.canceled -= Action_canceled_left;
        Debug.Log("TelekineseEnd");
        TelekineseEnd();
        TelekineseCoroutineStop();
    }

    private void Action_performed_left(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (telekineseObj != null)
            TelekineseBegin();
    }

    private void Action_canceled_left(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (isItemGrabbed)
            TelekineseEnd();
    }

    
    void TelekineseBegin()
    {
        //audioSource.clip = telekineseSoundClip;
        //audioSource.loop = true;
        audioSource.Play();
        isItemGrabbed = true;
        //telekineseDragObject.gameObject.AddComponent<AudioSource>();
        telekineseDragObject.SetActive(true);
        telekineseDragObject.GetComponent<TelekineseDragAndPull>().SetTelekineseHand(hand);
        telekineseRigidbody = telekineseObj.GetComponent<Rigidbody>();
        
        if (telekineseRigidbody == null)
        {
            telekineseRigidbody = telekineseObj.transform.parent.GetComponent<Rigidbody>();

            if (telekineseRigidbody == null)
                Debug.LogError("telekineseRigidbody is null!");
        }

        Debug.Log("telekinse obj: " + telekineseRigidbody.gameObject.name);

        followTarget.position = telekineseRigidbody.position;
    }

    void TelekineseEnd()
    {
        audioSource.Stop();
        if (telekineseDragObject == null)
            return;

        isItemGrabbed = false;
        telekineseDragObject.SetActive(false);
        telekineseRigidbody = null;
        ClearHighLight();
    }

    void ObjectRayHit()
    {
        
    }

    IEnumerator ObjectHitTimer()
    {
        while(true)
        {
            timeAfterHit += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
    }

    void HighLightObject(GameObject gObj)
    {
        if (telekineseObj == gObj)
            return;

        ClearHighLight();

        telekineseObj = gObj;

        Material[] materials = new Material[2];
        materials[0] = gObj.GetComponent<Renderer>().materials[0];
            
        materials[1] = highLightMaterial;
        gObj.GetComponent<Renderer>().materials = materials;
        telekineseObj = gObj;
    }

    void HighLightObjectWood(GameObject gObj){
        if(telekineseObj == gObj)
            return;

        //telekineseObjOld = gObj;

        ClearHighLightWood();

        telekineseObj = gObj;

        Material[] materials = new Material[2];
        if(gObj.name == "Log_1_Prefab")
        {
            materials[0] = highLightWoodMat1;
            materials[1] = highLightWoodMat11;
        }
        else
        {
            materials[0] = highLightWoodMat2;
            materials[1] = highLightWoodMat21;
        }

        gObj.GetComponent<Renderer>().materials = materials;
        telekineseObj = gObj;
    }

    void ClearHighLightWood(){
        if (telekineseObj == null)
            return;

        Material[] materials = new Material[2];
        materials[0] = telekineseObj.GetComponent<Renderer>().materials[0];
        materials[1] = telekineseObj.GetComponent<Renderer>().materials[1];
        telekineseObj.GetComponent<Renderer>().materials = materials;

        telekineseObj = null;
        telekineseObjOld = null;
        ClearTimer();
    }

    void ClearHighLight()
    {
        if (telekineseObj == null)
            return;

        Material[] materials = new Material[1];
        materials[0] = telekineseObj.GetComponent<Renderer>().materials[0];
        telekineseObj.GetComponent<Renderer>().materials = materials;

        telekineseObj = null;
        ClearTimer();
    }

    void ClearTimer()
    {
        if (timerCoroutine != null)
            StopCoroutine(timerCoroutine);
        timerCoroutine = null;
        timeAfterHit = 0;
    }

   void TelekineseCoroutineStart()
    {
        if (telekineseCoroutine != null)
            StopCoroutine(telekineseCoroutine);

        telekineseCoroutine = StartCoroutine(TelekineseRayCast(0.1f));
    }

    void TelekineseCoroutineStop()
    {
        if (telekineseCoroutine != null)
            StopCoroutine(telekineseCoroutine);
    }


    // Update is called once per frame
    void Update()
    {
        if (isItemGrabbed)
        {
            if (telekineseRigidbody == null)
                return;

            // Position
            var positionWithOffset = followTarget.TransformPoint(followPositionOffset);
            var distance = Vector3.Distance(positionWithOffset, telekineseObj.transform.position);

            telekineseRigidbody.velocity = (positionWithOffset - telekineseObj.transform.position).normalized * (followSpeed * distance);

            // Rotation
            var rotationWithOffset = followTarget.rotation * Quaternion.Euler(followRotationOffset);
            var q = rotationWithOffset * Quaternion.Inverse(telekineseRigidbody.rotation);
            q.ToAngleAxis(out float angle, out Vector3 axis);
            if (Mathf.Abs(axis.magnitude) != Mathf.Infinity)
            {
                if (angle > 180.0f) { angle -= 360.0f; }
                telekineseRigidbody.angularVelocity = axis * (angle * Mathf.Deg2Rad * rotateSpeed);
            }
        }
    }

    IEnumerator TelekineseRayCast(float interval)
    {

        while (true)
        {
            RaycastHit hit;
            Debug.DrawRay(transform.position, transform.forward * range, Color.red);

            Ray ray = new Ray(transform.position, transform.forward);

            if (Physics.Raycast(ray, out hit, range, layerForRaycast) || Physics.Raycast(ray, out hit, range, layerOnlyTelekinese))  //layerforraycast anpassen
            {
                if (!isItemGrabbed && (hit.collider.CompareTag(neeededTag) || hit.collider.CompareTag("Telekinese") || hit.collider.CompareTag("WaterBottle")))
                {
                    HighLightObject(hit.collider.gameObject);
                }
                if (!isItemGrabbed && hit.collider.CompareTag("KindleBig"))
                {
                    foreach (Transform child in hit.collider.gameObject.transform.parent.transform)
                    {
                        if (child.GetComponent<MeshRenderer>() != null)
                        {
                            HighLightObjectWood(child.gameObject);
                        }
                    }
                }
            }

            else if (telekineseObj != null && isItemGrabbed == false)
            {
                /*if (telekineseObj.tag == "KindleBig")
                {
                    ClearHighLightWood();
                }
                else
                {*/
                ClearHighLight();
            }

                yield return new WaitForSeconds(interval);
  //          }
        }
    }

    public void PushAndPullObject(float dragForce)
    {
        Vector3 distance = followTarget.transform.localPosition - Vector3.forward * dragForce * pullAndPushSpeed;
        
        followTarget.transform.localPosition = new Vector3(distance.x,distance.y,Mathf.Clamp(distance.z, telekineseMinRange, telekineseMaxRange));
    }
}

