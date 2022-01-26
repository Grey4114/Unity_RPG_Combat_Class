using UnityEngine;

// Object - Attached to the NewSavingSystem object, which is a child of the PersistanObjects prefab

namespace RPG.NewSaving
{
    // This script activates the saving/loading of info based on key presses
    public class NewSavingWrapper : MonoBehaviour 
    {
        const string defaultSaveFile = "newSave";


        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                GetComponent<NewSavingSystem>().Load(defaultSaveFile);
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                GetComponent<NewSavingSystem>().Save(defaultSaveFile);
            }
            
        }

    }
}