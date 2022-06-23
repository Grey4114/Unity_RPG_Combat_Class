using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;


// Object - Attached to the character prefab, thus it is added to both the player and enemy prefabs

// This saves the info for each specific character that the script is attched to
namespace RPG.NewSaving
{


    [ExecuteAlways]
    public class NewSaveableEntity : MonoBehaviour 
    {
        // Creates a random unique string id
        // System.Guid.NewGuid().ToString() - This is a builtin unity function
        [SerializeField] string uniqueIdentifier = "";


// This allows for this code to be built without issue 
# if UNITY_EDITOR         
        private void Update() 
        {
            if (Application.IsPlaying(gameObject)) return;

            if (string.IsNullOrEmpty(gameObject.scene.path)) return;

            // this = the oject the script is attached to
            SerializedObject serializedObject = new SerializedObject(this);
            SerializedProperty property = serializedObject.FindProperty("uniqueIdentifier");


            // The systems generates a unique ID for the object if the field is blank
            if (string.IsNullOrEmpty(property.stringValue))
            {
                property.stringValue = System.Guid.NewGuid().ToString(); // Generates the ID
                serializedObject.ApplyModifiedProperties();  // Tells Unity that the field has been modified

            }
        }
# endif

        public string GetUniqueIdentifier()
        {
            return uniqueIdentifier;
        }  

        // Captures the current state of the object to be saved
        public object CaptureState()
        {
            // print("Capturing state for " + GetUniqueIdentifier());
            // return null;

            return new NewSerializableVector3(transform.position);
        }

        // Restores the object to a previous state
        public void RestoreState(object state)
        {
            // pull the vector3 state and saves to new vector3
            NewSerializableVector3 position = (NewSerializableVector3)state;
            
            // Disables NavMeshAgent
            GetComponent<NavMeshAgent>().enabled = false;

            // Moves character to new position
            transform.position = position.NewToVector3();

            // Cancels the characters current action
            GetComponent<ActionScheduler>().CancelCurrentAction();

            // Enables NavMeshAgent
            GetComponent<NavMeshAgent>().enabled = true;
            

        }

    }
}
