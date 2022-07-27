using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBehavior : MonoBehaviour, IDamagable
{
    public Rigidbody2D body => rb;

    private Rigidbody2D rb;

    [SerializeField] Slider hp;

    private Animator animator;

    [SerializeField] private int knockbackForce;

    [SerializeField] private int maxHP;
    public int MaxHP => maxHP;

    [SerializeField] private int currentHP;
    public int CurrentHP => currentHP;

    [SerializeField] private int damage = 1;

    [SerializeField] float mushroomSpeed;

    private Path path;

    private int currDir = 0;

    private void Awake()
    {
        path = GetComponent<PathCreator>().path;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        currentHP = maxHP;
        hp.maxValue = maxHP;
        hp.value = currentHP;
        if (path != null)
        {
            body.velocity = new Vector2(Mathf.Sign(path[1].x - path[0].x) * mushroomSpeed, 0);
        }
    }
    private void changeDirection()
    {
        body.velocity *= -10;
    }

    private void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, path[currDir]) < 0)
        {
            currDir = currDir == 1 ? 0 : 1;
            changeDirection();
        }
    }

    public void attack(IDamagable col)
    {
        col.takeDamage(damage);
        knockback(col.body);
    }

    private void knockback(Rigidbody2D col)
    {
        var knockbackDir = (col.transform.position - transform.position).normalized;
        col.AddForce(knockbackDir * knockbackForce, ForceMode2D.Impulse);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player" && GameManager.Get.HeartManager.iFrames == 0)
        {
            attack(collision.transform.GetComponent<IDamagable>());
        }
    }

    public void takeDamage(int dmg)
    {
        currentHP -= dmg;
        hp.value = currentHP;
        animator.Play("mushroom_hit");
        if (currentHP <= 0)
            die();
    }

    private void die()
    {
        animator.Play("mushroom_death");
        hp.gameObject.SetActive(false);
        GetComponent<BoxCollider2D>().enabled = false;
    }

    private void destroy()
    {
        Destroy(gameObject);
    }
}
