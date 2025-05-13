using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UnitInfo
{
    public string title = "nothing";
    public int price = 0;
    public int level = 0;
    public int spawnCooldown = 5;
    public Texture icon;
    public UnitUpgradeInfo[] unitUpgradeTable;
}
