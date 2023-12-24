using MoreMountains.Feedbacks;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class StampTools : Tools
{
    [SerializeField] private bool isHaveInk;

    [Header("Stamp Correct Mail Count")]
    [SerializeField] private int stampCount;
    [SerializeField] private int tempStamp;

    [Header("Combo & Miss Count")]
    [SerializeField] private int comboCount;
    [SerializeField] private int missCount;
    
    [Header("Icon Stamp Prefabs")]
    [SerializeField] private GameObject approvedIconPrefab;
    [SerializeField] private GameObject denyIconPrefab;

    [Header("Text Animation Prefabs")]
    [SerializeField] private GameObject approvedTextAnim;
    [SerializeField] private GameObject denyTextAnim;
    [SerializeField] private GameObject fillInkTextAnim;
    [SerializeField] private GameObject getHeartTextAnim;
    [SerializeField] private GameObject comboTextAnim;

    private TMP_Text comboText;

    [Header("Animator & Animations")]
    [SerializeField] private Animator inkPadAnim;
    [SerializeField] private Animator childMailNameAnim;
    private Animator stampAnim;

    [Header("Canvas")]
    [SerializeField] private GameObject canvas;
    
    private bool approvedStamp;
    private bool denyStamp;

    private bool blackListCheck;
    
    private Mail _mail;

    #region -Feel Feedback-

    public UnityEvent Feedbacks;

    #endregion

    void Start()
    {
        stampAnim = GetComponent<Animator>();
        _mail = FindObjectOfType<Mail>();
        comboText = comboTextAnim.GetComponentInChildren<TextMeshProUGUI>().GetComponent<TMP_Text>();

        // StartCoroutine(_mail.RandomChildMail(2f));
    }
    
    // Update is called once per frame
    void Update()
    {
        if(GameController.Instance.isOver || GameController.Instance.isPause)
        {
            Cursor.visible = true;
            return;
        }
        ToolsPositionToCursor();
        CheckRaycastWithTarget();
    }

    public override void ToolsPositionToCursor()
    {
        Cursor.lockState = CursorLockMode.Confined;
        if (gameObject.tag == "StampTools")
        {
            Cursor.visible = false;
        }
        base.ToolsPositionToCursor();
    }

    public override void CheckRaycastWithTarget()
    {
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

        if (hit.collider == null) return;

        if (hit.collider.CompareTag("Ink") && Input.GetMouseButtonDown(0))
        {
            isHaveInk = true;
            Debug.Log("Fill Ink");
        }
        
        if (hit.collider.CompareTag("PaperMail"))
        {
            var sound = SoundManager.Instance;
            if (!isHaveInk && Input.GetMouseButtonDown(0) || !isHaveInk && Input.GetMouseButtonDown(1))
            {
                Debug.Log("You have to fill an ink first");
                sound.Play("FillInk");
                inkPadAnim.SetTrigger("InkShake");
                StampTextFloating(fillInkTextAnim,2f , quaternion.identity, 0.6f);
            }
            
            if (isHaveInk && Input.GetMouseButtonDown(0))
            {
                sound.Play("StampMail");
                approvedStamp = true;
                denyStamp = false;
                var iconRotate = Random.Range(-30f, 30f);
                approvedIconPrefab.transform.localRotation = Quaternion.Euler(0f, 0f, iconRotate);

                var approvedQuaternion = approvedIconPrefab.transform.localRotation;

                GameObject stampApproved = Instantiate(approvedIconPrefab, transform.position, approvedQuaternion);
                if (!CheckBlackListStatus())
                {
                    Debug.Log("Approved Mail");
                    childMailNameAnim.Play("GoodChildNameTextAnim");
                    stampAnim.Play("StampApproved");
                    StampTextFloating(approvedTextAnim,2.5f, approvedQuaternion, 0.5f);
                }
                StartCoroutine(_mail.RandomChildMail(0.75f));
                isHaveInk = false;
                Destroy(stampApproved, 0.5f);
            }

            if (isHaveInk && Input.GetMouseButtonDown(1))
            {
                sound.Play("StampMail");
                approvedStamp = false;
                denyStamp = true;
                var iconRotate = Random.Range(-30f, 30f);
                denyIconPrefab.transform.localRotation = Quaternion.Euler(0f, 0f, iconRotate);
                
                var denyQuaternion = denyIconPrefab.transform.localRotation;
                
                GameObject stampDeny = Instantiate(denyIconPrefab, transform.position, denyQuaternion);
                if (CheckBlackListStatus())
                {
                    Debug.Log("Deny Mail");
                    childMailNameAnim.Play("BadChildNameAnim");
                    stampAnim.Play("StampDenied");
                    StampTextFloating(denyTextAnim,2.5f , denyQuaternion, 0.5f);
                }
                StartCoroutine(_mail.RandomChildMail(0.75f));
                isHaveInk = false;
                Destroy(stampDeny, 0.5f);
            }
        }
    }

    public bool CheckBlackListStatus()
    {
        var child = _mail.ChildNameCheck;
        var list = GameController.Instance.allChildren;

        foreach (var all in list)
        {
            if (child == all.name)
            {
                if (all.onBlacklist) // Child name on mail is on blacklist
                {
                    Debug.Log("On Blacklist");
                    if (approvedStamp)
                    {
                        if(Feedbacks != null) Feedbacks.Invoke();
                        stampCount = 0;
                        comboCount = 0;
                        comboText.text = $"Miss";
                        missCount++;
                        childMailNameAnim.Play("missChildNameAnim");
                        StampTextFloating(comboTextAnim, 2f, quaternion.identity, 1f);
                        _mail.DecreaseStampMailCount(1);
                        GameController.Instance.DecreaseLives(1);
                    }
                    return true;
                }
                
                Debug.Log("Good Child");
                if (denyStamp)
                {
                    if(Feedbacks != null) Feedbacks.Invoke();
                    stampCount = 0;
                    comboCount = 0;
                    comboText.text = $"Miss";
                    missCount++;
                    childMailNameAnim.Play("missChildNameAnim");
                    StampTextFloating(comboTextAnim, 3f, quaternion.identity, 1f);
                    _mail.DecreaseStampMailCount(1);
                    GameController.Instance.DecreaseLives(1);
                }

                if (approvedStamp)
                {
                    tempStamp = stampCount;
                    stampCount++;
                    comboCount++;
                    
                    if (comboCount > 0)
                    {
                        comboText.text = $"{comboCount} Combo";
                        StampTextFloating(comboTextAnim, -2f, quaternion.identity, 2f);
                    }
                    if (tempStamp == 4)
                    {
                        Debug.Log("Combo 5 mail and get increase lives");
                        if(GameController.Instance.currentLives < GameController.Instance.maxLives)
                            StampTextFloating(getHeartTextAnim,3.5f ,
                            quaternion.identity, 1.5f);
                        GameController.Instance.IncreaseLives(1);
                        tempStamp = 0;
                        stampCount = 0;
                    }
                    _mail.IncreaseStampMailCount(1);
                }
                return false;
            }
        }

        return false;
    }

    void StampTextFloating(GameObject prefab, float height,Quaternion rotate, float time)
    {
        // standard height is 2f
        var textTrans = new Vector3(transform.position.x, transform.position.y + height, transform.position.z);
        var textAnim = Instantiate(prefab, textTrans,
            rotate, canvas.transform);
        Destroy(textAnim, time);
    }
}
