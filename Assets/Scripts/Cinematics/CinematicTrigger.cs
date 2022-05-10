using System.Collections;
using System.Collections.Generic;
using UnityEngine.Playables;
using UnityEngine;

namespace RPC.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
        private bool isPlayed = false;
        private void OnTriggerEnter(Collider other)
        {
            if (!isPlayed && other.gameObject.tag == "Player")
            {
                GetComponent<PlayableDirector>().Play();
                isPlayed = true;
            }
        }
    }

}