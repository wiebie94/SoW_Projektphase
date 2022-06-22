using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
//ACHTUNG SaveData müssen gelöscht werden falls eine neues Atribute angelegt wird
public class GameData { 
    public List<int> hasKeyIndex;

    public GameData(List<int> hasKeyIndex)
    {
        this.hasKeyIndex = hasKeyIndex;
    }

    public GameData()
    {
        hasKeyIndex = new List<int>();
    }
}
