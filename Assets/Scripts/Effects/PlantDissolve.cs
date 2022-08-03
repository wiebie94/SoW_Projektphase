using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantDissolve : MonoBehaviour
{
    Renderer rend;

    float dissolve;
    bool dissolving;
    void Start()
    {
        rend = GetComponent<Renderer>();

        dissolve = -1;
        dissolving = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (dissolving && dissolve <= 1)
        {
            dissolve += Time.deltaTime;
            rend.material.SetFloat("_dissolveAmount", dissolve);
        }
    }

    public void DissolvePlant()
    {
        dissolving = true;
    }
}
