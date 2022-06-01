using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallbackDestroyParent : MonoBehaviour
{
    public void childDestroy()
    {
        if(this.transform.childCount-1 == 0) Destroy(this.gameObject);
    }
}
