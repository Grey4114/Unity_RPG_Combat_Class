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
        Transform target;

        private void Update()
        {
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
                GetComponent<Mover>().Stop();
            }

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
            Debug.Log("Take that you dumb peasant!");
        }

        // This resets the target, so that the character does not get stuck on the target
        public void Cancel()
        {
           target = null; 
        }
        
    }
}
