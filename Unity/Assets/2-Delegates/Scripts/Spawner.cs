/*=============================================
-----------------------------------
Copyright (c) 2018 Wendi Tanaka
-----------------------------------
@file: Spawner.cs>
@date: 19/07/2018
@author: Wendi Tanaka
@brief: Script for spawning objects via delegates
===============================================*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Delegates
{
    public class Spawner : MonoBehaviour
    {
        public Transform target;
        public GameObject trollPrefab, orcPrefab;
        public float spawnAmount = 1; // Spawn amount for each prefab
        public float spawnRate = 2f; // 

        private float spawnTimer = 0f;

        public delegate void SpawnDelegate();
        public SpawnDelegate spawnCallback;

        // Use this for initialization
        void Start()
        {
            // Subscribe to the function
            spawnCallback += SpawnOrc;
            spawnCallback += SpawnTroll;
        }

        // Update is called once per frame
        void Update()
        {
            // count up the timer
            spawnTimer += Time.deltaTime;
            // has the timer reached spawn rate?
            if (spawnTimer >= spawnRate)
            {
                // loop through and spawn orcs and trolls
                for (int i = 0; i < spawnAmount; i++)
                {
                    spawnCallback();
                }
                // Reset spawn timer
                spawnTimer = 0f;
            }
        }
        void SpawnOrc()
        {
            GameObject clone = Instantiate(orcPrefab, transform.position, transform.rotation);

            FollowTarget agent = clone.GetComponent<FollowTarget>();
            agent.target= target;
        }

        void SpawnTroll()
        {
            GameObject clone = Instantiate(trollPrefab, transform.position, transform.rotation);

            FollowTarget agent = clone.GetComponent<FollowTarget>();
            agent.target = target;
        }
    }
}