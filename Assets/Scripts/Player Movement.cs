using UnityEngine;
using Unity.Netcode;

public class PlayerMovement : NetworkBehaviour
{
    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 16f;
    private bool isFacingRight = true;

    private bool isWallSliding;
    private float wallSlidingSpeed = 2f;

    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 0.4f;
    private Vector2 wallJumpingPower = new Vector2(8f, 16f);

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] AudioListener listener;

    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            listener.enabled = true;
        }
        else
        {

        }
    }
    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
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

    private void FixedUpdate()
    {
        if (!isWallJumping)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
    }

    private void IsGrounded()
    {
        // nimic
    }

    private void IsWalled()
    {
        // nimic
    }

    private void WallSlide()
    {
        if (horizontal != 0f)
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
