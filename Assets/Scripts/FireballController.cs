using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : MonoBehaviour
{

    public GameObject explosion_prefab;
    public GameObject steam_prefab;
    private GameObject explosion;
    private GameObject steam;
    public float steamDuration = 2;

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
        Destroy(explosion, 2.0f);   //zerstoeren nach 2 sec
        
        //burn something
        if(other.gameObject.tag == "Burnable"){
            Debug.Log("burn");

            //wenn object childs hat
            if(other.gameObject.transform.childCount > 0){
                foreach(Transform child in other.gameObject.transform){
                    child.gameObject.AddComponent<BurnController>();   //add burnscript to childs
                }
            }
            else{
                Debug.Log("elseeeee");
                other.gameObject.AddComponent<BurnController>();
            }
            
        }

        //kindle something
        if(other.gameObject.tag == "Kindle") {
            Debug.Log("kindle stuff");
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
