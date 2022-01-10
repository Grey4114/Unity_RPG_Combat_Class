using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using System;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 3f;
        [SerializeField] float suspicionTime = 3f;
        [SerializeField] PatrolPath patrolPath = null;
        [SerializeField] float wayPointTolerance = 2f;  // Increase is AI's stop following their path
        [SerializeField] float waypointDwellTime = 3f;        

        Fighter fighter;
        Health health;
        GameObject player;
        Mover mover;
        Vector3 guardPosition;

        float timeSinceLastSawPlayer = Mathf.Infinity;
        int currentWayPointIndex = 0;

        float timeSinceArrivedAtWaypoint = Mathf.Infinity;


        private void Start() 
        {
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            player = GameObject.FindWithTag("Player");
            mover = GetComponent<Mover>();

            guardPosition = transform.position;
        }


        private void Update()
        {

            if (health.IsDead()) return;

            if (InAttackRangeOfPlayer() && fighter.CanAttack(player))
            {
                AttackBehaviour();
            }
            else if (timeSinceLastSawPlayer < suspicionTime)
            {
                SuspicionBehaviour();
            }
            else
            {
                PatrolBehaviour();
            }

            UpdateTimers();
        }

        private void AttackBehaviour()
        {
            timeSinceLastSawPlayer = 0;
            fighter.Attack(player);
        }

        private void SuspicionBehaviour()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void PatrolBehaviour()
        {
            Vector3 nextPosition = guardPosition;
            if (patrolPath != null)
            {   
                if (AtWayPoint())
                {
                    timeSinceArrivedAtWaypoint = 0;
                    CycleWayPoint();
                }
                nextPosition = GetCurrentWayPoint();
            }

            if (timeSinceArrivedAtWaypoint > waypointDwellTime)
            {
                // AI moves to the next postion
                mover.StartMoveAction(nextPosition);
            }


        }

        private void UpdateTimers()
        {
            timeSinceLastSawPlayer += Time.deltaTime;
            timeSinceArrivedAtWaypoint += Time.deltaTime;
        }

        // Path Patrolling
        private bool AtWayPoint()
        {
            float distanceToWayPoint = Vector3.Distance(transform.position, GetCurrentWayPoint());
            return distanceToWayPoint < wayPointTolerance;
        }

        // Path Patrolling 
        private void CycleWayPoint()
        {
            currentWayPointIndex = patrolPath.GetNextIndex(currentWayPointIndex);          
        }

        // Path Patrolling 
        private Vector3 GetCurrentWayPoint()
        {
            return patrolPath.GetWayPoint(currentWayPointIndex);
        }

        // This calculates the distance from the Enemy to the Player
        private bool InAttackRangeOfPlayer()
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            return  distanceToPlayer < chaseDistance;
        }

        // Show Gizmos - Called by Unity in the editor
        private void OnDrawGizmosSelected() 
        {
            Gizmos.color = Color.blue; // Pick a pre-set color
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}

