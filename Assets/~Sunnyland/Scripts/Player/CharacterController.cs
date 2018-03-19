using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SunnyLand
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class CharacterController : MonoBehaviour
    {
        public float speed = 5f;
        public float maxVelocity = 2f;
        [Header("Grounding")]
        public float rayDistance = 0.2f;
        public float maxSlopeAngle = 45f;
        public bool isGrounded = false;
        [Header("Crouch")]
        public bool isCrouching = false;
        [Header("Jump")]
        public float jumpHeight = 2;
        public int maxJumpCount = 1;
        public bool isJumping = false;
        [Header("Jump")]
        public float climbSpeed = 5f;
        public bool isClimbing = false;
        public bool isOnSlope = false;
        [Header("References")]
        public Collider2D defaultCollider;
        public Collider2D crouchCollider;

        // Delegates
        public EventCallback onJump;
        public EventCallback onHurt;
        public BoolCallback onCrouchChanged;
        public BoolCallback onGroundedChanged;
        public BoolCallback onClimbChanged;
        public BoolCallback onSlopeChanged;
        public FloatCallback onMove;
        public FloatCallback onClimb;

        private Vector3 groundNormal = Vector3.up;
        private Vector3 moveDirection;
        private int currentJump = 0;

        private float vertical, horizontal;

        // References
        Animator anim;
        Rigidbody2D rb;
        SpriteRenderer rend;

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
                //onMove.Invoke();
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
            Gizmos.DrawLine(transform.position, transform.position + Vector3.down * rayDistance);

            Gizmos.color = Color.blue;
            Vector3 right = Vector3.Cross(Vector3.down, Vector3.forward);
            Gizmos.DrawLine(transform.position, transform.position + right * rayDistance);
            Gizmos.DrawLine(transform.position, transform.position + -right * rayDistance);
        }

        bool CheckSlope(RaycastHit2D hit)
        {
            // Grab the angle in degrees of the surface we're standing on
            float slopeAngle = Vector3.Angle(Vector3.up, hit.normal);
            if (slopeAngle >= maxSlopeAngle)
            {
                rb.AddForce(Physics.gravity);
            }
            if (slopeAngle > 0 && slopeAngle < maxSlopeAngle)
            {
                return true;
            }
            return false;
        }
        bool CheckGround(RaycastHit2D hit)
        {
            // if it something, not me, trigger
            if (hit.collider != null && hit.collider.name != name && !hit.collider.isTrigger)
            {
                // reset jump count
                currentJump = 0;
                // is grounded?
                isGrounded = true;
                // set ground normal now that we're grounded
                groundNormal = -hit.normal;

                bool wasOnSlope = isOnSlope;
                // check if we are on a slope
                isOnSlope = CheckSlope(hit);
                // has the 'isOnSlope' value changed?
                if (wasOnSlope != isOnSlope)
                {
                    if (onSlopeChanged != null)
                    {
                        onSlopeChanged.Invoke(isOnSlope);
                    }
                }
                // We have found our ground so exit the function
                // (No need to check any more hits)
                return true;
            }
            else
            {
                // We are no longer grounded
                isGrounded = false;
            }
            return false;
        }
        void DetectGround()
        {
            // record a copy of what isGrounded was
            bool wasGrounded = isGrounded;

            #region Ground Detection Logic

            Ray groundRay = new Ray(transform.position, Vector3.down);
            RaycastHit2D[] hits = Physics2D.RaycastAll(groundRay.origin, groundRay.direction, rayDistance);
            foreach (var hit in hits)
            {
                if (CheckGround(hit))
                {
                    // we found the ground! so exit the function
                    break;
                }

                // if hit collider is not null
                // reset currentJump
            }
            #endregion
            // Check if:
            if (wasGrounded != isGrounded && // IsGrounded has changed since before the detection AND
                onGroundedChanged != null) // Something is subscribed to this event
            {
                // Run all the things subscribed to event and give it "isGrounded" value
                onGroundedChanged.Invoke(isGrounded);
            }
        }


        void LimitVelocity()
        {
            // If Rigid's velocity (magnitude) is greater than maxVelocity
            if (rb.velocity.magnitude > maxVelocity)
            {
                rb.velocity = rb.velocity.normalized * maxVelocity;
            }
            // Set Rigid velocity to velocity normalized x maxVelocity
        }

        void Jump()
        {
            onJump.Invoke();
            if (currentJump < maxJumpCount)
            {
                currentJump++;
                rb.velocity = new Vector2(0, jumpHeight);
            }
        }

        void Crouch()
        {
            //onCrouch.Invoke();
        }

        void Climb()
        {

        }
        void Move()
        {
            Vector3 right = Vector3.Cross(Vector3.down, Vector3.forward);
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
}
