using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


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


# if UNITY_EDITOR       // This allows for this code to be built without issue   
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
#endif

        public string GetUniqueIdentifier()
        {
            return uniqueIdentifier;
        }  

        public object CaptureState()
        {
            print("Capturing state for " + GetUniqueIdentifier());
            return null;
        }

        public void RestoreState(object state)
        {
            print("Restoring state for " + GetUniqueIdentifier());
        }

    }
}
