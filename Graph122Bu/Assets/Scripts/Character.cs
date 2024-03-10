using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Character : Unit
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private Bullet bullet;
    [SerializeField]
    private Transform shootPosition;

    [SerializeField]
    private int lives;

    [SerializeField]
    private float jumpForce;

    new private Rigidbody2D rigidbody;
    private Animator animator;
    private SpriteRenderer sprite;

    public enum CharState { Idle, Run, Jump, Fall };


    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        bullet = Resources.Load<Bullet>("Bullet");
    }
    void Run()
    {
        if (IsGrounded) State = CharState.Run;
        Vector3 direction = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position,
        transform.position + direction, speed * Time.deltaTime);
        sprite.flipX = direction.x < 0;
    }
    void Jump()
    {
        rigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    void Shoot()
    {
        Vector3 position = transform.position;
        position.y += 0.5f;
        Bullet newBullet = Instantiate(bullet, position, bullet.transform.rotation);
        newBullet.Direction = newBullet.transform.right * (sprite.flipX ? -1.0f : 1.0f);
    }

    void Update()
    {
        if (IsGrounded) State = CharState.Idle;
        if (Input.GetButton("Horizontal")) Run();
        if (IsGrounded && Input.GetButtonDown("Jump")) Jump();
        if (Input.GetButtonDown("Fire1"))
            Shoot();
            
    }
    
    private bool IsGrounded = false;
    private void CheckGround()
    {
        float YVelocity = rigidbody.velocity.y;
        if (!IsGrounded && YVelocity > 0)
            State = CharState.Jump;
        else if (!IsGrounded && YVelocity < 0)
            State = CharState.Fall;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position,
       0.3f);
        IsGrounded = colliders.Length > 1;
    }
    private void FixedUpdate()
    {
        CheckGround();
    }

    private CharState State
    {
        get { return (CharState)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }
    public override void ReceiveDamage(Unit enemy)
    {
        lives--;
        if (lives == 0)
            Die();
        else
        {
            Vector2 dir = (transform.position - enemy.transform.position).normalized;
            rigidbody.velocity = Vector2.zero;
            rigidbody.AddForce(dir * 7.0f, ForceMode2D.Impulse);
        }
    }



}
