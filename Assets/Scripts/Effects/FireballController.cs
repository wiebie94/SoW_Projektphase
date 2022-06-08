using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : MonoBehaviour
{

    public GameObject explosion_prefab;
    public GameObject steam_prefab;
    public GameObject flame_prefab;
    public GameObject kindle_flame_prefab;
    public GameObject dissolveSound_prefab;
    private GameObject explosion;
    private GameObject steam;
    private GameObject kindleFlame;
    private GameObject woodBurn;
    private GameObject dissolveBurnSound;
    public float steamDuration;
    public float explosionDuration;
    public float burnDuration;
    public float dissolveSoundDuration;
    private Vector3 spawnPos;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0,0,1) * Time.deltaTime;

    }

    public void OnCollisionEnter(Collision other) {

        explosion = Instantiate(explosion_prefab, other.GetContact(0).point, Quaternion.identity);
        Destroy(explosion, explosionDuration);        

        //burn something
        if(other.gameObject.tag == "Burnable"){
                other.gameObject.AddComponent<BurnController>();
                dissolveBurnSound = Instantiate(dissolveSound_prefab, other.GetContact(0).point, Quaternion.identity);
                Destroy(dissolveBurnSound, dissolveSoundDuration);
                
        }

        //kindle something
        if(other.gameObject.tag == "Kindle") {
            spawnPos = other.gameObject.GetComponent<Transform>().position + other.gameObject.GetComponent<KindleOffset>().getOffset();
            kindleFlame = Instantiate(kindle_flame_prefab, spawnPos, Quaternion.identity);
            //other.gameObject.GetComponent<CandleValue>().offset
            Debug.Log("kindle stuff");
        }

        //burn on wood
        if(other.gameObject.tag == "WoodBurn") {
            woodBurn = Instantiate(flame_prefab, other.GetContact(0).point, Quaternion.identity);
            Destroy(woodBurn, burnDuration);
        }

        //melting wall
        if(other.gameObject.tag == "Melting") {
            steam = Instantiate(steam_prefab, other.transform.position, Quaternion.identity);
            Destroy(steam, steamDuration);
            other.gameObject.AddComponent<MeltingController>(); //add Melting Script
        }      

        //transform.GetComponent<Renderer>().enabled = false;
        Destroy(transform.gameObject);
    }
}
