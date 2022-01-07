using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 3f;
        [SerializeField] float suspicionTime = 3f;

        Fighter fighter;
        Health health;
        GameObject player;
        Mover mover;
        Vector3 guardPosition;

        float timeSinceLastSawPlayer = Mathf.Infinity;

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
            
            if(health.IsDead()) return;
            
            if (InAttackRangeOfPlayer() && fighter.CanAttack(player))
            {
                timeSinceLastSawPlayer = 0;
                AttackBehaviour();
            }
            else if(timeSinceLastSawPlayer < suspicionTime)
            {
                SuspicionBehaviour();
            }
            else
            {
                GuardBehaviour();
            }

            timeSinceLastSawPlayer += Time.deltaTime;
        }

        private void GuardBehaviour()
        {
            // AI returns to their starting postion
            mover.StartMoveAction(guardPosition);
        }

        private void SuspicionBehaviour()
        {
            // AI Suspicion 
            // fighter.Cancel();  // My Solution
            GetComponent<ActionScheduler>().CancelCurrentAction();  // Teacher solution
        }

        private void AttackBehaviour()
        {
            fighter.Attack(player);
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

