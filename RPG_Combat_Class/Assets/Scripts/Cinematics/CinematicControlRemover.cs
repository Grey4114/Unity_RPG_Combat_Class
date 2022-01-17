using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using RPG.Core;
using RPG.Control;


namespace RPG.Cinematics
{

    public class CinematicControlRemover : MonoBehaviour
    {
        GameObject player;

        private void Start() 
        {

            GetComponent<PlayableDirector>().played += DisableControl;
            GetComponent<PlayableDirector>().stopped += EnableControl;
        }

        void DisableControl(PlayableDirector aDirector)
        {
            player = GameObject.FindWithTag("Player");
            player.GetComponent<ActionScheduler>().CancelCurrentAction();
            player.GetComponent<PlayerController>().enabled = false;
        }

        void EnableControl(PlayableDirector aDirector)
        {
            // print("Enabled");
            player.GetComponent<PlayerController>().enabled = true;
        }

    }

}
