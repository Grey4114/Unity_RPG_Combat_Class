using UnityEngine;
using RPG.Movement;
using UnityEngine.AI;
// using RPG.Saving;

namespace RPG.Core
{
    public class Health : MonoBehaviour  //, ISaveable 
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


        // ISavable Interface - Capturing Heatlh info to the save file
        public object CaptureState()
        {
            return healthPoints;
        }

        // ISavable Interface - Can restore/pull/load any object info from the save file
        // This is called after Awake() but before Start()
        public void RestoreState(object state)
        {
            healthPoints = (float)state; 
            
            if (healthPoints == 0)
            {
                Die();
            }


        }

    }
}

