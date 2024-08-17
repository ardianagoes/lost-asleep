using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boatScript : MonoBehaviour
{
    public Vector3 movement;
    public GameObject boy;

    // Start is called before the first frame update
    void Start()
    {
        boy = GameObject.Find("Boy");
        movement = new Vector3(16f * Time.deltaTime, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Transform>().position.x < 851.4f)
        {
            if (boy.GetComponent<boyScript>().boatMove)
            {
                transform.position += movement;
                boy.GetComponent<Transform>().position += movement;
            }
        }
    }
}
