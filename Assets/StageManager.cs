using UnityEngine;
using System.Collections.Generic;
/*
Script that coordinates the gameObject's behavior in diffrent stage(attacking,talking,being healed)
and manages other important components (movement,health,animations) such that they match the 
character's current stage 
*/
public enum directions
{
    UP,DOWN,LEFT,RIGHT
}
public class StageManager : MonoBehaviour
{
    [SerializeField] private bool isPlayer;
    [Header("Fighting variables")]
    [SerializeField] private bool Attacking;
    [SerializeField] private bool underAttack;
    [SerializeField] private bool canAttack;
    [SerializeField] private bool blocking;

    [Header("Character interaction variables")]
    [SerializeField] private bool talking;
    [SerializeField] private bool walking;
    [SerializeField] private directions direction;


    [Header("Crowd control variables")]
    //[SerializeField] private Queue<CC_Ability> CC_Queue;
    [SerializeField] private bool stunned;
    [SerializeField] private bool slowed;
    [SerializeField] private bool healed;
    [SerializeField] private bool canCastAbility;
    [SerializeField] private float cc_duration;

    [Header("Important component reffrences")]
    private Health healthComponent;
    private movement movementComponent;
    // private AnimationSwitcher animationComponent;
    private InputSystem_Actions controls;
    private AttackManager attackManager;

    void Awake()
    {
        healthComponent = GetComponent<Health>();
        if (isPlayer)
            movementComponent = GetComponent<movement>();
        attackManager = GetComponent<AttackManager>();
        if (attackManager)
        {
            setAttackingStage(false);
            setAttackPosibility(false);
        }
        setStunningStage(false);
        setTalkingStage(false);
        setAbilityCastingPosibility(true);
        //movementComponent = GetComponent<movement>();
        //animationComponent = GetComponent<AnimationSwitcher()>;
    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {

    }

    public bool amIthePlayer()
    {
        return isPlayer;
    }

    public bool amIattacking()
    {
        return Attacking;
    }

    public bool amIUnderAttack()
    {
        return underAttack;
    }
    public bool canIMove()
    {
        return !stunned;
    }
    public bool canIAttack()
    {
        return canAttack && canIMove();
    }
    void Update()
    {

    }
    public bool canICastAbilities()
    {
        return canCastAbility;
    }
    public void setAbilityCastingPosibility(bool newAbilityCastingPosibility)
    {
        canCastAbility = newAbilityCastingPosibility;
    }
    public void setMovingStage(bool newWalkingStage)
    {
        walking = newWalkingStage;
    }
    public void setTalkingStage(bool newTalkingStage)
    {
        talking = newTalkingStage;
    }
    public void setAttackingStage(bool newAttackingStage)
    {
        Attacking = newAttackingStage;
    }
    public void setAttackPosibility(bool newAttackPosibility)
    {
        canAttack = newAttackPosibility;
    }
    public void setStunningStage(bool newStunningStage)
    {
        stunned = newStunningStage;
        if (stunned)
        {
            if (isPlayer) movementComponent.DenyMovement();
            if (attackManager)
            {
                attackManager.DenyAttacking();
                setAttackPosibility(false);
            }
            setMovingStage(false);
            setAttackingStage(false);
            return;
        }
        if (isPlayer)
            movementComponent.AllowMovement();
        if (attackManager)
        {
            //attackManager.AllowAttacking();
            setAttackPosibility(true);
        }
    }
    public void setMovementPosibility(bool newMovementPosibility)
    {
        stunned = !newMovementPosibility;
    }
    void FixedUpdate()
    {

    }
}
