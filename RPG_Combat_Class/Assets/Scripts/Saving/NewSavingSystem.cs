using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;


// This script controls all of the saving/loading for the save files
namespace RPG.Saving
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
                Transform playerTransform = GetPlayerTransform();  // get player position
                byte[] buffer = SerializeVector(playerTransform.position); // convert to byte array
                stream.Write(buffer, 0, buffer.Length); // write to save file
            }
        }




        //Loads all of the info to the file
        public void Load(string saveFile)
        {
            string path = GetPathFromSaveFile(saveFile);
            print("Loading from " + path);

            using (FileStream stream = new FileStream(path, FileMode.Open))
            {

                byte[] buffer = new byte[stream.Length];  // create empty byte array based on save file size 
                stream.Read(buffer, 0, buffer.Length);  // fill the buffer with the save file info
                Transform playerTransform = GetPlayerTransform();  // gets players current position
                playerTransform.position = DeserializeVector(buffer);  // Converts to a vector3 & loads new position
                
            }

        }


        // Get the players vectore3 position
        private Transform GetPlayerTransform()
        {
            return GameObject.FindWithTag("Player").transform;
        }

        // Convert the vector3 into an array of bytes
        private byte[] SerializeVector(Vector3 vector)
        {
            byte[] vectorBytes = new byte[3 * 4];
            BitConverter.GetBytes(vector.x).CopyTo(vectorBytes,0);
            BitConverter.GetBytes(vector.y).CopyTo(vectorBytes,4);
            BitConverter.GetBytes(vector.z).CopyTo(vectorBytes,8);
            return vectorBytes;
        }

        // Convert the array of bytes into a vector3
        private Vector3 DeserializeVector(byte[] buffer)
        {
            Vector3 result = new Vector3();
            result.x = BitConverter.ToSingle(buffer,0);
            result.y = BitConverter.ToSingle(buffer,4);
            result.z = BitConverter.ToSingle(buffer,8);
            return result;
        }


        // Returns the systems current default path/location for save files
        private string GetPathFromSaveFile(string saveFile)
        {
            // Saves the file to the system default location 
            return Path.Combine(Application.persistentDataPath, saveFile + ".sav");
        }
    }
}
