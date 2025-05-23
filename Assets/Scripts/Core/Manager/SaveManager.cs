using System.IO;
using UnityEngine;

public static class SaveManager
{
    static string savePath => Path.Combine(Application.persistentDataPath, "save.json");

    public static void SaveData(GameProgressData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);
    }

    public static GameProgressData LoadData()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            return JsonUtility.FromJson<GameProgressData>(json);
        }
        else
        {
            GameProgressData data = new GameProgressData();
            SaveData(data);
            return data;
        }   
    }

    public static void DeleteData()
    {
        if (File.Exists(savePath))
        {
            File.Delete(savePath);
        }
    }
}
