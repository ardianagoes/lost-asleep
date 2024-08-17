using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class typewriterScript : MonoBehaviour
{
    public bool moveToScene2;
    public bool deleteText;
    public bool doneTyping;
    public float timer;
    public GameObject startScreen;
    public TextMeshProUGUI text;
    public AudioSource wind;
    public AudioSource type;

    // Start is called before the first frame update
    void Start()
    {
        moveToScene2 = false;
        text.text = "Son, please come back to us...";
        text.maxVisibleCharacters = 0;
        wind.volume = 1f;
        type.volume = 0.4f;
        type.pitch = -0.03f;
        startScreen = GameObject.Find("StartScreen");
    }

    // Update is called once per frame
    void Update()
    {
        if (startScreen.GetComponent<StartScreenScript>().gameHasStarted)
        {
            StartCoroutine(Typing());
            if (moveToScene2)
            {
                StartCoroutine(SwitchToScene2());
            }
        }
    }

    public IEnumerator Typing()
    {
        yield return new WaitForSeconds(3f);
        timer += Time.deltaTime;
        type.Play();
        if (text.maxVisibleCharacters < text.textInfo.characterCount && doneTyping == false)
        {
            if (timer >= Random.Range(0.1f, 0.2f))
            {
                text.maxVisibleCharacters += 1;
                timer = 0;
            }
        }
        else
        {
            wind.volume = 0.3f;
            doneTyping = true;
            type.Stop();
            if (timer >= 2f)
            {
                deleteText = true;
            }
        }
        if (deleteText && moveToScene2 == false)
        {
            wind.Play();
            if (text.maxVisibleCharacters > 0)
            {
                if (timer >= 0.1f) 
                {
                    text.maxVisibleCharacters -= 1;
                    timer = 0;
                }
            }
            else
            {
                moveToScene2 = true;
                deleteText = false; 
                wind.Stop();
            }
        }
    }
    public IEnumerator SwitchToScene2()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("loadingScene");
    }
}
    



