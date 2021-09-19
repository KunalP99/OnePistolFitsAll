using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    public Boss_Health bossHealthScript;
    public MainMenu menu;
    public TextMeshProUGUI waveText;

    void Start()
    {
        waveText.text = "Wave: " + "x";
    }
    void Update()
    {
        if (bossHealthScript.isBossDead == true)
        {
            StartCoroutine(EndWait(5f));
        }
    }

    public IEnumerator EndWait(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        menu.PlayGame();

        Debug.Log("GAME COMPLETE");
    }
}
