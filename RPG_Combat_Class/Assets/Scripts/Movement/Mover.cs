﻿using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using RPG.Core;
using UnityEngine;
using UnityEngine.AI;


namespace RPG.Movement
{
    // NOTE - Cnnot inherit from more then 1 class, 
    // but can inherit from as many interfaces (ie. IAction) as needed
    public class Mover : MonoBehaviour, IAction
    {
        [SerializeField] Transform target;

        NavMeshAgent navMeshAgent;

        private void Start() 
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        void Update()
        {
            UpdateAnimator();
        }

        public void StartMoveAction(Vector3 destination)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            // Cancel();  // Cancels fighting before moving
            // GetComponent<Fighter>().Cancel();  // Cancels fighting before moving
            MoveTo(destination);
        }

        // Made it public so it can be called from outstide of 
        // the mover class
        public void MoveTo(Vector3 destination)
        {
            navMeshAgent.destination = destination;
            navMeshAgent.isStopped = false;
        }

        public void Stop()
        {
            navMeshAgent.isStopped = true; 
        }

        // NOTE - This method is required to allow the script to work with IAction
        public void Cancel()
        {
            GetComponent<Fighter>().Cancel();
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);  // Takes from Global and converts into Local
            float speed = localVelocity.z;  // Forward motion
            GetComponent<Animator>().SetFloat("ForwardSpeed", speed);

        }
    }
}



