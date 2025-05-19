using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyUpgrade : MonoBehaviour
{
    public UnitInfo unitInfo;
    void Start()
    {

    }

    // Update is called once per frame
    public void OnClick()
    {
        PlayerData playerData = SaveSystem.Load();
        if (playerData.FindUnitByName(unitInfo.title) == null)
        {
            playerData.unitsList.Add(new UnitData(unitInfo.title, 0));
        }
        UnitData unitData = playerData.FindUnitByName(unitInfo.title);
        if (unitData.level < unitInfo.unitUpgradeTable.Length - 1 && playerData.money >= unitInfo.unitUpgradeTable[unitData.level].priceOfUpgrade)
        {
            playerData.money -= unitInfo.unitUpgradeTable[unitData.level].priceOfUpgrade;
            playerData.UpdateLevel(unitInfo.title);
        }
        SaveSystem.Save(playerData);
    }
}
