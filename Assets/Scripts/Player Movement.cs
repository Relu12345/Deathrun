using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 20f;
    public float jumpSpeed = 500f;

    private float dirX;
    private bool isGrounded = true;
    private bool facingRight = true;
    private Rigidbody2D rb;
    private SpriteRenderer spr;
    private Animator anim;
    private Vector3 localScale;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        localScale = transform.localScale;
    }

    void CheckDirection()
    {
        if (dirX > 0)
            facingRight = true;
        else if (dirX < 0)
            facingRight = false;

        if((facingRight && (localScale.x < 0)) || (facingRight && (localScale.x < 0)))
        {
            localScale.x *= -1f;

            transform.localScale = localScale;
        }
    }

    void LateUpdate()
    {
        CheckDirection();
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(dirX, rb.velocity.y);
    }

    void Update()
    {
        if (facingRight == false)
        {
            spr.flipX = true;
        }
        else
        {
            spr.flipX = false;
        }

        dirX = Input.GetAxisRaw("Horizontal") * movementSpeed;

        if (dirX == 0)
        {
            anim.SetBool("isWalking", false);
        }

        if (Mathf.Abs(dirX) == movementSpeed && rb.velocity.y == 0)
        {
            anim.SetBool("isWalking", true);
        }    

        if (isGrounded)
        {
            rb.gravityScale = 2.1f;
        }
        
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (isGrounded)
            {
                anim.Play("RojickoJump");
                rb.AddForce(Vector3.up * jumpSpeed);
                isGrounded = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
    }
}
