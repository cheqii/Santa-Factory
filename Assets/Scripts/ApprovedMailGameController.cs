using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ApprovedMailGameController : MonoBehaviour
{
    public static ApprovedMailGameController Instance;

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
    [SerializeField] private GameObject cocoaMugGo;

    private Animator cocoaMugAnim;

    #endregion

    #region -Panel GameObject-

    [Header("GameEnd Panel")]
    [SerializeField] private GameObject gameEndPanel;
    
    [Header("GameOver Panel")]
    [SerializeField] private GameObject gameOverPanel;

    [Header("Paused Panel")]
    [SerializeField] private GameObject pausedPanel;

    [Header("Sum of Result GameObject")]
    [SerializeField] private GameObject sumOfResult;
    
    [Header("Particle")]
    [SerializeField] private GameObject snowParticle;
    
    #endregion

    #region -Time-

    private Timer timer;

    #endregion

    public bool isOver;
    public bool isPause;
    public bool isDone;
    
    [Header("Sum of Game Result")]
    public TextMeshProUGUI timeUsedText;
    public TextMeshProUGUI heartLeftText;
    public TextMeshProUGUI missComboCountText;
    public TextMeshProUGUI maxComboText;

    private StampTools _stampTools;
    
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
        _stampTools = FindObjectOfType<StampTools>();

        cocoaMugAnim = cocoaMugGo.GetComponentInChildren<Animator>().GetComponent<Animator>();
    }

    private void Update()
    {
        CheckGameStatus();

        if (Input.GetKeyDown(KeyCode.A))
        {
            isOver = true;
            isDone = true;
        }
        if (isOver || isPause || isDone)
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
        var cocoaMug = Instantiate(cocoaMugGo, cocoaMugGo.transform.position, quaternion.identity);
        Destroy(cocoaMug, 2f);
        
        if (currentLives > 0 && currentLives < maxLives)
        {
            tempLives = currentLives;
            currentLives += values;
            heartImg[tempLives].color = new Color32(255, 255, 255, 255);
        }

        if (currentLives >= maxLives)
        {
            currentLives = maxLives;
        }
        
        livesText.text = $"Lives: {currentLives} / {maxLives}";
    }
    public void DecreaseLives(int values)
    {
        SoundManager.Instance.Play("Hurt");
        
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
        if (currentLives == 0 && _mail.MailCount != allChildMail 
            || timer.TimeRemaining == 0 && _mail.MailCount != allChildMail) // Game Over
        {
            isOver = true;
            gameOverPanel.SetActive(true);
            timer.TimeIsRunning = false;
            sumOfResult.SetActive(true);
            
            timeUsedText.text = $"Used Time: Time Out!";
            heartLeftText.text = $"Heart Left: {currentLives}";
            missComboCountText.text = $"Miss: {_stampTools.MissCount}";
            maxComboText.text = $"Max Combo: {_stampTools.MaxCombo}";
        }
        
        if (_mail.MailCount == allChildMail) // Game End I mean Player win kub
        {
            isDone = true;
            gameEndPanel.SetActive(true);
            timer.TimeIsRunning = false;
            sumOfResult.SetActive(true);
            
            var usedTime = timer.MaxTime - timer.TimeRemaining;
            
            timeUsedText.text = $"Used Time: {timer.DisplayUsedTime(usedTime)}";
            heartLeftText.text = $"Heart Left: {currentLives}";
            missComboCountText.text = $"Miss: {_stampTools.MissCount}";
            maxComboText.text = $"Max Combo: {_stampTools.MaxCombo}";
        }

        if (Input.GetKeyDown(KeyCode.Escape)) // Pause Game
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
        SoundManager.Instance.Play("Button");
        isPause = false;
        pausedPanel.SetActive(isPause);
        timer.TimeIsRunning = true;
    }
    
    public void ChangeScene(string name)
    {
        SoundManager.Instance.Play("Button");
        SceneManager.LoadSceneAsync(name);
    }

    #endregion
    
    
    

}
