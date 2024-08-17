using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class tempscript : MonoBehaviour
{
    // Start is called before the first frame update

    public AudioSource starttheme;
    void Start()
    {
        starttheme.Play(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Menu()
    {
        SceneManager.LoadScene("startScene");
    }
}
