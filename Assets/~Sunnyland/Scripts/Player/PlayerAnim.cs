using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SunnyLand
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerAnim : MonoBehaviour
    {
        CharacterController controller;
        Animator anim;

        void Start()
        {
            controller = GetComponent<CharacterController>();
            anim = GetComponent<Animator>();
            controller.onGroundedChanged += OnGroundedChanged;
        }

        public void OnGroundedChanged(bool isGrounded)
        {
            if (isGrounded)
            {
                Debug.Log("Teehee");
            }
            else
            {
                Debug.Log("Doom");
            }
        }
        public void OnMove()
        {
            anim.SetBool("isRun", true);
        }

        public void OnCrouch()
        {
            anim.SetBool("isCrouch", true);
        }

        public void OnJump()
        {
            anim.SetBool("isJump", true);
        }

        public void OnClimb()
        {

        }
    }
}