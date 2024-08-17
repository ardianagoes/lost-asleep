using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StartScreenScript : MonoBehaviour
{
    public float headPhoneTimer;
    public bool gameHasStarted;
    public bool fadeOut;
    public bool fadeIn;
    public CanvasGroup canvas;
    public GameObject button;
    public GameObject background;
    public GameObject title;
    public GameObject author;
    public GameObject controls;
    public GameObject controlTitle;
    public GameObject back;
    public GameObject controlKeybinds;
    public GameObject headphonesText;
    public AudioSource startMenuSoundtrack;
    public AudioSource startGameSoundEffect;
    public AudioSource select;
    public string[] dialogue;
    // Start is called before the first frame update
    void Start()
    {
        fadeIn = true;
        fadeOut = false;
        startMenuSoundtrack.volume = 0.5f;
        startMenuSoundtrack.PlayDelayed(0.3f);
        dialogue = "Hello".Split();
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeIn)
        {
            canvas.GetComponent<CanvasGroup>().alpha -= 0.7f * Time.deltaTime;
            if (canvas.GetComponent<CanvasGroup>().alpha <= 0)
            {
                fadeIn = false;
            }
        }
        if (fadeOut)
        {
            canvas.GetComponent<CanvasGroup>().alpha += 0.5f * Time.deltaTime;
            if (canvas.GetComponent<CanvasGroup>().alpha >= 1)
            {
                background.gameObject.SetActive(false);
                canvas.gameObject.SetActive(false);
                StartCoroutine(Headphones());
                headPhoneTimer += Time.deltaTime;
                if (headPhoneTimer >= 2f)
                {
                    headphonesText.GetComponent<CanvasGroup>().alpha -= 0.5f * Time.deltaTime;
                    if (headphonesText.GetComponent<CanvasGroup>().alpha == 0)
                    {
                        gameHasStarted = true;
                        StopCoroutine(Headphones());
                        headphonesText.gameObject.SetActive(false);
                        StartCoroutine(DialogueBeginning());
                    }
                }
            }
        }
    }
    public void StartGame()
    {
        title.gameObject.SetActive(false);
        button.gameObject.SetActive(false);
        author.gameObject.SetActive(false);
        controls.gameObject.SetActive(false);
        startMenuSoundtrack.Stop();
        startGameSoundEffect.Play();
        fadeOut = true;
        fadeIn = false; 
    }

    public void ControlScript()
    {
        select.Play();
        StartCoroutine(Controls());
    }

    public void BackScript()
    {
        select.Play();
        StartCoroutine(Back());

    }

    public IEnumerator DialogueBeginning()
    {
        yield return new WaitForSeconds(2f);  
    }

    public IEnumerator Back()
    {
        yield return new WaitForSeconds(0.1f);
        title.gameObject.SetActive(true);
        button.gameObject.SetActive(true);
        author.gameObject.SetActive(true);
        controls.gameObject.SetActive(true);
        back.gameObject.SetActive(false);
        controlTitle.SetActive(false);
        controlKeybinds.SetActive(false);
    }

    public IEnumerator Controls()
    {
        yield return new WaitForSeconds(0.1f);
        title.gameObject.SetActive(false);
        button.gameObject.SetActive(false);
        author.gameObject.SetActive(false);
        controls.gameObject.SetActive(false);
        back.gameObject.SetActive(true);
        controlTitle.SetActive(true);
        controlKeybinds.SetActive(true);
    }
    public IEnumerator Headphones()
    {
        yield return new WaitForSeconds(1f);
        headphonesText.gameObject.SetActive(true);
    }
}


