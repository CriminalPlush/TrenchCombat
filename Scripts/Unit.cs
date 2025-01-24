using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Linq;
using UnityEngine.UIElements;
using UnityEngine.AI;
using Unity.AI.Navigation;

public class Unit : MonoBehaviour
{
    public bool isEnemy = false;

    public float HP = 20f;
    public float defence;
    [SerializeField]
    public float baseDefence {get; private set;}
   /* private bool isMoving = true;
    private bool isRetreating = false;*/ 

    void Start()
    {
        defence = baseDefence;
    }

    void FixedUpdate()
    {
        if(gameObject.GetComponent<UnitMovement>() == true)
        {
            UnitMovement UM = gameObject.GetComponent<UnitMovement>();
            if(UM.inTrench)
            {
                defence = baseDefence + 0.1f;
            }
            else
            {
                defence = baseDefence;
            }
        }
        if (HP <= 0)
        {
            if(isEnemy && GetComponent<UnitInfo>() == true)
            {
                FindObjectOfType<Resources>().gold += GetComponent<UnitInfo>().price / 2;
            }
            if(GetComponent<UnitMovement>() == true && GetComponent<UnitMovement>().inTrench)
            {
                GetComponent<UnitMovement>().link.GetComponents<NavMeshLink>()[0].enabled = true;
                GetComponent<UnitMovement>().link.GetComponents<NavMeshLink>()[1].enabled = true;
                GetComponent<UnitMovement>().link.GetComponent<TrenchSlot>().unit = null;
            }
            Destroy(gameObject);
        }
    }
}
//Shit + Alt + F to beautify code