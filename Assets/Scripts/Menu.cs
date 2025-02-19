using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;

    private void Start()
    {
        if (_scoreText != null) _scoreText.text = "Score: " + KeepInfo.score;
    }

    public void StartButton()
    {
        SceneManager.LoadScene("Level1");
        SoundManager.instance.PlaySound("blip", Vector3.zero);
    }

    public void ScreenButton()
    {
        SceneManager.LoadScene("Screen");
        SoundManager.instance.PlaySound("blip", Vector3.zero);
    }

    public void MenuButton()
    {
        SceneManager.LoadScene("StartMenu");
        SoundManager.instance.PlaySound("blip", Vector3.zero);
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(KeepInfo.sceneToRestartIndex);
        SoundManager.instance.PlaySound("blip", Vector3.zero);
    }

    public void ExitButton()
    {
        SoundManager.instance.PlaySound("blip", Vector3.zero);
        Application.Quit();
    }

}
