using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillAndRelease : MonoBehaviour
{
    Renderer renderer;
    bool underWater;
    bool filled;
    float fillAmount;
    public float fillSpeed;
    public float fillThreshold;
    public float fillStart;
    // Start is called before the first frame update
    void Start()
    {
        underWater = false;
        filled = false;
        renderer = GetComponent<Transform>().GetChild(0).GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(underWater && !filled){
            if(fillAmount < 0.1) fillAmount = fillStart;
            fillAmount = renderer.material.GetFloat("_Fill");
            fillAmount += Time.deltaTime * fillSpeed;
            renderer.material.SetFloat("_Fill", fillAmount);

            if(fillAmount >= fillThreshold){
                Debug.Log("fulll");
                filled = true;
                this.gameObject.transform.parent.gameObject.transform.GetChild(1).gameObject.SetActive(true);   //activate cork
                
            }
        }
    }

    public void setUnderWater(){
        Debug.Log("Underwater");
        underWater = true;
    }
    public void resetUnderWater(){
        Debug.Log("Not Underwater");
        underWater = false;
    }
    public bool getFilled(){
        return filled;
    }
}
