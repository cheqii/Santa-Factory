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
    
    public int score;
    public TextMeshProUGUI scoreText;

    #endregion

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        currentLives = maxLives;
    }

    public void DecreaseLives(int values)
    {
        Debug.Log("Lost Lives!!!");
        if (currentLives > 0) currentLives -= values;
        else if (currentLives <= 0) currentLives = 0;
    }
}
