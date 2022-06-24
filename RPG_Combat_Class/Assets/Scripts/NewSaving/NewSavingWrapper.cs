using UnityEngine;

// Object - Attached to the NewSavingSystem object, which is a child of the PersistanObjects prefab

namespace RPG.NewSaving
{
    // This script activates the saving/loading of info based on key presses
    public class NewSavingWrapper : MonoBehaviour 
    {
        const string defaultSaveFile = "newSave";

        private void Start() 
        {
            
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                // Note - Teacher is using Save() as methode name
                NewSave();
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                // Note - Teacher is using Load() as methode name
                NewLoad();
            }

        }

        // Note - Teacher is using Load() as methode name
        public void NewLoad()
        {
            GetComponent<NewSavingSystem>().Save(defaultSaveFile);
        }

        // Note - Teacher is using Save() as methode name
        public void NewSave()
        {
            GetComponent<NewSavingSystem>().Load(defaultSaveFile);
        }
    }
}