using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class heroMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] public float playerSpeed;
    [SerializeField] float jumpStrength;
    [SerializeField] float drag;
    Vector2 moveDirection = Vector2.zero;

    [SerializeField] private GameObject attackCollider;
    [SerializeField] private Collider2D groundDetector;

    public PlayerControls inputActions;
    public InputAction playerMovement;
    public InputAction playerAttack;
    public InputAction playerJump;

    public Animator animator;

    private bool isGrounded = false;
    public bool IsGrounded
    {
        get => isGrounded;
        set
        {
            isGrounded = value;
            animator.SetBool("IsOnGround", value);
        }
    }


    private void Awake()
    {
        inputActions = new PlayerControls();
    }

    private void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        attackCollider.SetActive(false);
    }

    private void OnEnable()
    {
        playerMovement = inputActions.Player.Move;
        playerMovement.Enable();

        playerAttack = inputActions.Player.Fire;
        playerAttack.Enable();
        playerAttack.performed += PlayerAttack;

        playerJump = inputActions.Player.Jump;
        playerJump.Enable();
        playerJump.performed += PlayerJump;
    }
    private void OnDisable()
    {
        playerMovement.Disable();
        playerAttack.Disable();
        playerJump.Disable();
    }

    private void Update()
    {
        animator.SetFloat("VerticalSpeed", rb.velocity.y);

    }

    private void FixedUpdate()
    {
        moveDirection = playerMovement.ReadValue<Vector2>();
        var isRunning = moveDirection.x != 0;
        animator.SetBool("IsRunning", isRunning);
        if (isRunning)
        {
            var currVel = rb.velocity;
            currVel.x = moveDirection.x * playerSpeed;
            rb.velocity = currVel;
            GetComponent<SpriteRenderer>().flipX = moveDirection.x < 0;
        }
        else if (Mathf.Abs(rb.velocity.x) > 0)
        {
            var xDelta = Mathf.Abs(rb.velocity.x);
            if (xDelta > 0.01f)
                rb.AddForce(Vector2.right * -Mathf.Sign(rb.velocity.x) * drag, ForceMode2D.Impulse);
        }
    }

    private void PlayerAttack(InputAction.CallbackContext context)
    {
        animator.SetBool("IsAttacking", true);
    }

    private void PlayerJump(InputAction.CallbackContext context)
    {
        if (groundDetector.IsTouchingLayers(Physics2D.AllLayers))
        {
            IsGrounded = false;
            animator.Play("herochar_jump_start");
            rb.AddForce(Vector2.up * jumpStrength, ForceMode2D.Impulse);
        }
    }

    private void attack()        //used by event in animation of attack
    {
        attackCollider.SetActive(true);
    }

    public void stopAttack()
    {
        attackCollider.SetActive(false);
        animator.SetBool("IsAttacking", false);
    }
}
