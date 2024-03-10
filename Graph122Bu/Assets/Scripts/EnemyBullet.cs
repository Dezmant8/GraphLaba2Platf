using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Unit
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
        transform.Translate(Vector2.left * speed * Time.deltaTime); // ѕеремещение пули вправо (в направлении выстрела)





    }



    public void OnTriggerEnter2D(Collider2D collision)
    {
        Unit unit = collision.GetComponent<Unit>();
        if (unit is Character)
        {
            unit.ReceiveDamage(this);
            Destroy(gameObject);
        }
            
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

}
