using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterLogic : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlantLogic>() != null)
        {
            if (other.gameObject.GetComponent<PlantLogic>().getSeed())
            {
                other.gameObject.GetComponent<PlantLogic>().growPlant();
            }                
        }
    }

    private void OnTriggerStay(Collider other) 
    {
        Debug.Log(other.gameObject.tag);
        if(other.gameObject.tag == "WaterBottle") {
            other.gameObject.GetComponent<FillAndRelease>().setUnderWater();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "WaterBottle") {
            other.gameObject.GetComponent<FillAndRelease>().resetUnderWater();
        }
    }
}
