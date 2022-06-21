using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockController : MonoBehaviour
{
    public Vector3 keyOffset;
    private int nrOfChilds;
    private float oldDissolve;
    public float dissolveSpeed; 
    public float openTimer;
    bool dissolve;
    // Start is called before the first frame update
    void Start()
    {
        nrOfChilds = gameObject.GetComponent<Transform>().childCount;
        dissolve = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(dissolve){
            for(int i = 0; i < nrOfChilds; i++){
                oldDissolve = gameObject.transform.GetChild(i).GetComponent<Renderer>().material.GetFloat("_DissolveAmount");
                gameObject.transform.GetChild(i).GetComponent<Renderer>().material.SetFloat("_DissolveAmount", oldDissolve + (dissolveSpeed * Time.deltaTime));
            }
        }
    }

    private void OnCollisionEnter(Collision other) {
        other.gameObject.GetComponent<Transform>().position = transform.position + keyOffset;
        openTimer = other.transform.GetComponent<KeyController>().rotationTime;
        Invoke("setDissolve", openTimer);
    }

    void setDissolve(){
        dissolve = true;
    }
}
