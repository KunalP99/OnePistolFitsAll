using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator anim;

    public GameObject playButton;
    public GameObject settingsButton;
    public GameObject quitButton;

    public void PlayGame()
    {
        StartCoroutine(LoadLevelFromMenu(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void RestartLevel()
    {
        Time.timeScale = 1;
        StartCoroutine(LoadLevelFromGame(SceneManager.GetActiveScene().buildIndex));
    }

    public void BackToMenu()
    {
        Time.timeScale = 1;
        StartCoroutine(LoadLevelFromGame(0));
    }

    IEnumerator LoadLevelFromMenu(int LevelIndex)
    {
        playButton.SetActive(false);
        settingsButton.SetActive(false);
        quitButton.SetActive(false);

        anim.SetTrigger("start");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(LevelIndex);
    }

    IEnumerator LoadLevelFromGame(int LevelIndex)
    {
        anim.SetTrigger("start");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(LevelIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
