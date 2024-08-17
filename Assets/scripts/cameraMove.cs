using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class cameraMove : MonoBehaviour
{
    public float timer;
    public float soundTimer;
    public bool introduceMan;
    public bool manDialogue;
    public bool introduceManAudio;
    public bool chaseAudio;
    public bool stopChaseAudio;
    public bool cameraMoveBackLevel2;
    public bool batsMove;
    public bool canHit;
    public GameObject attackCanvas;
    public GameObject boat;
    public GameObject runCanvas;
    public GameObject boy;
    public GameObject dialogueBox;
    public GameObject manDialogueBox;
    public AudioSource fightbatMusic;
    public AudioSource chaseAudioSound;
    // Start is called before the first frame update
    void Start()
    {
        boy = GameObject.Find("Boy");
        manDialogue = false;
        chaseAudio = true;
        introduceMan = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetSceneByName("loadingScene").isLoaded)
        {
            GetComponent<Transform>().rotation = Quaternion.identity;
            if (dialogueBox.GetComponent<dialogueTypeWriter>().cameraMove)
            {
                timer += Time.deltaTime;
                soundTimer += Time.deltaTime;
                if (soundTimer >= 1.5f && introduceMan)
                {
                    dialogueBox.GetComponent<dialogueTypeWriter>().introduceMan.Play();
                    introduceMan = false;
                }
                if (timer >= 1.5f && transform.position.x > -17f)
                {
                    transform.position -= new Vector3(40f * Time.deltaTime, 0f);
                }
                else if (transform.position.x <= -17)
                {
                    dialogueBox.GetComponent<dialogueTypeWriter>().cameraMove = false;
                    dialogueBox.GetComponent<dialogueTypeWriter>().introduceMan.Stop();
                    manDialogue = true;
                    timer = 0;
                }
            }
            if (manDialogueBox.GetComponent<manDialogueScript>().cameraMoveBack)
            {
                if (chaseAudio)
                {
                    chaseAudioSound.Play();
                    chaseAudio = false;
                }
                timer += Time.deltaTime;
                if (timer >= 1f && transform.position.x < 145f)
                {
                    transform.position += new Vector3(80f * Time.deltaTime, 0f);
                }
                else if (transform.position.x >= 145)
                {
                    manDialogueBox.GetComponent<manDialogueScript>().cameraMoveBack = false;
                    timer = 0;
                    boy.GetComponent<boyScript>().canMove = true;
                    runCanvas.gameObject.SetActive(true);
                }
            }
            if (stopChaseAudio)
            {
                chaseAudioSound.volume -= 0.1f;
                if (chaseAudioSound.volume == 0)
                {
                    chaseAudioSound.Stop();
                    stopChaseAudio = false;
                }
            }
        }
        if (SceneManager.GetSceneByName("scene3").isLoaded)
        {
            if (dialogueBox.GetComponent<dialogueTypeWriter>().cameraMoveLevel2)
            {
                timer += Time.deltaTime;
                if (timer >= 1f && transform.position.x < 285f)
                {
                    transform.position += new Vector3(40f * Time.deltaTime, 0f);
                }
                else if (transform.position.x >= 285f)
                {
                    cameraMoveBackLevel2 = true;
                    dialogueBox.GetComponent<dialogueTypeWriter>().cameraMoveLevel2 = false;
                    timer = 0;
                }
            }
            if (cameraMoveBackLevel2)
            {
                batsMove = true;
                timer += Time.deltaTime;
                if (timer >= 2f && transform.position.x > 125f)
                {
                    transform.position -= new Vector3(80f * Time.deltaTime, 0f);
                }
                else if (transform.position.x <= 125f)
                {
                    boat.GetComponent<boatScript>().movement = new Vector3(16f * Time.deltaTime, 0f);
                    cameraMoveBackLevel2 = false;
                    boy.GetComponent<boyScript>().canMove = true;
                    canHit = true;
                    fightbatMusic.Play();
                    attackCanvas.gameObject.SetActive(true);
                }
            }
        }
    }
}
        
