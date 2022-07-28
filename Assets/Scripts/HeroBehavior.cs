using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class HeroBehavior : MonoBehaviour, IDamagable
{
    public Rigidbody2D body => rb;
    private Rigidbody2D rb;
    [SerializeField] public float playerSpeed;
    [SerializeField] float jumpStrength;
    [SerializeField] float knockbackForce;
    [SerializeField] float drag;
    [SerializeField] private int playerDamage = 1;
    [SerializeField] private Vector2 colOffset;
    Vector2 moveDirection = Vector2.zero;

    [SerializeField] private BoxCollider2D attackCollider;
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
        colOffset = attackCollider.offset;
    }

    private void Start()
    {

        rb = GetComponent<Rigidbody2D>();
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
            var offsetX = moveDirection.x < 0 ? -colOffset.x : colOffset.x;
            attackCollider.offset = new Vector2(offsetX, colOffset.y);

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
        animator.Play("herochar_sword_attack");
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
        var results = new List<Collider2D>();
        var cf = new ContactFilter2D();
        cf.SetLayerMask(Physics2D.GetLayerCollisionMask(attackCollider.gameObject.layer));
        attackCollider.OverlapCollider(cf, results);
        foreach (Collider2D enemy in results)
        {
            enemy.GetComponent<IDamagable>()?.takeDamage(playerDamage);
            var knockbackDir = (enemy.transform.position - transform.position).normalized;
            enemy.attachedRigidbody.AddForce(knockbackDir * knockbackForce, ForceMode2D.Impulse);
        }
    }

    public void disableInput()
    {
        inputActions.Disable();
    }

    public void takeDamage(int damage)
    {
        GameManager.Get.HeartManager.doDamage(damage);

    }
}
