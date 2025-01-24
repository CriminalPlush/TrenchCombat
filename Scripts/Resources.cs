using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resources : MonoBehaviour
{
    public int gold = 0;
    [SerializeField]
    private float cooldown = 0.5f;
    [SerializeField]
    private int goldPerTick = 5;

    void Start()
    {
        StartCoroutine(Tick());
    }
    private IEnumerator Tick()
    {
        yield return new WaitForSeconds(cooldown);
        gold += goldPerTick;
        StartCoroutine(Tick());
    }
}
