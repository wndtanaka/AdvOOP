using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SunnyLand
{
    [RequireComponent(typeof(PlayerController))]
    public class PlayerAudio : MonoBehaviour
    {
        public AudioSource onHurtSound;

        private PlayerController m_Player;
        public PlayerController Player
        {
            get
            {
                if (m_Player == null)
                {
                    m_Player = GetComponent<PlayerController>();
                }
                return m_Player;
            }
        }

        // Use this for initialization
        void Start()
        {
            Player.onHurt += OnHurt;
        }

        // Update is called once per frame
        void Update()
        {

        }
        void OnHurt()
        {
            onHurtSound.Play();
        }
    }
}