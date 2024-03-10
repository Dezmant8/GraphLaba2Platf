using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : Monster
{
    private Animator animator;
    public EnemyBullet bullet;
    public Transform firePoint;
    public float fireRate = 1f; // Скорость стрельбы в выстрелах в секунду
    private float nextFireTime;
    private SpriteRenderer sprite;
    
    private void Awake()
    {
        
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        bullet = Resources.Load<EnemyBullet>("EnemyBullet");
    }

    void Update()
    {
        if (Time.time > nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void Shoot()
    {
        
        EnemyBullet newBullet = Instantiate(bullet, firePoint.position, bullet.transform.rotation);
        newBullet.Direction = newBullet.transform.right * (sprite.flipX ? -1.0f : 1.0f);

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Unit unit = collision.GetComponent<Unit>();
        if (unit)
            if (unit is Character)
            {
                if (Mathf.Abs(unit.transform.position.x - transform.position.x)< 0.3f)
                {
                    
                    ReceiveDamage(unit);
                }

                else
                {
                    
                    unit.ReceiveDamage(this);

                }
                    
            }
            else if (unit is Bullet)
            {
                ReceiveDamage(unit);
            }
    }
}
