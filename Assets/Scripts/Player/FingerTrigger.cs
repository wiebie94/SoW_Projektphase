using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerTrigger : MonoBehaviour
{
    private Hand hand;

    void Start()
    {
        hand = transform.parent.parent.GetComponent<Hand>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FingerTrigger"))
        {
            if (hand == null)
            {
                Debug.LogError("hand == null!");
                return;
            }

            hand.FingerTriggerEnter();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("FingerTrigger"))
        {
            if (hand == null)
            {
                Debug.LogError("hand == null!");
                return;
            }

            hand.FingerTriggerExit();
        }
    }

    private void OnDisable()
    {
        if (hand == null)
        {
            Debug.LogError("hand == null!");
            return;
        }
    }
}
