using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Level : MonoBehaviour
{
    [SerializeField] float delayTimeBeforeNextScene = 1.5f;

    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }

    IEnumerator DelayBeforeNextScene()
    {
        yield return new WaitForSeconds(delayTimeBeforeNextScene);
        SceneManager.LoadScene("Game Over");
    }

    public void LoadGameOver()
    {
        StartCoroutine(DelayBeforeNextScene());       
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
