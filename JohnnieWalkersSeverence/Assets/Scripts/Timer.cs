using TMPro;
using System;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static event Action OnTimerEnd;
    public float timeRemaining = 10;
    public bool timerRunning = true;
    [SerializeField] private TMP_Text _timerUI;
    private Color light_blue = new Color(162f / 255f, 246f / 255f, 244f / 255f);
    
    void Start()
    {
        _timerUI.color = light_blue;
    }

    public void SetTimeRemaining(float timeRemaining)
    {
        this.timeRemaining = timeRemaining;
    }
    void Update()
    {

        if (timeRemaining > 0 && timerRunning == true)
        {
            timeRemaining -= Time.deltaTime;

            //// All this is visual labelling stuff
            float minutes = Mathf.FloorToInt(timeRemaining / 60);
            float seconds = Mathf.FloorToInt(timeRemaining % 60);
            if (minutes < 0 || seconds < 0)
            {
                minutes = 0; seconds = 0;
            }
            string min_str = minutes.ToString();
            string sec_str = seconds.ToString();
            if (sec_str.Length < 2)
            {
                sec_str = "0" + sec_str;
            }
            _timerUI.text = "Time Remaining: " + min_str + ":" + sec_str;
        }
        else if (timeRemaining <= 0 && timerRunning == true)
        {
        
            timerRunning = false;
            timeRemaining = 0;
            _timerUI.text = "Time Remaining: 0:00";
            OnTimerEnd?.Invoke();

        }
    }
}
