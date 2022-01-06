using UnityEngine;
using RPG.Movement;
using UnityEngine.AI;

namespace RPG.Core
{
    public class Health : MonoBehaviour 
    {
        [SerializeField] float healthPoints = 100f;

        bool isDead = false;

        // This helps other classes to check the charaters state
        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);

            if (healthPoints == 0)
            {
                Die();
            }
        }

        private void Die()
        {
            if (isDead) return;
                
            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction(); // Stops anything that is running
        }
    }
}

