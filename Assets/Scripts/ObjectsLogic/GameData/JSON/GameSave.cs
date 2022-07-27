using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameSave : MonoBehaviour
{
    //Quelle: https://github.com/Tutorials-By-Kaupenjoe/SavingAndLoadingData/blob/main/Assets/Scripts/Data/JSONSaving.cs
    // Start is called before the first frame update
    private GameData gameData;

    private string path = "";
    private string persistentPath = "";

    // Start is called before the first frame update
    void Awake()
    {
        SetPaths();
        CheckJsonData();
    }

    private void CheckJsonData()
    {
        if (!File.Exists(persistentPath)) {
            gameData = new GameData();
            SaveData();
            return;
        }
        LoadData();
    }

    private void SetPaths()
    {
        path = Application.dataPath + Path.AltDirectorySeparatorChar + "SaveData.json";
        persistentPath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "SaveData.json";
    }

    public void SaveData()
    {
        string savePath = persistentPath;

        Debug.Log("Saving Data at " + savePath);
        string json = JsonUtility.ToJson(gameData);
        Debug.Log(json);

        using StreamWriter writer = new StreamWriter(savePath);
        writer.Write(json);
    }

    public void LoadData()
    {
        using StreamReader reader = new StreamReader(persistentPath);
        string json = reader.ReadToEnd();

        this.gameData = JsonUtility.FromJson<GameData>(json);
        Debug.Log(gameData.ToString());
    }
    public GameData getGameData() 
    {
        return gameData;
    }
    public void resetKey()
    {
        this.gameData.resetKey();
        this.SaveData();
    }

}
