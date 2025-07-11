using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TrenchSlot : MonoBehaviour
{
    public GameObject unit;
    private bool playerOnly = false;
    private bool enemyOnly = false;
    public Trench trench;
    void Awake()
    {
        unit = null;
        trench = transform.parent.gameObject.GetComponent<Trench>();
        playerOnly = trench.playerOnly;
        enemyOnly = trench.enemyOnly;
    }

    void Update()
    {
    }
    void OnTriggerEnter(Collider col)
    {
        Unit unitComponent;
        if ((col.GetComponent<Unit>() != null) && unit == null && col.GetComponent<Unit>().UM.trenchIndex == trench.index)
        {
            unitComponent = col.GetComponent<Unit>();
            unit = col.gameObject;
            if (unit == col.gameObject && (!(enemyOnly && !unitComponent.isEnemy)) && (!(playerOnly && unitComponent.isEnemy)))
            {
                Debug.Log("Checkit checkit");
                unitComponent.UM.inTrench = true;
                unit.GetComponent<NavMeshAgent>().isStopped = true;
                if (transform.parent.GetComponent<Trench>().lockedIn == false && !unitComponent.isEnemy)
                {
                    if (unitComponent.UM.isMoving)
                    {
                        unitComponent.UM.trenchIndex++;
                        unitComponent.UM.Move();
                    }
                    else if (unitComponent.UM.isRetreating)
                    {
                        unitComponent.UM.trenchIndex--;
                        unitComponent.UM.Retreat();
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
