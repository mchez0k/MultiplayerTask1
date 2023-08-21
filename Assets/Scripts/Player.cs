using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Unity.VisualScripting;

public class Player : MonoBehaviour
{
    public Vector2 direction;
    public int directionState;

    public int health;

    public float speed = 4;

    public Transform sensor; // Сенсор проверяет область перед собой на возможность передвижения
    public float sensorSize;
    public float sensorRange;

    public bool canMove;
    public LayerMask obj;

    public Bullet bullet;

    private PhotonView view;
    private Rigidbody2D rb;
    private Joystick joystick;
    private Animator anim;

    void Start()
    {
        view = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody2D>();
        joystick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<Joystick>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckDeath();
        GetInput();
        getDirectionState();
        HandleSensor();
        if (view.IsMine)
        {
            Move();
        } 

    }
    void Move()
    {
        if (canMove) 
        { 
            switch (directionState)
            {
                case 3:
                    rb.velocity = new Vector2(0, direction.y * speed);
                    break;
                case 0:
                    rb.velocity = new Vector2(direction.x * speed, 0);
                    break;
                case 2:
                    rb.velocity = new Vector2(direction.x * speed, 0);
                    break;
                case 1:
                    rb.velocity = new Vector2(0, direction.y * speed);
                    break;
            }
        } else
        {
            rb.velocity = Vector2.zero;
        }
        
    }
    void HandleSensor()
    {
        sensor.transform.localPosition = Vector2.zero;
        switch (directionState)
        {
            case 3:
                sensor.transform.localPosition = new Vector2(0, -2f*sensorRange);
                break;
            case 2:
                sensor.transform.localPosition = new Vector2(-1.3f*sensorRange, 0);
                break;
            case 0:
                sensor.transform.localPosition = new Vector2(sensorRange*1.3f, 0);
                break;
            case 1:
                sensor.transform.localPosition = new Vector2(0, 2f*sensorRange);
                break;
        }
        canMove = !Physics2D.OverlapBox(sensor.position, new Vector2(sensorSize, sensorSize), 0, obj);
    }

    void getDirectionState()
    {
        if (Mathf.Abs(direction.x)  > Mathf.Abs(direction.y) && direction.x > 0 ) { directionState = 0; }
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y) && direction.x < 0) { directionState = 2; }
        if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y) && direction.y > 0) { directionState = 1; }
        if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y) && direction.y < 0) { directionState = 3; }
    }


    void GetInput()
    {
        direction = new Vector2(joystick.Horizontal, joystick.Vertical);
    }

    public void Shoot()
    {
        bullet.directionState = directionState; 
        Instantiate(bullet, sensor.transform.position, Quaternion.Euler(0, 0, 90f*directionState));
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
    
    void CheckDeath()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}

