using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TrenchSlot : MonoBehaviour
{
    public GameObject unit;
    //public GameObject obstacle = null;
    public bool playerOnly = false;
    public bool enemyOnly = false;
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
        if ((col.tag == "Unit" || col.tag == "Enemy") && unit == null)
        {
            unit = col.gameObject;
          //  if (obstacle != null)
           // {
                //obstacle.SetActive(true);
          //  }
            //UpdatePaths();
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject == unit)
        {
            unit = null;
           /* if (obstacle != null)
            {
                obstacle.SetActive(false);
            }*/

        }
       // UpdatePaths();
    }
   /* void UpdatePaths()
    {
        NavMeshAgent[] agents = FindObjectsOfType<NavMeshAgent>();
        foreach (NavMeshAgent agent in agents)
        {
            agent.SetDestination(agent.destination);
        }

    }*/

}
