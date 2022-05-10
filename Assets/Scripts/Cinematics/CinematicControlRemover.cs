using System.Collections;
using System.Collections.Generic;
using UnityEngine.Playables;

using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Cinematics
{
    public class CinematicControlRemover : MonoBehaviour
    {
        private void Start()
        {
            GetComponent<PlayableDirector>().played += DisableControl;
            GetComponent<PlayableDirector>().stopped += EnableControl;
        }
        void DisableControl(PlayableDirector pd)
        {
           GameObject player = GameObject.FindWithTag("Player");
           player.GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        void EnableControl(PlayableDirector pd)
        {
            Debug.Log("EnableControl");
        }
    }

}