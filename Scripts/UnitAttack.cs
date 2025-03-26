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
   // [SerializeField]
    public ParticleSystem attackEffect;
    [SerializeField]
    private Unit unit;
    [SerializeField]
    private float damage;
    [SerializeField]
    private float baseDamage;
    [SerializeField]
    private float attackDistance;
    [SerializeField]
    private float baseAttackDistance;

    private UnitMovement UM;
    [SerializeField]
    private AudioSource[] attackSound;

    [SerializeField]
    private GameObject rangeCollider = null;
    private Vector3 rangeBaseScale;
    void Start()
    {
        attackDistance = baseAttackDistance;
        rangeBaseScale = rangeCollider.transform.localScale;
        if (unit == null)
        {
            unit = gameObject.GetComponent<Unit>();
        }
        UM = unit.UM;
    }
    void FixedUpdate()
    {
        if (rangeCollider != null)
        {
            if (UM != null && UM.inTrench)
            {
                rangeCollider.transform.localScale = rangeBaseScale * 1.35f;
                attackDistance = baseAttackDistance * 1.35f;

            }
            else
            {
                rangeCollider.transform.localScale = rangeBaseScale;
                attackDistance = baseAttackDistance;
            }
        }
        if (unit != null)
        {
            damage = baseDamage * (unit.isTrenchBoosted ? 1.2f : 1f) * (unit.isOfficerBoosted ? 1.5f : 1f);
        }
        else
        {
            damage = baseDamage;
        }
        if (attackTarget != null && IsTargetInRange())
        {
            StartCoroutine(Attack(damage));
        }
        else if (UM != null && attackTarget != null)
        {
            UM.agent.SetDestination(attackTarget.transform.position);
        }
        else if (UM != null)
        {
            if (UM.isMoving && UM.agent.isStopped && !UM.inTrench)
            {
                UM.Move();
            }
            else if (UM.isRetreating && UM.agent.isStopped && !UM.inTrench)
            {
                UM.Retreat();
            }
        }
        if (attackTarget != null)
        {
            Vector3 attackTargetPosition = new Vector3(attackTarget.transform.position.x, transform.position.y, attackTarget.transform.position.z);
            Quaternion lookRotation = Quaternion.LookRotation(attackTargetPosition - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
        }
        if (!isAttacking)
        {
            if (attackSound.Length != 0)
            {
                foreach (AudioSource sound in attackSound)
                {
                    if (sound.loop)
                    {
                        sound.Pause();
                    }
                }
            }
        }
    }
    void OnTriggerStay(Collider col)
    {
        Debug.Log(gameObject.name + " === " + col.name);
        Unit other = col.GetComponent<Unit>();
        if (col.GetComponent<Unit>() != null && col.GetComponent<Unit>().isDying == false)
        {
            other = col.GetComponent<Unit>();
            if ((other.isEnemy && !unit.isEnemy) || (!other.isEnemy && unit.isEnemy))
            {
                if (attackTarget == null)
                {
                    attackTarget = col.gameObject;
                }
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
        if (!isAttacking && attackTarget != null && enabled)
        {
            isAttacking = true;
            if (attackEffect != null)
            {
                attackEffect.Play();
            }
            if (attackSound.Length != 0)
            {
                int x = Random.Range(0, attackSound.Length);
                if (!(attackSound[x].loop && attackSound[x].isPlaying))
                {
                    attackSound[x].Play();
                }

            }
            attackTarget.GetComponent<Unit>().HP -= damage - (damage * attackTarget.GetComponent<Unit>().defence);
            if (attackTarget != null && IsTargetInRange() && attackTarget.GetComponent<Unit>().HP > 0)
            {
                if (UM != null)
                {
                    UM.Stay();
                }
                yield return new WaitForSeconds(attackCooldown);
                isAttacking = false;
                StartCoroutine(Attack(damage));
            }
            else
            {
                if (UM != null)
                {
                    if (UM.isMoving && attackTarget == null)
                    {
                        UM.Move();
                    }
                    else if (UM.isRetreating && attackTarget == null)
                    {
                        UM.Retreat();
                    }
                    if(attackTarget.GetComponent<Unit>().HP <= 0)
                    {
                        attackTarget = null;
                    }
                    yield return new WaitForSeconds(0);
                    isAttacking = false;
                }
            }
        }
    }
    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, baseAttackDistance);
    }
    public bool IsTargetInRange()
    {
        if (attackTarget != null)
        {
            return Vector3.Distance(transform.position, attackTarget.transform.position) < attackDistance;
        }
        else
        {
            return false;
        }
    }

}
