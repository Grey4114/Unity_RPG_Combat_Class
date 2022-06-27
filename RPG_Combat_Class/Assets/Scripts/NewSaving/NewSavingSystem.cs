using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

// Object - Attached to the NewSavingSystem object, which is a child of the PersistanObjects prefab

// This script controls all of the saving/loading for the save files
namespace RPG.NewSaving
{
    public class NewSavingSystem : MonoBehaviour 
    {
        public IEnumerator LoadLastScene(string saveFile)
        {
            // get the current save state
            Dictionary<string, object> state = LoadFile(saveFile);

            // Check if the save file exists or has an scene index key value
            if(state.ContainsKey("lastSceneBuildIndex"))
            {
                // Get the last scene index number (cast into an int)
                int buildIndex = (int)state["lastSceneBuildIndex"];

                // if not the current scene,  load scene
                if (buildIndex != SceneManager.GetActiveScene().buildIndex)
                {
                    // Load the last scene
                    yield return SceneManager.LoadSceneAsync(buildIndex);
                }
            }

            // Restore the state
            RestoreState(state);

        }

        // Saves the new position info to the savefile
        public void Save(string saveFile)
        {
            // Load the current save file
            Dictionary<string, object> state = LoadFile(saveFile);

            // Captures the new state of the objects and write that info to the state object
            CaptureState(state);

            // Saves the positions to the save file 
            SaveFile(saveFile, state);   
        }


        //Loads all of the info to the file
        public void Load(string saveFile)
        {
            RestoreState(LoadFile(saveFile));
        }


        private Dictionary<string, object> LoadFile(string saveFile)
        {
            string path = GetPathFromSaveFile(saveFile);
            
            // Check to see if file does not exist
            if (!File.Exists(path))
            {
                // return a new dictonary if there is no file
                return new Dictionary<string, object>();
            }

            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter(); // Initiallize the formatter               
                //RestoreState(formatter.Deserialize(stream));  // Deserialize the stream & convert to 3 float positions
                return (Dictionary<string, object>)formatter.Deserialize(stream); 
            }
        }

        private void SaveFile(string saveFile, object state)
        {
            string path = GetPathFromSaveFile(saveFile);
            // print("Saving to " + path);

            // Working with Using
            // Once the Using statement is exited then the stream in closed automatically
            using (FileStream stream = File.Open(path, FileMode.Create))
            {
                // Using Unity's Built-in Serialization - BinaryFormatter
                BinaryFormatter formatter = new BinaryFormatter();  // This initializes the formatter
                formatter.Serialize(stream, state);  // Get the objects state & Converts to binary               
            }
        }



        // Create a dictionary, captures each objects state and saves it with a unique identifier 
        private void CaptureState(Dictionary<string, object> state)
        {
            // Sets up a dictionary
            // Dictionary<string, object> state = new Dictionary<string, object>();  

            // Finds all of the objects that are saveable
            foreach(NewSaveableEntity saveable in FindObjectsOfType<NewSaveableEntity>())
            {
                // Key = Identifier & Value = the objects' serialized states
                // This fills the dictionay with the state of each object
                state[saveable.GetUniqueIdentifier()] = saveable.CaptureState();
            }
            // capture scene index number
            state["lastSceneBuildIndex"] = SceneManager.GetActiveScene().buildIndex;
        }

        // Fill a dictionary with the save files info
        // Find each object and restore its state based on its unique identifier
        private void RestoreState(Dictionary<string, object> state)
        {
            // Create an dictionary and fill with the info from the state object
            // Dictionary<string, object> stateDict = (Dictionary<string, object>)state;

            foreach(NewSaveableEntity saveable in FindObjectsOfType<NewSaveableEntity>())
            {
                // for each object Restore its state
                string id = saveable.GetUniqueIdentifier();

                // Check to see if there is a file, 
                // by checking if the dictionary contains a key
                if (state.ContainsKey(id))
                {
                    saveable.RestoreState(state[id]);
                }
            }

        }

        // Returns the systems current default path/location for save files
        private string GetPathFromSaveFile(string saveFile)
        {
            // Saves the file to the system default location 
            return Path.Combine(Application.persistentDataPath, saveFile + ".sav");
        }
    }
}
