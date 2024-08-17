using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class batMove : MonoBehaviour
{
    public GameObject boy;
    public GameObject mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        boy = GameObject.Find("Boy");
        mainCamera = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, 0f, 0f);
        if (mainCamera.GetComponent<cameraMove>().batsMove)
        {
            GetComponent<Transform>().position = Vector3.MoveTowards(GetComponent<Transform>().position, boy.GetComponent<Transform>().position, 12f * Time.deltaTime);
        }
    }

    private void OnMouseDown()
    {
        if (mainCamera.GetComponent<cameraMove>().canHit)
        {
            boy.GetComponent<boyScript>().hitSound.Stop();
            boy.GetComponent<boyScript>().hitSound.Play();
            Destroy(gameObject);
        }
    }
}
