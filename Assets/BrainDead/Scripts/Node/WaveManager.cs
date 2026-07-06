using System.Collections;
using UnityEngine;
using UnityEngine.AI;

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

    private int aliveEnemies = 0;

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

            if (zombie != null)
            {
                currentWave.Enqueue(zombie);
            }
        }

        Debug.Log("Zombies en cola: " + currentWave.Count);
    }

    private IEnumerator SpawnWave()
    {
        while (!currentWave.IsEmpty())
        {
            ZombieData zombie = currentWave.Dequeue();

            SpawnZombie(zombie);

            yield return new WaitForSeconds(spawnDelay);
        }

        Debug.Log("Todos los zombies fueron generados.");
    }

    private void SpawnZombie(ZombieData zombie)
    {
        if (zombie == null)
        {
            Debug.LogError("ZombieData es NULL.");
            return;
        }

        if (zombie.prefab == null)
        {
            Debug.LogError($"El zombie '{zombie.name}' no tiene prefab asignado.");
            return;
        }

        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogError("No existen SpawnPoints asignados.");
            return;
        }

        Transform point = spawnPoints[Random.Range(0, spawnPoints.Length)];

        if (point == null)
        {
            Debug.LogError("Uno de los SpawnPoints está vacío.");
            return;
        }

        GameObject enemy = Instantiate(zombie.prefab, point.position, point.rotation);

        Debug.Log(enemy.transform.position);

        NavMeshAgent agent = enemy.GetComponent<NavMeshAgent>();

        if (agent != null)
        {
            NavMeshHit hit;

            if (NavMesh.SamplePosition(enemy.transform.position, out hit, 3f, NavMesh.AllAreas))
            {
                agent.Warp(hit.position);
            }
        }

        aliveEnemies++;

        EnemyHealth health = enemy.GetComponent<EnemyHealth>();

        if (health != null)
        {
            health.waveManager = this;
        }

        EnemyMovement movement = enemy.GetComponent<EnemyMovement>();

        if (movement != null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (player != null)
            {
                movement.player = player.transform;
            }
            else
            {
                Debug.LogError("No existe un objeto con el Tag 'Player'.");
            }
        }

        Debug.Log($"Zombie generado: {enemy.name}");
    }

    public void EnemyKilled()
    {
        aliveEnemies--;

        Debug.Log($"Zombies vivos: {aliveEnemies}");

        if (aliveEnemies <= 0)
        {
            wave++;

            Debug.Log($"Comienza la oleada {wave}");

            StartNextWave();
        }
    }
}