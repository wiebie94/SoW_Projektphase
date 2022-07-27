using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    private GameObject mykey;
    bool move;
    bool rotate;
    public bool dissolve;
    public Vector3 rotation;
    private float oldDissolve;
    public float dissolveSpeed = 1f; 
    private float timer;
    public float rotationTime;
    private Material keyMaterial;
    [SerializeField]
    private keyColor Key_Color;
    public enum keyColor{
        Blue,
        Red,
        Green,
        Yellow,
        Purple,
        White
    }

    // Start is called before the first frame update
    void Start()
    {
        move = false;
        rotate = false;
        dissolve = false;
        timer = 0;;
        keyMaterial = gameObject.GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        //movment to lock (destroy later)
        if(move){
            transform.position += new Vector3(-1,0,0) * Time.deltaTime;
        }

        //rotation in lock
        if(rotate){

            //aktivate key on lock and animation
            //if rotation is finished set dissolve true

            /*
            transform.Rotate(rotation, Space.World);
            timer += Time.deltaTime;
            if(timer > rotationTime){
                rotate = false;
                timer = 0;
                dissolve = true;
            }
            */
        }

        //dissolve key
        if(dissolve){
            Debug.Log(oldDissolve);
            oldDissolve = gameObject.transform.GetComponent<Renderer>().material.GetFloat("_DissolveAmount");
            gameObject.transform.GetComponent<Renderer>().material.SetFloat("_DissolveAmount", oldDissolve + (dissolveSpeed * Time.deltaTime));
            if(oldDissolve > 1){
                gameObject.active = false;
                dissolve = false;
            }
        }

        //change Color
        /*if(keyMaterial.GetColor("_KeyLockColor") != getCurrentColor()){
            keyMaterial.SetColor("_KeyLockColor", getCurrentColor());
        }*/
    }

    private void OnCollisionEnter(Collision other) {
        if(getCurrentColor() == other.gameObject.GetComponent<LockController>().getCurrentColor()){
            gameObject.GetComponent<Collider>().enabled = false;
            //key destroy, but safe the hands
            dissolveSpeed = other.gameObject.GetComponent<LockController>().dissolveSpeed;
            move = false;
            rotate = true;
            //start animation
            //delete later when animation is finished
            dissolve = true;
        }
    }

    /*public Color getCurrentColor(){
        if(Key_Color == keyColor.Red){
            return Color.red;
        }
        if(Key_Color == keyColor.Blue){
            return Color.blue;
        }
        if(Key_Color == keyColor.Green){
            return Color.green;
        }
        if(Key_Color == keyColor.Yellow){
            return Color.yellow;
        }
        if(Key_Color == keyColor.Purple){
            return Color.magenta;
        } 
        return Color.white;
    }*/
    public Color getCurrentColor()
    { 
        return this.GetComponent<Renderer>().material.GetColor("_KeyLockColor");
    }
    public void setDissolve(){
        dissolve = true;
    }

    /*public void setColor(Color c){
        if(c == Color.red){
            Key_Color = keyColor.Red;
        }
        else if(c == Color.blue){
            Key_Color = keyColor.Blue;
        }
        else if(c == Color.green){
            Key_Color = keyColor.Green;
        }
        else if(c == Color.yellow){
            Key_Color = keyColor.Yellow;
        }
        else if(c == Color.magenta){
            Key_Color = keyColor.Purple;
        }
        else{
            Key_Color = keyColor.White;
        }
    }*/
}
