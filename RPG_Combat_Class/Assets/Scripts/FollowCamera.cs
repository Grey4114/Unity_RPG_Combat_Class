using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] Transform target;

    // LateUpdate - Check order of excution, followe Mover update
    void LateUpdate()
    {
        transform.position = target.position;
    }
}
