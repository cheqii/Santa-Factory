using System;
using UnityEngine;

public class Tools : MonoBehaviour
{
    private Camera cam;
    private Vector3 screenPoint;
    private Vector3 offset;

    private void Start()
    {
        cam = Camera.main;
    }

    public virtual void ToolsPositionToCursor()
    {
        if(GameController.Instance.isOver || GameController.Instance.isPause) return;
        var mousePos = Input.mousePosition;
        mousePos.z = 8f; //distance of the plane from the camera
        transform.position = cam.ScreenToWorldPoint(mousePos);
    }

    public virtual void CheckRaycastWithTarget()
    {
        
    }

}
