using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    private Transform player;
    private Vector2 Offset = new Vector2(3.0f, 2.0f);
    private float damping = 3f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        Vector3 target = new Vector3(player.position.x + Offset.x,
       player.position.y + Offset.y, transform.position.z);
        Vector3 currentPosition = Vector3.Lerp(transform.position, target,
       damping * Time.deltaTime);
        transform.position = currentPosition;
    }

}
