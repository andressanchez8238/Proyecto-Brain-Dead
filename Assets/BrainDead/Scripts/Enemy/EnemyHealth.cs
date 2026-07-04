using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private ZombieData zombieData;

    private Animator animator;

    private int currentLife;
    private bool dead;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();

        currentLife = zombieData.life;
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

        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;

        Collider[] colliders = GetComponentsInChildren<Collider>();

        foreach (Collider c in colliders)
            c.enabled = false;

        Destroy(gameObject, 3f);
    }
}
