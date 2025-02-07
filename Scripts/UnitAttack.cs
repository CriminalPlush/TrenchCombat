using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class UnitAttack : MonoBehaviour
{
    [SerializeField]
    private float attackCooldown;
    public GameObject attackTarget;
    [SerializeField]
    private bool isAttacking = false;
    [SerializeField]
    private ParticleSystem attackEffect;
    private Unit unit;
    [SerializeField]
    private float damage;
    [SerializeField]
    private float baseDamage;
    [SerializeField]
    private float attackDistance;
    // Start is called before the first frame update
    void Start()
    {
        unit = gameObject.GetComponent<Unit>();
    }
    void FixedUpdate()
    {
        damage = baseDamage * (unit.isTrenchBoosted ? 1.2f : 1f) * (unit.isOfficerBoosted ? 1.5f : 1f);
        if (attackTarget != null && IsTargetInRange())
        {
            StartCoroutine(Attack(damage));
            /*if (GetComponent<UnitMovement>() != null)
            {
                if (GetComponent<UnitMovement>().isMoving)
                {
                    GetComponent<UnitMovement>().Move();
                }
                else if (GetComponent<UnitMovement>().isRetreating)
                {
                    GetComponent<UnitMovement>().Retreat();
                }
            }*/
        }
        else if (GetComponent<UnitMovement>() != null && attackTarget != null)
        {
            GetComponent<UnitMovement>().agent.SetDestination(attackTarget.transform.position);
        }
    }
    void OnTriggerStay(Collider col)
    {
        if ((col.tag == "Enemy" && !unit.isEnemy) || (col.tag == "Unit" && unit.isEnemy))
        {
            if (attackTarget == null)
            {
                attackTarget = col.gameObject;
            }
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject == attackTarget)
        {
            attackTarget = null;
        }
    }
    private IEnumerator Attack(float damage)
    {
        if (!isAttacking && attackTarget != null)
        {
            isAttacking = true;
            attackEffect.Play();
            attackTarget.GetComponent<Unit>().HP -= damage - (damage * attackTarget.GetComponent<Unit>().defence);
            yield return new WaitForSeconds(attackCooldown);
            isAttacking = false;
            if (attackTarget != null && IsTargetInRange())
            {
                StartCoroutine(Attack(damage));
            }
            else
            {
                isAttacking = false;
                if (gameObject.GetComponent<UnitMovement>() == true)
                {
                    UnitMovement UM = gameObject.GetComponent<UnitMovement>();
                    if (UM.isMoving && !UM.inTrench)
                    {
                        UM.Move();
                    }
                    else if (UM.isRetreating)
                    {
                        UM.Retreat();
                    }
                }
            }
        }
    }
    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, attackDistance);
    }
    public bool IsTargetInRange()
    {
        if (attackTarget != null)
        {
            Debug.Log(Vector3.Distance(transform.position, attackTarget.transform.position));
            return Vector3.Distance(transform.position, attackTarget.transform.position) < attackDistance;
        }
        else
        {
            return false;
        }
    }

}
