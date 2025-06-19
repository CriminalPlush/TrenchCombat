using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayUnitData : MonoBehaviour
{
    [SerializeField] RawImage image;
    [SerializeField] TMP_Text description;
    [SerializeField] TMP_Text priceOfUpgrade;
    public void Display(UnitInfo unitInfo)
    {
        PlayerData playerData = SaveSystem.Load();

        image.texture = unitInfo.picture;
        switch (PlayerPrefs.GetString("language", "EN"))
        {
            case "EN":
                description.text = unitInfo.description_EN;
                break;
            case "RU":
                description.text = unitInfo.description;
                break;
            default:
                description.text = unitInfo.description_EN;
                break;
        }
        UnitData unitData = playerData.FindUnitByName(unitInfo.title);
        Debug.Log(unitData);
        Debug.Log(unitInfo.unitUpgradeTable);
        if (unitData.level < unitInfo.unitUpgradeTable.Length - 1)
        {
            priceOfUpgrade.text = unitInfo.unitUpgradeTable[unitData.level + 1].priceOfUpgrade.ToString(); // Compares level to unit upgrade table and shows the price of upgrading
        }
        else
        {
            priceOfUpgrade.text = "MAX";
        }
        FindObjectOfType<BuyUpgrade>().unitInfo = unitInfo;
        FindObjectOfType<UnitUpgradeBar>().UpdateInfo(unitInfo);
    }
}
