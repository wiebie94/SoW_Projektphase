using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helper
{
    public static void ChangeLayerInObjectHierarchy(GameObject obj, string layerName)
    {
        int layer = LayerMask.NameToLayer(layerName);
        Debug.Log("Layer: " + layer);

        if (layer == -1)
        {
            Debug.LogError("layerName not Found");
            return;
        }    

        obj.layer = layer;

        foreach (Transform child in obj.GetComponentsInChildren<Transform>(true))
        {
            child.gameObject.layer = layer;
        }
    }

    public static void ChangeLayerInObject(GameObject obj, string layerName)
    {
        int layer = LayerMask.NameToLayer(layerName);
        Debug.Log("Layer: " + layer);

        if (layer == -1)
        {
            Debug.LogError("layerName not Found");
            return;
        }

        obj.layer = layer;
    }
}
