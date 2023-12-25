using UnityEngine;
using UnityEngine.SceneManagement;


public class ChangeScene : MonoBehaviour
{
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
