using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantLogic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Seed")
        {
            Debug.Log("Seed");
            other.gameObject.SetActive(false);
            this.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
