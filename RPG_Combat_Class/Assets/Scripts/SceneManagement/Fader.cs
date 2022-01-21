using UnityEngine;
using System.Collections;

namespace RPG.SceneManagement
{
    
    public class Fader : MonoBehaviour 
    {
        CanvasGroup canvasGroup;

        public void Awake()     // Changed from Start to Awake
        {
            canvasGroup = GetComponent<CanvasGroup>();
            // StartCoroutine(FadeOutIn());      
        }


        // IEnumerator FadeOutIn()
        // {
        //     yield return FadeOut(3f);
        //     print("Fadeout");
        //     yield return FadeIn(3f);
        //     print("FadeIn");
        // }


        // This is for Loading a save game from launching the game
        // Loads the game with the screen faded out
        public void FadoutImmediate()
        {
            canvasGroup.alpha = 1;
        }

        public IEnumerator FadeOut(float time) 
        {
            while(canvasGroup.alpha < 1) 
            {
                canvasGroup.alpha += Time.deltaTime / time;
                yield return null;
            }
        }


        public IEnumerator FadeIn(float time) 
        {
            while(canvasGroup.alpha > 0) 
            {
                canvasGroup.alpha -= Time.deltaTime / time;
                yield return null;
            }
        }
    }
}