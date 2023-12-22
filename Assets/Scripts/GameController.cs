using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    #region -Child & Mail Variables-

    public int childMailRemaining;

    public List<Child> allChildren;

    #endregion
    

    #region -Lives & Score Variables-

    [Header("Santa Lives")]
    public int maxLives;
    public int currentLives;
    
    public TextMeshProUGUI livesText;
    
    public int score;

    #endregion

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        currentLives = maxLives;
        livesText.text = $"Lives: {currentLives} / {maxLives}";
    }

    public void IncreaseLives(int values)
    {
        if (currentLives > 0 && currentLives < maxLives) currentLives += values;
        if (currentLives >= maxLives) currentLives = maxLives;
        
        livesText.text = $"Lives: {currentLives} / {maxLives}";
    }
    public void DecreaseLives(int values)
    {
        if (currentLives > 0)
        {
            currentLives -= values;
            livesText.text = $"Lives: {currentLives} / {maxLives}";
        }
        if (currentLives <= 0)
        {
            currentLives = 0;
            livesText.text = $"Game Over";
        }
    }
}
