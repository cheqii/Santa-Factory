using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
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
    [SerializeField] private int tempLives;
    
    [Header("Heart Lives")]
    public List<Image> heartImg;
    public TextMeshProUGUI livesText;

    [SerializeField] private Animator heartShakeAnim;

    #endregion

    #region -Animator & Animations-

    [Header("Animations")]
    [SerializeField] private Animator santaEmoAnim;

    [Header("Animation GameObject Prefabs")]
    [SerializeField] private GameObject santaEmoGo;

    #endregion

    #region -Panel GameObject-
    
    [Header("GameOver Panel")]
    [SerializeField] private GameObject gameOverPanel;

    #endregion

    #region -Time-

    private Timer timer;

    #endregion

    public bool isOver;
    
    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        currentLives = maxLives;
        tempLives = currentLives;
        livesText.text = $"Lives: {currentLives} / {maxLives}";

        timer = GetComponent<Timer>();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            IncreaseLives(1);
        }

        if (gameOverPanel.activeInHierarchy)
        {
            PPSetting.Instance.ActivateBloomEffect(true);
        }
        else
        {
            PPSetting.Instance.ActivateBloomEffect(false);
        }
    }

    public void IncreaseLives(int values)
    {
        if (currentLives > 0 && currentLives < maxLives)
        {
            tempLives = currentLives;
            currentLives += values;
            heartImg[tempLives].color = new Color32(255, 255, 255, 255);
        }
        if (currentLives >= maxLives) currentLives = maxLives;
        
        livesText.text = $"Lives: {currentLives} / {maxLives}";
    }
    public void DecreaseLives(int values)
    {
        var santaEmo = Instantiate(santaEmoGo, santaEmoGo.transform.position, 
            quaternion.identity);
        Destroy(santaEmo, 0.5f);
        
        if (currentLives > 0)
        {
            heartShakeAnim.SetTrigger("isShake"); // shake heart anim
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

    #region -Button Functions-

    public void ChangeScene(string name)
    {
        SceneManager.LoadSceneAsync(name);
    }

    public void QuitFunc()
    {
        Application.Quit();
    }

    #endregion
    
    
    

}
