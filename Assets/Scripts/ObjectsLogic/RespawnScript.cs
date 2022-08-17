using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnScript : MonoBehaviour
{
    private TeleportKugelManager tm;
    private GameObject player;
    private Transform playerCam;

    private GameObject respawnPoint;

    [SerializeField] private string playerGameObjectName = "XR Origin";
    [SerializeField] private float teleportDelay = 1f;
    [SerializeField] private GameObject teleportEffect;
    // Start is called before the first frame update

    private bool firstTime = true;
    void Start()
    {
        tm = GameObject.Find("VRPlayer").GetComponentInChildren<TeleportKugelManager>();
        playerCam = Camera.main.transform;

        this.player = GameObject.Find(playerGameObjectName);
        if (player == null) Debug.Log("Objektname vom Spieler ist falsch oder nicht angegeben");

        this.respawnPoint = GameObject.Find("RespawnPoint");

    }
    public void respawn()
    {
        StartCoroutine(this.teleport(this.transform.position));
    }
    public void keyTeleport() {
        if (!this.firstTime) return;
        Vector3 position = this.transform.position;
        if (respawnPoint != null)
            position = respawnPoint.transform.position;


        StartCoroutine(this.teleport(position));
        firstTime = false;

    }
    private IEnumerator teleport(Vector3 point) {
        this.startingTelportAnimation();
        yield return new WaitForSeconds(this.teleportDelay);

        float playerColiderOffsetY = this.player.GetComponent<Collider>().bounds.extents.y * 2 + 0.1f; // 0.1f da sonst spieler durch den boden f?llt
        //Debug.Log(playerColiderOffsetY);

        this.player.transform.position = (point + this.player.transform.position - this.playerCam.transform.position) + Vector3.up * playerColiderOffsetY;
    }
    private void startingTelportAnimation()
    {
        GameObject effectObjekt = Instantiate(teleportEffect, this.playerCam.position, Quaternion.identity);
        effectObjekt.transform.parent = this.playerCam;
    }
}
