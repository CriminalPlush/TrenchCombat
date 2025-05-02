using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Linq;
using UnityEngine.UI;
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
    public UnitMovement UM;
    public UnitAttack UA;
    public UnitBoost UB;
    public bool isDying = false;
    public bool explosionResist = false;
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
            UM = gameObject.GetComponent<UnitMovement>();
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
        defence = baseDefence + (isTrenchBoosted ? 0.1f : 0) + (isOfficerBoosted ? 0.2f : 0);
        if (HP <= 0)
        {
            StartCoroutine(Die());
        }
    }
    private IEnumerator Die()
    {
        if (!isDying)
        {
            isDying = true;
            if (isEnemy && GetComponent<UnitInfo>() == true)
            {
                FindObjectOfType<PlayerResources>().gold += GetComponent<UnitInfo>().price / 2;
            }
            if (UM != null && UM.inTrench)
            {
                if (UM.link.GetComponents<NavMeshLink>().Length == 2)
                {
                    UM.link.GetComponents<NavMeshLink>()[0].enabled = true;
                    UM.link.GetComponents<NavMeshLink>()[1].enabled = true;
                }
                UM.link.GetComponent<TrenchSlot>().unit = null;
            }
            if (GetComponent<UnitBoost>() == true)
            {
                foreach (Unit x in GetComponent<UnitBoost>().unitsBoosted)
                {
                    x.isOfficerBoosted = false;
                }
            }
            if (UM != null)
            {
                UM.enabled = false;
                UM.agent.isStopped = true;
            }
            if (UA != null) UA.enabled = false;
            if (UB != null) UB.enabled = false;
            if (UA != null && UA.attackEffect != null)
            {
                UA.attackEffect.Stop();
            }
            foreach (Transform x in transform)
            {
                if (x.GetComponent<AudioSource>() != null) x.GetComponent<AudioSource>().Stop();
                if (x.GetComponent<Canvas>() != null) x.gameObject.SetActive(false);
            }
            yield return new WaitForSeconds(5.4f); //Time befo dessapearing ♂
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