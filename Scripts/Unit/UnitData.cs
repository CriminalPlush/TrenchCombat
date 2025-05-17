using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UnitData

{
    public string title;
    public int level;

    public UnitData(string title, int level)
    {
        this.title = title;
        this.level = level;
    }
}
