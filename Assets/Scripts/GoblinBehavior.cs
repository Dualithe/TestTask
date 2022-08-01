using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoblinBehavior : MonoBehaviour, IDamagable
{
    public Rigidbody2D body => rb;

    private Rigidbody2D rb;

    [SerializeField] private GoblinAttack atk;

    [SerializeField] private Collider2D atkCol;

    [SerializeField] Slider hp;

    public Animator animator;

    [SerializeField] private int knockbackForce;

    [SerializeField] private int maxHP;

    [SerializeField] private Vector2 colOffset;
    public int MaxHP => maxHP;

    [SerializeField] private int currentHP;
    public int CurrentHP => currentHP;

    [SerializeField] private int damage = 1;

    [SerializeField] private float enemySpeed;

    [SerializeField] public float enemyStartingAttackCooldown;
    public float enemyAttackCooldown;

    private Path path;

    private bool currDir = false;
    public bool isMoving = false;

    private void Awake()
    {
        path = GetComponent<PathCreator>().path;
        rb = GetComponent<Rigidbody2D>();
        colOffset = atkCol.offset;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        currentHP = maxHP;
        hp.maxValue = maxHP;
        hp.value = currentHP;
        if (path != null)
        {
            isMoving = true;
            animator.SetBool("IsPatrolling", true);
        }
    }

    private void FixedUpdate()
    {
        enemyAttackCooldown -= Time.deltaTime;
        enemyAttackCooldown = Mathf.Max(enemyAttackCooldown, 0);
        if (path != null && isMoving == true)
        {
            var newVel = Mathf.Sign(currDir == true ? path[0].x - path[1].x : path[1].x - path[0].x) * enemySpeed;
            body.velocity = new Vector2(newVel, body.velocity.y);
            var switchDir = currDir == true
                ? path[0].x < transform.position.x && path[1].x < transform.position.x
                : path[0].x > transform.position.x && path[1].x > transform.position.x;
            if (switchDir)
            {
                currDir = !currDir;
                GetComponent<SpriteRenderer>().flipX = !currDir;
                var offsetX = colOffset.x * (currDir == true ? 1 : -1);
                atkCol.offset = new Vector2(offsetX, colOffset.y);
                body.velocity = Vector2.zero;
            }
        }
    }

    public void attack()
    {
        IDamagable col = atk.lastPlayerHit.GetComponent<IDamagable>();
        if (GameManager.Get.HeartManager.iFrames == 0 && atk.isInCollider)
        {
            col.takeDamage(damage);
            knockback(col.body);
        }
        isMoving = true;
    }

    private void knockback(Rigidbody2D col)
    {
        var knockbackDir = (col.transform.position - transform.position).normalized;
        col.AddForce(knockbackDir * knockbackForce, ForceMode2D.Impulse);
    }

    private void resetMovement()
    {
        isMoving = true;
    }

    public void takeDamage(int dmg)
    {
        isMoving = false;
        currentHP -= dmg;
        hp.value = currentHP;
        animator.Play("goblin_hit");
        if (currentHP <= 0)
            die();
    }

    private void die()
    {
        animator.Play("goblin_death");
        hp.gameObject.SetActive(false);
        GetComponent<BoxCollider2D>().enabled = false;
    }

    private void destroy()
    {
        Destroy(gameObject);
    }
}
