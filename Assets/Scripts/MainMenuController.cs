using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Animator Fade;

    private void Start()
    {
        AudioClip clip = Resources.Load("audio/BGM/MainMenu") as AudioClip;

        AudioManager.instance.PlaySong(clip);
    }

    public void NewGame()
    {
        StartCoroutine(FadeEffect());
        
        SceneManager.LoadScene("Intro");
    }

    public void Continue()
    {

    }

    public void Extra()
    {

    }

    public void Exit()
    {
        Debug.Log("Exit");
        Application.Quit();
    }


    IEnumerator FadeEffect()
    {
        Fade.SetTrigger("FadeTrigger");
        yield return new WaitForEndOfFrame();
    }
}
