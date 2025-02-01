using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBoost : MonoBehaviour
{
    public List<Unit> unitsBoosted = new List<Unit>();
    private Unit unit;
    void Start()
    {
        unit = GetComponent<Unit>();
    }
    void FixedUpdate()
    {
        foreach(var x in unitsBoosted)
        {
            StartCoroutine(x.OfficerBoost(0.1f,this));
        }
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider col)
    {
        if ((col.tag == "Enemy" && unit.isEnemy) || (col.tag == "Unit" && !unit.isEnemy))
        {
            unitsBoosted.Add(col.GetComponent<Unit>());
        }
    }
    void OnTriggerExit(Collider col)
    {
        if(col.gameObject.GetComponent<Unit>() != null)
        {
            if(unitsBoosted.Contains(col.gameObject.GetComponent<Unit>()))
            {
                unitsBoosted.Remove(col.gameObject.GetComponent<Unit>());
            }
        }
    }
}
