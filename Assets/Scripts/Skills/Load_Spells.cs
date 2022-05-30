using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Load_Spells : MonoBehaviour
{
    public GameObject rim;
    public GameObject center;
    RectTransform centerImage;

    public float maxSize;
    public float time;
    float timePassed = 0f;
    // Start is called before the first frame update
    void Start()
    {
        rim.SetActive(false);
        center.SetActive(false);
        centerImage = center.GetComponent<RectTransform>();

        rim.GetComponent<RectTransform>().sizeDelta = new Vector2(maxSize, maxSize);

        rim.SetActive(true);
        center.SetActive(true);
        centerImage.sizeDelta = new Vector2(0,0);
        timePassed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(Camera.main.transform.position); 
        timePassed += Time.deltaTime; 
        if(timePassed <= time){
            centerImage.sizeDelta = new Vector2(((timePassed) / time) * maxSize, ((timePassed) / time) * maxSize);
        } else {
            rim.SetActive(false);
            center.SetActive(false);
        }
    }

    public void loadMenu()
    {
        rim.SetActive(true);
        center.SetActive(true);
        centerImage.sizeDelta = new Vector2(0,0);
        timePassed = 0;
    }
}
