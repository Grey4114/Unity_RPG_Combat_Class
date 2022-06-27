using System.Collections;
using RPG.NewSaving;
using UnityEngine;

// Object - Attached to the NewSavingSystem object, which is a child of the PersistanObjects prefab

namespace RPG.SceneManagement
{
    // This script activates the saving/loading of info based on key presses
    public class NewSavingWrapper : MonoBehaviour 
    {
        const string defaultSaveFile = "newSave";

        [SerializeField] float fadeInTime = 0.2f;

        // Launch the app and load from a savefile
        private IEnumerator Start() 
        {
            // Load fader
            Fader fader = FindObjectOfType<Fader>();

            // Fade out screen
            fader.FadoutImmediate();

            // loads the last saved scene 
            yield return GetComponent<NewSavingSystem>().LoadLastScene(defaultSaveFile);

            // Fade into scene - part of coroutine,  so needs yield return
            yield return fader.FadeIn(fadeInTime);
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                // Note - Teacher is using Save() as methode name
                NewLoad();
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                // Note - Teacher is using Load() as methode name
                NewSave();
            }

        }

        // Note - Teacher is using Load() as methode name
        public void NewLoad()
        {
            GetComponent<NewSavingSystem>().Load(defaultSaveFile);
        }

        // Note - Teacher is using Save() as methode name
        public void NewSave()
        {
            GetComponent<NewSavingSystem>().Save(defaultSaveFile);
        }
    }
}