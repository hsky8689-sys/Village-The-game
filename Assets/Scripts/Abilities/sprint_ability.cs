using UnityEngine;
using System.Collections;
public class sprint_ability : Ability
{
    [SerializeField] protected float sprintSpeed;
    [SerializeField] protected float sprintStamina;
    [SerializeField] protected float currentStamina;
    [SerializeField] protected bool sprinting;
    [SerializeField] protected float regularSpeed;
    [SerializeField] private float increaseBoost;
    [SerializeField] private float decreaseBoost;
    private bool usingTimer;
    protected override void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        controls = new InputSystem_Actions();
        move = GetComponent<movement>();
        center = GetComponent<StageManager>();
        isPlayer = center.amIthePlayer();
        regularSpeed = move.getSpeed();
        sprinting = false;
        usingTimer = false;
        currentStamina = sprintStamina;
    }
    /*protected override void SetUpStats()
    {
        regularSpeed = move.getSpeed();
        Debug.Log("Dau sprint nervos");
        sprinting = true;
    }
    
    protected override void ResetStats()
    {
        move.setSpeed(regularSpeed);
        Debug.Log("Nu mai dau sprint nervos");
        sprinting = false;
    }

    protected override void TryUseAbility()
    {
        if (CanUseAbility() && sprintStamina > 0)
        {
            SetUpStats();
            StartCoroutine(durationTimer());
        }
    }

    protected override IEnumerator cooldownTimer()
    {
        usingTimer = true;
        while (currentStamina < sprintStamina && !sprinting)
        {
            currentStamina += Time.deltaTime;
            if (move.getSpeed() > regularSpeed)
            {
                move.setSpeed(move.getSpeed() - Time.deltaTime);
            }
        }
        if (sprinting)
            StartCoroutine(durationTimer());
        usingTimer = false;
        yield break;
    }
    protected override IEnumerator durationTimer()
    {
        usingTimer = true;
        while (sprintStamina > 0 && sprinting)
        {
            if (move.getSpeed() < sprintSpeed)
            {
                move.setSpeed(move.getSpeed() + Time.deltaTime);
            }
            sprintStamina -= Time.deltaTime;
        }
        if(!CanUseAbility() || sprintStamina<=0)
            StartCoroutine(cooldownTimer());
        usingTimer = false;
        yield break;
    }

    protected void OnEnable()
    {
        if (!isPlayer)
            return;
        controls.Player.Enable();

        controls.Player.Sprint.performed += ctx =>
        {
            if (!usingTimer)
            {
                TryUseAbility();
            }
            ;
        };
        controls.Player.Sprint.canceled += ctx =>
        {
            if (!usingTimer)
                StartCoroutine(cooldownTimer());
            ResetStats();
        };
    }
    protected void OnDisable()
    {
        if (!isPlayer)
            return;

        controls.Player.Sprint.performed -= ctx =>
        {
            if (!usingTimer)
            {
                TryUseAbility();
            }
            ;
        };
        controls.Player.Sprint.canceled -= ctx =>
        {
            if (!usingTimer)
                StartCoroutine(cooldownTimer());
            ResetStats();
        };

        controls.Player.Disable();
    }
    
*/
    protected void OnEnable()
    {
        if (!isPlayer)
            return;
        controls.Player.Enable();
        controls.Player.Sprint.performed += ctx => sprinting = true;
        controls.Player.Sprint.canceled += ctx => sprinting = false;
    }
    protected void OnDisable()
    {
        if (!isPlayer)
            return;
        controls.Player.Sprint.performed -= ctx => sprinting = true;
        controls.Player.Sprint.canceled -= ctx => sprinting = false;
        controls.Player.Disable();
    }
    private void FixedUpdate()
    {
        if (sprinting)
        {
            if (currentStamina <= 0)
                sprinting = false;
            if (sprinting && sprintStamina >= 0)
                {
                    currentStamina -= Time.deltaTime * decreaseBoost;
                    if (move.getSpeed() < sprintSpeed)
                        move.setSpeed(move.getSpeed() + Time.deltaTime * increaseBoost);
                }
                else
                {
                    if (!sprinting || (currentStamina < sprintStamina) || currentStamina <= 0)
                        currentStamina += Time.deltaTime;
                    if (move.getSpeed() > regularSpeed)
                        move.setSpeed(move.getSpeed() - Time.deltaTime * decreaseBoost);
                }
        }
        else
        {
            if (currentStamina < sprintStamina)
                currentStamina += Time.deltaTime*increaseBoost;
            if (move.getSpeed() > regularSpeed)
                move.setSpeed(move.getSpeed() - Time.deltaTime*decreaseBoost);
        }
    }
    
}
