using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int zombiesKilled;
    public int waveReached;
    public float survivalTime;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ResetStats()
    {
        zombiesKilled = 0;
        waveReached = 1;
        survivalTime = 0;
    }
    public void EndGame()
    {
        RankingManager.Instance.AddScore(new Score(GameManager.Instance.zombiesKilled, GameManager.Instance.waveReached, GameManager.Instance.survivalTime));
        SceneManager.LoadScene("Escena de Derrota");
    }
}