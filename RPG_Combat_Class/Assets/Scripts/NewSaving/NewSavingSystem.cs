using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;

// Object - Attached to the NewSavingSystem object, which is a child of the PersistanObjects prefab

// This script controls all of the saving/loading for the save files
namespace RPG.NewSaving
{
    public class NewSavingSystem : MonoBehaviour 
    {

        // Saves the info to the file
        public void Save(string saveFile)
        {
            string path = GetPathFromSaveFile(saveFile);
            print("Saving to " + path);

            // Working with Using
            // Once the Using statement is exited then the stream in closed automatically
            using (FileStream stream = File.Open(path, FileMode.Create))
            {
                // Using Unity's Built-in Serialization - BinaryFormatter
                BinaryFormatter formatter = new BinaryFormatter();  // This initializes the formatter
                formatter.Serialize(stream, CaptureState());  // Capture the objects state & Converts to binary               
            }
        }

        //Loads all of the info to the file
        public void Load(string saveFile)
        {
            string path = GetPathFromSaveFile(saveFile);
            print("Loading from " + path);

            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter(); // Initiallize the formatter               
                RestoreState(formatter.Deserialize(stream));  // Deserialize the stream & convert to 3 float positions               
            }
        }



        // Create a dictionary, captures each objects state and saves it with a unique identifier 
        private object CaptureState()
        {
            Dictionary<string, object> state = new Dictionary<string, object>();  // Sets up a dictionary

            // Finds all of the objects that are saveable
            foreach(NewSaveableEntity saveable in FindObjectsOfType<NewSaveableEntity>())
            {
                // Key = Identifier & Value = the objects' serialized states
                // This fills the dictionay with the state of each object
                state[saveable.GetUniqueIdentifier()] = saveable.CaptureState();
            }
            return state;
        }

        // Fill a dictionary with the save files info
        // Find each object and restore its state based on its unique identifier
        private void RestoreState(object state)
        {
            // Create an dictionary and fill with the info from the state object
            Dictionary<string, object> stateDict = (Dictionary<string, object>)state;

            foreach(NewSaveableEntity saveable in FindObjectsOfType<NewSaveableEntity>())
            {
                // fore each object Restore its state
                saveable.RestoreState(stateDict[saveable.GetUniqueIdentifier()]);
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
