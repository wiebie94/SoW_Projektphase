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
    // Start is called before the first frame update
    void Start()
    {
        underWater = false;
        filled = false;
        renderer = GetComponent<Transform>().GetChild(0).GetChild(0).GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(underWater && !filled){
            fillAmount = renderer.material.GetFloat("_Fill");
            fillAmount += Time.deltaTime * fillSpeed;
            renderer.material.SetFloat("_Fill", fillAmount);

            if(fillAmount > fillThreshold){
                filled = true;
                GetComponent<Transform>().GetChild(1).GetComponent<GameObject>().SetActive(true);   //activate cork
            }
        }
    }

    public void setUnderWater(){
        underWater = true;
    }
    public void resetUnderWater(){
        underWater = false;
    }
}
