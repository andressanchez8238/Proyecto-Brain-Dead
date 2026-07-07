using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject optionsMenu;
    public GameObject rankingPanel;

    public void OpenOptionsPanel()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }
    public void OpenMainMenu()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }
    public void OpenRankingPanel()
    {
        mainMenu.SetActive(false);

        rankingPanel.SetActive(true);
    }
    public void CloseRankingPanel()
    {
        rankingPanel.SetActive(false);

        mainMenu.SetActive(true);
    }
    public void PlayGame()
    {
        GameManager.Instance.ResetStats();
        SceneManager.LoadScene("Andres");
    }
    public void PlayMenu()
    {
        SceneManager.LoadScene("Pantalla de inicio");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
