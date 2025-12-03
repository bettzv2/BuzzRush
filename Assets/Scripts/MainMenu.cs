using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject aboutWindow;
    public GameObject blackoutPanel;

    public void StartGame()
    {
        SceneManager.LoadScene("Game1");
    }

    public void ShowAbout()
    {
        if (blackoutPanel != null)
            blackoutPanel.SetActive(true);

        if (aboutWindow != null)
            aboutWindow.SetActive(true);
    }

    public void HideAbout()
    {
        if (aboutWindow != null)
            aboutWindow.SetActive(false);

        if (blackoutPanel != null)
            blackoutPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
