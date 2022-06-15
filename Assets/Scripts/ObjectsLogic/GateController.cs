using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{
    public GameObject[] gates;
    public GameObject[] levers;
    public GameObject[] levels;
    public GameObject[] levelPostitions;
    private GameObject g1;

    [SerializeField] float gateCloseTime = 2.0f;

    public void Open(GameObject lever) {
        for(int i = 0; i < gates.Length; i++) {
            if(lever == levers[i]) {
                gates[i].GetComponent<GateLogic>().OpenGate();
                //levels[i].SetActive(true);
                Destroy(g1);
                g1 = Instantiate(levels[i], levelPostitions[i].transform, false);
            }
            else {
                gates[i].GetComponent<GateLogic>().CloseGate();
                //StartCoroutine(HideLevel(levels[i]));
            }
        }
    }

    public void Close(GameObject lever) {
        for(int i = 0; i < gates.Length; i++) {
            if(lever == levers[i]) {
                gates[i].GetComponent<GateLogic>().CloseGate();
                StartCoroutine(HideLevel(levels[i]));
            }
        }
    }

    private IEnumerator HideLevel(GameObject level)
    {
        yield return new WaitForSeconds(gateCloseTime);

        //level.SetActive(false);
        Destroy(g1);
    }
}
