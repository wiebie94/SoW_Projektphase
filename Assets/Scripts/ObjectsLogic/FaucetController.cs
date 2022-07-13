using System.IO;
using System.ComponentModel.Design;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaucetController : MonoBehaviour
{
    private ParticleSystem stream;
    private ParticleSystem drops;

    public bool isOn;
    bool played;

    // Start is called before the first frame update
    void Start()
    {
        stream = this.GetComponent<ParticleSystem>();
        drops = this.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
        isOn = false;
        played = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isOn && !played) {
            stream.Clear();
            stream.Play(true);
            played = true;
        } else if(!isOn)
        {
            stream.Stop(true);
            played = false;
        }
    }
}
