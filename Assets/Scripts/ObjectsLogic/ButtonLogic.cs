using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonLogic : MonoBehaviour
{
    [SerializeField] UnityEvent OnButtonPressed;
    [SerializeField] float activationValue = 0.013f;

    private bool isActivated = false;

    private float defaultXPos;

    private void Start()
    {
        defaultXPos = transform.position.x;
    }

    void OnEnable()
    {
        StartCoroutine(CheckStatus());
    }

    //private void OnDisable()
    //{
    //    ResetButton();
    //}

    IEnumerator CheckStatus()
    {
        while(true)
        {
            if (!isActivated && transform.localPosition.x > activationValue)
            {
                isActivated = true;
                OnButtonPressed.Invoke();
            }
            else if (isActivated && transform.localPosition.x <= activationValue)
            {
                isActivated = false;
            }
                
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void ResetButton()
    {
        Vector3 oldPos = transform.localPosition;
        transform.localPosition = new Vector3(defaultXPos, oldPos.y, oldPos.z);

        isActivated = false;
    }
}
