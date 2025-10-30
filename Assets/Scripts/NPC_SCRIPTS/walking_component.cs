using UnityEngine;
using UnityEngine.AI;
public class walking_component : MonoBehaviour
{
    protected GameObject[] walkingAreas;
    protected int currentArea;

    [SerializeField] protected float speed;
    protected NavMeshAgent agent;
    [SerializeField] protected float stopDistance;
    protected GameObject lastWaypoint;
    public bool choose_randomly;
    private NPC npcScript;

    protected void PickRandomDestination()
    {
        Vector3 random = Random.insideUnitSphere * 100f;

        random += transform.position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(random, out hit, 100f, NavMesh.AllAreas))
        {
            Vector3 dest = hit.position;
            dest.z = 0;
            agent.SetDestination(dest);
        }
    }
    public void FindPathingAreasWithTag(string tag)
    {
        if (string.IsNullOrEmpty(tag))
        {
            Debug.LogError("Empty or null tag requested");
            return;
        }
        walkingAreas = GameObject.FindGameObjectsWithTag(tag);
    }
    public void PickDestinationFromArea(int index)
    {
        if(index<=0 || index >= walkingAreas.Length)
        {
            Debug.LogError("Invalid indexed destination requested");
            return;
        }
    }
    void Awake()
    {
        choose_randomly = false;
        agent = GetComponent<NavMeshAgent>();
        npcScript = GetComponent<NPC>();
        if (npcScript != null) choose_randomly = (npcScript.getType() == npc_type.walker); 
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = speed;
        agent.stoppingDistance = stopDistance;
        //Debug.Log(walkingAreas.Length);
        PickRandomDestination();
    }

    void Update()
    {
        if(choose_randomly)
        {
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                PickRandomDestination();
            }
            if (agent.velocity.magnitude < 0.1f)
            {
                PickRandomDestination();
            }
        }
    }
}
