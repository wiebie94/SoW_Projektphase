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

    private Rigidbody rbOfLever;               

    [SerializeField] float gateCloseTime = 1.75f;
    [SerializeField] float leverStopTime = 2.5f;

    // Oeffnet ein Tor zum dazugehoerigen Hebel und schliesst alle anderen Tore
    public void Open(GameObject lever) {

        for(int i = 0; i < gates.Length; i++) {
            if(lever == levers[i]) {
                Debug.Log("Lever found: " + levers[i]);
                gates[i].GetComponent<GateLogic>().OpenGate();
                // Zerstoert das vorherig instantiierte Level
                Destroy(activeGate);
                // Instantiieren des Levels
                activeGate = Instantiate(levels[i], levelPostitions[i].transform, false);
                rbOfLever = lever.GetComponent<Rigidbody>();
                StartCoroutine(StopLever(rbOfLever));
            }
            else {
                Debug.Log("Lever not found: " + levers[i]);
                // Schliessen der anderen 
                levers[i].GetComponent<LeverLogic>().SetToClosed();
                gates[i].GetComponent<GateLogic>().CloseGate();                
            }
        }
    }

    public void Close(GameObject lever) {
        for(int i = 0; i < gates.Length; i++) {
            if(lever == levers[i]) {
                gates[i].GetComponent<GateLogic>().CloseGate();
                rbOfLever = lever.GetComponent<Rigidbody>();
                StartCoroutine(StopLever(rbOfLever));
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

    //stops the lever for 2.5 seconds to prevent 
    private IEnumerator StopLever(Rigidbody rb){

        rb.isKinematic = true;
        yield return new WaitForSeconds(leverStopTime);
        rb.isKinematic = false;
        
    }
}
