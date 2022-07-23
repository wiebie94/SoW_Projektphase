using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1LogicController : MonoBehaviour
{
    public GameObject objectiveLeft;
    public GameObject objectiveFront;
    public GameObject objectiveRight;
    public GameObject trophy;

    bool objLeftCompleted = false;
    bool objFrontCompleted = false;
    bool objRightCompleted = false;

    public void setObjLeftCompleted(bool c) {
        objLeftCompleted = c;
        Debug.Log(c);
    }

    public void setObjFrontCompleted(bool c) {
        objFrontCompleted = c;
    }
    
    public void setObjRightCompleted(bool c) {
        objRightCompleted = c;
    }

    private void Update()
    {
        if(checkCompleted())
        {
            //do something
            if(trophy != null)
            {
                trophy.SetActive(true);
            }            
            Debug.Log("Win");
        }
    }

    private bool checkCompleted()
    {
        return objLeftCompleted && objFrontCompleted && objRightCompleted;
    }
}
