using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator anim;

    // Main menu variables
    public GameObject playButton;
    public GameObject wavexButton;
    public GameObject settingsButton;
    public GameObject quitButton;

    //Settings variables
    public GameObject settingsMenu;

    private PlayerController player;
    private WaveSpawner waveSpawner;

    void Start()
    {
        GameObject pistol = GameObject.Find("Pistol");
        player = pistol.GetComponent<PlayerController>();

        GameObject spawner = GameObject.Find("Pistol");
        waveSpawner = spawner.GetComponent<WaveSpawner>();
    }

    public void PlayGame()
    {
        StartCoroutine(LoadLevelFromMenu(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void BossLevel()
    {
        StartCoroutine(LoadLevelFromMenu(SceneManager.GetActiveScene().buildIndex + 3));
    }

    public void Settings()
    {
        // Hide buttons main menu buttons and show settings objects
        playButton.SetActive(false);
        wavexButton.SetActive(false);
        settingsButton.SetActive(false);
        quitButton.SetActive(false);

        settingsMenu.SetActive(true);
    }

    public void Back()
    {
        // Hide settings objects and show main menu buttons
        playButton.SetActive(true);
        wavexButton.SetActive(true);
        settingsButton.SetActive(true);
        quitButton.SetActive(true);

        settingsMenu.SetActive(false);
    }

    public void RestartLevel()
    {
        // If boss has spawned and player restarts level, restart at point where boss spawns; disable wave spawner, set boss to active
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
        wavexButton.SetActive(false);
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
