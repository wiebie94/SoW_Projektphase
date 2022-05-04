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

    private bool x = true;

    [SerializeField] Material burningMat;
    private Material lastDefaultMat;

    private Color object_color;
    

    private bool go = false;
    void Start()
    {
        renderer = GetComponent<Renderer> ();   //renderer
        burn_default = burningMat.GetFloat("prop_burn");    //anfangswert burn    0.55
        dissolve_default = burningMat.GetFloat("prop_dissolve");    //anfangswert dissolve  0
        burn_amount = burn_default;     //aktiver burn wert
        dissolve_amount = dissolve_default;     //aktiver dissolve wert
        object_color = renderer.material.color;
        //Debug.Log("color: " + object_color);
        burningMat.SetColor("_object_color", object_color);
        
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKey("b")){

            
            Material[] mats = {renderer.material, burningMat};      //erstelle neues mat array mit old mat und burn mat
            lastDefaultMat = renderer.material; //save old mat

            renderer.materials = mats;  //set new mats

            burningMat.SetFloat("prop_dissolve", 0);
            burn_amount = 0;
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
                if(burn_amount < dissolve_threshold){   //burn bis threshold (0.75)
                    Material[] mats = {burningMat};
                    renderer.materials = mats;
                    burn_amount += speed * Time.deltaTime;
                    burningMat.SetFloat("prop_burn", burn_amount);
            } 
            else{
                if(x){
                    Material[] mats = {burningMat};
                    renderer.materials = mats;
                    x = false;
                }
                dissolve_amount += dissolveSpeed * Time.deltaTime;
                burningMat.SetFloat("prop_dissolve", dissolve_amount);

                if(dissolve_amount > 1.1f){
                    Material[] mats = {};
                    renderer.materials = mats;
                }
            }
        }
        
    }
}
