using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

namespace RPG.Core
{   
    public class PersistantObjectsSpawner : MonoBehaviour 
    {
        [SerializeField] GameObject persistentObjectPrefab;

        static bool hasSpawned = false;

        private void Awake() 
        {
            if (hasSpawned) return;
            {
                SpawnPersistentObjects();

                hasSpawned = true;
            }

            
        }

        private void SpawnPersistentObjects()
        {
            GameObject persistentObject = Instantiate(persistentObjectPrefab);
            DontDestroyOnLoad(persistentObject);
            
        }
    }
}