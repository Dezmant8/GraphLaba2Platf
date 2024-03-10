using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Unit
{
    private int lives = 1;
    public override void Die()
    {
        Destroy(gameObject);
    }
    public override void ReceiveDamage(Unit enemy)
    {
        lives--;
        if (lives == 0)
        {
            Die();
        }
    }

    protected SpriteRenderer sprite;
    public void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

}

