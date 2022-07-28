using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PhysicsButton : MonoBehaviour
{
    [SerializeField] private float threshold = 0.1f;
    [SerializeField] private float deadZone = 0.025f;
    [SerializeField] private GameObject teleportationEffect;
    [SerializeField] private float teleportDelay = 2.0f;
    [SerializeField] private GameObject player;
    private bool isPressed = false;
    private Vector3 startPostion; 
    private ConfigurableJoint joint;
    private Transform playerCam;
    private Coroutine startAnimation;

    public UnityEvent onPressed, onReleased;
    // Start is called before the first frame update
    void Start()
    {
        playerCam = Camera.main.transform;
        startPostion = transform.localPosition;
        joint = GetComponent<ConfigurableJoint>();
    }


   
    // Update is called once per frame
    void Update()
    {
        if (!isPressed && GetValue() + threshold >=1)
        {
            Debug.Log("Pressed");
            isPressed = true;
            Pressed();
            
        }
        if(isPressed && GetValue() - threshold <= 1)
        {
            Release();
            
        }
    }

    private void OnEnable()
    {
        if(startAnimation != null)
        {
            StopCoroutine(startAnimation);
            startAnimation = null;
        }
        Debug.Log("OnEnable called");
        
    }




    private void Pressed() {
        
        onPressed.Invoke();
       
    

    }


    public void startResetPlayer() {
        
        Debug.Log("startAnimationRoute" + startAnimation);
 
            Debug.Log("StarteAnimation");
            startAnimation = StartCoroutine(startNewSceneAnimation());
            
    }

    private void Release() {
        
        onReleased.Invoke();
        Debug.Log("Released");
        
    }

    private float GetValue()
    {

        var value = Vector3.Distance(startPostion, transform.localPosition) / joint.linearLimit.limit;

        if (Mathf.Abs(value) < deadZone)
        {
            value = 0;
        }
        return Mathf.Clamp(value,-1f,1f);
    }

    private IEnumerator startNewSceneAnimation()
    {
       AsyncOperation operation =  SceneManager.LoadSceneAsync("LoadingScene", LoadSceneMode.Single);
        operation.priority = 1;
        operation.allowSceneActivation = false;
        GameObject effectObject = Instantiate(teleportationEffect, this.playerCam.position, Quaternion.identity);
            effectObject.transform.parent = this.playerCam;
        

        yield return new WaitForSeconds(1.7f);
        while (!operation.isDone)
        {
            Debug.Log(operation.progress);
            if (operation.progress >= 0.9f)
            {
                operation.allowSceneActivation = true;
                StopAllCoroutines();
                
            }
            yield return null;
        }
        
    }


    // called second
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);
        this.player.transform.position = new Vector3(-20f, 0, 0);
        this.isPressed = false;
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

}
