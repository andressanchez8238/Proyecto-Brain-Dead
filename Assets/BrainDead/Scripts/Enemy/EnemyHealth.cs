using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private ZombieData zombieData;
    public WaveManager waveManager;

    private Animator animator;

    private AudioSource audioSource;

    private int currentLife;
    private bool dead;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();

        currentLife = zombieData.life;

        audioSource = GetComponent<AudioSource>();
    }

    public void TakeDamage(int damage)
    {
        if (dead)
            return;

        Debug.Log("Daño recibido: " + damage);

        currentLife -= damage;

        Debug.Log("Vida restante: " + currentLife);

        animator.SetTrigger("Hit");

        if (currentLife <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        dead = true;

        animator.SetTrigger("Death");

        NavMeshAgent agent = GetComponent<NavMeshAgent>();

        if (agent != null)
            agent.enabled = false;

        Collider[] colliders = GetComponentsInChildren<Collider>();

        foreach (Collider c in colliders)
            c.enabled = false;

        WaveManager.Instance.EnemyKilled();
        GameManager.Instance.zombiesKilled++;
        waveManager.EnemyKilled();

        if (audioSource != null)
        {
            audioSource.Stop();
        }

        Destroy(gameObject, 3f);
    }
}