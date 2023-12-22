using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Mail : MonoBehaviour
{
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

    private int randomChildChance;
    
    // Start is called before the first frame update
    void Start()
    {
        RandomChildMail();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            RandomChildMail();
        }
    }

    public void RandomChildMail()
    {
        randomChildChance = Random.Range(0, 10);
        var childIndex = Random.Range(0, 13); // to random child name
        if (randomChildChance <= 5)
        {
            // use child from good list
            childNameText.color = Color.black;
            childNameText.text = goodChild[childIndex].name;
        }
        else if (randomChildChance > 5)
        {
            // use child from blacklist
            childNameText.color = Color.red;
            childNameText.text = blackListChild[childIndex].name;
        }
    }
}
