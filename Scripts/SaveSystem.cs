using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using YG;
public static class SaveSystem
{
    private static string saveFilePath = Path.Combine(Application.persistentDataPath, "playerData.json");
    public static void Save(PlayerData playerData)
    {
        if (YG2.isSDKEnabled)
        {
            YG2.saves.playerData = playerData;
            YG2.SaveProgress();
        }
        else
        {
            string json = JsonUtility.ToJson(playerData);
            File.WriteAllText(saveFilePath, json);
        }
    }
    public static PlayerData Load()
    {
        Debug.Log(YG2.isSDKEnabled);
        if (YG2.isSDKEnabled)
        {
            if (YG2.saves.playerData == null)
            {
                YG2.saves.playerData = new PlayerData();
                YG2.SaveProgress();
            }
            Debug.Log(YG2.saves.playerData.money);
            return YG2.saves.playerData;
        }
        else
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
            Debug.Log(data.unitsList == null);
            if (data.unitsList == null || data.unitsList.Count == 0)
            {
                data = new PlayerData();
                Save(data);
            }
            return data;
        }
    }
}
