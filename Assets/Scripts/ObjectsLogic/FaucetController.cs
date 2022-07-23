using System.IO;
using System.ComponentModel.Design;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaucetController : MonoBehaviour
{
    private ParticleSystem stream;
    private GameObject water;

    public bool isOn;
    bool played;

    // Start is called before the first frame update
    void Start()
    {
        water = this.gameObject.transform.GetChild(0).gameObject;
        stream = this.gameObject.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>();
        
        isOn = true;
        played = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isOn && !played) {
            water.SetActive(true);
            stream.Clear();
            stream.Play(true);
            played = true;
        } 
        else if(!isOn)
        {
            water.SetActive(false);
            stream.Stop(true);
            played = false;
        }
    }
}
