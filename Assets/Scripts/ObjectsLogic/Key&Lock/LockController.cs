using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockController : MonoBehaviour
{
    private GameObject lever;
    public Vector3 keyOffset;
    private int nrOfChilds;
    private float oldDissolve;
    public float dissolveSpeed; 
    private float openTimer;
    private bool dissolve;
    private bool leverAppear;
    public float leverSpawnTimer;
    private Material lockMaterial;
    [SerializeField]
    private lockColor Lock_Color;
    public enum lockColor{
        Blue,
        Red,
        Green,
        Yellow,
        Purple
    }
    // Start is called before the first frame update
    void Start()
    {
        dissolve = false;
        leverAppear = false;
        lockMaterial = gameObject.GetComponent<Renderer>().material;
        lever = gameObject.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(dissolve){
            oldDissolve = gameObject.transform.GetComponent<Renderer>().material.GetFloat("_DissolveAmount");
            gameObject.transform.GetComponent<Renderer>().material.SetFloat("_DissolveAmount", oldDissolve + (dissolveSpeed * Time.deltaTime));
            if(oldDissolve > leverSpawnTimer){
                gameObject.GetComponent<Collider>().enabled = false;
                lever.gameObject.active = true;
                dissolve = false;
                leverAppear = true;
            }
        }

        //change Color
        if(lockMaterial.GetColor("_KeyLockColor") != getCurrentColor()){
            lockMaterial.SetColor("_KeyLockColor", getCurrentColor());
        }

        if(leverAppear){
            foreach(Transform child in lever.transform){
                if(child.gameObject.active == true){
                    oldDissolve = child.GetComponent<Renderer>().material.GetFloat("_DissolveAmount");
                    child.GetComponent<Renderer>().material.SetFloat("_DissolveAmount", oldDissolve - (dissolveSpeed * Time.deltaTime));
                }
            }
            if(oldDissolve > 1){
                leverAppear = false;
                //lever configuration
            } 
        }
    }

    private void OnCollisionEnter(Collision other) {
        if(getCurrentColor() == other.gameObject.GetComponent<KeyController>().getCurrentColor()){
            other.gameObject.GetComponent<Transform>().position = transform.position + keyOffset;
            openTimer = other.transform.GetComponent<KeyController>().rotationTime;
            Invoke("setDissolve", openTimer);
        } 
    }

    void setDissolve(){
        dissolve = true;
    }

    private lockColor GetLockColor(){
        return Lock_Color;
    }
    public Color getCurrentColor(){
        if(Lock_Color == lockColor.Red){
            return Color.red;
        }
        if(Lock_Color == lockColor.Blue){
            return Color.blue;
        }
        if(Lock_Color == lockColor.Green){
            return Color.green;
        }
        if(Lock_Color == lockColor.Yellow){
            return Color.yellow;
        }
        if(Lock_Color == lockColor.Purple){
            return Color.magenta;
        } 
        return Color.white;
    }
}
