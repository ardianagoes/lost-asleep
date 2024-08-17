using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class FadeInScript : MonoBehaviour
{
    public bool fadeIn;
    public CanvasGroup canvas2;
    public AudioSource gameSoundtrack;
    // Start is called before the first frame update
    void Start()
    {
        fadeIn = true;
        gameSoundtrack.volume = 0.5f;
        gameSoundtrack.pitch = 0.5f;
        gameSoundtrack.Play();
        canvas2.GetComponent<CanvasGroup>().alpha = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetSceneByName("loadingScene").isLoaded || SceneManager.GetSceneByName("scene3").isLoaded)
        {
            if (fadeIn)
            {
                canvas2.GetComponent<CanvasGroup>().alpha -= 0.5f * Time.deltaTime;
            }
            if (canvas2.GetComponent<CanvasGroup>().alpha <= 0)
            {
                fadeIn = false;
            }

        }
    }
}
