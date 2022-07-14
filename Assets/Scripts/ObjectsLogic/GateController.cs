using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{
    public GameObject[] gates;                  // Tore die bewegt werden
    public GameObject[] levers;                 // Hebel der jeweiligen Tore
    public GameObject[] levels;                 // Level Prefabs
    public GameObject[] levelPostitions;        // Positionen an denen die Level instantiiert werden
    private GameObject activeGate;                      

    [SerializeField] float gateCloseTime = 1.75f;

    // Oeffnet ein Tor zum dazugehoerigen Hebel und schliesst alle anderen Tore
    public void Open(GameObject lever) {
        for(int i = 0; i < gates.Length; i++) {
            if(lever == levers[i]) {
                gates[i].GetComponent<GateLogic>().OpenGate();
                // Zerstoert das vorherig instantiierte Level
                Destroy(activeGate);
                // Instantiieren des Levels
                activeGate = Instantiate(levels[i], levelPostitions[i].transform, false);
            }
            else {
                // Schliessen der anderen 
                levers[i].GetComponent<LeverLogic>().SetToNeutral();
                gates[i].GetComponent<GateLogic>().CloseGate();                
            }
        }
    }

    public void Close(GameObject lever) {
        for(int i = 0; i < gates.Length; i++) {
            if(lever == levers[i]) {
                gates[i].GetComponent<GateLogic>().CloseGate();
                // Checkt ob ein Level bereits in einem Positions Objekt instantiiert wurde
                if(levelPostitions[i].transform.childCount > 0){
                    StartCoroutine(HideLevel());
                }                
            }
        }
    }

    private IEnumerator HideLevel()
    {
        // Level wird nach bestimmter Zeit zerstoert, damit das Tor vorher noch schliessen kann
        yield return new WaitForSeconds(gateCloseTime);

        Destroy(activeGate);
    }
}
