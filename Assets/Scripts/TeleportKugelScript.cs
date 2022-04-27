using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportKugelScript : MonoBehaviour
{
    [SerializeField] private bool isEnable = true;
    [SerializeField] private bool destroy = true;
    [SerializeField] private bool tagTeleport = true;

    [SerializeField] private string playerGameObjectName = "XR Origin";
    [SerializeField] private string leftHandGameObjectName = "LeftHand Controller";
    [SerializeField] private string rightHandGameObjectName = "RightHand Controller";

    private ActionBasedController controller;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        this.player =  GameObject.Find(playerGameObjectName);
        if (player != null)
        {
            GameObject leftHandGameobject = GameObject.Find(leftHandGameObjectName);
            GameObject rightHandGameobject = GameObject.Find(rightHandGameObjectName);

            this.controller = rightHandGameobject.GetComponent<ActionBasedController>();   
            this.controller.selectAction.action.canceled += enable;

            this.controller = rightHandGameobject.GetComponent<ActionBasedController>();
            this.controller.selectAction.action.canceled += enable;
        }
        else 
        {
            Debug.Log("Objektname vom Spieler ist Falsch oder nicht angegeben");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision collision)
    {
        GameObject collisionGameObject = collision.gameObject;
        if (!isEnable) return;

        if (collisionGameObject.tag == "Hand" || collisionGameObject.tag == "Player") return;

        if (this.tagTeleport && collisionGameObject.tag != "Teleport") return;

        this.teleportation(collision.contacts[0].point);
        this.isEnable = false;
        Debug.Log("Teleport");
        if (destroy) Object.Destroy(this.gameObject);
        
    }

    private void teleportation(Vector3 point) 
    {
        // Funktioniert noch nicht aus Gründen
        //yield return new WaitForSeconds(2f);
        Debug.Log("test");
        float playerColiderOffsetY = this.player.GetComponent<Collider>().bounds.extents.y;

        this.player.transform.position = point + Vector3.up * playerColiderOffsetY;

    }

    private void enable(InputAction.CallbackContext obj)
    {
        this.isEnable = true;
    }
}
