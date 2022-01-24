using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;


// This script controls all of the saving/loading to the save files
namespace RPG.Saving
{
    public class NewSavingSystem : MonoBehaviour 
    {



        // Saves the info to the file
        public void Save(string saveFile)
        {
            string path = GetPathFromSaveFile(saveFile);
            print("Saving to " + path);

            // Method 1 - Open and Close
            // Writing a byte array from a string - this is prone to not closing the stream when an error occurs 
            // FileStream stream = File.Open(path, FileMode.Create);
            // byte[] bytes = Encoding.UTF8.GetBytes("!Hola Mundo!");
            // stream.Write(bytes, 0, bytes.Length);
            // stream.Close();  // Must have this after finished adding/removing from file


            // Working with Using
            // Once the Using statement is exited then the stream in closed automatically
            using (FileStream stream = File.Open(path, FileMode.Create))
            {
                byte[] bytes = Encoding.UTF8.GetBytes("!Hola Mundo!");
                stream.Write(bytes, 0, bytes.Length);
            }
        }

        //Loads all of the info to the file
        public void Load(string saveFile)
        {
            print("Loading from " + GetPathFromSaveFile(saveFile));
        }


        private string GetPathFromSaveFile(string saveFile)
        {
            // Saves the file to the system default location 
            return Path.Combine(Application.persistentDataPath, saveFile + ".sav");
        }
    }
}
