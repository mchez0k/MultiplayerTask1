using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Player : MonoBehaviour
{
    bool facingRight;
    public float speed;
    Vector2 moveInput;

    PhotonView view;
    Animator anim;

    private void Start()
    {
        view = GetComponent<PhotonView>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (view.IsMine)
        {
            moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            Vector2 moveAmount = moveInput.normalized * speed/5f;
            transform.position += (Vector3)moveAmount;
            CheckFacing();
            CheckRunning();
        }
    }

    private void CheckRunning()
    {
        if (moveInput == Vector2.zero)
        {
            anim.SetBool("isRunning", false);
        }
        else
        {
            anim.SetBool("isRunning", true);
        }
    }
    private void CheckFacing()
    {
        if (facingRight && moveInput.x > 0)
        {
            Flip();
        }
        else if (!facingRight && moveInput.x < 0)
        {
            Flip();
        }
    }
    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(Vector3.up * 180);
    }
}

/*
public class Player : MonoBehaviour
{
    private PhotonView view;

    private bool facingRight = true;

    public float basicSpeed = 4f;

    public Vector2 moveInput;
    private Vector2 moveVelocity;

    public Joystick joystick;
    public Transform shootOffset;
    //public GameObject bullet;

    private Rigidbody2D rb;
    private Animator anim;

    private void Start()
    {
        view = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (view.IsMine)
        {
            moveInput = new Vector2(joystick.Horizontal, joystick.Vertical);

            moveVelocity = moveInput.normalized * basicSpeed;

            rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
        }    
        CheckRunning();
        CheckFacing();
    }

    public void OnShootButtonDown()
    {
       // Instantiate(bullet, shootOffset.position, transform.rotation);
    }

    

    

    
}*/
