using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Mail : MonoBehaviour
{
    #region -Child Variables-

    [SerializeField] private TextMeshProUGUI childNameText;
    
    [Header("Children Data List")]
    [SerializeField] private List<Child> blackListChild;
    public List<Child> BlackListChild
    {
        get => blackListChild;
        set => blackListChild = value;
    }
        
    [SerializeField] private List<Child> goodChild;
    public List<Child> GoodChild
    { 
        get => goodChild;
        set => goodChild = value;
    }
    
    private string childNameCheck;
    
    public string ChildNameCheck
    { 
        get => childNameCheck;
        set => childNameCheck = value;
    }

    private int randomChildChance;

    private Blacklist _blacklist;

    #endregion
    
    
    [Header("Mail Count")]
    [SerializeField] private TextMeshProUGUI mailCountText;
    
    [SerializeField] private int mailCount;
    public int MailCount
    {
        get => mailCount;
        set => mailCount = value;
    }
    
    private bool firstMail;

    // Start is called before the first frame update
    void Start()
    {
        _blacklist = FindObjectOfType<Blacklist>();
        
        firstMail = true;
        RandomChildMail();
    }

    // Update is called once per frame
    void Update()
    {
        // noteList = _blacklist.TempIndex;
        if (Input.GetKeyDown(KeyCode.O))
        {
            RandomChildMail();
        }
    }

    public void IncreaseStampMailCount(int values)
    {
        var mailLeft = GameController.Instance.childMailRemaining;

        if (mailCount < mailLeft)
        {
            mailCount += values;
            mailCountText.text = $"{mailCount} / {mailLeft}";
        }
        if (mailCount >= mailLeft)
        {
            mailCount = mailLeft;
            mailCountText.text = "Done!";
        }
    }

    public void DecreaseStampMailCount(int values)
    {
        var mailLeft = GameController.Instance.childMailRemaining;
        if (mailCount <= mailLeft)
        {
            mailCount -= values;
        }
        if (mailCount <= 0)
        {
            mailCount = 0;
        }
        mailCountText.text = $"{mailCount} / {mailLeft}";
    }

    public void RandomChildMail()
    {
        randomChildChance = Random.Range(0, 10);
        var blackListIndex = Random.Range(0,7); // to random child name
        Debug.Log(blackListIndex);
        var childIndex = Random.Range(0, 13);
        
        if (firstMail) randomChildChance = 1; // this line make first mail is going to be good child mail
        if (randomChildChance <= 5)
        {
            firstMail = false;
            // use child from good list
            childNameText.text = goodChild[childIndex].name;
            childNameCheck = goodChild[childIndex].name;
        }
        else if (randomChildChance > 5)
        {
            // use child from blacklist
            childNameText.text = _blacklist.ChildNotes[blackListIndex];
            childNameCheck = _blacklist.ChildNotes[blackListIndex];
        }
    }
}
