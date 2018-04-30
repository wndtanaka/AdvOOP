using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SunnyLand
{
    [RequireComponent(typeof(PlayerController))]
    public class UserInput : MonoBehaviour
    {
        private PlayerController player;

        public float inputH, inputV;
        private bool isJumping;
        private bool isCrouching;

        // Use this for initialization
        void Start()
        {
            player = GetComponent<PlayerController>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                // Hurt yourself when you press U
                player.Hurt(10);
            }
            GetInput();
            player.Move(inputH);
            player.Climb(inputV);
            if (isJumping)
            {
                player.Jump();
            }
            if (isCrouching)
            {
                player.Crouch();
            }
            else
            {
                player.UnCrouch();
            }
        }

        void GetInput()
        {
            inputH = Input.GetAxisRaw("Horizontal");
            inputV = Input.GetAxisRaw("Vertical");
            isJumping = Input.GetKeyDown(KeyCode.Space);
            isCrouching = Input.GetKeyDown(KeyCode.LeftControl);
        }
    }
}