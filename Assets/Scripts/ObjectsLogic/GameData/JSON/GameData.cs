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

    public float playerHeight;
    public float volume;

    public GameData(List<Color> hasKeyIndex, float playerHeight, float volume)
    {
        this.keySave = hasKeyIndex;
        this.playerHeight = playerHeight;
        this.volume = volume;
    }

    public GameData()
    {
        keySave = new List<Color>();
        playerHeight = 0;
        volume = 0;
    }
    public void resetKey() {
        keySave.Clear();
    }
}
