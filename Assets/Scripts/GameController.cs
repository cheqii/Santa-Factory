using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    public List<Image> heartImg;

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

    private void Update()
    {
        
    }

    public void IncreaseLives(int values)
    {
        if (currentLives > 0 && currentLives < maxLives)
        {
            currentLives += values;
            heartImg[currentLives].color = new Color32(255, 255, 255, 255);
        }
        if (currentLives >= maxLives) currentLives = maxLives;
        
        livesText.text = $"Lives: {currentLives} / {maxLives}";
    }
    public void DecreaseLives(int values)
    {
        if (currentLives > 0)
        {
            currentLives -= values;
            heartImg[currentLives].color = new Color32(80, 80, 80, 255);
            livesText.text = $"Lives: {currentLives} / {maxLives}";
        }
        if (currentLives <= 0)
        {
            currentLives = 0;
            heartImg[0].color = new Color32(80, 80, 80, 255);
            livesText.text = $"Game Over";
        }
    }
    
}
