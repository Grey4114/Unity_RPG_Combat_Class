using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 3f;

        Fighter fighter;
        GameObject player;


        private void Start() 
        {
            fighter = GetComponent<Fighter>();
            player = GameObject.FindWithTag("Player");
        }


        private void Update()
        {
            if (InAttackRangeOfPlayer() && fighter.CanAttack(player))
            {
                fighter.Attack(player);
            }
            else
            {
                fighter.Cancel();
            }
        }


        // This calculates the distance from the Enemy to the Player
        private bool InAttackRangeOfPlayer()
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            return  distanceToPlayer < chaseDistance;
        }
    }
}

