using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerAnim : MonoBehaviour
{
    CharacterController controller;
    Animator anim;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        controller.onMove += OnMove;
        controller.onJump += OnJump;
        controller.onClimb += OnClimb;
        controller.onCrouch += OnCrouch;
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
