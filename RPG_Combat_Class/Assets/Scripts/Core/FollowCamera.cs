using UnityEngine;

namespace RPG.Core
{
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] Transform target;

        // LateUpdate - Check order of excution, followe Mover update
        void LateUpdate()
        {
            transform.position = target.position;
        }
    }

}


