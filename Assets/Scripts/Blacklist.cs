using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Blacklist : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> backListNameText;
    [SerializeField] private List<Child> badChild;
    
    public List<Child> BadChild
    {
        get => badChild;
        set => badChild = value;
    }

    public List<string> tempChild;
    private int randomInt;
    public List<int> tempInt;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var nameText in backListNameText)
        {
            randomInt = Random.Range(0, 13);
            tempInt.Add(randomInt);

            CheckDuplicateName();
            nameText.text = badChild[randomInt].name;
            tempChild.Add(badChild[randomInt].name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        RefreshBlackListName();
    }

    void RefreshBlackListName()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            tempChild.Clear();
            tempInt.Clear();
            foreach (var nameText in backListNameText)
            {
                randomInt = Random.Range(0, 13);
                tempInt.Add(randomInt);

                CheckDuplicateName();
                nameText.text = badChild[randomInt].name;
                tempChild.Add(badChild[randomInt].name);
            }
        }
    }

    void CheckDuplicateName()
    {
        foreach (var temp in tempChild)
        {
            if (temp == badChild[randomInt].name)
            {
                Debug.Log($"Name {temp} is already have");
                tempInt.Remove(randomInt);
                randomInt = RandomName();
                tempInt.Add(randomInt);
            }
        }
    }

    int RandomName()
    {
        int result;

        foreach (var temp in tempInt)
        {
            if (randomInt == temp)
            {
                randomInt = Random.Range(0, 13);
                result = randomInt;
                return result;
            }
        }

        return randomInt;
    }
    
}
