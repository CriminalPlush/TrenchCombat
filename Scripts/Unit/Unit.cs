using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.AI;
using Unity.AI.Navigation;

public class Unit : MonoBehaviour //Core script for unit related scripts
{
    public bool isEnemy = false;
    public float HP;
    [HideInInspector] public float defence;
    [SerializeField] private int level = 1; //Works only for AI
    public float baseDefence;//{ get; private set; }
    public bool isTrenchBoosted = false;
    public bool isOfficerBoosted = false;
    public UnitMovement UM;
    public UnitAttack UA;
    public UnitBoost UB;
    public UnitInfo unitInfo;
    public bool isDying = false;
    public float explosionResist = 0f;
    [SerializeField] GameObject explosion;
    [SerializeField] GameObject fire;

    private UnitData unitData;

    void Start()
    {
        defence = baseDefence;
        UnitDataApply();
    }

    void FixedUpdate()
    {
        if (gameObject.GetComponent<UnitMovement>() == true)
        {
            UM = gameObject.GetComponent<UnitMovement>();
            if (UM.inTrench)
            {
                isTrenchBoosted = true;
            }
            else
            {
                isTrenchBoosted = false;
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
            if (isEnemy && unitInfo != null)
            {
                FindObjectOfType<PlayerResources>().gold += unitInfo.price / 2;
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
                UM.agent.isStopped = true;
                Destroy(UM);
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

    private void UnitDataApply()
    { 
        if (!isEnemy)
        {
            level = SaveSystem.Load().FindUnitByName(unitInfo.title).level;  
            HP = unitInfo.unitUpgradeTable[level].HP;
            if (UA != null)
            {
                UA.UnitDataApply(unitInfo.unitUpgradeTable, level);
            }
        }
    }
}
//Shit + Alt + F to beautify code