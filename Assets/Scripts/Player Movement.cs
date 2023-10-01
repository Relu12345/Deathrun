using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 16f;
    private bool isFacingRight = true;

    private bool isWallSliding;
    private float wallSlidingSpeed = 2f;
    private bool isTouchingWall;
    private bool isTouchingGround;

    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.1f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 0.2f;
    private Vector2 wallJumpingPower = new Vector2(1f, 1f);

    [SerializeField] private Rigidbody2D rb;

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        WallSlide();
        WallJump();

        if (!isWallJumping)
        {
            Flip();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
     {
        var groundLayer = LayerMask.NameToLayer("Ground Layer");
        var wallLayer = LayerMask.NameToLayer("Wall Layer");
        if (collision.gameObject.layer == groundLayer)
        {
            isTouchingWall = true;
        }
        if(collision.gameObject.layer == wallLayer)
        {
            isTouchingGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        var groundLayer = LayerMask.NameToLayer("Ground Layer");
        var wallLayer = LayerMask.NameToLayer("Wall Layer");
        {
            isTouchingWall = false;
        }
        if (collision.gameObject.layer == wallLayer)
        {
            isTouchingGround = false;
        }
    }

    private void FixedUpdate()
    {
        if (!isWallJumping)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
    }

    private bool IsGrounded()
    {
        return isTouchingGround || rb.velocity.y == 0;
    }

    private bool IsWalled()
    {
        return isTouchingWall;
    }

    private void WallSlide()
    {
        if (IsWalled() && !IsGrounded() && horizontal != 0f)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }

    private float wallJumpCooldown = 0.5f;
    private float wallJumpCooldownTimer = 0f;

    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if (wallJumpCooldownTimer > 0f)
        {
            wallJumpCooldownTimer -= Time.deltaTime;
        }
        else
        {
            wallJumpCooldownTimer = 0f;

            if (Input.GetButtonDown("Jump") && wallJumpingCounter > 0f)
            {
                isWallJumping = true;

                rb.velocity = new Vector2(rb.velocity.x, wallJumpingPower.y);

                wallJumpingCounter = 0f;

                wallJumpCooldownTimer = wallJumpCooldown;

                Invoke(nameof(StopWallJumping), wallJumpingDuration);
            }
        }
    }




    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
