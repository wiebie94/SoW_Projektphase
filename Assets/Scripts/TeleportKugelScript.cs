using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportKugelScript : MonoBehaviour
{
    [SerializeField] private bool isEnable = true;
    [SerializeField] private bool destroy = true;
    [SerializeField] private bool tagTeleport = true;
    [SerializeField] private float speedFac = 1.03f;
    [SerializeField] private float maxSpeed = 9.0f;

    [SerializeField] private float bounciness = 0.5f;

    [SerializeField] private float teleportDelay = 2.0f;
    [SerializeField] private int colisionCounter = 0;
    [SerializeField] private float destroyMaxTime = 5.0f;

    [SerializeField] private float destroyMinVelocity = 0.05f;

    [SerializeField] private string playerGameObjectName = "XR Origin";

    private Rigidbody rb;
    private PhysicMaterial ph;
    private MeshRenderer m;

    private ActionBasedController controller;

    private GameObject player;

    private IKugelDestroy manager;

    private bool isTeleportation = false;
    private float destroyTime = 0;

    // Start is called before the first frame update
    void Start()
    {   
        this.manager = GameObject.Find("TeleportManager").GetComponent<IKugelDestroy>();
        if (this.manager == null) Debug.Log("Achtung es befindet sich kein TeleporManager in der Scene");

        this.rb = this.GetComponent<Rigidbody>();
        this.m = this.GetComponent<MeshRenderer>();
        this.ph = this.GetComponent<Collider>().material;


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


        Debug.Log(this.rb.velocity.magnitude);
        if (this.isEnable) limitVelocity();

        //######### Destroy wenn zu langsam #########
        if (this.isEnable && this.rb.velocity.magnitude < destroyMinVelocity) 
            Object.Destroy(this.gameObject);
    }

    private void limitVelocity()
    {
        /*Vector3 velocity = this.rb.velocity;
        this.rb.velocity = velocity * 1.03f;
        if (this.rb.velocity.magnitude > this.maxSpeed)
            rb.velocity = this.rb.velocity.normalized * this.maxSpeed;*/

        Debug.Log(this.rb);
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

        Debug.Log("Teleport");
        
    }

    private IEnumerator teleportation(Vector3 point) 
    {
        this.isTeleportation = true;
        this.startingTelportAnimation();
        yield return new WaitForSeconds(this.teleportDelay);
  
        float playerColiderOffsetY = this.player.GetComponent<Collider>().bounds.extents.y;

        this.player.transform.position = (point + Vector3.up) * playerColiderOffsetY;

        if (destroy) {
            Object.Destroy(this.gameObject);
        } 

    }

    private void startingTelportAnimation()
    {
        //ToDo partikel Animation ausführen
    }

    

    public void activate()
    {
        this.isEnable = true;
        this.destroyTime = 0;
        this.ph.bounciness = this.bounciness;
    }
}
