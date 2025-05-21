using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitUpgradeBar : MonoBehaviour
{
    [SerializeField] private Slider HPSlider, potentialHPSlider, damageSlider, potentialDamageSlider;
    public void UpdateInfo(UnitInfo unitInfo)
    {
        UnitData unitData = SaveSystem.Load().FindUnitByName(unitInfo.title);
        UnitUpgradeInfo[] unitUpgradeTable = unitInfo.unitUpgradeTable;
        int HP = unitUpgradeTable[unitData.level].HP;
        float damage = unitUpgradeTable[unitData.level].damage;
        float cooldown = unitUpgradeTable[unitData.level].postAttackCooldown + unitUpgradeTable[unitData.level].preAttackCooldown;
        HPSlider.value = HP;
        damageSlider.value = damage / cooldown;

        if (unitData.level + 1 < unitInfo.unitUpgradeTable.Length)
        {
            int potentialHP = unitUpgradeTable[unitData.level + 1].HP;
            float potentialdamage = unitUpgradeTable[unitData.level + 1].damage;
            float potentialcooldown = unitUpgradeTable[unitData.level + 1].postAttackCooldown + unitUpgradeTable[unitData.level + 1].preAttackCooldown;
            potentialHPSlider.value = potentialHP;
            potentialDamageSlider.value = potentialdamage / potentialcooldown;
        }
        else
        {
            potentialHPSlider.value = 0;
            potentialDamageSlider.value = 0;
        }
    }
}
