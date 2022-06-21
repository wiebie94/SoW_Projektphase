using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    bool move;
    bool rotate;
    bool dissolve;
    public Vector3 rotation;
    private float oldDissolve;
    private float dissolveSpeed; 
    private float timer;
    public float rotationTime;
    private int nrOfChilds;

    // Start is called before the first frame update
    void Start()
    {
        move = true;
        rotate = false;
        dissolve = false;
        timer = 0;
        nrOfChilds = gameObject.GetComponent<Transform>().childCount;
    }

    // Update is called once per frame
    void Update()
    {
        //movment to lock (destroy later)
        if(move){
            transform.position += new Vector3(0,0,1) * Time.deltaTime;
        }

        //rotation in lock
        if(rotate){
            transform.Rotate(rotation, Space.World);
            timer += Time.deltaTime;
            if(timer > rotationTime){
                rotate = false;
                timer = 0;
                dissolve = true;
            }
        }

        //dissolve key
        if(dissolve){

            oldDissolve = gameObject.transform.GetComponent<Renderer>().material.GetFloat("_DissolveAmount");
            gameObject.transform.GetComponent<Renderer>().material.SetFloat("_DissolveAmount", oldDissolve + (dissolveSpeed * Time.deltaTime));

            for(int i = 0; i < nrOfChilds; i++){
                oldDissolve = gameObject.transform.GetChild(i).GetComponent<Renderer>().material.GetFloat("_DissolveAmount");
                gameObject.transform.GetChild(i).GetComponent<Renderer>().material.SetFloat("_DissolveAmount", oldDissolve + (dissolveSpeed * Time.deltaTime));
            }
        }
    }

    private void OnCollisionEnter(Collision other) {
        gameObject.GetComponent<Collider>().enabled = false;
        dissolveSpeed = other.gameObject.GetComponent<LockController>().dissolveSpeed;
        move = false;
        rotate = true;
    }
}
