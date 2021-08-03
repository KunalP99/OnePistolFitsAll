using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DashCoutdownTimer : MonoBehaviour
{
    [SerializeField] float dashCurrentTime = 0;

    float dashStartingTime = 2f;

    [SerializeField] TextMeshProUGUI countdownText;

    public PlayerController player;

    public GameObject dashTimer;
    [HideInInspector] public bool dashTimerEnabled = false;

    void OnEnable()
    {
        dashCurrentTime = dashStartingTime;

        dashTimer.SetActive(true);

    }

    void Update()
    {
        if (dashTimerEnabled)
        {
            DashTimer();
        }
    }

    public void DashTimer()
    {
        // Timer functionality
        dashCurrentTime -= 1 * Time.deltaTime;
        // Convert the integer to string so it can be displayed in UI
        countdownText.text = dashCurrentTime.ToString("0");

        if (dashCurrentTime <= 0)
        {
            dashCurrentTime = 0;

            gameObject.SetActive(false);
        }
    }

    // Enable and disable timer script so the timer resets
    public IEnumerator DashTimerEnabled()
    {
        GetComponent<DashCoutdownTimer>().enabled = true;

        dashTimerEnabled = true;

        yield return new WaitForSeconds(2f);

        GetComponent<DashCoutdownTimer>().enabled = false;

        dashTimerEnabled = false;
    }

    void OnDisable()
    {
        dashTimer.SetActive(false);
    }
}
