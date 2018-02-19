/*=============================================
-----------------------------------
Copyright (c) 2018 Wendi Tanaka
-----------------------------------
@file: Spawner.cs>
@date: 19/07/2018
@author: Wendi Tanaka
@brief: Helper script for updating agent's target position
===============================================*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Delegates
{
    public class FollowTarget : MonoBehaviour
    {
        public Transform target;

        private NavMeshAgent agent;
        // Use this for initialization
        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        // Update is called once per frame
        void Update()
        {
            agent.SetDestination(target.position);
        }
    }
}