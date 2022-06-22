using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnDisable()
    {
        Reset();
    }

    private void Reset()
    {
        Vector3 oldPos = transform.localPosition;
        transform.localPosition = new Vector3(oldPos.x, oldPos.y, 0);
    }

}
