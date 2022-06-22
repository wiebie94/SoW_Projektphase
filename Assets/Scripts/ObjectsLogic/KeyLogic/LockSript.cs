using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockSript : MonoBehaviour
{
    public int keyIndex;

    private void OnTriggerEnter(Collider other)
    {
        KeySript keySript = other.GetComponent<KeySript>();
        if (keySript == null) return;

        if (keySript.keyIndex != keyIndex) return;

        //ToDo Animation Triggern

    }
}
