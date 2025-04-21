using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class DefaultSettings : ScriptableObject
{
    [Header("Resources")]
    public int gold;
    public float cooldown;
    public int goldPerTick;
}
