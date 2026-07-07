using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private ZombieData zombieData;
    public WaveManager waveManager;

    private Animator animator;
    private ParticleSystem BloodZombie;

    private int currentLife;
    private bool dead;

    private void Awake()
    {
        BloodZombie = GetComponentInChildren<ParticleSystem>();
        animator = GetComponentInChildren<Animator>();

        currentLife = zombieData.life;
    }

    public void TakeDamage(int damage)
    {
        if (dead)
            return;

        Debug.Log("Daño recibido: " + damage);

        currentLife -= damage;
        ActiveBlood();

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
        Destroy(gameObject, 3f);
    }
    void ActiveBlood()
    {
        BloodZombie.Play();
    }
}