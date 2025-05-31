using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TrenchSlot : MonoBehaviour
{
    public GameObject unit;
    public bool playerOnly = false;
    public bool enemyOnly = false;
    void Start()
    {
        unit = null;
    }

    void Update()
    {
    }
    void OnTriggerEnter(Collider col)
    {
        if ((col.tag == "Unit" || col.tag == "Enemy") && unit == null)
        {
            unit = col.gameObject;
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject == unit)
        {
            unit = null;
        }
    }

}
