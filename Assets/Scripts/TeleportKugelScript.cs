using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportKugelScript : MonoBehaviour
{
    [SerializeField] private bool isEnable = true;
    [SerializeField] private bool destroy = true;
    [SerializeField] private string playerGameObjectName = "XR Origin";

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player =  GameObject.Find(playerGameObjectName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision collision)
    {
        if (!isEnable) return;
        Debug.Log("Trigger");
        Debug.Log(collision.contacts[0].point);
        foreach (ContactPoint contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.white);
        }
        // Überarebiten
        this.player.transform.position = collision.contacts[0].point + Vector3.up * this.player.GetComponent<Collider>().bounds.extents.y;

        this.isEnable = false;
        if (destroy) 
        {
            DestroyObject(this.gameObject);
        }
    }
}
