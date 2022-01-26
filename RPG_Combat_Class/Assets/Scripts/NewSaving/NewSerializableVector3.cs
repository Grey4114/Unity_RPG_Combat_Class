using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

// Object - Not attached to an object
// Script - Used by NewSavingSystem.cs

namespace RPG.NewSaving
{

    [System.Serializable] 
    public class NewSerializableVector3
    {
        float x, y, z;

        // Converts a vector3 into 3 variables
        public NewSerializableVector3(Vector3 vector)
        {
            x = vector.x;
            y = vector.y;
            z = vector.z;
        }

        // Convert binary into a Vector3
        public Vector3 NewToVector3()
        {
            return new Vector3(x, y, z);
        }
    }
}

