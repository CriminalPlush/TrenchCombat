using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class UnitInfo : ScriptableObject
{
    public string title = "nothing";
    public string description = "";
    public int price = 0;
    public int spawnCooldown = 5;
    public Texture icon;
    public Texture picture;
    public UnitUpgradeInfo[] unitUpgradeTable;
    [Serializable]

    public class UnitUpgradeInfo
    {
        public int priceOfUpgrade;
        public float damage;
        public int HP;
        public UnitUpgradeInfo(int _priceOfUpgrade, float _damage, int _HP)
        {
            priceOfUpgrade = _priceOfUpgrade;
            damage = _damage;
            HP = _HP;
        }
    }
}
