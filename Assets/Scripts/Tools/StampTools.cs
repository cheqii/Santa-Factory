using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class StampTools : Tools
{
    [SerializeField] private bool isHaveInk;
    
    [Header("Icon Stamp Prefabs")]
    [SerializeField] private GameObject approvedIconPrefab;
    [SerializeField] private GameObject denyIconPrefab;

    [Header("Text Animation Prefabs")]
    [SerializeField] private GameObject approvedTextAnim;
    [SerializeField] private GameObject denyTextAnim;

    [SerializeField] private GameObject canvas;
    
    private bool approvedStamp;
    private bool denyStamp;

    private bool blackListCheck;
    
    private Mail _mail;

    void Start()
    {
        _mail = FindObjectOfType<Mail>();
    }
    
    // Update is called once per frame
    void Update()
    {
        ToolsPositionToCursor();
        CheckRaycastWithTarget();
    }

    public override void ToolsPositionToCursor()
    {
        if (gameObject.tag == "StampTools")
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;
        }
        else Cursor.visible = true;
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
            if (isHaveInk && Input.GetMouseButtonDown(0))
            {
                approvedStamp = true;
                denyStamp = false;
                var iconRotate = Random.Range(-30f, 30f);
                approvedIconPrefab.transform.localRotation = Quaternion.Euler(0f, 0f, iconRotate);

                var approvedQuaternion = approvedIconPrefab.transform.localRotation;

                GameObject stampApproved = Instantiate(approvedIconPrefab, transform.position, approvedQuaternion);
                if (!CheckBlackListStatus())
                {
                    Debug.Log("Approved Mail");
                    StampTextFloating(approvedTextAnim, approvedQuaternion);
                }
                _mail.RandomChildMail();
                isHaveInk = false;
                Destroy(stampApproved, 1f);
            }
            
            if (isHaveInk && Input.GetMouseButtonDown(1))
            {
                approvedStamp = false;
                denyStamp = true;
                var iconRotate = Random.Range(-30f, 30f);
                denyIconPrefab.transform.localRotation = Quaternion.Euler(0f, 0f, iconRotate);
                
                var denyQuaternion = denyIconPrefab.transform.localRotation;
                
                GameObject stampDeny = Instantiate(denyIconPrefab, transform.position, denyQuaternion);
                if (CheckBlackListStatus())
                {
                    Debug.Log("Deny Mail");
                    StampTextFloating(denyTextAnim, denyQuaternion);
                }
                _mail.RandomChildMail();
                isHaveInk = false;
                Destroy(stampDeny, 1f);
            }

            if (!isHaveInk && Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                Debug.Log("You have to fill an ink first");
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
                if (all.onBlacklist)
                {
                    Debug.Log("On Blacklist");
                    if (approvedStamp)
                    {
                        _mail.DecreaseStampMailCount(1);
                        GameController.Instance.DecreaseLives(1);
                    }
                    return true;
                }
                
                Debug.Log("Good Child");
                if (denyStamp)
                {
                    _mail.DecreaseStampMailCount(1);
                    GameController.Instance.DecreaseLives(1);
                }
                if(approvedStamp) _mail.IncreaseStampMailCount(1);
                return false;
            }
        }

        return false;
    }

    void StampTextFloating(GameObject prefab, Quaternion rotate)
    {
        var textTrans = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z);
        var textAnim = Instantiate(prefab, textTrans,
            rotate, canvas.transform);
        Destroy(textAnim, 1f);
    }
}
