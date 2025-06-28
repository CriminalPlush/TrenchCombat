using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitMovement : MonoBehaviour
{
    [SerializeField]
    public bool inTrench = false;
    public int trenchIndex = 0;
    public List<string> commandQueue;
    private bool executeCommand = true;
    public NavMeshAgent agent;
    public Transform startPoint;
    public Transform endPoint;
    // [SerializeField]
    //   public GameObject link;
    public bool isMoving = true;
    public bool isRetreating = false;
    private UnitAttack UA = null;
    [SerializeField]
    private bool ignoreOffMeshLink = false;

    private Unit unit;


    // DebugOnly
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
            trenchIndex = FindObjectsOfType<Trench>().Length - 1;
        }
        agent.destination = endPoint.position;
        StartCoroutine(FreezeFix());
        StartCoroutine(SelectTrench());
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //Debug//
        isImoveinImovout = agent.isStopped;


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
        if (UA != null && UA.attackTarget != null && UA.IsTargetInRange())
        {
            Stay();
        }
        FaceTarget();
    }
    public void Move()
    {
        agent.isStopped = false;
        isMoving = true;
        isRetreating = false;
        agent.ResetPath();
        agent.SetDestination(endPoint.position);
        inTrench = false;
    }
    public IEnumerator SelectTrench()
    {
        yield return new WaitForSeconds(0.2f);
        if ((UA == null || (UA != null && UA.attackTarget == null)) && !inTrench)//Check if there's no targets and if unit is not in trench                                                           
        {

            if (TrenchFinder.FindTrenchByIndex(trenchIndex) != null // checks if trench exists
            && !(TrenchFinder.FindTrenchByIndex(trenchIndex).playerOnly && unit.isEnemy)
            && !(TrenchFinder.FindTrenchByIndex(trenchIndex).enemyOnly && !unit.isEnemy) // if unit not excluded from trench by it's rules
            && TrenchFinder.FindTrenchByIndex(trenchIndex).HasAnyFreeSlot() // checks if there're free slots (only when close)
            )
            {
                agent.SetDestination(TrenchFinder.FindNext(unit, trenchIndex) ?? Vector3.zero);
            }
            else if (TrenchFinder.FindNext(unit, trenchIndex + (unit.isEnemy ? -1 : 1)) != null) // if there's next trench        )
            {
                if (!unit.isEnemy)
                {
                    trenchIndex++;
                }
                else
                {
                    trenchIndex--;
                }
            }
            // }
        }
        else if ((UA == null || (UA != null && UA.attackTarget == null)) && inTrench)
        {

            agent.SetDestination(endPoint.position);
        }
        StartCoroutine(SelectTrench());
    }
    public void Retreat()
    {
        agent.isStopped = false;
        isRetreating = true;
        isMoving = false;
        agent.ResetPath();
        agent.destination = startPoint.position;
        inTrench = false;

    }
    public void Stay()
    {

        if (agent.hasPath)
            agent.isStopped = true;

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
    private IEnumerator FreezeFix()
    {
        yield return new WaitForSeconds(0.1f);
        if (!inTrench && unit.UA.attackTarget == null && agent.isPathStale)
        {
            if (isMoving)
                Move();
            else if (isRetreating)
                Retreat();
        }
        StartCoroutine(FreezeFix());
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
