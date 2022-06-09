using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonLogic : MonoBehaviour
{
    [SerializeField] UnityEvent OnButtonPressed;
    [SerializeField] float activationValue = 0.013f;
    [SerializeField] float resetValue = -0.012f;

    ConfigurableJoint cJoint;

    private bool isActivated = false;

    private float defaultXPos;

    private void Start()
    {
        defaultXPos = transform.localPosition.x;

        cJoint = GetComponent<ConfigurableJoint>();
    }

    void OnEnable()
    {
        //if (cJoint != null)
            //cJoint.enableCollision = true;

        

        StartCoroutine(CheckStatus());

        //InvokeRepeating("ResetButton", 2, 2);
    }

    private void OnDisable()
    {
        ResetButton();

        //cJoint.enableCollision = false;

        //ResetButton();
    }

    IEnumerator CheckStatus()
    {
        while(true)
        {
            if (!isActivated && transform.localPosition.x > activationValue)
            {
                isActivated = true;
                OnButtonPressed.Invoke();
            }
            else if (isActivated && transform.localPosition.x <= resetValue)
            {
                isActivated = false;
            }
                
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void ResetButton()
    {
        Collider collider = GetComponentInChildren<BoxCollider>();

        //collider.enabled = false;

        Vector3 oldPos = transform.localPosition;
        transform.localPosition = new Vector3(0, oldPos.y, oldPos.z);

        isActivated = true;

        //Invoke(nameof(EnableCollision), 2);
    }

    private void EnableCollision()
    {
        Collider collider = GetComponentInChildren<BoxCollider>();

        collider.enabled = true;
    }
}
