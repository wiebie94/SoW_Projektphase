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

    bool isActivated = false;

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
        if (other.CompareTag("MainCamera"))
        {
            respawnPlayer();
        }
    }

    public void respawnPlayer()
    {
        if (isActivated)
            return;

        isActivated = true;

        //Debug.Log("startAnimationRoute" + startAnimation);

        //Debug.Log("StarteAnimation");
        startAnimation = StartCoroutine(startNewSceneAnimation());
    }

    public void respawnPlayerAndResetData()
    {
        if (isActivated)
            return;

        isActivated = true;

        //Debug.Log("startAnimationRoute" + startAnimation);

        //Debug.Log("StarteAnimation");
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
