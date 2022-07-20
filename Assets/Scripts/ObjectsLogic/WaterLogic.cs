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
}
