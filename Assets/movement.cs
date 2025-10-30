using UnityEngine;
using UnityEngine.InputSystem;
//Component that handles movement actions themselves such as 
//the ability to move,movement speed and other
//movement based features

public class movement : MonoBehaviour
{
    [SerializeField] private bool isPlayer;
    [SerializeField] private float speed;
    private InputSystem_Actions controls;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 currentMove;
    private bool canMove;
    public directions direction;
    private StageManager center;

    public void setSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
    public float getSpeed()
    {
        return speed;
    }
    private void Awake()
    {
        if (isPlayer)
            controls = new InputSystem_Actions();
        rb = GetComponent<Rigidbody2D>();
        AllowMovement();
        rb.gravityScale = 0f;
        center = GetComponent<StageManager>();
    }
    private void OnEnable()
    {
        if (!isPlayer)
            return;
        controls.Player.Enable();

        controls.Player.Move.performed += ctx => TryMove(ctx.ReadValue<Vector2>());
        controls.Player.Move.canceled += ctx => StopMove();
    }

    private void OnDisable()
    {
        if (!isPlayer)
            return;

        controls.Player.Move.performed -= ctx => TryMove(ctx.ReadValue<Vector2>());
        controls.Player.Move.canceled -= ctx => StopMove();

        controls.Player.Disable();
    }

    private void TryMove(Vector2 dir)
    {
        if (canMove)
        {
            moveInput = dir;
            if (moveInput == new Vector2(0, 1)) direction = directions.UP;
            if (moveInput == new Vector2(-1, 0)) direction = directions.LEFT;
            if (moveInput == new Vector2(0, -1)) direction = directions.DOWN;
            if (moveInput == new Vector2(1, -0)) direction = directions.RIGHT;
            center.setMovingStage(true);
            currentMove = dir.normalized;
        }
    }
    public directions GetDirection()
    {
        return direction;
    }
    private void StopMove()
    {
        center.setMovingStage(false);
        currentMove = Vector2.zero;
    }

    public void AllowMovement()
    {
        canMove = true;
    }
    public void DenyMovement()
    {
        canMove = false;
    }
    private void FixedUpdate()
    {
        if (center.canIMove())
            rb.MovePosition(rb.position + currentMove * speed * Time.fixedDeltaTime);
    }
}
