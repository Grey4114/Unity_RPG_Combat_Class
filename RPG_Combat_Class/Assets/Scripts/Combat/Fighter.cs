using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] float weaponDamage = 5f;

        Transform target;

        float timeSinceLastAttack = 0f;

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            // If there is no target then do not proceed
            if (target == null) return;

            // Move to the target and stops when in range
            // if (target != null && !GetIsInRange())
            if (!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(target.position);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();  // activates the attack animation
            }

        }


        private void AttackBehaviour()
        {
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                // This will trigger the Hit() event
                GetComponent<Animator>().SetTrigger("attack");
                timeSinceLastAttack = 0f;
            }
        }


        // Animation Event - Called from animator
        void Hit()
        {
            // NOTE - note damage is applied after animation is played
            Health healthComponent = target.GetComponent<Health>();
            healthComponent.TakeDamage(weaponDamage);
        }


        private bool GetIsInRange()
        {
            // Vector3.Distance(current position, target position)
            return Vector3.Distance(transform.position, target.position) < weaponRange;
        }


        // Attack the target
        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this); // Add notes 
            target = combatTarget.transform;
            // target.GetComponent<Health>().TakeDamage(20f);
            Debug.Log("Take that you dumb peasant!");
        }

        // This resets the target, so that the character does not get stuck on the target
        public void Cancel()
        {
           target = null; 
        }


        
        
    }
}
