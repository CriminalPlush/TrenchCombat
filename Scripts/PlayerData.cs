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
                unitToReturn = unit;
                break;
            }
        }
        if (unitToReturn == null)
        {
            unitToReturn = new UnitData(title, 0);
            unitsList.Add(unitToReturn);
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
    public PlayerData()
    {
        money = 0;
        unitsList = new List<UnitData> { new UnitData("Soldier", 1), new UnitData("Base", 1) };
        levelsPassed = new List<string>();
    }
}
