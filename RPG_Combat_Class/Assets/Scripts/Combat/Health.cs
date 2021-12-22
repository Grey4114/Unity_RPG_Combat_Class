using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour 
    {
        [SerializeField] float health = 100f;

        public void TakeDamage(float damage)
        {
            // Teacher version
            health = Mathf.Max(health - damage, 0);
            print(health);

            // My way of reducing health and stopping at 0
            // if (health > 0)
            // {
            //     health -= damage;
            // }
        }

    }
}

