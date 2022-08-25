using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KindleOffset : MonoBehaviour
{
    public Vector3 offset;

    public GameObject light;
    // Start is called before the first frame update
    void Start()
    {     
        
    }

    // Update is called once per frame
    void Update()
    {     
    }

    public Vector3 getOffset(){
        if (light != null) {
            light.SetActive(true);
        }
        return offset;
    }
}
