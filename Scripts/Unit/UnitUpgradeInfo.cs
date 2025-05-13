using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
