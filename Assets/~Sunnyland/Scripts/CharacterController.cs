using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            anim.SetBool("isCrouch", true);
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            anim.SetBool("isCrouch", false);
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            anim.SetBool("isRun", true);
        }
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            anim.SetBool("isRun", false);
        }

    }
}
