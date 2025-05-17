using System.Collections.Generic;
using System.IO;
using UnityEditor.Localization.Platform.Android;
using UnityEngine;

public static class SaveSystem
{
    private static string saveFilePath = Path.Combine(Application.persistentDataPath, "playerData.json");
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
            PlayerData playerData = new PlayerData();
            playerData.unitsList = new List<UnitData> { new UnitData("Soldier", 1) };
            return new PlayerData();
        }
    }
}
