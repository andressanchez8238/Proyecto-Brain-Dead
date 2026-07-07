using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] TMP_Text zombiesText;
    [SerializeField] TMP_Text waveText;
    [SerializeField] TMP_Text timeText;

    private void Start()
    {
        zombiesText.text = "Zombies eliminados: " + GameManager.Instance.zombiesKilled;

        waveText.text = "Oleada alcanzada: " + GameManager.Instance.waveReached;

        int minutes = Mathf.FloorToInt(GameManager.Instance.survivalTime / 60);

        int seconds = Mathf.FloorToInt(GameManager.Instance.survivalTime % 60);

        timeText.text = $"Tiempo: {minutes:00}:{seconds:00}";
    }
}