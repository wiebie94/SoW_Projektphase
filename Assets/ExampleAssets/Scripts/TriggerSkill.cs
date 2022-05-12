using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSkill : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(this.GetComponent<SphereCollider>().gameObject.name);
        Debug.Log("Other Collider" + other.name);

            //Spawn Teleport
            GameObject g1 = Instantiate(this.gameObject, GameObject.Find("RightHand Controller").transform.position,Quaternion.identity);
            g1.transform.SetParent(GameObject.Find("RightHand Controller").transform);
    }
}
