using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroScene : MonoBehaviour
{
    public Text introText;
    AudioSource audioSource;
    void Start()
    {
        StartCoroutine(LoadNextScene());

    }

    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(7f); 

        SceneManager.LoadSceneAsync("level game");
        
    }
        

}
