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
            FileStream stream = File.Open(path, FileMode.Create);

            // Write Ascii code to file
            // stream.WriteByte(102); 

            // Write Hexadecimal code to file 
            //stream.WriteByte(0xc3);   // This and the next line are combined to form one charater
            //stream.WriteByte(0x80);

            // Challenge - write "!Hola Mundo!" in ascii & hex
            // One byte at a time
            stream.WriteByte(0xc2);
            stream.WriteByte(0xa1);
            stream.WriteByte(0x48);
            stream.WriteByte(0x6f);
            stream.WriteByte(0x6c);
            stream.WriteByte(0x61);
            stream.WriteByte(0x20);
            stream.WriteByte(0x4d);
            stream.WriteByte(0x75);
            stream.WriteByte(0x6e);
            stream.WriteByte(0x64);
            stream.WriteByte(0x6f);
            stream.WriteByte(0x21);

            stream.WriteByte(0x20);  //space

            // Writing a byte array
            byte[] array_1 = {0xc2, 0xa1, 0x48, 0x6f, 0x6c, 0x61, 0x20, 0x4d, 0x75, 0x6e, 0x64, 0x6f, 0x21};
            stream.Write(array_1, 0, 13);

            // Writing a byte array from a string
            byte[] bytes = Encoding.UTF8.GetBytes("!Hola Mundo!");
            stream.Write(bytes, 0, bytes.Length);

                
            stream.Close();  // Must have this after finished adding/removing from file
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
