using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Unity.VisualScripting;

public class PlayerController : MonoBehaviour
{
    public Vector2 direction;
    public int directionState;
    public float speed = 4;
    private Vector2 speedVector;

    public Transform sensor; // Сенсор проверяет область перед собой на возможность передвижения
    public float sensorSize;
    public float sensorRange;

    public bool canMove;
    public LayerMask obj;

    private PhotonView view;
    private Rigidbody2D rb;
    private Joystick joystick;
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        joystick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<Joystick>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GetInput();
        getDirectionState();
        HandleSensor();
        Move();
    }
    void Move()
    {
        if (canMove) 
        { 
            switch (directionState)
            {
                case 2:
                    rb.velocity = new Vector2(0, direction.y * speed);
                    break;
                case 4:
                    rb.velocity = new Vector2(direction.x * speed, 0);
                    break;
                case 6:
                    rb.velocity = new Vector2(direction.x * speed, 0);
                    break;
                case 8:
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
            case 2:
                sensor.transform.localPosition = new Vector2(0, -3*sensorRange);
                break;
            case 4:
                sensor.transform.localPosition = new Vector2(-sensorRange, 0);
                break;
            case 6:
                sensor.transform.localPosition = new Vector2(sensorRange, 0);
                break;
            case 8:
                sensor.transform.localPosition = new Vector2(0, 3*sensorRange);
                break;
        }
        canMove = !Physics2D.OverlapBox(sensor.position, new Vector2(sensorSize, sensorSize), 0, obj);
    }

    void getDirectionState()
    {
        if (Mathf.Abs(direction.x)  > Mathf.Abs(direction.y) && direction.x > 0 ) { directionState = 6; }
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y) && direction.x < 0) { directionState = 4; }
        if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y) && direction.y > 0) { directionState = 8; }
        if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y) && direction.y < 0) { directionState = 2; }
    }


    void GetInput()
    {
        direction = new Vector2(joystick.Horizontal, joystick.Vertical);
    }
}

