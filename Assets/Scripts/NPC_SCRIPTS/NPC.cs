using UnityEngine;


public enum npc_type
{
    farmer,fisherman,salesman,walker,guard,enemy,hunter
}
public enum npc_state
{
    idle,walking,working,hunting,fighting,running,resting,sleeping,eating
}
public class NPC : MonoBehaviour
{
    [SerializeField] protected npc_type type;
    [SerializeField] protected npc_state state;
    protected walking_component movement;
    protected decision_component brain;

    protected walker_decision_component walkerAI;
    /*
    protected salesman_decision_component salesmanAI;
    protected hunter_decision_component hunterAI;
    protected fisherman_decision_component fishermanAI;
    protected enemy_decision_component enemyAI;
    protected farmer_decision_component farmerAI;
    protected guard_decision_component guardAI;
     */

    //protected animation_component anim;
    protected Health healthComponent;

    public void setState(npc_state state)
    {
        this.state = state;
    }
    public npc_type getType()
    {
        return this.type;
    }
    protected virtual void Start()
    {
        state = npc_state.idle;
        healthComponent = GetComponent<Health>();
        movement = GetComponent<walking_component>();
        switch (type)
        {
            case npc_type.farmer:
                break;
            case npc_type.fisherman:
                break;
            case npc_type.walker:
                walkerAI = gameObject.AddComponent<walker_decision_component>();
                break;
            case npc_type.salesman:
                break;
            case npc_type.guard:
                break;
        }
    }
    protected void LateUpdate()
    {
        Vector3 poz = transform.position;
        poz.z = 0;
        transform.position = poz;
    }
}
