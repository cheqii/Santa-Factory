using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Mail : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI childNameText;

    [Header("Children Data List")]
    [SerializeField] private List<Child> blackListChild;
    [SerializeField] private List<Child> goodChild;
    
    [SerializeField] private List<Child> allChildList;

    [SerializeField] public string childNameMail;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
