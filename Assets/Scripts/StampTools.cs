using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StampTools : Tools
{
    [SerializeField] private bool isHaveInk;
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
        if (isHaveInk && Input.GetMouseButtonDown(1))
        {
            isHaveInk = false;
            Debug.Log("Deny Mail");
        }
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
                isHaveInk = false;
                Debug.Log("Approved Mail");
            }
            if (isHaveInk && Input.GetMouseButtonDown(1))
            {
                isHaveInk = false;
                Debug.Log("Deny Mail");
            }

            if (!isHaveInk && Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                Debug.Log("You have to fill an ink first");
            }
        }
    }
}
