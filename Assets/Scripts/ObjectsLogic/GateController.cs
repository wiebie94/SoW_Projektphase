using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{
    public GameObject[] gates;
    public GameObject[] levers;

    public void Open(GameObject lever) {
        Debug.Log("GateController Open");
        for(int i = 0; i < gates.Length; i++) {
            if(lever == levers[i]) gates[i].GetComponent<GateLogic>().OpenGate();
            else gates[i].GetComponent<GateLogic>().CloseGate();
        }
    }

    public void Close(GameObject lever) {
        for(int i = 0; i < gates.Length; i++) {
            if(lever == levers[i]) gates[i].GetComponent<GateLogic>().CloseGate();
        }
    }
}
