using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeltingController : MonoBehaviour
{
    private Material icewall;
    private float melting;
    private Collider col;
    private float meltingthreshold = -3.0f;
    public float meltingspeed = 0.5f; 

    // Start is called before the first frame update
    void Start()
    {
        icewall = GetComponent<Renderer>().material;
        melting = icewall.GetFloat("_melting");
        col = GetComponent<Collider>();
        icewall.SetFloat("_speed", meltingspeed);
    }
    // Update is called once per frame
    void Update()
    {
        if(melting > meltingthreshold){
            melting = melting - (meltingspeed * Time.deltaTime);
        icewall.SetFloat("_melting", melting);
        }
        col.enabled = false;
    }
}
