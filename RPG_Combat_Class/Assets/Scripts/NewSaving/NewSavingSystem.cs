using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;


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
                Transform playerTransform = GetPlayerTransform();  // get player position
                BinaryFormatter formatter = new BinaryFormatter();  // This initializes the formatter
                NewSerializableVector3 position = new NewSerializableVector3(playerTransform.position); // serailizes the vector 3
                formatter.Serialize(stream, position);  // Converts to binary               
            }
        }




        //Loads all of the info to the file
        public void Load(string saveFile)
        {
            string path = GetPathFromSaveFile(saveFile);
            print("Loading from " + path);

            using (FileStream stream = new FileStream(path, FileMode.Open))
            {

                Transform playerTransform = GetPlayerTransform();  
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Deserialize(stream);
                NewSerializableVector3 position = (NewSerializableVector3)formatter.Deserialize(stream);
                playerTransform.position = position.NewToVector3();
                
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
