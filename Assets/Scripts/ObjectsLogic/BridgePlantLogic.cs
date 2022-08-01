using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgePlantLogic : MonoBehaviour
{
    void Update()
    {
        if(!this.gameObject.transform.GetChild(0).gameObject.activeSelf && !this.gameObject.transform.GetChild(1).gameObject.activeSelf && !this.gameObject.transform.GetChild(2).gameObject.activeSelf)
        {
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
