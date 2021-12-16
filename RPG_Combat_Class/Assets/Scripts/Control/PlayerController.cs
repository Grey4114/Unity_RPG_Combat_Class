using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using System;


// RPG is simply used to make the namespace unique, could be anything
namespace RPG.Control
{

    // Moved the class into a namespace
    public class PlayerController : MonoBehaviour
    {
        void Update()
        {
            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;
            // print("Nothing to do");
        }


        // 
        private bool InteractWithCombat()
        {
            //An Array of all raycast hits
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                //Teacher version
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if(target == null) continue;  //Continues through the loop is null

                if(Input.GetMouseButtonDown(0))
                {
                    GetComponent<Fighter>().Attack(target);
                }

                return true;  // Found a combat tarfet to interact with

                // My version
                // if (hit.transform.GetComponent<CombatTarget>())
                // {
                //     GetComponent<CombatTarget>().HitTarget();
                // }
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
                    GetComponent<Mover>().MoveTo(hit.point);
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


