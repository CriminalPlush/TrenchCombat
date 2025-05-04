using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResources : MonoBehaviour
{
    [SerializeField]
    private DefaultSettings defaultSettings;
    public int gold;
    private float cooldown;
    private int goldPerTick;
    [SerializeField] private float goldModifier = 1f; 
    [SerializeField] private float goldProductionModifier = 1f; 

    void Start()
    {
        gold = Convert.ToInt32(defaultSettings.gold * goldModifier);
        cooldown = defaultSettings.cooldown / goldProductionModifier;
        goldPerTick = defaultSettings.goldPerTick;
        StartCoroutine(Tick());
    }
    private IEnumerator Tick()
    {
        yield return new WaitForSeconds(cooldown);
        gold += goldPerTick;
        StartCoroutine(Tick());
    }
}
