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
    public NavMeshAgent agent;
    public Transform startPoint;
    public Transform endPoint;
    [SerializeField]
    public GameObject link;
    public bool isMoving = true;
    public bool isRetreating = false;
    private UnitAttack UA = null;
    [SerializeField]
    private bool ignoreOffMeshLink = false;

    private Unit unit;


    // DebugOnly
    [SerializeField]
    private bool isOnOffLink;
    [SerializeField]
    private bool isImoveinImovout;

    void Start()

    {
        unit = gameObject.GetComponent<Unit>();
        UA = unit.UA;
        startPoint = GameObject.FindGameObjectWithTag("StartPoint").transform;
        endPoint = GameObject.FindGameObjectWithTag("EndPoint").transform;
        agent = GetComponent<NavMeshAgent>();
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
        isOnOffLink = agent.isOnOffMeshLink;
        isImoveinImovout = agent.isStopped;
        if (agent.isOnOffMeshLink && !inTrench && !ignoreOffMeshLink)
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
        && !(UA != null && UA.attackTarget != null && UA.IsTargetInRange())
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
        if (UA != null && UA.attackTarget != null && UA.IsTargetInRange() && !isOnOffLink)
        {
            Stay();
        }

    }
    public void Move()
    {
        if (!isOnOffLink || inTrench)
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
        }
        //agent.ResetPath();
    }
    public void Retreat()
    {
        if (!isOnOffLink || inTrench)
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
        }
        //agent.ResetPath();
    }
    public void Stay()
    {
        if (!isOnOffLink || inTrench)
        {
            if (agent.hasPath)
                agent.isStopped = true;
        }
    }

    void FaceTarget()
    {
        if (UA == null || UA.attackTarget == null)
        {
            if (agent.velocity.magnitude == 0 || inTrench)
            {
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(agent.destination.x, transform.position.y, agent.destination.z) - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
            }
            else if (agent.velocity.magnitude > 0 && agent.isStopped == false)
            {
                Vector3 direction = agent.velocity;
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, direction.y, direction.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
            }
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
        if (col.tag == "StopIfNoPath")
        {
            if (col.transform.parent.GetComponent<Trench>().HasAnyFreeSlot() == false)
            {
                Stay();
            }
            else
            {
                if (isMoving && commandQueue.Count > 0 && commandQueue[commandQueue.Count - 1] != "Move")
                {
                    commandQueue.Add("Move");
                }
                else if (isRetreating && commandQueue.Count > 0 && commandQueue[commandQueue.Count - 1] != "Retreat")
                {
                    commandQueue.Add("Retreat");
                }
            }
        }
        /*if ((col.tag == "Enemy" && !unit.isEnemy) || (col.tag == "Unit" && unit.isEnemy))
        {
            if (gameObject.GetComponent<UnitAttack>() != null && gameObject.GetComponent<UnitAttack>().IsTargetInRange())
            {
                Debug.Log(gameObject.name + "|||" + "Beware the beast in black");
                Stay();
            }
        }*/
    }
    void OnDrawGizmosSelected()
    {

        var nav = GetComponent<NavMeshAgent>();
        if (nav == null || nav.path == null)
            return;

        var line = this.GetComponent<LineRenderer>();
        if (line == null)
        {
            line = this.gameObject.AddComponent<LineRenderer>();
            line.material = new Material(Shader.Find("Sprites/Default")) { color = Color.yellow };
            line.SetWidth(0.5f, 0.5f);
            line.SetColors(Color.yellow, Color.yellow);
        }

        var path = nav.path;

        line.SetVertexCount(path.corners.Length);

        for (int i = 0; i < path.corners.Length; i++)
        {
            line.SetPosition(i, path.corners[i]);
        }

    }
}
