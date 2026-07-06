using UnityEngine;
using System.Collections;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;

    [Header("Database")]
    [SerializeField] private ZombiesDataBase zombieDatabase;

    [Header("Spawn Points")]
    [SerializeField] private Transform[] spawnPoints;

    [SerializeField] private float spawnDelay = 2f;

    private EnemyQueue currentWave = new EnemyQueue();

    private int wave = 1;

    private int zombiesAlive = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartNextWave();
    }

    private void StartNextWave()
    {
        Debug.Log($"===== OLEADA {wave} =====");

        GenerateWave();

        StartCoroutine(SpawnWave());
    }

    private void GenerateWave()
    {
        currentWave = new EnemyQueue();

        for (int i = 0; i < wave * 5; i++)
        {
            ZombieData zombie = zombieDatabase.GetZombie();

            currentWave.Enqueue(zombie);
        }

        Debug.Log("Zombies en cola: " + currentWave.Count);
    }

    IEnumerator SpawnWave()
    {
        while (!currentWave.IsEmpty())
        {
            ZombieData zombie = currentWave.Dequeue();

            SpawnZombie(zombie);

            yield return new WaitForSeconds(spawnDelay);
        }

        Debug.Log("Todos los zombies fueron generados");
    }

    private void SpawnZombie(ZombieData zombie)
    {
        Transform point = spawnPoints[Random.Range(0, spawnPoints.Length)];

        Instantiate(zombie.prefab, point.position, Quaternion.identity);

        zombiesAlive++;

        Debug.Log("Zombies vivos: " + zombiesAlive);
    }

    public void ZombieKilled()
    {
        zombiesAlive--;

        Debug.Log("Zombie eliminado");

        Debug.Log("Quedan vivos: " + zombiesAlive);

        if (zombiesAlive <= 0 && currentWave.IsEmpty())
        {
            wave++;

            StartNextWave();
        }
    }
}