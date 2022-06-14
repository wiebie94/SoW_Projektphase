using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportKugelScript : MonoBehaviour
{
    [SerializeField] private bool isEnable = true;
    [SerializeField] private bool destroy = true;
    [SerializeField] private bool tagTeleport = true;

    [SerializeField] private float bounciness = 0.5f;

    [SerializeField] private float teleportDelay = 2.0f;
    [SerializeField] private int colisionCounter = 0;
    [SerializeField] private float destroyMaxTime = 5.0f;

    [SerializeField] private float destroyMinVelocity = 0.05f;

    [SerializeField] private string playerGameObjectName = "XR Origin";

    [SerializeField] private GameObject teleportEffect;

    private Rigidbody rb;
    private PhysicMaterial ph;
    private MeshRenderer m;

    private ActionBasedController controller;

    private GameObject player;

    private IKugelDestroy manager;

    private bool isTeleportation = false;
    private float destroyTime = 0;

    private Transform playerCam;

    // Start is called before the first frame update
    void Start()
    {
        playerCam = Camera.main.transform;

        this.manager = GameObject.Find("TeleportManager").GetComponent<IKugelDestroy>();
        if (this.manager == null) Debug.Log("Achtung es befindet sich kein TeleporManager in der Scene");

        this.rb = this.GetComponent<Rigidbody>();
        this.m = this.GetComponentInChildren<MeshRenderer>();
        this.ph = this.GetComponentInChildren<Collider>().material;


        this.player =  GameObject.Find(playerGameObjectName);
        if (player == null) Debug.Log("Objektname vom Spieler ist falsch oder nicht angegeben");


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //######### Destroy over Time #########
        if (this.isEnable && !this.isTeleportation) 
            this.destroyTime += Time.deltaTime;

        if (this.destroyMaxTime < this.destroyTime) 
            Object.Destroy(this.gameObject);


        Debug.Log("RB+Vel" + this.rb.velocity.magnitude);

        //######### Destroy wenn zu langsam #########
        if (this.isEnable && this.rb.velocity.magnitude < destroyMinVelocity) 
            Object.Destroy(this.gameObject);
    }

    void OnDestroy()
    {
        if (this.manager != null) manager.isDestroyed();
    }
    void OnCollisionEnter(Collision collision)
    {
        //######### Collision checks #########
        GameObject collisionGameObject = collision.gameObject;
        if (!isEnable) return;

        if (collisionGameObject.tag == "Hand" || collisionGameObject.tag == "Player") return;

        if(this.isEnable) colisionCounter++;

        if (this.tagTeleport && collisionGameObject.tag != "Teleport") return;


        if (destroy) {
            this.rb.isKinematic = true;
            this.rb.detectCollisions = false;
            this.m.enabled = false;
        }


        //######### Teleportaion #########
        StartCoroutine(this.teleportation(collision.contacts[0].point));

        this.isEnable = false;

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        Debug.Log("Teleport");
        
    }

    private IEnumerator teleportation(Vector3 point) 
    {
        this.isTeleportation = true;
        this.startingTelportAnimation();
        yield return new WaitForSeconds(this.teleportDelay);
  
        float playerColiderOffsetY = this.player.GetComponent<Collider>().bounds.extents.y * 2 + 0.1f; // 0.1f da sonst spieler durch den boden f�llt
        //Debug.Log(playerColiderOffsetY);

        this.player.transform.position = (point + this.player.transform.position - this.playerCam.transform.position) + Vector3.up * playerColiderOffsetY;

        if (destroy) {
            Object.Destroy(this.gameObject);
        } 

    }

    private void startingTelportAnimation()
    {
        GameObject effectObjekt = Instantiate(teleportEffect, this.playerCam.position, Quaternion.identity);
        effectObjekt.transform.parent = this.playerCam;
    }

    

    public void activate()
    {
        this.isEnable = true;
        this.destroyTime = 0;
        this.ph.bounciness = this.bounciness;
    }
    public void deactivate()
    {
        this.isEnable = false;
        this.destroyTime = 0;
        this.ph.bounciness = this.bounciness;
    }
}
