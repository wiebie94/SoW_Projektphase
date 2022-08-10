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

    private AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        water = this.gameObject.transform.GetChild(0).gameObject;
        stream = this.gameObject.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>();
        audio = this.gameObject.GetComponent<AudioSource>();
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
            audio.Play();
            audio.time = 1.85f;
        } 
        else if(!isOn)
        {
            water.SetActive(false);
            stream.Stop(true);
            played = false;
            audio.Stop();
        }
        if (isOn)
        {
            if (audio.time > 5.5f)
            {
                audio.time = 1.85f;
                audio.Play();
            }
        }
    }
}
