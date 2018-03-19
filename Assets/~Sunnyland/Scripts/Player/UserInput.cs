using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SunnyLand
{
    [RequireComponent(typeof(CharacterController))]
    public class UserInput : MonoBehaviour
    {
        CharacterController controller;

        void Start()
        {
            controller = GetComponent<CharacterController>();
        }
        void Update()
        {
        }
        void DetectInput()
        {

        }
    }
}