using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class PersistentObject : MonoBehaviour
    {
        [SerializeField] private GameObject persitentObjectPrefab;
        public static bool hasCreated = false;

        private void Start()
        {
            if (!hasCreated)
            {
                GameObject obj = Instantiate(persitentObjectPrefab);
                hasCreated = true;
            }
        }
    }

}