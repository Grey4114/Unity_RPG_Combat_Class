using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    //[SerializeField] Transform target;

    // Ray lastRay;

    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            MoveToCursor();

            // Raycast: Gets the from the camera to the mouse click position
            // lastRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        }
        UpdateAnimator();

        // Raycast: This will show the raycast line in Scene window during Play
        //Debug.DrawRay(lastRay.origin, lastRay.direction * 100);

        // GetComponent<NavMeshAgent>().destination = target.position;
    }



    private void MoveToCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // RayCast: 
        // Must have the keyword "out" in front of hit
        bool hasHit = Physics.Raycast(ray, out hit); 

        if (hasHit)
        {
            GetComponent<NavMeshAgent>().destination = hit.point;
        }
    }    
    
    private void UpdateAnimator()
    {
        Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);  // Takes from Global and converts into Local
        float speed = localVelocity.z;  // Forward motion
        GetComponent<Animator>().SetFloat("ForwardSpeed", speed);

    }
}
