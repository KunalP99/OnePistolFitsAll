using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenu;
    public GameObject crosshair;

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

        crosshair.SetActive(true);
        Cursor.visible = false;

        // Unfreeze time
        Time.timeScale = 1f;

        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenu.SetActive(true);

        crosshair.SetActive(false);
        Cursor.visible = true;

        // Freeze time
        Time.timeScale = 0f;

        GameIsPaused = true;
    }
}
