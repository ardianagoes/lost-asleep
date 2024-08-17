using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;

public class dialogueTypeWriter : MonoBehaviour
{
    public float startTimer;
    public float textTimer;
    public bool textReady;
    public bool text1Done;
    public bool text2Done;
    public bool text2Start;
    public bool text3Done;
    public bool text3Start;
    public bool cameraMove;
    public bool cameraMoveLevel2;
    public bool text4Text;
    public bool text4Done;
    public bool text5Start;
    public bool text5Done;
    public bool text6Start;
    public bool text7Start;
    public bool text7Done;
    public bool text6Done;
    public bool batAudioPlay;
    public int textCount;
    public GameObject boat;
    public GameObject manDialogueBox;
    public GameObject boy;
    public GameObject dialogueBox;
    public GameObject textBox;
    public GameObject theMan;
    public GameObject Canvas2;
    public GameObject Man;
    public AudioSource spawn;
    public AudioSource introduceMan;
    public AudioSource batAudio;
    public TextMeshProUGUI speech;
    // Start is called before the first frame update
    void Start()
    {
        batAudioPlay = false;
        boy = GameObject.Find("Boy");
        speech.maxVisibleCharacters = 0;
        text4Text = true;
        textReady = false;
        text1Done = false;
        text2Start = false;
        text2Done = false;
        text3Start = false;
        text3Done = false;
        text4Done = false;
        text5Start = false;
        text5Done = false;
        text6Start = false;
        text6Done = false;
        text7Start = false;
        text7Done = false;
        Man = GameObject.Find("theMan");
        spawn.volume = 0.8f;
        dialogueBox.gameObject.SetActive(false);
        speech.text = "  Where am I? Mom? Dad?";
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetSceneByName("loadingScene").isLoaded)
        {
            if (text1Done == false)
            {
                boy.GetComponent<boyScript>().canMove = false;
                startTimer += Time.deltaTime;
                if (startTimer >= 2.5f)
                {
                    dialogueBox.gameObject.SetActive(true);
                    textTimer += Time.deltaTime;
                }
                if (textTimer >= 0.1 && speech.maxVisibleCharacters <= speech.textInfo.characterCount)
                {
                    speech.maxVisibleCharacters += 1;
                    textBox.GetComponent<RectTransform>().sizeDelta += new Vector2(13.8f, 0f);
                    textTimer = 0;
                }
                else if (Input.GetKeyDown(KeyCode.X) && textReady == false && startTimer >= 2.5)
                {
                    speech.maxVisibleCharacters = speech.textInfo.characterCount;
                    if (textCount == 0)
                    {
                        textBox.GetComponent<RectTransform>().sizeDelta = new Vector2(359f, 100f);
                    }
                    if (textCount == 1)
                    {
                        textBox.GetComponent<RectTransform>().sizeDelta = new Vector2(419f, 100f);
                    }
                }
                else if (speech.maxVisibleCharacters > speech.textInfo.characterCount && textReady == false)
                {
                    textCount += 1;
                    textReady = true;
                }
                if (textReady && Input.GetKeyDown(KeyCode.X) && text1Done == false && textCount < 2)
                {
                    speech.text = "  I have to get out of here...";
                    speech.maxVisibleCharacters = 0;
                    textTimer = 0f;
                    textReady = false;
                    textBox.GetComponent<RectTransform>().sizeDelta = new Vector2(5f, 100f);
                }
                else if (textCount == 2)
                {
                    text1Done = true;
                }
            }
            else if (text1Done && Input.GetKeyDown(KeyCode.X) && text2Start == false && text2Done == false)
            {
                boy.GetComponent<boyScript>().canMove = true;
                dialogueBox.gameObject.SetActive(false);
                textReady = false;
                text2Start = true;
                startTimer = 0f;
                textTimer = 0f;
                textCount = 0;
                speech.maxVisibleCharacters = 0;
                textBox.GetComponent<RectTransform>().sizeDelta = new Vector2(10f, 100f);
            }
            if (boy.GetComponent<Transform>().position.x >= 65f && text2Start)
            {
                boy.GetComponent<boyScript>().boyAnimation.SetBool("isWalking", false);
                boy.GetComponent<boyScript>().canMove = false;
                if (textCount == 0)
                {
                    speech.text = "  A giant pit...";
                }
                if (text2Done == false)
                {
                    startTimer += Time.deltaTime;
                    if (startTimer >= 1f)
                    {
                        dialogueBox.gameObject.SetActive(true);
                        textTimer += Time.deltaTime;
                    }
                    if (textTimer >= 0.1 && speech.maxVisibleCharacters <= speech.textInfo.characterCount)
                    {
                        speech.maxVisibleCharacters += 1;
                        textBox.GetComponent<RectTransform>().sizeDelta += new Vector2(13.5f, 0f);
                        textTimer = 0;
                    }
                    else if (Input.GetKeyDown(KeyCode.X) && textReady == false && startTimer >= 1f)
                    {
                        speech.maxVisibleCharacters = speech.textInfo.characterCount;
                        if (textCount == 0)
                        {
                            textBox.GetComponent<RectTransform>().sizeDelta = new Vector2(226f, 100f);
                        }
                        if (textCount == 1)
                        {
                            textBox.GetComponent<RectTransform>().sizeDelta = new Vector2(470.5f, 100f);
                        }
                    }
                    else if (speech.maxVisibleCharacters > speech.textInfo.characterCount && textReady == false)
                    {
                        textCount += 1;
                        textReady = true;
                    }
                    if (textReady && Input.GetKeyDown(KeyCode.X) && text2Done == false && textCount < 2)
                    {
                        speech.text = "  I have to get to the other side";
                        speech.maxVisibleCharacters = 0;
                        textTimer = 0f;
                        textReady = false;
                        textBox.GetComponent<RectTransform>().sizeDelta = new Vector2(25f, 100f);
                    }
                    else if (textCount == 2)
                    {
                        text2Done = true;
                    }
                }
                else if (text2Done && Input.GetKeyDown(KeyCode.X) && text3Start == false)
                {
                    boy.GetComponent<boyScript>().canMove = true;
                    dialogueBox.gameObject.SetActive(false);
                    textReady = false;
                    startTimer = 0f;
                    text2Start = false;
                    text3Start = true;
                    textTimer = 0f;
                    textCount = 0;
                    speech.maxVisibleCharacters = 0;
                    textBox.GetComponent<RectTransform>().sizeDelta = new Vector2(20f, 100f);
                }
            }
            if (boy.GetComponent<Transform>().position.x >= 150f && boy.GetComponent<Transform>().position.x <= 151f && text3Start)
            {
                boy.GetComponent<Transform>().position += new Vector3(1.1f, 0f);
                spawn.Play();
                Canvas2.GetComponent<FadeInScript>().gameSoundtrack.Stop();
            }
            if (boy.GetComponent<Transform>().position.x > 151f && text3Start)
            {
                boy.GetComponent<boyScript>().boyAnimation.SetBool("isWalking", false);
                boy.GetComponent<boyScript>().canMove = false;
                if (textCount == 0)
                {
                    speech.text = "  What was that noise?";
                }
                if (text3Done == false)
                {
                    startTimer += Time.deltaTime;
                    if (startTimer >= 1.5f)
                    {
                        dialogueBox.gameObject.SetActive(true);
                        textTimer += Time.deltaTime;
                    }
                    if (textTimer >= 0.1 && speech.maxVisibleCharacters <= speech.textInfo.characterCount)
                    {
                        speech.maxVisibleCharacters += 1;
                        textBox.GetComponent<RectTransform>().sizeDelta += new Vector2(14f, 0f);
                        textTimer = 0;
                    }
                    else if (Input.GetKeyDown(KeyCode.X) && textReady == false && startTimer >= 1.5f)
                    {
                        speech.maxVisibleCharacters = speech.textInfo.characterCount;
                        if (textCount == 0)
                        {
                            textBox.GetComponent<RectTransform>().sizeDelta = new Vector2(328f, 100f);
                        }
                    }
                    else if (speech.maxVisibleCharacters > speech.textInfo.characterCount && textReady == false)
                    {
                        textCount += 1;
                        textReady = true;
                    }
                    else if (textCount == 1)
                    {
                        text3Done = true;
                    }
                }
                else if (text3Done && Input.GetKeyDown(KeyCode.X) && manDialogueBox.GetComponent<manDialogueScript>().cameraMoveBack == false && Man.GetComponent<manScript>().lossDialogue == false)
                {
                    boy.GetComponent<SpriteRenderer>().flipX = true;
                    dialogueBox.gameObject.SetActive(false);
                    textReady = false;
                    startTimer = 0f;
                    text3Start = false;
                    textTimer = 0f;
                    textCount = 0;
                    speech.maxVisibleCharacters = 0;
                    textBox.GetComponent<RectTransform>().sizeDelta = new Vector2(20f, 100f);
                    cameraMove = true;
                    Man.GetComponent<Transform>().position += new Vector3(0f, 20f);
                    speech.text = "  What is this place?";
                }
            }
        }
        else if (SceneManager.GetSceneByName("scene3").isLoaded)
        {
            text1Done = true;
            text2Done = true;
            text3Done = true;
            if (text4Text)
            {
                speech.text = "  What is this place?";
                text4Text = false;
            }
            if (text4Done == false)
            {
                boy.GetComponent<boyScript>().canMove = false;
                startTimer += Time.deltaTime;
                if (startTimer >= 2.5f)
                {
                    dialogueBox.gameObject.SetActive(true);
                    textTimer += Time.deltaTime;
                }
                if (textTimer >= 0.1 && speech.maxVisibleCharacters <= speech.textInfo.characterCount)
                {
                    speech.maxVisibleCharacters += 1;
                    textBox.GetComponent<RectTransform>().sizeDelta += new Vector2(13.5f, 0f);
                    textTimer = 0;
                }
                else if (Input.GetKeyDown(KeyCode.X) && textReady == false && startTimer >= 2.5)
                {
                    speech.maxVisibleCharacters = speech.textInfo.characterCount;
                    if (textCount == 0)
                    {
                        textBox.GetComponent<RectTransform>().sizeDelta = new Vector2(323.5f, 100f);
                    }
                }
                else if (speech.maxVisibleCharacters > speech.textInfo.characterCount && textReady == false)
                {
                    textCount += 1;
                    textReady = true;
                }
                if (textCount == 1)
                {
                    text4Done = true;
                }
            }
            else if (text4Done && Input.GetKeyDown(KeyCode.X) && text5Start == false && text5Done == false)
            {
                boy.GetComponent<boyScript>().canMove = true;
                dialogueBox.gameObject.SetActive(false);
                textReady = false;
                text5Start = true;
                startTimer = 0f;
                textTimer = 0f;
                textCount = 0;
                speech.maxVisibleCharacters = 0;
                textBox.GetComponent<RectTransform>().sizeDelta = new Vector2(20f, 100f);
            }
            if (boy.GetComponent<Transform>().position.x >= 30f && text5Start)
            {
                boy.GetComponent<boyScript>().boyAnimation.SetBool("isWalking", false);
                boy.GetComponent<boyScript>().canMove = false;
                if (textCount == 0)
                {
                    speech.text = "  A boat!";
                }
                if (text5Done == false)
                {
                    startTimer += Time.deltaTime;
                    if (startTimer >= 1f)
                    {
                        dialogueBox.gameObject.SetActive(true);
                        textTimer += Time.deltaTime;
                    }
                    if (textTimer >= 0.1 && speech.maxVisibleCharacters <= speech.textInfo.characterCount && textReady == false)
                    {
                        speech.maxVisibleCharacters += 1;
                        textBox.GetComponent<RectTransform>().sizeDelta += new Vector2(14f, 0f);
                        textTimer = 0;
                    }
                    else if (Input.GetKeyDown(KeyCode.X) && textReady == false && startTimer >= 1f)
                    {
                        speech.maxVisibleCharacters = speech.textInfo.characterCount;
                        if (textCount == 0)
                        {
                            textBox.GetComponent<RectTransform>().sizeDelta = new Vector2(146f, 100f);
                        }
                        if (textCount == 1)
                        {
                            textBox.GetComponent<RectTransform>().sizeDelta = new Vector2(529f, 100f);
                        }
                    }
                    else if (speech.maxVisibleCharacters > speech.textInfo.characterCount && textReady == false)
                    {
                        textCount += 1;
                        textReady = true;
                    }
                    if (textReady && Input.GetKeyDown(KeyCode.X) && text5Done == false && textCount == 1)
                    {
                        speech.text = "  I should use it to cross the water";
                        speech.maxVisibleCharacters = 0;
                        textTimer = 0f;
                        textReady = false;
                        textBox.GetComponent<RectTransform>().sizeDelta = new Vector2(25f, 100f);
                    }
                    else if (textCount == 2)
                    {
                        text5Done = true;
                    }
                }
                else if (text5Done && Input.GetKeyDown(KeyCode.X) && text6Start == false && text6Done == false)
                {
                    boy.GetComponent<boyScript>().canMove = true;
                    dialogueBox.gameObject.SetActive(false);
                    textReady = false;
                    startTimer = 0f;
                    textTimer = 0f;
                    text5Start = false;
                    textCount = 0;
                    speech.maxVisibleCharacters = 0;
                    textBox.GetComponent<RectTransform>().sizeDelta = new Vector2(20f, 100f);
                    boy.GetComponent<boyScript>().checkPoint2 = true;
                }
            }
            if (text6Start)
            {
                boat.GetComponent<boatScript>().movement = new Vector3(0f, 0f);
                boy.GetComponent<boyScript>().boyAnimation.SetBool("isWalking", false);
                boy.GetComponent<boyScript>().canMove = false;
                if (textCount == 0)
                {
                    speech.text = "  I wonder what these rocks in the boat are for";
                }
                if (text6Done == false)
                {
                    startTimer += Time.deltaTime;
                    if (startTimer >= 1f)
                    {
                        dialogueBox.gameObject.SetActive(true);
                        textTimer += Time.deltaTime;
                    }
                    if (textTimer >= 0.1 && speech.maxVisibleCharacters <= speech.textInfo.characterCount && textReady == false)
                    {
                        speech.maxVisibleCharacters += 1;
                        textBox.GetComponent<RectTransform>().sizeDelta += new Vector2(14.5f, 0f);
                        textTimer = 0;
                    }
                    else if (Input.GetKeyDown(KeyCode.X) && textReady == false && startTimer >= 1f)
                    {
                        speech.maxVisibleCharacters = speech.textInfo.characterCount;
                        if (textCount == 0)
                        {
                            textBox.GetComponent<RectTransform>().sizeDelta = new Vector2(701.5f, 100f);
                        }
                    }
                    else if (speech.maxVisibleCharacters > speech.textInfo.characterCount && textReady == false)
                    {
                        textCount += 1;
                        textReady = true;
                    }
                    else if (textCount == 1)
                    {
                        text6Done = true;
                    }
                }
                else if (text6Done && Input.GetKeyDown(KeyCode.X) && text7Start == false && text7Done == false)
                {
                    boat.GetComponent<boatScript>().movement = new Vector3(16f * Time.deltaTime, 0f);
                    boy.GetComponent<boyScript>().canMove = true;
                    dialogueBox.gameObject.SetActive(false);
                    textReady = false;
                    startTimer = 0f;
                    textTimer = 0f;
                    text6Start = false;
                    text7Start = true;
                    textCount = 0;
                    speech.maxVisibleCharacters = 0;
                    textBox.GetComponent<RectTransform>().sizeDelta = new Vector2(20f, 100f);
                }
            }
            if (boy.GetComponent<Transform>().position.x >= 100f && boy.GetComponent<Transform>().position.x <= 101f && text7Start)
            {
                boy.GetComponent<SpriteRenderer>().flipX = false;
                batAudio.Play();
                Canvas2.GetComponent<FadeInScript>().gameSoundtrack.Stop();
            }
            if (boy.GetComponent<Transform>().position.x > 101f && text7Start)
            {
                boat.GetComponent<boatScript>().movement = new Vector3(0f, 0f);
                boy.GetComponent<boyScript>().boyAnimation.SetBool("isWalking", false);
                boy.GetComponent<boyScript>().canMove = false;
                if (textCount == 0)
                {
                    speech.text = "  Not again...";
                }
                if (text7Done == false)
                {
                    startTimer += Time.deltaTime;
                    if (startTimer >= 3f)
                    {
                        dialogueBox.gameObject.SetActive(true);
                        textTimer += Time.deltaTime;
                    }
                    if (textTimer >= 0.1 && speech.maxVisibleCharacters <= speech.textInfo.characterCount)
                    {
                        speech.maxVisibleCharacters += 1;
                        textBox.GetComponent<RectTransform>().sizeDelta += new Vector2(14f, 0f);
                        textTimer = 0;
                    }
                    else if (Input.GetKeyDown(KeyCode.X) && textReady == false && startTimer >= 3f)
                    {
                        speech.maxVisibleCharacters = speech.textInfo.characterCount;
                        if (textCount == 0)
                        {
                            textBox.GetComponent<RectTransform>().sizeDelta = new Vector2(216f, 100f);
                        }
                    }
                    else if (speech.maxVisibleCharacters > speech.textInfo.characterCount && textReady == false)
                    {
                        textCount += 1;
                        textReady = true;
                    }
                    else if (textCount == 1)
                    {
                        text7Done = true;
                    }
                }
                else if (text7Done && Input.GetKeyDown(KeyCode.X) && text7Start)
                {
                    dialogueBox.gameObject.SetActive(false);
                    textReady = false;
                    startTimer = 0f;
                    text7Start = false;
                    textTimer = 0f;
                    textCount = 0;
                    speech.maxVisibleCharacters = 0;
                    textBox.GetComponent<RectTransform>().sizeDelta = new Vector2(20f, 100f);
                    cameraMoveLevel2 = true;

                }
            }
        }
    }
}