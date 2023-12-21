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

    private List<Child> tempChild;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var nameText in backListNameText)
        {
            var randomInt = Random.Range(0, 11);
            for (int i = 0; i < backListNameText.Count; i++)
            {
                nameText.text = badChild[randomInt].name;
            }
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
            foreach (var nameText in backListNameText)
            {
                var randomInt = Random.Range(0, 10);
                for (int i = 0; i < backListNameText.Count; i++)
                {
                    nameText.text = badChild[randomInt].name;
                }
            }
        }
    }
    
}
