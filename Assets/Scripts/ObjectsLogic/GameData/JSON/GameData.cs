using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
//ACHTUNG SaveData m�ssen gel�scht werden falls eine neues Atribute angelegt wird
public class GameData { 

    /*public struct colindex {
        Color color;
        int index;
    }*/
    public List<Color> hasKeyIndex;

    public GameData(List<Color> hasKeyIndex)
    {
        this.hasKeyIndex = hasKeyIndex;
    }

    public GameData()
    {
        hasKeyIndex = new List<Color>();
        this.hasKeyIndex.Add(new Color(1f,0f,0f,1f));
    }
}
