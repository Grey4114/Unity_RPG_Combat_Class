using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;

namespace RPG.SceneManagement

{

    public class SavingWrapper : MonoBehaviour
    {
        const string defaultSaveFile = "save";
        [SerializeField] float fadeInTime = 0.2f;

        IEnumerator Start() 
        {
            Fader fader = FindObjectOfType<Fader>();
            fader.FadoutImmediate();
            yield return GetComponent<SavingSystem>().LoadLastScene(defaultSaveFile);
            yield return fader.FadeIn(fadeInTime);
        }

        // void Update()
        // {
        //     // Loading a saved game
        //     if (Input.GetKeyDown(KeyCode.L))
        //     {
        //         Load();
        //     }

        //     // Save a game
        //     if (Input.GetKeyDown(KeyCode.S))
        //     {
        //         Save();
        //     }

        // }

        // call to the saving system and tell it what to load
        public void Load()
        {
            GetComponent<SavingSystem>().Load(defaultSaveFile);
        }

        // call to the saving system and tell it what to save
        public void Save()
        {
            GetComponent<SavingSystem>().Save(defaultSaveFile);
        }


    }
}


