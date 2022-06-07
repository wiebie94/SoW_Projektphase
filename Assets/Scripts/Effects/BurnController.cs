using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnController : MonoBehaviour
{
    public Material burningMat;

    public float speed = 0.5f;
    public float dissolveSpeed = 0.5f;
    public float dissolve_threshold = 0.75f;
    private float burn_amount;
    private float burn_default;
    private float dissolve_amount;
    private float dissolve_default;
    private bool x = true;

    private Material lastDefaultMat;
    private Material burnTransparent;
    private Renderer renderer;
    private GameObject fireball;

    //Texture Components
    private Color object_color;
    private Texture object_tex;
    private Texture normal_tex;
    private float metallic;
    private float smoothness;

    //Start effect
    private bool go = false;
    
    void Start()
    {
        //fireball = GameObject.Find("Fireball_final");
        burningMat = Instantiate(Resources.Load("burning_material", typeof(Material)) as Material);
        //burnTransparent = Instantiate(Resources.Load("burning_material_transparent", typeof(Material)) as Material);
        renderer = GetComponent<Renderer> ();   //renderer
        burn_default = burningMat.GetFloat("prop_burn");    //anfangswert burn    0.55
        dissolve_default = burningMat.GetFloat("prop_dissolve");    //anfangswert dissolve  0
        burn_amount = burn_default;     //aktiver burn wert
        dissolve_amount = dissolve_default;     //aktiver dissolve wert
        
        object_tex = renderer.material.GetTexture("_MainTex");
        normal_tex = renderer.material.GetTexture("_BumpMap");
        metallic = renderer.material.GetFloat("_Metallic");
        smoothness = renderer.material.GetFloat("_Smoothness");
             
        burningMat.SetTexture("_object_tex", object_tex);
        burningMat.SetTexture("_normal_tex", normal_tex);
        burningMat.SetFloat("_metallic", metallic);
        burningMat.SetFloat("_smoothness", smoothness);

        Material mat = burningMat;      
        lastDefaultMat = renderer.material; //save old mat

        renderer.material = mat;  //set new mats

        burningMat.SetFloat("prop_dissolve", 0);
        burn_amount = 0;
        dissolve_amount = 0;
        go = true;

        //object_color = renderer.material.color;
        //Debug.Log("color: " + object_color);
        //burningMat.SetColor("_object_color", object_color);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("r")){
            go = false;
            dissolve_amount = dissolve_default;

            Material mat = lastDefaultMat;
            renderer.material = mat;

            burningMat.SetFloat("prop_dissolve", dissolve_default);
            burn_amount = burn_default;
            burningMat.SetFloat("prop_burn", burn_default);
        }
    
        if(go){
            if(burn_amount < dissolve_threshold){   //burn bis threshold (0.75)
                Material mat = burningMat;                  
                renderer.material = mat;
                burn_amount += speed * Time.deltaTime;
                burningMat.SetFloat("prop_burn", burn_amount);
            } 
            else{
                if(x){
                    //burnTransparent.CopyPropertiesFromMaterial(burningMat);
                    //Material mat = burnTransparent;
                    Material mat = burningMat;
                    renderer.material = mat;
                    x = false;
                }
                dissolve_amount += dissolveSpeed * Time.deltaTime;
                burningMat.SetFloat("prop_dissolve", dissolve_amount);

                if(dissolve_amount > 1.1f){
                    bool disable = false;

                    //deaktivate Object Parent
                    GameObject ob = gameObject.transform.parent.gameObject;
                    ob.SetActive(false);
                    Material mat = null;
                    renderer.material = mat;
                }
            }
        }
    }

    /*
    public Material getBurnMaterial(){
        
        return fireball.GetComponent<FireballController>().burnMat;
    }
    */
}
