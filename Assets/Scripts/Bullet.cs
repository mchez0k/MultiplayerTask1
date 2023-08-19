using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject player;

    private Rigidbody2D rb;
    private Rigidbody2D shooterRB;

    public float bulletSpeed;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        shooterRB = player.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(shooterRB.velocity.x, shooterRB.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
