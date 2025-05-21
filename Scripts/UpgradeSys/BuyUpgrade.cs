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
        if (unitInfo != null)
        {
            PlayerData playerData = SaveSystem.Load();
            UnitData unitData = playerData.FindUnitByName(unitInfo.title);
            if (unitData.level < unitInfo.unitUpgradeTable.Length - 1 && playerData.money >= unitInfo.unitUpgradeTable[unitData.level].priceOfUpgrade)
            {
                playerData.money -= unitInfo.unitUpgradeTable[unitData.level + 1].priceOfUpgrade;
                playerData.UpdateLevel(unitInfo.title);
            }
            SaveSystem.Save(playerData);
            FindObjectOfType<DisplayMoney>().UpdateInfo(playerData.money);
            FindObjectOfType<DisplayUnitData>().Display(unitInfo);
        }
    }
}
