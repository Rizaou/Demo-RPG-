using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Saving;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {

        private const string defaultSaveFile = "save";

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }
            else if(Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }
        }

        private void Save()
        {
            GetComponent<SavingSystem>().Save(defaultSaveFile);
        }

        private void Load()
        {
           GetComponent<SavingSystem>().Load(defaultSaveFile);
        }
    }

}