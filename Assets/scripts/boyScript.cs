using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Timers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class boyScript : MonoBehaviour
{
    public float batSpeed;
    public float batTimer;
    public bool batFight;
    public bool fadeIn;
    public bool fadeOut;
    public bool canJump;
    public bool canMove;
    public bool endGameFade;
    public bool boatMove;
    public bool nextLevel;
    public bool endGameAudioPlay;
    public bool boatText;
    public bool batGameOver;
    public bool checkPoint2;
    public bool deathAudio;
    public Vector3 playerSpawn;
    public GameObject[] batsArray;
    public GameObject manDialogueBox;
    public GameObject dialogueCanvas;
    public GameObject dialogueBox;
    public GameObject deathCanvas;
    public GameObject fadeCanvas;
    public GameObject runCanvas;
    public GameObject attackCanvas;
    public GameObject deathCanvas2;
    public GameObject mainCamera;
    public GameObject man;
    public GameObject leftBatPrefab;
    public GameObject boat;
    public GameObject deathCanvas3;
    public GameObject deathCanvas4;
    public AudioSource fall;
    public AudioSource death;
    public AudioSource loading;
    public AudioSource hitSound;
    public AudioSource dieToBat;
    public AudioSource nextLevelAudio;
    public Animator boyAnimation;
    // Start is called before the first frame update
    void Start()
    {
        batFight = true;
        deathAudio = true;
        boatText = true;
        nextLevel = false;
        endGameFade = false;
        fadeIn = false;
        fadeOut = true;
        canMove = false;
        endGameAudioPlay = false;
        playerSpawn = new Vector3(-16f, -16f);
        deathCanvas.gameObject.SetActive(false);
        dialogueCanvas.gameObject.SetActive(true);
        death.volume = 0.8f;
        fall.volume = 0.8f;
        man = GameObject.Find("theMan");
        boyAnimation.SetBool("isWalking", false);
        batSpeed = 12f * Time.deltaTime;
        if (SceneManager.GetSceneByName("scene3").isLoaded)
        {
            for (int i = 0; i < 3; i++)
            {
                Instantiate(leftBatPrefab, new Vector3(Random.Range(265f, 300f), Random.Range(-8f, -16f)), Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Transform>().rotation = Quaternion.identity;
        if (canMove)
        {
            if (Input.GetKey(KeyCode.A))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    GetComponent<SpriteRenderer>().flipX = true;
                    boyAnimation.SetBool("isWalking", true);
                    GetComponent<Transform>().position += new Vector3(-20f * Time.deltaTime, 0f, 0f);
                }
                else
                {
                    GetComponent<SpriteRenderer>().flipX = true;
                    boyAnimation.SetBool("isWalking", true);
                    GetComponent<Transform>().position += new Vector3(-10f * Time.deltaTime, 0f, 0f);
                }
            }
            else if (Input.GetKeyUp(KeyCode.A))
            {
                GetComponent<SpriteRenderer>().flipX = true;
                boyAnimation.SetBool("isWalking", false);
            }
            if (Input.GetKey(KeyCode.D))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    GetComponent<SpriteRenderer>().flipX = false;
                    boyAnimation.SetBool("isWalking", true);
                    GetComponent<Transform>().position += new Vector3(20f * Time.deltaTime, 0f, 0f);
                }
                else
                {
                    GetComponent<SpriteRenderer>().flipX = false;
                    boyAnimation.SetBool("isWalking", true);
                    GetComponent<Transform>().position += new Vector3(10f * Time.deltaTime, 0f, 0f);
                }
            }
            else if (Input.GetKeyUp(KeyCode.D))
            {
                GetComponent<SpriteRenderer>().flipX = false;
                boyAnimation.SetBool("isWalking", false);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (canJump)
                {
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 1200f));
                    boyAnimation.SetBool("isJump", true);
                    canJump = false;
                }
            }
        }
        if (SceneManager.GetSceneByName("loadingScene").isLoaded)
        {
            if (man.GetComponent<manScript>().gameOver == false)
            {
                if (GetComponent<Transform>().position.y <= -20f && GetComponent<Transform>().position.y >= -30f)
                {
                    mainCamera.GetComponent<cameraMove>().stopChaseAudio = true;
                    runCanvas.gameObject.SetActive(false);
                    canMove = false;
                    fall.Play();
                    fadeCanvas.GetComponent<FadeInScript>().gameSoundtrack.Stop();
                }
                if (GetComponent<Transform>().position.y <= -30)
                {
                    manDialogueBox.GetComponent<manDialogueScript>().manMove = false;
                    if (fadeOut)
                    {
                        fadeCanvas.GetComponent<CanvasGroup>().alpha += 0.7f * Time.deltaTime;
                    }
                    if (fadeCanvas.GetComponent<CanvasGroup>().alpha >= 1)
                    {
                        fadeOut = false;
                        fall.Stop();
                        if (gameObject.transform.position.y < -400f)
                        {
                            death.Play();
                            GetComponent<Rigidbody2D>().velocity = new Vector3(0f, 0f, 0f);
                            GetComponent<Transform>().position = playerSpawn;
                            deathCanvas.gameObject.SetActive(true);
                            dialogueCanvas.gameObject.SetActive(false);
                        }
                    }
                }
                if (fadeIn)
                {
                    fadeCanvas.GetComponent<CanvasGroup>().alpha -= 0.7f * Time.deltaTime;
                    if (fadeCanvas.GetComponent<CanvasGroup>().alpha <= 0)
                    {
                        fadeCanvas.GetComponent<FadeInScript>().gameSoundtrack.Play();
                        loading.Stop();
                        fadeIn = false;
                        fadeOut = true;
                        canMove = true;
                    }
                }
                if (endGameFade)
                {
                    fadeCanvas.GetComponent<CanvasGroup>().alpha += 0.7f * Time.deltaTime;
                    mainCamera.GetComponent<cameraMove>().stopChaseAudio = true;
                    boyAnimation.SetBool("isWalking", false);
                    boyAnimation.SetBool("isJump", false);
                    if (fadeCanvas.GetComponent<CanvasGroup>().alpha >= 1)
                    {
                        endGameFade = false;
                        SceneManager.LoadScene("scene3");
                    }
                }
                if (endGameAudioPlay)
                {
                    nextLevelAudio.Play();
                    endGameAudioPlay = false;
                }
            }
        }
        if (SceneManager.GetSceneByName("scene3").isLoaded)
        {
            if (transform.position.y < -30)
            {
                attackCanvas.gameObject.SetActive(false);
                batsArray = GameObject.FindGameObjectsWithTag("leftBat");
                foreach (GameObject bat in batsArray)
                {
                    Destroy(bat);
                }
                mainCamera.GetComponent<cameraMove>().batsMove = false;
                boyAnimation.SetBool("isWalking", false);
                boyAnimation.SetBool("isJump", false);
                mainCamera.GetComponent<cameraMove>().fightbatMusic.Stop();
                fadeCanvas.GetComponent<FadeInScript>().gameSoundtrack.Stop();
                boat.GetComponent<boatScript>().movement = new Vector3(0f, 0f);
                fadeOut = true;
                if (fadeOut)
                {
                    fadeCanvas.GetComponent<CanvasGroup>().alpha += 0.9f * Time.deltaTime;
                }
                if (fadeCanvas.GetComponent<CanvasGroup>().alpha >= 1)
                {
                    GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                    mainCamera.GetComponent<cameraMove>().fightbatMusic.Stop();
                    fadeCanvas.GetComponent<FadeInScript>().gameSoundtrack.Stop();
                    GetComponent<Rigidbody2D>().velocity = new Vector3(0f, 0f, 0f);
                    deathCanvas4.gameObject.SetActive(true);
                    mainCamera.GetComponent<cameraMove>().canHit = false;
                    dialogueCanvas.gameObject.SetActive(false);
                    if (deathAudio)
                    {
                        death.Play();
                        deathAudio = false;
                    }
                }
            }
            if (transform.position.x >= 250f && transform.position.x <= 250.3f)
            {
                dialogueCanvas.GetComponent<dialogueTypeWriter>().batAudio.Play();
            }
            if (transform.position.x >= 250.3f && transform.position.x <= 750f)
            {
                batTimer += Time.deltaTime;
                if (batTimer >= 3f)
                {
                    batTimer = 0;
                    for (int i = 0; i < 4; i++)
                    {
                        Instantiate(leftBatPrefab, new Vector3(Random.Range(transform.position.x + 100f, transform.position.x + 150f), Random.Range(6f, -20f)), Quaternion.identity);
                    }

                }
            }
            else if (boat.GetComponent<Transform>().position.x >= 851.4 && batFight)
            {
                attackCanvas.gameObject.SetActive(false);
                batFight = false;
                fadeCanvas.GetComponent<FadeInScript>().gameSoundtrack.Play();
                mainCamera.GetComponent<cameraMove>().fightbatMusic.Stop();
            }
            if (batGameOver)
            {
                attackCanvas.gameObject.SetActive(true);
                boatMove = false;
                canMove = false;
                fadeOut = true;
                if (fadeOut)
                {
                    fadeCanvas.GetComponent<CanvasGroup>().alpha += 0.7f * Time.deltaTime;
                }
                if (fadeCanvas.GetComponent<CanvasGroup>().alpha >= 1)
                {
                    fadeOut = false;
                    if (deathAudio)
                    {
                        death.Play();
                        deathAudio = false;
                    }
                    GetComponent<Rigidbody2D>().velocity = new Vector3(0f, 0f, 0f);
                    deathCanvas3.gameObject.SetActive(true);
                    dialogueCanvas.gameObject.SetActive(false);
                    batsArray = GameObject.FindGameObjectsWithTag("leftBat");
                    foreach (GameObject bat in batsArray)
                    {
                        Destroy(bat);
                    }

                }
            }
            if (fadeIn)
            {
                fadeCanvas.GetComponent<CanvasGroup>().alpha -= 0.7f * Time.deltaTime;
                if (fadeCanvas.GetComponent<CanvasGroup>().alpha <= 0)
                {
                    fadeCanvas.GetComponent<FadeInScript>().gameSoundtrack.Play();
                    loading.Stop();
                    fadeIn = false;
                    fadeOut = true;
                    canMove = true;
                }
            }
            if (endGameFade)
            {
                fadeCanvas.GetComponent<CanvasGroup>().alpha += 0.7f * Time.deltaTime;
                mainCamera.GetComponent<cameraMove>().stopChaseAudio = true;
                boyAnimation.SetBool("isWalking", false);
                boyAnimation.SetBool("isJump", false);
                if (fadeCanvas.GetComponent<CanvasGroup>().alpha >= 1)
                {
                    endGameFade = false;
                    SceneManager.LoadScene("scene4");
                }
            }
            if (endGameAudioPlay)
            {
                nextLevelAudio.Play();
                endGameAudioPlay = false;
            }

        }
    }
    
    public void TryAgain()
    {
        if (SceneManager.GetSceneByName("loadingScene").isLoaded)
        {

            if (manDialogueBox.GetComponent<manDialogueScript>().checkPoint1 == false)
            {
                manDialogueBox.GetComponent<manDialogueScript>().typeAudio = true;
                GetComponent<SpriteRenderer>().flipX = false;
                canMove = false;
                fadeIn = true;
                death.Stop();
                loading.Play();
                deathCanvas.gameObject.SetActive(false);
                dialogueCanvas.gameObject.SetActive(true);
                dialogueCanvas.GetComponent<dialogueTypeWriter>().text1Done = false;
                dialogueCanvas.GetComponent<dialogueTypeWriter>().text2Done = false;
                dialogueCanvas.GetComponent<dialogueTypeWriter>().text3Start = false;
                dialogueCanvas.GetComponent<dialogueTypeWriter>().speech.text = "   Where am I? Mom? Dad?";
                dialogueBox.GetComponent<RectTransform>().sizeDelta = new Vector3(40f, 100f);
                mainCamera.GetComponent<cameraMove>().chaseAudio = true;
                mainCamera.GetComponent<cameraMove>().chaseAudioSound.volume = 0.8f;
                mainCamera.GetComponent<cameraMove>().soundTimer = 0f;
                mainCamera.GetComponent<cameraMove>().introduceMan = true;
                GetComponent<boyScript>().boyAnimation.SetBool("isJump", false);
                GetComponent<boyScript>().boyAnimation.SetBool("isWalking", false);
            }
            else if (manDialogueBox.GetComponent<manDialogueScript>().checkPoint1 && checkPoint2 == false)
            {
                manDialogueBox.GetComponent<manDialogueScript>().typeAudio = true;
                death.Stop();
                loading.Play();
                GetComponent<SpriteRenderer>().flipX = false;
                deathCanvas.gameObject.SetActive(false);
                fadeIn = true;
                canMove = false;
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                GetComponent<boyScript>().boyAnimation.SetBool("isJump", false);
                GetComponent<boyScript>().boyAnimation.SetBool("isWalking", false);
                GetComponent<SpriteRenderer>().flipX = false;
                GetComponent<Transform>().position = new Vector3(142f, -16.5f);
                dialogueCanvas.gameObject.SetActive(true);
                dialogueCanvas.GetComponent<dialogueTypeWriter>().text3Start = true;
                dialogueCanvas.GetComponent<dialogueTypeWriter>().text3Done = false;
                man.GetComponent<Transform>().position = new Vector3(-26f, -32.76f);
                mainCamera.GetComponent<Transform>().position += new Vector3(22.5f, 0f);
                mainCamera.GetComponent<cameraMove>().manDialogue = false;
                manDialogueBox.GetComponent<manDialogueScript>().playerRun = false;
                manDialogueBox.GetComponent<manDialogueScript>().manText1Done = false;
                manDialogueBox.GetComponent<manDialogueScript>().manSpeech.text = "  Where are you going?";
                mainCamera.GetComponent<cameraMove>().chaseAudio = true;
                mainCamera.GetComponent<cameraMove>().chaseAudioSound.volume = 0.8f;
                mainCamera.GetComponent<cameraMove>().soundTimer = 0;
                mainCamera.GetComponent<cameraMove>().introduceMan = true;
            }
        }
        if (checkPoint2)
        {
            attackCanvas.gameObject.SetActive(false);
            deathAudio = true;
            boatText = true;
            death.Stop();
            loading.Play();
            GetComponent<SpriteRenderer>().flipX = false;
            deathCanvas3.gameObject.SetActive(false);
            deathCanvas4.gameObject.SetActive(false);
            fadeIn = true;
            canMove = false;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            GetComponent<boyScript>().boyAnimation.SetBool("isJump", false);
            GetComponent<boyScript>().boyAnimation.SetBool("isWalking", false);
            GetComponent<SpriteRenderer>().flipX = false;
            GetComponent<Transform>().position = new Vector3(26, -17f);
            dialogueCanvas.gameObject.SetActive(true);
            dialogueCanvas.GetComponent<dialogueTypeWriter>().text4Done = true;
            dialogueCanvas.GetComponent<dialogueTypeWriter>().text5Start = true;
            dialogueCanvas.GetComponent<dialogueTypeWriter>().text6Start = false;
            dialogueCanvas.GetComponent<dialogueTypeWriter>().text7Start = false;
            dialogueCanvas.GetComponent<dialogueTypeWriter>().text5Done = false;
            dialogueCanvas.GetComponent<dialogueTypeWriter>().text6Done = false;
            dialogueCanvas.GetComponent<dialogueTypeWriter>().text7Done = false;
            batGameOver = false;
            batFight = true;
            boat.GetComponent<Transform>().position = new Vector3(60.7f, -17.1f);
            mainCamera.GetComponent<Transform>().position -= new Vector3(4.9f, 0f);
            mainCamera.GetComponent<cameraMove>().canHit = false;
            for (int i = 0; i < 3; i++)
            {
                Instantiate(leftBatPrefab, new Vector3(Random.Range(265f, 300f), Random.Range(-8f, -16f)), Quaternion.identity);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            canJump = true;
            boyAnimation.SetBool("isJump", false);
        }
        if (collision.gameObject.tag == "tile")
        {
            canJump = true;
            boyAnimation.SetBool("isJump", false);
        }
        if (collision.gameObject.tag == "door")
        {
            nextLevel = true;
            endGameAudioPlay = true;
            endGameFade = true;
            canMove = false;
            runCanvas.gameObject.SetActive(false);
            manDialogueBox.GetComponent<manDialogueScript>().manMove = false;
        }
        if (collision.gameObject.tag == "boat")
        {
            canJump = true;
            boyAnimation.SetBool("isJump", false);
            boatMove = true;
            if (boatText)
            {
                dialogueCanvas.GetComponent<dialogueTypeWriter>().text6Start = true;
                boatText = false;
            }
        }
        if (collision.gameObject.tag == "leftBat")
        {
            batGameOver = true;
            mainCamera.GetComponent<cameraMove>().batsMove = false;
            mainCamera.GetComponent<cameraMove>().fightbatMusic.Stop();
            mainCamera.GetComponent<cameraMove>().canHit = false;
            dieToBat.Play();
        }
    }
}
