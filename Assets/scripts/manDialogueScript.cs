using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;

public class manDialogueScript : MonoBehaviour
{
    public int textCount;
    public float textBoxIncrease;
    public float startTimer;
    public float textTimer; 
    public bool manText1Done;
    public bool textReady;
    public bool cameraMoveBack;
    public bool manMove;
    public bool doneTyping;
    public bool fadeIn;
    public bool deathCanvas2Start;
    public bool playerRun;
    public bool checkPoint1;
    public bool typeAudio;
    public bool typeAudio2;
    public GameObject fadeCanvas;
    public GameObject deathCanvas2;
    public GameObject textBox;
    public GameObject mainCamera;
    public GameObject manDialogueBox;
    public GameObject boy;
    public GameObject man;
    public GameObject boyDialogue;
    public AudioSource manNoise;
    public TextMeshProUGUI manSpeech;
    public TextMeshProUGUI manDeathSpeech;
    // Start is called before the first frame update
    void Start()    
    {
        playerRun = false;
        fadeIn = false;
        manDeathSpeech.maxVisibleCharacters = 0;
        manSpeech.maxVisibleCharacters = 0;
        textBoxIncrease = 14.5f;
        textReady = false;
        deathCanvas2Start = false;
        manText1Done = false;
        cameraMoveBack = false;
        manMove = false;
        typeAudio = true;
        typeAudio2 = true;
        manDialogueBox.gameObject.SetActive(false);
        textCount = 0;
        textBox.GetComponent<RectTransform>().sizeDelta = new Vector2(15f, 100f);
        manSpeech.text = "  Where are you going?";
        boy = GameObject.Find("Boy");
        man = GameObject.Find("theMan");
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetSceneByName("loadingScene").isLoaded)
        {
            if (mainCamera.GetComponent<cameraMove>().manDialogue)
            {
                if (manText1Done == false)
                {
                    startTimer += Time.deltaTime;
                    if (startTimer >= 1.5f)
                    {
                        if (typeAudio)
                        {
                            manNoise.Play();
                            typeAudio = false;
                        }
                        manDialogueBox.gameObject.SetActive(true);
                        textTimer += Time.deltaTime;
                    }
                    if (textTimer >= 0.1 && manSpeech.maxVisibleCharacters <= manSpeech.textInfo.characterCount)
                    {
                        manSpeech.maxVisibleCharacters += 1;
                        textBox.GetComponent<RectTransform>().sizeDelta += new Vector2(textBoxIncrease, 0f);
                        textTimer = 0;
                    }
                    else if (Input.GetKeyDown(KeyCode.X) && textReady == false && startTimer >= 1.5)
                    {
                        manSpeech.maxVisibleCharacters = manSpeech.textInfo.characterCount;
                        if (textCount == 0)
                        {
                            manNoise.Stop();
                            textBox.GetComponent<RectTransform>().sizeDelta = new Vector2(334f, 100f);
                        }
                        if (textCount == 1)
                        {
                            manNoise.Stop();
                            textBox.GetComponent<RectTransform>().sizeDelta = new Vector2(437, 100f);
                        }
                        manNoise.Stop();
                        if (textCount == 2)
                        {
                            textBox.GetComponent<RectTransform>().sizeDelta = new Vector2(305, 100f);
                        }
                    }
                    else if (manSpeech.maxVisibleCharacters > manSpeech.textInfo.characterCount && textReady == false)
                    {
                        textCount += 1;
                        textReady = true;
                        manNoise.Stop();
                    }
                    if (Input.GetKeyDown(KeyCode.X) && textReady && manText1Done == false && textCount == 1)
                    {
                        textBoxIncrease = 13.5f;
                        manSpeech.text = "  You can't leave, it's too late";
                        manSpeech.maxVisibleCharacters = 0;
                        textTimer = 0f;
                        textReady = false;
                        textBox.GetComponent<RectTransform>().sizeDelta = new Vector2(10f, 100f);
                        typeAudio = true;
                    }
                    if (Input.GetKeyDown(KeyCode.X) && textReady && manText1Done == false && textCount == 2)
                    {
                        textBoxIncrease = 15f;
                        manSpeech.text = "  Get back here now!";
                        manSpeech.maxVisibleCharacters = 0;
                        textTimer = 0f;
                        textReady = false;
                        textBox.GetComponent<RectTransform>().sizeDelta = new Vector2(5f, 100f);
                        typeAudio = true;
                    }
                    else if (textCount == 3)
                    {
                        manText1Done = true;
                        manNoise.Stop();
                    }

                }
                else if (manText1Done && Input.GetKeyDown(KeyCode.X) && playerRun == false)
                {
                    playerRun = true;
                    boy.GetComponent<SpriteRenderer>().flipX = false;
                    manDialogueBox.gameObject.SetActive(false);
                    textReady = false;
                    cameraMoveBack = true;
                    manMove = true;
                    startTimer = 0f;
                    textTimer = 0f;
                    textCount = 0;
                    manSpeech.maxVisibleCharacters = 0;
                    textBox.GetComponent<RectTransform>().sizeDelta = new Vector2(20f, 100f);
                    checkPoint1 = true;
                }
            }
            if (man.GetComponent<manScript>().lossDialogue)
            {
                manDeathSpeech.gameObject.SetActive(true);
                startTimer += Time.deltaTime;
                if (startTimer >= 1.5f)
                {
                    if (typeAudio2)
                    {
                        manNoise.Play();
                        typeAudio2 = false;
                    }
                    textTimer += Time.deltaTime;
                    if (doneTyping == false && manDeathSpeech.maxVisibleCharacters < manDeathSpeech.textInfo.characterCount)
                    {
                        if (textTimer >= Random.Range(0.05f, 0.1f))
                        {
                            manDeathSpeech.maxVisibleCharacters += 1;
                            textTimer = 0;
                        }
                    }
                    else if (textTimer >= 2f)
                    {
                        manNoise.Stop();
                        doneTyping = true;
                    }
                    if (doneTyping)
                    {
                        if (manDeathSpeech.maxVisibleCharacters > 0)
                        {
                            if (textTimer >= 0.05f)
                            {
                                manDeathSpeech.maxVisibleCharacters -= 1;
                                textTimer = 0;
                            }
                        }
                        else
                        {
                            deathCanvas2Start = true;
                            doneTyping = false;
                            man.GetComponent<manScript>().lossDialogue = false;
                            typeAudio2 = true;
                        }
                    }
                }
            }
            if (deathCanvas2Start)
            {
                boy.GetComponent<boyScript>().death.Play();
                manDeathSpeech.gameObject.SetActive(false);
                deathCanvas2.gameObject.SetActive(true);
                deathCanvas2Start = false;
            }
            if (fadeIn)
            {
                fadeCanvas.GetComponent<CanvasGroup>().alpha -= 0.7f * Time.deltaTime;
                if (fadeCanvas.GetComponent<CanvasGroup>().alpha <= 0f)
                {
                    boy.GetComponent<boyScript>().loading.Stop();
                    fadeIn = false;
                    fadeCanvas.GetComponent<FadeInScript>().gameSoundtrack.Play();
                    boy.GetComponent<boyScript>().canMove = true;
                }
            }
        }
    }
    public void TryAgain()
    {
        deathCanvas2.gameObject.SetActive(false);
        fadeIn = true;
        boy.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        boy.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        boy.GetComponent<boyScript>().boyAnimation.SetBool("isJump", false);
        boy.GetComponent<boyScript>().boyAnimation.SetBool("isWalking", false);
        boy.GetComponent<SpriteRenderer>().flipX = false;
        boy.GetComponent<Transform>().position = new Vector3(148.7f, -16.2f);
        boy.GetComponent<boyScript>().loading.Play();
        boy.GetComponent<boyScript>().death.Stop();
        boyDialogue.GetComponent<dialogueTypeWriter>().text3Start = true;
        boyDialogue.GetComponent<dialogueTypeWriter>().text3Done = false;
        man.GetComponent<Transform>().position = new Vector3(-26f, -32.76f);
        man.GetComponent<manScript>().gameOver = false;
        mainCamera.GetComponent<Transform>().position += new Vector3(22.5f, 0f);
        mainCamera.GetComponent<cameraMove>().manDialogue = false;
        mainCamera.GetComponent<cameraMove>().introduceMan = true;
        mainCamera.GetComponent<cameraMove>().soundTimer = 0f;
        playerRun = false;
        manText1Done = false;
        typeAudio = true;
        manSpeech.text = "  Where are you going?";
        mainCamera.GetComponent<cameraMove>().chaseAudio = true;
        mainCamera.GetComponent<cameraMove>().chaseAudioSound.volume = 0.8f;
    }
}
        

