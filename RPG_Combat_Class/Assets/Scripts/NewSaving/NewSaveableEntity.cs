using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using RPG.Movement;
using UnityEngine.AI;
using RPG.Core;


// Object - Attached to the character prefab, thus it is added to both the player and enemy prefabs

// This saves the info for each specific character that the script is attched to
namespace RPG.NewSaving
{

    [ExecuteAlways]
    public class NewSaveableEntity : MonoBehaviour
    {
        // Creates a random unique string id
        // System.Guid.NewGuid().ToString() - This is a builting unity function
        [SerializeField] string uniqueIdentifier = "";




// # if UNITY_EDITOR  // Uncomment before building - This allows for this code to be built without issue   
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
                serializedObject.ApplyModifiedProperties();  // Tells Unity that the field has been modified so that it does not update it

            }
        }
// #endif

        // Returns the objects unique identifier
        public string GetUniqueIdentifier()
        {
            return uniqueIdentifier;
        }  

        // Captures the objects state and returns a serializable vector3 info
        public object CaptureState()
        {
            return new NewSerializableVector3(transform.position);
        }

        // Restores the previous state of the object and cancles its movement
        public void RestoreState(object state)
        {
            NewSerializableVector3 position = (NewSerializableVector3)state;
            GetComponent<NavMeshAgent>().enabled = false;
            transform.position = position.NewToVector3();
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

    }
}
