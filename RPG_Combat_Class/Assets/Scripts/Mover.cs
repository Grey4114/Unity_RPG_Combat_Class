using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    [SerializeField] Transform target;

    // Ray lastRay;

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            MoveToCursor();

            // Raycast: Gets the from the camera to the mouse click position
            // lastRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        }
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
}
