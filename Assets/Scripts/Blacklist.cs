using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class Blacklist : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> backListNameText;
    [SerializeField] private List<Child> badChild;

    [SerializeField] private List<string> childNotes;

    public List<string> ChildNotes
    {
        get => childNotes;
        set => childNotes = value;
    }

    private int randomInt;
    [SerializeField] private List<int> tempIndex;

    public List<int> TempIndex
    {
        get => tempIndex;
        set => tempIndex = value;
    }

// Start is called before the first frame update
    void Start()
    {
        foreach (var nameText in backListNameText)
        {
            randomInt = Random.Range(0, 13);
            tempIndex.Add(randomInt);

            CheckDuplicateName();
            nameText.text = badChild[randomInt].name;
            childNotes.Add(badChild[randomInt].name);
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
            childNotes.Clear();
            tempIndex.Clear();
            foreach (var nameText in backListNameText)
            {
                randomInt = Random.Range(0, 13);
                tempIndex.Add(randomInt);

                CheckDuplicateName();
                nameText.text = badChild[randomInt].name;
                childNotes.Add(badChild[randomInt].name);
            }
        }
    }

    void CheckDuplicateName()
    {
        foreach (var temp in childNotes)
        {
            if (temp == badChild[randomInt].name)
            {
                // Debug.Log($"Name {temp} is already have");
                tempIndex.Remove(randomInt);
                randomInt = RandomNewIndex();
                tempIndex.Add(randomInt);
                CheckDuplicateName(); // check again boi
            }
        }
    }

    int RandomNewIndex()
    {
        int result;

        foreach (var temp in tempIndex)
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
