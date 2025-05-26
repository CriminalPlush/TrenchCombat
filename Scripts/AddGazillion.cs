using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddGazillion : MonoBehaviour
{
    private PlayerData playerData;
    void Start()
    {
        playerData = SaveSystem.Load();
    }

    // Update is called once per frame
    public void OnClick()
    {
        playerData.money += 1000;
        SaveSystem.Save(playerData);
        FindObjectOfType<DisplayMoney>().UpdateInfo(playerData.money);
    }
}
