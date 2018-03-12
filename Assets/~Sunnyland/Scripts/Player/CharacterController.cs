using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]

public class CharacterController : MonoBehaviour
{
    public float speed = 5f;
    public float maxVelocity = 2f;
    public float rayDistance = 0.2f;
    public float jumpHeight = 2;
    public int maxJumpCount = 1;
    public LayerMask groundLayer;

    private Vector3 moveDirection;
    private int jumpCount = 0;
    private Vector3 groundDir = Vector3.down;

    Animator anim;
    Rigidbody2D rb;
    SpriteRenderer rend;

    public delegate void OnMove();
    public event OnMove onMove;

    public delegate void OnClimb();
    public event OnClimb onClimb;

    public delegate void OnJump();
    public event OnJump onJump;

    public delegate void OnCrouch();
    public event OnCrouch onCrouch;


    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        DetectGround();
    }

    void Update()
    {
        // Apply gravity to move direction
        moveDirection.y += Physics.gravity.y * Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftControl))
        {
            Crouch();
        }
        else
        {
            anim.SetBool("isCrouch", false);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            Move();
            onMove.Invoke();
        }
        else
        {
            anim.SetBool("isRun", false);
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            anim.SetBool("isClimb", true);
        }
        else
        {
            anim.SetBool("isClimb", false);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        else
        {
            anim.SetBool("isJump", false);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + groundDir * rayDistance);

        Gizmos.color = Color.blue;
        Vector3 right = Vector3.Cross(groundDir, Vector3.forward);
        Gizmos.DrawLine(transform.position, transform.position + right * rayDistance);
        Gizmos.DrawLine(transform.position, transform.position + -right * rayDistance);
    }
    void DetectGround()
    {
        Ray groundRay = new Ray(transform.position, groundDir);
        RaycastHit2D hit = Physics2D.Raycast(groundRay.origin, groundRay.direction, rayDistance, groundLayer);
        if (hit.collider != null)
        {
            jumpCount = 0;
            groundDir = -hit.normal;
        }
    }
    void LimitVelocity()
    {
        // If Rigid's velocity (magnitude) is greater than maxVelocity
            // Set Rigid velocity to velocity normalized x maxVelocity
    }

    void Jump()
    {
        onJump.Invoke();
        if (jumpCount < maxJumpCount)
        {
            jumpCount++;
            rb.velocity = new Vector2(0, jumpHeight);
        }
    }

    void Crouch()
    {
        onCrouch.Invoke();
    }

    void Climb()
    {

    }
    void Move()
    {
        Vector3 right = Vector3.Cross(groundDir, Vector3.forward);
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(right * speed * Time.deltaTime);
            rend.flipX = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(-right * speed * Time.deltaTime);
            rend.flipX = false;
        }
        // If horizontal > 0
            // Flip Character
        // If horizontal < 0
            // Flip Character

        // Add force to player in the right direction
        // Limit Velocity

    }
}
