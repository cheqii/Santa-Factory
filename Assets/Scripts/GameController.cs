using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    #region -Child & Mail Variables-

    private Mail _mail;
    public int allChildMail;

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

    [Header("Heart Shake Take Damage")]
    [SerializeField] private Animator heartShakeAnim;

    #endregion

    #region -Animator & Animations-

    [Header("Animation GameObject Prefabs")]
    [SerializeField] private GameObject santaEmoGo;

    #endregion

    #region -Panel GameObject-
    
    [Header("GameOver Panel")]
    [SerializeField] private GameObject gameOverPanel;

    [Header("Paused Panel")]
    [SerializeField] private GameObject pausedPanel;
    
    [Header("Particle")]
    [SerializeField] private GameObject snowParticle;
    
    #endregion

    #region -Time-

    private Timer timer;

    #endregion

    public bool isOver;
    public bool isPause;

    #region -Unity Medthods-

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
        _mail = FindObjectOfType<Mail>();
    }

    private void Update()
    {
        CheckGameStatus();

        
        if (gameOverPanel.activeInHierarchy || isPause)
        {
            PPSetting.Instance.ActivateBloomEffect(true);
            snowParticle.SetActive(true);
        }
        else
        {
            PPSetting.Instance.ActivateBloomEffect(false);
            snowParticle.SetActive(false);
        }
    }

    #endregion
    
    #region -Lives Medthods-

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

    #endregion

    public void CheckGameStatus()
    {
        if (currentLives == 0 && _mail.MailCount != allChildMail || timer.TimeRemaining == 0)
        {
            isOver = true;
            gameOverPanel.SetActive(isOver);
            timer.TimeIsRunning = false;
        }
        else if (_mail.MailCount == allChildMail)
        {
            Debug.Log("NextStage");
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentLives != 0 && !isPause && _mail.MailCount != allChildMail && timer.TimeIsRunning)
            {
                isPause = true;
                pausedPanel.SetActive(isPause);
                timer.TimeIsRunning = false;
            }
            else
            {
                isPause = false;
                pausedPanel.SetActive(isPause);
                timer.TimeIsRunning = true;
            }
        }

    }
    

    #region -Button Functions-

    public void ResumeGame()
    {
        isPause = false;
        pausedPanel.SetActive(isPause);
        timer.TimeIsRunning = true;
    }
    
    public void ChangeScene(string name)
    {
        SceneManager.LoadSceneAsync(name);
    }

    public void GameQuit()
    {
        Application.Quit();
    }

    #endregion
    
    
    

}
