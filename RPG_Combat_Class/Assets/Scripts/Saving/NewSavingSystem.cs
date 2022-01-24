using UnityEngine;


// This script controls all of the saving/loading to the save files
namespace RPG.Saving
{
    public class NewSavingSystem : MonoBehaviour 
    {
        // Saves the info to the file
        public void Save(string saveFile)
        {
            print("Saving to " + saveFile);
        }

        //Loads all of the info to the file
        public void Load(string saveFile)
        {
            print("Loading from " + saveFile);
        }
    }
}
