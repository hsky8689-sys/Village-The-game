using UnityEngine;

public class block_ability : Ability
{
    [SerializeField] private float initialDamage;
    //[SerializeField] private Mob attacker;
    [SerializeField] protected directions defend_pos;
    protected AttackManager attackComponent; 
    protected Health healthComponent;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        healthComponent = GetComponent<Health>();
        attackComponent = GetComponent<AttackManager>();
        center = GetComponent<StageManager>();
    }
    protected override void SetUpStats()
    {

    }
    protected override void ResetStats()
    {

    }
    protected void decreaseHP()
    {
        
    }
}

