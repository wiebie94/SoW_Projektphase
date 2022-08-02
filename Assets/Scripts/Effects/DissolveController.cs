using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveController : MonoBehaviour
{
    public float duration = 2; // in Sekunden
    public bool startUp = false;
    public bool startDown = false;
    private bool dissolve = false;

    private float dissolveSpeed = 0f;
    private float factor = 1f;
    private float limitMin = 0f;
    private float limitMax = 0f;


    void Update()
    {
        if (startUp) 
        {
            dissolveUp();
            startUp = false;
        }
        if (startDown)
        {
            dissolveDown();
            startDown = false;
        }
        if (dissolve) this.dissolveUpdate();
    }
    void dissolveUpdate() {
        float oldDissolve = gameObject.transform.GetComponent<Renderer>().material.GetFloat("_DissolveAmount");

        float newDissolve = oldDissolve + (factor * dissolveSpeed * Time.deltaTime);

        if (oldDissolve < limitMin) 
        {
            newDissolve = limitMin;
            dissolve = false;
        } 
        if (oldDissolve > limitMax)
        {
            newDissolve = limitMax;
            dissolve = false;
        }
        gameObject.transform.GetComponent<Renderer>().material.SetFloat("_DissolveAmount", newDissolve);
    } 
    public void dissolveUp()
    {
        this.setDissolve(-1f, -1f, 1f);
    }
    public void dissolveDown()
    {
        this.setDissolve(1f, -1f, 1f);
    }
    public void setDissolve(float factor, float limitMin, float limitMax) 
    {
        this.factor = factor;
        this.limitMin = limitMin;
        this.limitMax = limitMax;

        this.dissolveSpeed = 2f / duration;
        this.dissolve = true;
    }

}
