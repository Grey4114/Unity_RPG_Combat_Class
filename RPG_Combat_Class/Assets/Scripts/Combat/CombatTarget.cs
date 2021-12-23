using UnityEngine;


namespace RPG.Combat
{
    
    // Targets that have this script must have a health component script, and adds one if needed
    [RequireComponent(typeof(Health))]  
    public class CombatTarget : MonoBehaviour 
    {


    }
}
