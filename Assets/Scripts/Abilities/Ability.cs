using UnityEngine;
using System.Collections;
public enum AbilityType
{
    speedIncrease,//
    bodyMovement,//
    block,//
    attack,//
    chargedAttack//
}

public enum EffectType
{
    self,  //applies only to the character where it has been casted from
    other, //applies to other character and is being called from the current character's script
    splash //applies to multiple other characters/buildings and other damageable objects
}

public enum AreaOfEffect
{
    none,
    ground,
    air,
    groundAndAir
}

public abstract class Ability : MonoBehaviour
{
    protected bool isPlayer;
    protected InputSystem_Actions controls;
    protected Rigidbody2D rb;
    protected movement move;
    [SerializeField] protected float damage;
    [SerializeField] protected float cooldown;
    [SerializeField] protected AbilityType type;
    [SerializeField] protected EffectType effect;
    [SerializeField] protected AreaOfEffect area;
    [SerializeField] protected float duration;
    protected StageManager center;

    protected virtual void Awake()
    {
        center = GetComponent<StageManager>();
        isPlayer = center.amIthePlayer();
    }

    protected virtual void SetUpStats(){}
    protected bool CanUseAbility()
    {
        if (!center.canICastAbilities())
            return false;
        switch (type)
            {
                case AbilityType.speedIncrease:
                    return center.canIMove();
                case AbilityType.bodyMovement:
                    return center.canIMove();
                case AbilityType.block:
                    break;
                case AbilityType.attack:
                    return center.canIAttack();
                case AbilityType.chargedAttack:
                    break;
            }
        return false;
    }

    protected virtual void BeforeAction(){}

    protected virtual void PerformAction(){}
    protected virtual IEnumerator cooldownTimer()
    {
        yield return new WaitForSeconds(cooldown);
    } 
    protected virtual IEnumerator durationTimer()
    {
        yield return new WaitForSeconds(duration);
    }

    protected virtual void ResetStats(){}

    protected virtual void TryUseAbility()
    {
        if (CanUseAbility())
        {
            StartCoroutine(MainBody());
        }
    }
    
    protected virtual IEnumerator MainBody()
    {
        SetUpStats();
        BeforeAction();
        PerformAction();

        yield return StartCoroutine(durationTimer());

        ResetStats();

        yield return StartCoroutine(cooldownTimer());
    }
}
