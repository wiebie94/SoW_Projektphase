using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class burn_script : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed = 0.001f;
    public float dissolve_threshold = 0.75f;
    public Renderer renderer;
    private float burn_amount;
    private float burn_default;
    private float dissolve_amount;
    private float dissolve_default;
    private bool go = false;
    void Start()
    {
        renderer = GetComponent<Renderer> ();
        burn_default = renderer.material.GetFloat("prop_burn");
        dissolve_default = renderer.material.GetFloat("prop_dissolve");
        burn_amount = burn_default;
        dissolve_amount = dissolve_default;
        
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKey("b")){
            go = true;
        }

        if(Input.GetKey("r")){
            go = false;
            dissolve_amount = dissolve_default;
            renderer.material.SetFloat("prop_dissolve", dissolve_default);
            burn_amount = burn_default;
            renderer.material.SetFloat("prop_burn", burn_default);
        }

        if(go){
            burn_amount += speed;
            if(burn_amount < dissolve_threshold){
                renderer.material.SetFloat("prop_burn", burn_amount);
            } 
            else{
                dissolve_amount += speed;
                renderer.material.SetFloat("prop_dissolve", dissolve_amount);
            }
        }
    }
}
