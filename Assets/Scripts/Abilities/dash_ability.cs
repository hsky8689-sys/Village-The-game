using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
public class dash_ability : Ability
{   /*
    private InputSystem_Actions controls;
    private Rigidbody2D rb;
    protected movement move;
    */
    private Vector2 dash_direction;
    public float dash_force;
    public float range;
    private bool Dashing;
    private bool canDash;
    protected float character_speed;
    protected override void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        controls = new InputSystem_Actions();
        center = GetComponent<StageManager>();
        move = GetComponent<movement>();
        isPlayer = center.amIthePlayer();
        canDash = true;
    }
    protected override void SetUpStats()
    {
        //center.setMovementPosibility(false);
        center.setAttackPosibility(false);

        Dashing = true;
        canDash = false;
    }
    protected override void ResetStats()
    {
        //center.setMovementPosibility(true);
        center.setAttackPosibility(true);

        Dashing = false;
    }
    protected override void BeforeAction()
    {
          /*ceva animatie nush...*/
    }
    protected override void PerformAction()
    {
        if (!CanUseAbility())
        {
            return;
        }
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 targetPos = new Vector2(mouseWorld.x, mouseWorld.y);

        dash_direction = targetPos - rb.position;
        float dist = dash_direction.magnitude;

        if (dist > range)
        {
            Dashing = false;
            return;
        }

        dash_direction.Normalize();
    }
    protected override void TryUseAbility()
    {
        if (CanUseAbility() && canDash)
        {
            StartCoroutine(MainBody());
        }
    }
    protected void OnEnable()
    {
        if (!isPlayer)
            return;
        controls.Player.Enable();

        controls.Player.Dash.performed += ctx => TryUseAbility();
    }
    protected override IEnumerator cooldownTimer()
    {
        move.setSpeed(character_speed);

        yield return new WaitForSeconds(cooldown);
        center.setAbilityCastingPosibility(true);
        canDash = true;
    }
    protected override IEnumerator durationTimer()
    {
        character_speed = move.getSpeed();

        move.setSpeed(0);
        center.setAbilityCastingPosibility(false);
        canDash = false;
        
        yield return new WaitForSeconds(duration);
    }
    protected void OnDisable()
    {
        if (!isPlayer)
            return;

        controls.Player.Dash.performed -= ctx => TryUseAbility();

        controls.Player.Disable();
    }
    protected override IEnumerator MainBody()
    {
        SetUpStats();
        BeforeAction();
        PerformAction();

        if (Dashing)
        {
            yield return StartCoroutine(durationTimer());

            ResetStats();

            yield return StartCoroutine(cooldownTimer());
        }
        else
        {
            canDash = true;
            ResetStats();
        }
    }
    private void FixedUpdate()
    {
        if (Dashing)
        {
            rb.AddForce(dash_direction * dash_force, ForceMode2D.Force);
        }
    }
}
