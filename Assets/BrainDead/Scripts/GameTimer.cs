using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private float gameTime = 300f;

    [SerializeField] private TMP_Text timerText;

    private bool finished;

    private void Update()
    {
        if (finished)
            return;

        gameTime -= Time.deltaTime;

        if (gameTime < 0)
            gameTime = 0;

        int minutes = Mathf.FloorToInt(gameTime / 60);

        int seconds = Mathf.FloorToInt(gameTime % 60);

        timerText.text = $"{minutes:00}:{seconds:00}";

        GameManager.Instance.survivalTime = 300 - gameTime;

        if (gameTime <= 0)
        {
            FinishGame();
        }
    }

    void FinishGame()
    {
        finished = true;

        RankingManager.Instance.scores.Add(new Score( GameManager.Instance.zombiesKilled, GameManager.Instance.waveReached, GameManager.Instance.survivalTime));
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("Pantalla de Victoria");


    }
}