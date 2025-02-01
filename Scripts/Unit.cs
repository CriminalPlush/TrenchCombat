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
    public float baseDefence { get; private set; }
    public bool isTrenchBoosted = false;
    public bool isOfficerBoosted = false;
    /* private bool isMoving = true;
     private bool isRetreating = false;*/

    void Start()
    {
        defence = baseDefence;
    }

    void FixedUpdate()
    {
        if (gameObject.GetComponent<UnitMovement>() == true)
        {
            UnitMovement UM = gameObject.GetComponent<UnitMovement>();
            if (UM.inTrench)
            {
                isTrenchBoosted = true;
                //defence = baseDefence + 0.1f;
            }
            else
            {
                isTrenchBoosted = false;
                //defence = baseDefence;
            }
        }
        defence = baseDefence + (isTrenchBoosted ? 0.1f : 0) + (isOfficerBoosted ? 0.1f : 0);
        if (HP <= 0)
        {
            if (isEnemy && GetComponent<UnitInfo>() == true)
            {
                FindObjectOfType<PlayerResources>().gold += GetComponent<UnitInfo>().price / 2;
            }
            if (GetComponent<UnitMovement>() == true && GetComponent<UnitMovement>().inTrench)
            {
                GetComponent<UnitMovement>().link.GetComponents<NavMeshLink>()[0].enabled = true;
                GetComponent<UnitMovement>().link.GetComponents<NavMeshLink>()[1].enabled = true;
                GetComponent<UnitMovement>().link.GetComponent<TrenchSlot>().unit = null;
            }
            if(GetComponent<UnitBoost>() == true)
            {
                foreach(Unit x in GetComponent<UnitBoost>().unitsBoosted)
                {
                    x.isOfficerBoosted = false;
                }
            }
            Destroy(gameObject);
        }
    }
    public IEnumerator OfficerBoost(float time, UnitBoost UB)
    {
        if (!isOfficerBoosted)
        {
            isOfficerBoosted = true;
            yield return new WaitForSeconds(time);
            if (UB == null || !UB.unitsBoosted.Contains(this))
            {
                isOfficerBoosted = false;
            }
        }
    }
}
//Shit + Alt + F to beautify code