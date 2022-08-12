using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetPlayer : MonoBehaviour
{
    [SerializeField] private GameObject teleportationEffect;
    [SerializeField] private float teleportDelay = 2.0f;
    [SerializeField] private GameObject SaveDataJson;
    private Coroutine startAnimation;
    private Transform playerCam;
    private GameSave gSave;
    // Start is called before the first frame update
    void Start()
    {
        playerCam = Camera.main.transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Entered Respawn");
        if(other.gameObject.name.Equals("XR Origin"))
        {
            
            
            respawnPlayer();
        }
    }

    public void respawnPlayer()
    {
        Debug.Log("startAnimationRoute" + startAnimation);

        Debug.Log("StarteAnimation");
        startAnimation = StartCoroutine(startNewSceneAnimation());
    }

    public void respawnPlayerAndResetData()
    {
        Debug.Log("startAnimationRoute" + startAnimation);

        Debug.Log("StarteAnimation");
        gSave = SaveDataJson.GetComponent<GameSave>();
        gSave.resetKey();
        startAnimation = StartCoroutine(startNewSceneAnimation());
    }

    private IEnumerator startNewSceneAnimation()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("LoadingScene", LoadSceneMode.Single);
        operation.priority = 1;
        operation.allowSceneActivation = false;
        Debug.Log("Pro :" + operation.progress);
        GameObject effectObject = Instantiate(teleportationEffect, this.playerCam.position, Quaternion.identity);
        effectObject.transform.parent = this.playerCam;


        yield return new WaitForSeconds(1.7f);
        while (operation.progress < 0.9f)
        {
            yield return new WaitForSeconds(0.05f);
            Debug.Log(operation.progress);
        }
        operation.allowSceneActivation = true;
    }
}
