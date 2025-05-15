using System.IO;
using UnityEditor.Localization.Platform.Android;
using UnityEngine;

public static class SaveSystem
{
    private static string saveFilePath = saveFilePath = Path.Combine(Application.persistentDataPath, "playerData.json");


    public static void Save(PlayerData playerData)
    {
        string json = JsonUtility.ToJson(playerData);
        File.WriteAllText(saveFilePath, json);
    }
    public static PlayerData Load()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);
            return data;
        }
        else 
        {
            return new PlayerData();
        }
    }
}
