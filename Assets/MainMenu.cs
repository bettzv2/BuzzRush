using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject aboutWindow;
    public GameObject blackoutPanel;

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void OpenAbout()
    {
        SceneManager.LoadScene("About");
    }

    public void QuitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
