using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlLogic : MonoBehaviour
{
    [SerializeField] GameObject[] lvls;

    void Start()
    {

    }

    public void EnableLvl(int index)
    {
        if (lvls.Length - 1 < index || lvls[index] == null)
        {
            Debug.LogError("There is no lvl with index: " + index);
            return;
        }

        lvls[index].SetActive(true);
    }

    public void DisbleLvl(int index)
    {
        if (lvls.Length - 1 < index || lvls[index] == null)
        {
            Debug.LogError("There is no lvl with index: " + index);
            return;
        }

        lvls[index].SetActive(false);
    }
}
