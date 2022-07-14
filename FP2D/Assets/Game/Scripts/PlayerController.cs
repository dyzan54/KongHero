using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform frontCheck;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private LayerMask WallMask;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float checkRadius;
    [SerializeField] private float wallSlidingSpeed;

    private float horizontalValue;
    private bool isFacingRight = true;
    private bool isGrounded;
    private bool wallSliding;
    private bool isTouchingFront;
    [SerializeField] private bool wallJumping;
    [SerializeField] private float xJumpWall;
    [SerializeField] private float yJumpWall;
    [SerializeField] private float timeSliding;

    Animator animator;
    Rigidbody2D rb;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        animator.SetFloat("VelocityY", rb.velocity.y);
        horizontalValue = Input.GetAxisRaw("Horizontal");


        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, layerMask);
        isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, checkRadius, WallMask);


        //GroundCheck and Jumping
        if (isGrounded == true)
        {
            animator.SetBool("isJump", false);
        }
        else { animator.SetBool("isJump", true); }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            Jump();
        }

        Sliding();

        WallJumping();

        MeleeAttack();
    }

    private void FixedUpdate()
    {
        Movement(horizontalValue);
    }

    public void Movement(float value)
    {
        //Movement & animation
        float Move = value * moveSpeed * 100 * Time.fixedDeltaTime;
        Vector2 Velocity = new Vector2(Move, rb.velocity.y);
        rb.velocity = Velocity;
        animator.SetFloat("VelocityX", Mathf.Abs(rb.velocity.x));

        if (isFacingRight && value < 0)
            Flip();
        else if (!isFacingRight && value > 0)
            Flip();
    }
    public void Jump()
    {
        rb.velocity = Vector2.up * jumpSpeed;
        animator.SetBool("isJump", true);
    }

    public void Sliding()
    {
        if (isTouchingFront == true && isGrounded == false && horizontalValue != 0)
        {
            wallSliding = true;
        }
        else wallSliding = false;

        if (wallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
            animator.SetBool("isFall", true);
        }
        else if (!wallSliding)
        {
            animator.SetBool("isFall", false);
            animator.SetBool("isLand", true);
        }
    }

    public void WallJumping()
    {
        if (Input.GetKeyDown(KeyCode.Space) && wallSliding == true)
        {
            wallJumping = true;
            Invoke("SetWallJumpingToFalse", timeSliding);
        }

        if (wallJumping == true)
        {
            rb.velocity = new Vector2(xJumpWall * -horizontalValue, yJumpWall);
            animator.SetBool("isFall", false);
            animator.SetBool("isJump", true);
        }
    }

    public void MeleeAttack()
    {
        //Just animation
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.SetBool("isAttack", true);
        }
        else animator.SetBool("isAttack", false);
    }

    private void SetWallJumpingToFalse()
    {
        wallJumping = false;
        Debug.Log("Successfull");
    }
    private void Flip()
    {
        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
        isFacingRight = !isFacingRight;
    }


    //This code below will be remove later
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("DeadZone"))
        {
            Debug.Log("Compare tag here");
            Destroy(gameObject);
        }
    }

}
