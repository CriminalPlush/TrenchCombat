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
    public void UpdateLevel(string title, int level)
    {
        foreach (UnitData unit in unitsList)
        {
            if (unit.title == title)
            {
                unit.level = level;
                break;
            }
        }
    }
    public void UpdateLevel(string title)
    {
        foreach (UnitData unit in unitsList)
        {
            if (unit.title == title)
            {
                unit.level++;
                break;
            }
        }
    }
}
