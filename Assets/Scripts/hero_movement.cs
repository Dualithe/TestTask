using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class hero_movement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] public float playerSpeed;
    [SerializeField] float jumpStrength;
    [SerializeField] float drag;
    Vector2 moveDirection = Vector2.zero;

    [SerializeField] private GameObject attackCollider;

    public PlayerControls inputActions;
    public InputAction playerMovement;
    public InputAction playerAttack;
    public InputAction playerJump;

    public Animator animator;

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
        playerMovement.performed += PlayerMovement;

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
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("VerticalSpeed", rb.velocity.y);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            animator.SetBool("IsOnGround", true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            animator.SetBool("IsOnGround", false);

    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(rb.velocity.x) > 0 && !playerMovement.inProgress)
            rb.AddForce(Vector2.right * -Mathf.Sign(rb.velocity.x) * drag, ForceMode2D.Impulse);

    }

    private void PlayerMovement(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();
        if (moveDirection.x != 0)
        {
            var currVel = rb.velocity;
            currVel.x = moveDirection.x * playerSpeed;
            rb.velocity = currVel;
        }
    }

    private void PlayerAttack(InputAction.CallbackContext context)
    {
        animator.SetBool("IsAttacking", true);
    }

    private void PlayerJump(InputAction.CallbackContext context)
    {
        rb.AddForce(Vector2.up * jumpStrength, ForceMode2D.Impulse);
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
