using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public int money;
    public List<UnitData> unitsList;
    public List<UnitData> unitsSelected;
    public List<string> levelsPassed;
    public UnitData FindUnitByName(string title)
    {
        UnitData unitToReturn = null;
        foreach (UnitData unit in unitsList)
        {
            if (unit.title == title)
            {
                break;
            }
        }
        return unitToReturn;
    }
} 
