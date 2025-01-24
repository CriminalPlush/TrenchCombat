using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;
using System;

public class UnitMovement : MonoBehaviour
{
    [SerializeField]
    public bool inTrench = false;
    public List<string> commandQueue;
    private bool executeCommand = true;
    [SerializeField]
    private NavMeshAgent agent;
    public Transform startPoint;
    public Transform endPoint;
    [SerializeField]
    public GameObject link;
    public bool isMoving = true;
    public bool isRetreating = false;

    private Unit unit;
    private GameObject rangeCollider = null;
    private Vector3 rangeBaseScale;


    // DebugOnly
    [SerializeField]
    private bool isOnOffShit;
    [SerializeField]
    private bool isImoveinImovout;
    [SerializeField]
    private int numbaboy;

    void Start()
    {
        unit = gameObject.GetComponent<Unit>();
        startPoint = GameObject.FindGameObjectWithTag("StartPoint").transform;
        endPoint = GameObject.FindGameObjectWithTag("EndPoint").transform;
        agent = GetComponent<NavMeshAgent>();
        foreach (Transform x in transform)
        {
            if (x.tag == "Range")
            {
                rangeCollider = x.gameObject;
                rangeBaseScale = rangeCollider.transform.localScale;
            }
        }
        if (unit.isEnemy)
        {
            Transform x = endPoint;
            endPoint = startPoint;
            startPoint = x;
        }
        agent.destination = endPoint.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isOnOffShit = agent.isOnOffMeshLink;
        isImoveinImovout = agent.isStopped;
        if (rangeCollider != null)
        {
            if (inTrench)
            {
                rangeCollider.transform.localScale = rangeBaseScale * 1.2f;
            }
            else
            {
                rangeCollider.transform.localScale = rangeBaseScale;
            }
        }
        if (agent.isOnOffMeshLink && !inTrench)
        {
            if (commandQueue.Count > 0)
            {
                commandQueue = new List<string> { commandQueue[0] };
            }
            EnterOffMeshLink();
        }
        if (executeCommand
        && commandQueue.Count > 0
        && !(agent.isOnOffMeshLink && !inTrench)
        && !(GetComponent<UnitAttack>() != null && GetComponent<UnitAttack>().attackTarget != null)
        )//&& !isAttacking)
        {
            switch (commandQueue[0])
            {
                case "Move":
                    Move();
                    break;
                case "Retreat":
                    Retreat();
                    break;
                case "Stay":
                    Stay();
                    break;
            }
            commandQueue.RemoveAt(0);
        }
        FaceTarget();
    }
    public void Move()
    {
        agent.isStopped = false;
        isMoving = true;
        isRetreating = false;
        agent.ResetPath();
        agent.destination = endPoint.position;
        if (link != null)
        {
            link.GetComponents<NavMeshLink>()[0].enabled = true;
            link.GetComponents<NavMeshLink>()[1].enabled = true;
            link = null;
        }
        inTrench = false;
        //agent.ResetPath();
    }
    public void Retreat()
    {
        agent.isStopped = false;
        isRetreating = true;
        isMoving = false;
        agent.ResetPath();
        agent.destination = startPoint.position;
        if (link != null)
        {
            link.GetComponents<NavMeshLink>()[0].enabled = true;
            link.GetComponents<NavMeshLink>()[1].enabled = true;
            link = null;
        }
        inTrench = false;
        //agent.ResetPath();
    }
    public void Stay()
    {
        if(agent.hasPath)
        agent.isStopped = true;
    }

    void FaceTarget()
    {
        if (GetComponent<UnitAttack>() == null || GetComponent<UnitAttack>().attackTarget == null)
        {
            if (agent.velocity.magnitude > 0 && agent.isStopped == false)
            {
                Vector3 direction = agent.velocity;
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, direction.y, direction.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
                numbaboy = 1;
            }
            else if (agent.velocity.magnitude == 0 || inTrench)
            {
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(agent.destination.x, transform.position.y, agent.destination.z) - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
                numbaboy = 2;
            }
        }
        else if (GetComponent<UnitAttack>().attackTarget != null)
        {
            Vector3 attackTargetPosition = GetComponent<UnitAttack>().attackTarget.transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(attackTargetPosition - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
            numbaboy = 4;
        }
    }
    void EnterOffMeshLink()
    {
        OffMeshLinkData data = agent.currentOffMeshLinkData;

        //calculate the final point of the link
        Vector3 endPos = data.endPos + Vector3.up * agent.baseOffset;

        //Move the agent to the end point
        agent.transform.position = Vector3.MoveTowards(agent.transform.position, endPos, agent.speed * Time.deltaTime);

        //when the agent reach the end point you should tell it, and the agent will "exit" the link and work normally after that
        if (agent.transform.position == endPos)
        {
            agent.CompleteOffMeshLink();
        }
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Trench")
        {
            if (col.transform.parent.GetComponent<TrenchSlot>().unit == gameObject)
            {
                inTrench = true;
                //agent.ResetPath();
                gameObject.GetComponent<NavMeshAgent>().isStopped = true;
                link = col.transform.parent.gameObject;
                link.GetComponents<NavMeshLink>()[0].enabled = false;
                link.GetComponents<NavMeshLink>()[1].enabled = false;
                if (col.transform.parent.parent.GetComponent<Trench>().lockedIn == false && !unit.isEnemy)
                {
                    if (isMoving)
                    {
                        commandQueue.Add("Move");
                    }
                    else if (isRetreating)
                    {
                        commandQueue.Add("Retreat");
                    }
                }
                else if (col.transform.parent.parent.GetComponent<Trench>().lockedIn == true)
                {
                    commandQueue = new List<string>();
                }
            }
            else
            {
                inTrench = false;
            }
        }
    }
    void OnTriggerStay(Collider col)
    {
        if (col.tag == "StopIfNoPath" /*&& !inTrench*/)
        {
            if (col.transform.parent.GetComponent<Trench>().HasAnyFreeSlot() == false)
            {
                Stay();
            }
            else
            {
                if (isMoving)
                {
                    commandQueue.Add("Move");
                }
                else if (isRetreating)
                {
                    commandQueue.Add("Retreat");
                }
            }
        }
        if ((col.tag == "Enemy" && !unit.isEnemy) || (col.tag == "Unit" && unit.isEnemy))
        {
            Stay();
        }
    }
}
