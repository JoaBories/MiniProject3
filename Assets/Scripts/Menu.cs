using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void StartButton()
    {
        SceneManager.LoadScene("Level1");
    }

    public void ScreenButton()
    {
        SceneManager.LoadScene("Screen");
    }

    public void MenuButton()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(KeepInfo.sceneToRestartIndex);
    }

    public void ExitButton()
    {
        Application.Quit();
    }

}
