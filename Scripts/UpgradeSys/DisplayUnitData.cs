using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class DisplayUnitData : MonoBehaviour
{
    [SerializeField] RawImage image;
    [SerializeField] TMP_Text description;
    [SerializeField] TMP_Text priceOfUpgrade;
    public void Display(UnitInfo unitInfo)
    {
        PlayerData playerData = SaveSystem.Load();

        image.texture = unitInfo.picture;
        if (YG2.isSDKEnabled)
        {
            switch (YG2.lang)
            {
                case "en":
                    description.text = unitInfo.description_EN;
                    break;
                case "ru":
                    description.text = unitInfo.description;
                    break;
                default:
                    description.text = unitInfo.description_EN;
                    break;
            }
        }
        else
        {
            description.text = unitInfo.description_EN;
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
