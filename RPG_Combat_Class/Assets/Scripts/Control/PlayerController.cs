using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using System;
using RPG.Core;

// This script is only used by the player character 
// RPG is simply used to make the namespace unique, could be anything
namespace RPG.Control
{

    // Moved the class into a namespace
    public class PlayerController : MonoBehaviour
    {
        Fighter fighter;
        Health health;

        private void Start() 
        {
            health = GetComponent<Health>();    
        }


        void Update()
        {
            if (health.IsDead()) return;
            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;
        }


        private bool InteractWithCombat()
        {
            //An Array of all raycast hits
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                //Teacher version
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();

                if (target == null) continue;

                if(!GetComponent<Fighter>().CanAttack(target.gameObject))
                {
                    continue;  //Continues through the foreach loop if true
                }

                

                if(Input.GetMouseButton(0))
                {
                    GetComponent<Fighter>().Attack(target.gameObject);
                }

                return true;  // Found a combat target to interact with
            }
            return false;  // did not find a combat targets to interact with
        }


        // 
        private bool InteractWithMovement()
        {
            RaycastHit hit;

            // RayCast: 
            // Must have the keyword "out" in front of hit
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);

            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    GetComponent<Mover>().StartMoveAction(hit.point, 1f);
                }
                return true;  // if it hits something return true
            }
            return false; // if it doe not hit anything return false
        }


        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}


