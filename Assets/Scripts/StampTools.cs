using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class StampTools : Tools
{
    [SerializeField] private bool isHaveInk;
    [SerializeField] private GameObject approvedIconPrefab;
    [SerializeField] private GameObject denyIconPrefab;
    
    private void Awake()
    {
        // Cursor.visible = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Cursor.visible = false;
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
            Cursor.visible = false;
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
                var iconRotate = Random.Range(-30f, 30f);
                Debug.Log($"rotation : {iconRotate}");
                approvedIconPrefab.transform.localRotation = Quaternion.Euler(0f, 0f, iconRotate);

                var approvedQuaternion = approvedIconPrefab.transform.localRotation;

                GameObject stampApproved = Instantiate(approvedIconPrefab, transform.position, approvedQuaternion);
                isHaveInk = false;
                Debug.Log("Approved Mail");
                Destroy(stampApproved, 1f);
            }
            
            if (isHaveInk && Input.GetMouseButtonDown(1))
            {
                var iconRotate = Random.Range(-30f, 30f);
                Debug.Log($"rotation : {iconRotate}");
                denyIconPrefab.transform.localRotation = Quaternion.Euler(0f, 0f, iconRotate);
                
                var denyQuaternion = denyIconPrefab.transform.localRotation;
                
                GameObject stampDeny = Instantiate(denyIconPrefab, transform.position, denyQuaternion);
                isHaveInk = false;
                Debug.Log("Deny Mail");
                Destroy(stampDeny, 1f);
            }

            if (!isHaveInk && Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                Debug.Log("You have to fill an ink first");
            }
        }
    }
}
