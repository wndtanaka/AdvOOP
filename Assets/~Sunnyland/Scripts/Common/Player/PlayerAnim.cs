using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SunnyLand
{
    [RequireComponent(typeof(PlayerController))]
    [RequireComponent(typeof(Animator))]
    public class PlayerAnim : MonoBehaviour
    {
        private PlayerController player;
        private Animator anim;
        private Rigidbody2D rigid;

        #region Unity Functions
        void Start()
        {
            anim = GetComponent<Animator>();
            player = GetComponent<PlayerController>();
            rigid = GetComponent<Rigidbody2D>();
            // Subscribe animator to player events
            player.onGroundedChanged += OnGroundedChanged;
            player.onJump += OnJump;
            player.onHurt += OnHurt;
            player.onMove += OnMove;
            player.onClimb += OnClimb;
        }
        #endregion
        void Update()
        {
            anim.SetBool("isGrounded", player.isGrounded);
            anim.SetBool("isClimb", player.isCrouching);
            anim.SetBool("isCrouch", player.isCrouching);
            anim.SetFloat("jumpY", rigid.velocity.normalized.y);
        }
        #region Custom Functions
        void OnJump()
        {

        }
        void OnHurt()
        {
            anim.SetTrigger("hurt");
        }
        void OnMove(float input)
        {
            anim.SetBool("isRun", input != 0);
        }
        void OnClimb(float input)
        {
            anim.SetFloat("climbY", Mathf.Abs(input));
        }
        void OnGroundedChanged(bool isGrounded)
        {
            if (isGrounded)
            {
                print("I'm grounded :(");
            }
            else
            {
                print("I'm not grounded! :)");
            }
        }
        #endregion
    }
}