using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 direction;
    public int directionState;

    public Vector2 initDirection;
    public int initDirectionState;
    

    public float bulletSpeed = 5f;
    public float lifetime;
    public float distance;
    public int damage;
    public LayerMask whatIsSolid;

    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        CheckForHit();
        GetDirection();
        Move();
    }
    
    void Move()
    {
        rb.velocity = direction * bulletSpeed;
    }

    void GetDirection()
    {
        switch (directionState)
        {
            case 3:
                direction = Vector2.down;
                break;
            case 0:
                direction = Vector2.right;
                break;
            case 2:
                direction = Vector2.left;
                break;
            case 1:
                direction = Vector2.up;
                break;
        }
    }

    void CheckForHit()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, direction, distance, whatIsSolid);
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Player"))
            {
                hitInfo.collider.GetComponent<Player>().TakeDamage(damage);
            }
            Destroy(gameObject);
        }

    }

}
