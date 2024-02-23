using System;
using UnityEngine;

public class Tools : MonoBehaviour
{
    
    public virtual void ToolsPositionToCursor()
    {
        if(ApprovedMailGameController.Instance.isOver || ApprovedMailGameController.Instance.isPause
           || ApprovedMailGameController.Instance.isDone) return;
        var mousePos = Input.mousePosition;
        mousePos.z = 8f; //distance of the plane from the camera
        transform.position = Camera.main.ScreenToWorldPoint(mousePos);
    }

    public virtual void CheckRaycastWithTarget()
    {
        
    }

}
