using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyHighlightLogic : MonoBehaviour
{
    // Laesst das Key Highlight verschwinden wenn Haende den Collider beruehren
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "LeftHand" || other.gameObject.tag == "RightHand") {
            Destroy(this.gameObject);
        }
    }
}
