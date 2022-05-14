using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace RPG.Saving
{
    public class SavingSystem : MonoBehaviour
    {
        public void Save(string saveFile)
        {
            string path = GetPathFromSaveFile(saveFile);

            Debug.Log("Save to " + path);
            using (FileStream stream = File.Open(path, FileMode.Create))
            {

                Transform playerTransform = GetPlayerTransform();

                byte[] playerByte = SerializeVector3(playerTransform.position);

                //byte[] bytes = Encoding.UTF8.GetBytes("Merhaba benim adım üüüüüüğüğüğüğ");

                stream.Write(playerByte, 0, playerByte.Length);

                // We don't need to use stream.Close anymore, because we use using statement.
                //stream.Close();
            }


        }

        private Transform GetPlayerTransform()
        {
            return GameObject.FindWithTag("Player").transform;
        }

        public void Load(string saveFile)
        {
            string path = GetPathFromSaveFile(saveFile);
            Debug.Log("Loading from " + saveFile);
            using (FileStream stream = File.Open(path, FileMode.Open))
            {
                byte[] bytes = new byte[stream.Length];

                stream.Read(bytes, 0, bytes.Length);

                Transform playerTransform = GetPlayerTransform();

                playerTransform.position = DeserializeVector3(bytes);

              
            }
        }


        private string GetPathFromSaveFile(string saveFile)
        {
            return Path.Combine(Application.persistentDataPath, saveFile + ".sav");
        }

        private byte[] SerializeVector3(Vector3 vector)
        {
            byte[] vectorBytes = new byte[3 * 4];
            BitConverter.GetBytes(vector.x).CopyTo(vectorBytes, 0);
            BitConverter.GetBytes(vector.y).CopyTo(vectorBytes, 4);
            BitConverter.GetBytes(vector.z).CopyTo(vectorBytes, 8);
            return vectorBytes;
        }

        private Vector3 DeserializeVector3(byte[] buffer)
        {
            Vector3 result = new Vector3();
            result.x = BitConverter.ToSingle(buffer, 0);
            result.y = BitConverter.ToSingle(buffer, 4);
            result.z = BitConverter.ToSingle(buffer, 8);

            return result;
        }

    }

}