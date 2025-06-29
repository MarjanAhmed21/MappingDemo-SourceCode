using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeltaTimeDownTimer : MonoBehaviour
{
    public GameObject FailScrn;
    public GameObject CanvasMain;
    public GameObject Ball;
    // we're going to report our timer values using a TextMeshPro
    public TextMeshProUGUI timerText;

    // this float keeps track of how much time is remaining on the timer
    // I've made it public so we can set it in the inspector should we want to
    public float countFrom = 30;

    void Update()
    {
        // the if statement here means that we don't keep updating the timer
        // once it has expired - handy!
        if (countFrom > 0)
        {
            // every time update is called, we take Time.deltaTime away from the countFrom
            // delta time is the amount of time since Update was last called
            countFrom -= Time.deltaTime;

            // for reporting purposes, we ceil to the nearest int (in seconds)
            int secondsRemaining = Mathf.CeilToInt(countFrom);

            // update our timer's value
            UpdateTimerValue(secondsRemaining);
        }
    }

    private void UpdateTimerValue(int numberOfSeconds)
    {
        // check if the timer has expired (i.e. number of Seconds is 0)
        if (numberOfSeconds <= 0)
        {
            // call a special function with code to execute once the timer expires
            TimerExpired();
        }
        else if (numberOfSeconds == 1)
        {
            timerText.text = numberOfSeconds + " second";
        }
        else
        {
            timerText.text = numberOfSeconds + " seconds";
        }
    }

    // this just updates the timer to say it ran out, but we could also do
    // anything else here if we needed to
    private void TimerExpired()
    {
        timerText.text = "Time is up!";
        FailScrn.gameObject.SetActive(true);
        CanvasMain.gameObject.SetActive(false);
        Ball.gameObject.SetActive(false);
    }
}
