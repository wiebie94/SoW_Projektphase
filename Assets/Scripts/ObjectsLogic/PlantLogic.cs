using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantLogic : MonoBehaviour
{
    bool seed = false;
    bool plant = false;
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
            other.gameObject.SetActive(false);
            this.transform.GetChild(0).gameObject.SetActive(true);
            seed = true;
        }
    }

    public void growPlant()
    {
        seed = false;
        plant = true;
        this.transform.GetChild(0).gameObject.SetActive(false);
        this.transform.GetChild(1).gameObject.SetActive(true);
    }

    public bool getSeed()
    {
        return this.seed;
    }

    public bool getPlant()
    {
        return this.plant;
    }
}
