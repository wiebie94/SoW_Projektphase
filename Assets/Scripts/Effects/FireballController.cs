using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : MonoBehaviour
{

    public GameObject explosion_prefab;
    public GameObject steam_prefab;
    public GameObject flame_prefab;
    private GameObject explosion;
    private GameObject steam;
    private GameObject kindleFlame;
    private GameObject woodBurn;
    public float steamDuration = 2.0f;
    public float explosionDuration = 2.0f;
    public float burnDuration = 2.0f;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0,0,10) * Time.deltaTime;
    }

    public void OnCollisionEnter(Collision other) {

        //explosion on hit
        explosion = Instantiate(explosion_prefab, other.GetContact(0).point, Quaternion.identity);
        Destroy(explosion, explosionDuration);
        
        //burn something
        if(other.gameObject.tag == "Burnable"){
                other.gameObject.AddComponent<BurnController>();
        }

        //kindle something
        if(other.gameObject.tag == "Kindle") {
            kindleFlame = Instantiate(flame_prefab, other.GetContact(0).point, Quaternion.identity);
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

        //Destroy firball, mit ruben besprechen
        transform.gameObject.SetActive(false);
    }
}
