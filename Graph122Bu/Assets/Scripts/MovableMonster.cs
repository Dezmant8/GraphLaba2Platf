using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MovableMonster : Monster
{
    [SerializeField]
    private float speed;

    public enum MovableMonsterState2 { Idle, Run };
    private Animator animator;
    private MovableMonsterState2 State2
    {
        get { return (MovableMonsterState2)animator.GetInteger("State2"); }
        set { animator.SetInteger("State2", (int)value); }
    }

    private new void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
    }

    Vector3 direction;
    public void Start()
    {
        direction = transform.right * -1.0f;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Unit unit = collision.GetComponent<Unit>();
        if (unit)
            if (unit is Character)
            {
                if (Mathf.Abs(unit.transform.position.x - transform.position.x)
               < 0.3f)
                    ReceiveDamage(unit);
                else
                    unit.ReceiveDamage(this);
            }
            else if (unit is Bullet)
            {
                ReceiveDamage(unit);
            }
    }


    public void Update()
    {
        Move();
    }

    private void Move()
    {
        
        State2 = MovableMonsterState2.Run;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + transform.up * 0.6f + transform.right * direction.x * 0.2f, 0.2f);
        if (colliders.Length > 1 && colliders.All(x => !x.GetComponent<Character>()))
        {
            direction *= -1.0f;
            sprite.flipX = !sprite.flipX;
        }

        transform.position = Vector3.MoveTowards(transform.position,
        transform.position + direction, speed * Time.deltaTime);
    }

}
