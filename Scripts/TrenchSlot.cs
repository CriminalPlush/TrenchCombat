using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TrenchSlot : MonoBehaviour
{
    public GameObject unit;
    [SerializeField]
    public GameObject obstacle = null;
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
            if (obstacle != null)
            {
                //obstacle.SetActive(true);
            }
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject == unit)
        {
            unit = null;
            if (obstacle != null)
            {
                obstacle.SetActive(false);
            }

        }
    }

}
