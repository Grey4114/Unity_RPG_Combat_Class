using UnityEngine;
using RPG.Movement;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour 
    {
        [SerializeField] float weaponRange = 2f;
        Transform target;

        private void Update() 
        {
            // Move to the target
            if (target != null)
            {
                GetComponent<Mover>().MoveTo(target.position);
            }
            
        }


        // Attack the target
        public void Attack(CombatTarget combatTarget)
        {
            target = combatTarget.transform;
            Debug.Log("Take that you dumb peasant!");
        }
        
    }
}
