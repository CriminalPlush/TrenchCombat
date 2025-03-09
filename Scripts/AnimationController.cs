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
    private UnitAttack UA;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       // string clipName = animator.GetCurrentAnimatorStateInfo(0).IsName("Fire");
        if (UA.attackTarget != null && UA.IsTargetInRange())
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Fire"))
            { 
                animator.Play("Fire");
            }
        }
        else if (agent.isStopped)
        {
            if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Idle")){
            animator.Play("Idle");
            }
        }
        else if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
        {
            animator.Play("Walk");
        }
    }
}
