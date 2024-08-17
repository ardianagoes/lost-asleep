using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class manScript : MonoBehaviour
{
    public bool fadeOut;
    public bool lossDialogue;
    public bool gameOver;
    public Vector3 movementSpeed;
    public AudioSource boyTakeSound;
    public GameObject runCanvas;
    public GameObject fadeCanvas;
    public GameObject manDialogueBox;
    public GameObject boyDialogueBox;
    public GameObject mainCamera;
    public GameObject boy;
    // Start is called before the first frame update
    void Start()
    {
        boy = GameObject.Find("Boy");
        fadeOut = false;
        lossDialogue = false;
        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetSceneByName("loadingScene").isLoaded)
        {
            if (manDialogueBox.GetComponent<manDialogueScript>().manMove)
            {
                transform.Rotate(0f, 0f, 0f);
                GetComponent<Transform>().position = Vector3.MoveTowards(GetComponent<Transform>().position, boy.GetComponent<Transform>().position, 19f * Time.deltaTime);
                if (GetComponent<Transform>().position.x + 5.2f > boy.GetComponent<Transform>().position.x && GetComponent<Transform>().position.y - 3.5f < boy.GetComponent<Transform>().position.x && boy.GetComponent<boyScript>().nextLevel == false)
                {
                    manDialogueBox.GetComponent<manDialogueScript>().manMove = false;
                    boy.GetComponent<boyScript>().canMove = false;
                    boy.GetComponent<boyScript>().boyAnimation.SetBool("isWalking", false);
                    boy.GetComponent<boyScript>().boyAnimation.SetBool("isJump", false);
                    boy.GetComponent<SpriteRenderer>().flipX = true;
                    boy.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                    fadeOut = true;
                    runCanvas.gameObject.SetActive(false);
                    boyDialogueBox.GetComponent<dialogueTypeWriter>().text3Done = false;
                    mainCamera.GetComponent<cameraMove>().stopChaseAudio = true;
                    boyTakeSound.Play();
                    gameOver = true;
                }
            }
            if (fadeOut)
            {
                fadeCanvas.GetComponent<CanvasGroup>().alpha += 0.7f * Time.deltaTime;
                if (fadeCanvas.GetComponent<CanvasGroup>().alpha >= 1f)
                {
                    boyTakeSound.Stop();
                    fadeOut = false;
                    lossDialogue = true;
                }
            }
        }
    }
}
