using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SmgSwitchTimer : MonoBehaviour
{
    [SerializeField] float smgSwitchCurrentTime = 0;

    float smgSwitchStartingTime = 9f;

    [SerializeField] TextMeshProUGUI countdownText;

    public PlayerController player;

    public GameObject smgSwitchTimer;
    [HideInInspector] public bool smgSwitchTimerEnabled = false;

    void OnEnable()
    {
        smgSwitchCurrentTime = smgSwitchStartingTime;

        smgSwitchTimer.SetActive(true);

    }

    void Update()
    {
        if (smgSwitchTimerEnabled)
        {
            smgTimer();
        }
    }

    public void smgTimer()
    {
        // Timer functionality
        smgSwitchCurrentTime -= 1 * Time.deltaTime;
        // Convert the integer to string so it can be displayed in UI
        countdownText.text = smgSwitchCurrentTime.ToString("0");

        if (smgSwitchCurrentTime <= 0)
        {
            smgSwitchCurrentTime = 0;

            gameObject.SetActive(false);
        }
    }

    // Enable and disable timer script so the timer resets
    public IEnumerator SmgTimerEnabled()
    {
        GetComponent<SmgSwitchTimer>().enabled = true;

        smgSwitchTimerEnabled = true;

        yield return new WaitForSeconds(9f);

        GetComponent<SmgSwitchTimer>().enabled = false;

        smgSwitchTimerEnabled = false;
    }

    void OnDisable()
    {
        smgSwitchTimer.SetActive(false);
    }
}
