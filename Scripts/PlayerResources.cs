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

    void Start()
    {
        gold = defaultSettings.gold;
        cooldown = defaultSettings.cooldown;
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
