using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class burn_script : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed = 0.001f;

    public float dissolveSpeed = 0.001f;
    public float dissolve_threshold = 0.75f;
    public Renderer renderer;
    private float burn_amount;
    private float burn_default;
    private float dissolve_amount;
    private float dissolve_default;

    [SerializeField] Material burningMat;
    private Material lastDefaultMat;
    

    private bool go = false;
    void Start()
    {
        renderer = GetComponent<Renderer> ();
        burn_default = burningMat.GetFloat("prop_burn");
        dissolve_default = burningMat.GetFloat("prop_dissolve");
        burn_amount = burn_default;
        dissolve_amount = dissolve_default;
        
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKey("b")){

            Material[] mats = {renderer.material, burningMat};

            lastDefaultMat = renderer.material;

            Debug.Log("in b");

            renderer.materials = mats;

            burn_amount = 0;

            burningMat.SetFloat("prop_dissolve", 0);
            dissolve_amount = 0;

            go = true;
        }

        if(Input.GetKey("r")){
            go = false;
            dissolve_amount = dissolve_default;

             Material[] mats = {lastDefaultMat};
            renderer.materials = mats;

            burningMat.SetFloat("prop_dissolve", dissolve_default);
            burn_amount = burn_default;
            burningMat.SetFloat("prop_burn", burn_default);
        }
    
        if(go){
            burn_amount += speed * Time.deltaTime;

            Debug.Log("burnamount " + burn_amount + " dissolve " + dissolve_amount);
            if(burn_amount < dissolve_threshold){
                burningMat.SetFloat("prop_burn", burn_amount);

                Debug.Log("in if");
            } 
            else{
                
                Debug.Log("in else");


                Material[] mats = {burningMat};
                renderer.materials = mats;

                dissolve_amount += dissolveSpeed * Time.deltaTime;
                burningMat.SetFloat("prop_dissolve", dissolve_amount);
            }
        }
        
    }
}
