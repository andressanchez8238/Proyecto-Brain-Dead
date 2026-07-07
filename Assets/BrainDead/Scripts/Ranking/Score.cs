using System;

[Serializable]
public class Score
{
    public int zombiesKilled;
    public int waveReached;
    public float survivalTime;

    public Score(int zombies, int wave, float time)
    {
        zombiesKilled = zombies;
        waveReached = wave;
        survivalTime = time;
    }
}