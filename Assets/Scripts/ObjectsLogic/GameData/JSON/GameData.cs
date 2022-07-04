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
    public List<Color> keySave;

    public GameData(List<Color> hasKeyIndex)
    {
        this.keySave = hasKeyIndex;
    }

    public GameData()
    {
        keySave = new List<Color>();

    }
}
