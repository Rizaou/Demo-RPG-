using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {

        public enum DestinationIdentifier
        {
            A, B, C, D, E, F
        }

        [SerializeField] private int sceneToLoad = -1;
        [SerializeField] private Transform spawnPos;
        public DestinationIdentifier destination;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                StartCoroutine(Transition());
            }
        }

        public Transform GetSpawnPoint => spawnPos;

        private IEnumerator Transition()
        {
            if (sceneToLoad < 0)
            {
                Debug.Log("sceneTOLoad is less than zero");
                yield break;
            }

            DontDestroyOnLoad(this.gameObject);
            yield return SceneManager.LoadSceneAsync(sceneToLoad);
            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);
            Destroy(gameObject);
        }

        private Portal GetOtherPortal()
        {
            foreach (Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal == this) continue;

                if (portal.destination == this.destination)
                {
                    return portal;
                }

            }

            return null;
        }

        private void UpdatePlayer(Portal portal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<NavMeshAgent>().Warp(portal.GetSpawnPoint.position);
            player.transform.rotation = portal.GetSpawnPoint.rotation;

        }
    }

}