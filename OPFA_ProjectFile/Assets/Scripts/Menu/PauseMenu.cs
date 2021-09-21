using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenu;
    public GameObject crosshair;

    public AudioSource backgroundMusic;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Continue();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Continue()
    {
        pauseMenu.SetActive(false);
        backgroundMusic.volume = 0.49f;

        crosshair.SetActive(true);
        Cursor.visible = false;

        // Unfreeze time
        Time.timeScale = 1f;

        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenu.SetActive(true);
        backgroundMusic.volume = 0.2f;

        crosshair.SetActive(false);
        Cursor.visible = true;

        // Freeze time
        Time.timeScale = 0f;

        GameIsPaused = true;
    }
}
