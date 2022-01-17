using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;


// This script is used by both the player fighter and the AI characters
namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] float weaponDamage = 5f;

        Health target;
        
        // bool canAttack = false;

        float timeSinceLastAttack = Mathf.Infinity;  // Using Infinity makes bool comparison true

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            // If there is no target then do not proceed
            if (target == null) return;

            // If the bool is true == dead, then do not proceed
            if (target.IsDead()) return;

            // Move to the target and stops when in range
            if (!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(target.transform.position, 1f);
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
                // This make the plaer look at the target before attacking
                transform.LookAt(target.transform);

                // This will trigger the Hit() event
                TriggerAttack();
                timeSinceLastAttack = 0f;
            }
        }


        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("stopAttack");
            GetComponent<Animator>().SetTrigger("attack");
        }


        // Animation Event - Called from animator
        void Hit()
        {
            if(target == null) return;  // prevent a null target bug
            
            // NOTE - note damage is applied after animation is played
            target.TakeDamage(weaponDamage);
        }


        private bool GetIsInRange()
        {
            // Vector3.Distance(current position, target position)
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }


        // 
        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null) {return false;}
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
        }

        // Attack the target
        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this); // Add notes 
            target = combatTarget.GetComponent<Health>();
        }

        // This resets the target, so that the character does not get stuck on the target
        public void Cancel()
        {
            StopAttack();
            target = null;
            GetComponent<Mover>().Cancel();
        }

        private void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
        }
    }
}
