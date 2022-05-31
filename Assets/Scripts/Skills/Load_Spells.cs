using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Load_Spells : MonoBehaviour
{
    public GameObject rim;
    public GameObject center;
    RectTransform centerImage;

    public float maxSize;

    [SerializeField] float loadingStep = 0.05f;

    Coroutine loadingCoroutine;

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
    }

    // Update is called once per frame
    /*void Update()
    {
        this.transform.LookAt(Camera.main.transform.position); 
        timePassed += Time.deltaTime; 
        if(timePassed <= time){
            centerImage.sizeDelta = new Vector2(((timePassed) / time) * maxSize, ((timePassed) / time) * maxSize);
        } else {
            rim.SetActive(false);
            center.SetActive(false);
        }
    }*/

    IEnumerator LoadingCoroutine(float loadingTime)
    {
        centerImage.sizeDelta = Vector2.zero;

        rim.SetActive(true);
        center.SetActive(true);

        float timePassed = 0;

        while (timePassed < loadingTime)
        {
            this.transform.LookAt(Camera.main.transform.position);
            
            centerImage.sizeDelta = new Vector2(((timePassed) / loadingTime) * maxSize, ((timePassed) / loadingTime) * maxSize);

            timePassed += loadingStep;
            yield return new WaitForSeconds(loadingStep);
        }

        rim.SetActive(false);
        center.SetActive(false);

    }

    public void StartLoading(float loadingTime)
    {
        if (loadingCoroutine != null)
            StopCoroutine(loadingCoroutine);

        loadingCoroutine = StartCoroutine(LoadingCoroutine(loadingTime));
    }

    public void StopLoading()
    {
        if (loadingCoroutine != null)
            StopCoroutine(loadingCoroutine);

        rim.SetActive(false);
        center.SetActive(false);
    }
}
