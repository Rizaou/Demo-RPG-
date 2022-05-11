using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.SceneManagement
{

    public class Fade : MonoBehaviour
    {
        CanvasGroup canvasGroup;
        private void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();

            StartCoroutine(FadeOutIn());
        }

        public IEnumerator FadeOutIn()
        {
            yield return FadeOut(3f);
            Debug.Log("Fade Out");
            yield return FadeIn(1f);
            Debug.Log("Fade In");
        }


        IEnumerator FadeOut(float time)
        {
            while (canvasGroup.alpha < 1)
            {

                canvasGroup.alpha += Time.deltaTime / time;

                yield return null;

            }
        }


        IEnumerator FadeIn(float time)
        {
            while (canvasGroup.alpha > 0)
            {

                canvasGroup.alpha -= Time.deltaTime / time;

                yield return null;

            }
        }
    }

}