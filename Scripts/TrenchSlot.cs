using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrenchSlot : MonoBehaviour
{
    public GameObject unit;
    void Start()
    {
        unit = null;
    }

    // Update is called once per frame
    void Update()
    {
    }
    void OnTriggerEnter(Collider col)
    {
        if((col.tag == "Unit" || col.tag == "Enemy") && unit == null)
        {
            unit = col.gameObject;
        }
    }
    void OnTriggerExit(Collider col)
    {
        if(col.gameObject == unit)
        {
            unit = null;
        }
    }

}
