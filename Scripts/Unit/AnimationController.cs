using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimationController : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private NavMeshAgent agent;
    [SerializeField]
    private Unit unit;
    private UnitAttack UA;
    private bool unitIsDyeing = false;
    void Start()
    {
        UA = unit.UA;
    }

    // Update is called once per frame
    void Update()
    {
        if (!unitIsDyeing)
        {
            if (unit.HP <= 0)
            {
                unitIsDyeing = true;
                animator.Play("Death");
            }
            else if (UA.attackTarget != null && UA.IsTargetInRange())
            {
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Fire"))
                {
                    animator.Play("Fire");
                }
            }
            else if (agent.isStopped)
            {
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                {
                    animator.Play("Idle");
                }
            }
            else if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
            {
                animator.Play("Walk");
            }
        }
    }
}
