using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Object - Attached to the character prefab, thus it is added to both the player and enemy prefabs

// This saves the info for each specific character that the script is attched to
namespace RPG.NewSaving
{

    public class NewSaveableEntity : MonoBehaviour 
    {
        public string GetUniqueIdentifier()
        {
            return "";
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
