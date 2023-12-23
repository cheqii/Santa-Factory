using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private float timeRemaining;
    public float TimeRemaining
    {
        get => timeRemaining;
        set => timeRemaining = value;
    }
    
    [SerializeField] private bool timeIsRunning;

    public bool TimeIsRunning
    {
        get => timeIsRunning;
        set => timeIsRunning = value;
    }
    
    private void Start()
    {
        timeIsRunning = true; 
    }

    private void Update()
    {
        if (timeIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                timeText.color = Color.red;
                timeText.text = "Time Out";
                timeRemaining = 0;
                timeIsRunning = false;
            }
        }
    }

    void DisplayTime(float time)
    {
        time += 1;
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
