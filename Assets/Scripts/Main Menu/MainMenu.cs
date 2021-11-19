using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Animator fadeImage;
    
    public void Play()
    {
        fadeImage.Play("Fade Out", -1, 0f);
        StartCoroutine(Delay());
    }

    public void Quit()
    {
        Application.Quit();
    }
    
    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(1);
    }
}
