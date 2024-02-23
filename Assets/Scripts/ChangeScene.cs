using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ChangeScene : MonoBehaviour
{
    public Texture2D cursor;

    private void Awake()
    {
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.ForceSoftware);
    }

    public void ManageScene(string name)
    {
        SoundManager.Instance.Play("Button");
        SceneManager.LoadSceneAsync(name);
    }
    
    public void GameQuit()
    {
        SoundManager.Instance.Play("Button");
        Application.Quit();
    }

    public void ClickButton()
    {
        SoundManager.Instance.Play("Button");
    }
    
}
