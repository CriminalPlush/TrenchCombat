using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public int money;
    public UnitInfo[] unitsList;
    public UnitInfo[] unitsSelected = new UnitInfo[6];
    public List<string> levelsPassed;
} 
