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
        Unit unitComponent;
        if ((col.tag == "Unit" || col.tag == "Enemy") && unit == null)
        {
            unitComponent = col.GetComponent<Unit>();
            unit = col.gameObject;
            if (unit == col.gameObject && (!(enemyOnly && !unitComponent.isEnemy)) && (!(playerOnly && unitComponent.isEnemy)))
            {
                Debug.Log("Checkit checkit");
                unitComponent.UM.inTrench = true;
                //agent.ResetPath();
                unit.GetComponent<NavMeshAgent>().isStopped = true;
                if (transform.parent.GetComponent<Trench>().lockedIn == false && !unitComponent.isEnemy)
                {
                    if (unitComponent.UM.isMoving)
                    {
                        unitComponent.UM.commandQueue.Add("Move");
                    }
                    else if (unitComponent.UM.isRetreating)
                    {
                        unitComponent.UM.commandQueue.Add("Retreat");
                    }
                }
                else if (transform.parent.GetComponent<Trench>().lockedIn == true)
                {
                    unitComponent.UM.commandQueue = new List<string>();
                }
            }
            else
            {
                unitComponent.UM.inTrench = false;
            }
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
