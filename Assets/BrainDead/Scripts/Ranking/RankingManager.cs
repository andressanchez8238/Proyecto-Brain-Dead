using System.Collections.Generic;
using UnityEngine;

public class RankingManager : MonoBehaviour
{
    public static RankingManager Instance;

    public List<Score> scores = new List<Score>();

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
    public void PrintRanking()
    {
        Debug.Log("====== RANKING ======");

        for (int i = 0; i < scores.Count; i++)
        {
            Debug.Log($"{i + 1}. " + $"Kills: {scores[i].zombiesKilled} | " + $"Wave: {scores[i].waveReached} | " + $"Time: {scores[i].survivalTime:F1}s");
        }
    }
    public void AddScore(Score score)
    {
        scores.Add(score);

        SortRanking();

        PrintRanking();
    }
    private void SortRanking()
    {
        if (scores.Count <= 1)
            return;

        scores = MergeSort(scores);
    }
    private List<Score> MergeSort(List<Score> list)
    {
        if (list.Count <= 1)
            return list;

        int middle = list.Count / 2;

        List<Score> left = list.GetRange(0, middle);
        List<Score> right = list.GetRange(middle, list.Count - middle);

        left = MergeSort(left);
        right = MergeSort(right);

        return Merge(left, right);
    }
    private List<Score> Merge(List<Score> left, List<Score> right)
    {
        List<Score> result = new List<Score>();

        int i = 0;
        int j = 0;

        while (i < left.Count && j < right.Count)
        {
            if (left[i].zombiesKilled >= right[j].zombiesKilled)
            {
                result.Add(left[i]);
                i++;
            }
            else
            {
                result.Add(right[j]);
                j++;
            }
        }

        while (i < left.Count)
        {
            result.Add(left[i]);
            i++;
        }

        while (j < right.Count)
        {
            result.Add(right[j]);
            j++;
        }

        return result;
    }
}