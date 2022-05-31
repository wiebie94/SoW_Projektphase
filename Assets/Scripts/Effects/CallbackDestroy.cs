using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallbackDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var main = GetComponent<ParticleSystem>().main;
        main.stopAction = ParticleSystemStopAction.Callback;
    }

    void OnParticleSystemStopped()
    {
        Debug.Log("System has stopped!");
    }
}
