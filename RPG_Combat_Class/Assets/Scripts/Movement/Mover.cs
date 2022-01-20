using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;
using UnityEngine.AI;
using RPG.Saving;


namespace RPG.Movement
{
    // NOTE - Cannot inherit from more then 1 class, 
    // but can inherit from as many interfaces (ie. IAction or ISavable) as needed
    public class Mover : MonoBehaviour, IAction, ISaveable
    {
        [SerializeField] Transform target;
        [SerializeField] float maxSpeed = 6f;

        NavMeshAgent navMeshAgent;
        Health health;

        private void Start() 
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            health = GetComponent<Health>();

        }

        void Update()
        {
            navMeshAgent.enabled = !health.IsDead();  // Disbale the collider capsule
            UpdateAnimator();
        }

        public void StartMoveAction(Vector3 destination, float speedFraction)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            // GetComponent<Fighter>().Cancel();  // Cancels fighting before movingok
            MoveTo(destination, speedFraction);
        }

        // Made it public so it can be called from outstide of 
        // the mover class
        public void MoveTo(Vector3 destination, float speedFraction)
        {
            navMeshAgent.destination = destination;
            navMeshAgent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
            navMeshAgent.isStopped = false;
        }

        // NOTE - This method is required to allow the script to work with IAction
        // Renamed Stop to Cancel and removed Cancel
        public void Cancel()
        {
            navMeshAgent.isStopped = true; 
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);  // Takes from Global and converts into Local
            float speed = localVelocity.z;  // Forward motion
            GetComponent<Animator>().SetFloat("ForwardSpeed", speed);

        }


        // ISavable Interface - Can capture/input any object info to the save file
        public object CaptureState()
        {
            // Saves the position of the character
            return new SerializableVector3(transform.position);
        }

        // ISavable Interface - Can restore/pull/load any object info from the save file
        // This is called after Awake() but before Start()
        public void RestoreState(object state)
        {
            SerializableVector3 position = (SerializableVector3)state;
            GetComponent<NavMeshAgent>().enabled = false;   // Disable character movement
            transform.position = position.ToVector();   // Get position of character
            GetComponent<NavMeshAgent>().enabled = true;    // // Enable character movement
        }
    }
}



