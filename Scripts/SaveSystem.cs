using System;
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
        Debug.Log(Path.Combine(Application.persistentDataPath, "playerData.json"));
        PlayerData data;
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            data = JsonUtility.FromJson<PlayerData>(json);
        }
        else
        {
            data = new PlayerData();
        }
        if (data.unitsList.Count == 0)
        {
            data.unitsList = new List<UnitData> { new UnitData("Soldier", 1) };
            Save(data);
        }
        return data;

    }
}
