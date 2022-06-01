using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallbackDestroy : MonoBehaviour
{
    private void OnDestroy()
    {
        CallbackDestroyParent callbackDestroyParent = this.GetComponentInParent<CallbackDestroyParent>();
        if(callbackDestroyParent != null)
            callbackDestroyParent.childDestroy();
    }
}
