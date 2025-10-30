using UnityEngine;
using System.Collections.Generic;

public class AttackManager : MonoBehaviour
{
    private bool isPlayer;
    [SerializeField] private float damage;
    [SerializeField] private float cooldown;
    [SerializeField] private float range;
    [SerializeField] private InputSystem_Actions control;
    private Rigidbody2D rb;
    private bool attackEnabled;
    //[SerializeField] private Weapon weapon;
    
    [SerializeField] private bool weapon = false;
    private StageManager center;
    
    void Awake()
    {
        center = GetComponent<StageManager>();
        isPlayer = center.amIthePlayer();
        rb = GetComponent<Rigidbody2D>();
        if (isPlayer)
            control = new InputSystem_Actions();
    }
    private bool canPerformAttack()
    {
        return center.canIMove() && center.canIAttack();
    }

    public void AllowAttacking()
    {
        center.setAttackPosibility(true);
    }

    public void DenyAttacking()
    {
        center.setAttackPosibility(false);
    }

    private void OnEnable()
    {
        if (!isPlayer)
            return;
        control.Player.Enable();

        control.Player.Attack.performed += ctx => TryAttack();
        control.Player.Attack.canceled += ctx => StopAttack();
    }

    private void OnDisable()
    {
        if (!isPlayer)
            return;

        control.Player.Attack.performed -= ctx => TryAttack();
        control.Player.Attack.canceled -= ctx => StopAttack();

        control.Player.Disable();
    }

    void TryAttack()
    {
        if (canPerformAttack())
        {
            center.setAttackingStage(true);

        }
    }

    void StopAttack()
    {
        center.setAttackingStage(false);

    }
    private void FixedUpdate()
    {

    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("I started colliding with " + other);
        if(other.gameObject.activeSelf)
        {
            Debug.Log("Attacked by...");
        }
        //attackers.Add(other.gameObject);
    }
    private void OnCollisionStay2D(Collision2D other) {
        Debug.Log("I keep colliding with " + other);
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        Debug.Log("I stopped colliding with" + other);
        //attackers.Remove(other.gameObject);
    }
    void Update()
    {
        if (center.amIattacking())
        {
            TryAttack();
        }
    }
}
