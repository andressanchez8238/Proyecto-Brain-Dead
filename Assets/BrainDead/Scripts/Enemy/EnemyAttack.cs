using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private ZombieData zombieData;

    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float attackCooldown = 1.5f;

    private Transform player;
    private Animator animator;

    private float timer;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();

        GameObject p = GameObject.FindGameObjectWithTag("Player");

        if (p != null)
            player = p.transform;
    }

    private void Update()
    {
        if (player == null)
            return;

        timer += Time.deltaTime;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= attackRange && timer >= attackCooldown)
        {
            timer = 0;

            animator.SetTrigger("Attack");

            StatsPlayer stats = player.GetComponent<StatsPlayer>();

            if (stats != null)
                stats.DisminuirVida(zombieData.damage);
        }
    }
}