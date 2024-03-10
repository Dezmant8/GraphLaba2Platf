using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Unit
{
    private float speed = 10.0f;
    private SpriteRenderer sprite;
    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }


    void Start()
    {
        Destroy(gameObject, 1.5f);
    }

    private Vector3 direction;
    public Vector3 Direction
    {
        set { direction = value; }
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);





    }
    
    

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Unit unit = collision.GetComponent<Unit>();
        if (unit is Monster)
        Destroy(gameObject);
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

}
