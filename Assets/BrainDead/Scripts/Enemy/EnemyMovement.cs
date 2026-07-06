using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;

    [SerializeField] private ZombieData zombieData;

    [SerializeField] private float detectionRange = 10f;
    [SerializeField] private float attackRange = 2f;

    private NavMeshAgent agent;
    private Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();

        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null)
                player = p.transform;
        }

        if (agent != null && zombieData != null)
        {
            agent.speed = zombieData.speed;
        }
    }

    void Update()
    {
        if (player == null)
            return;

        if (agent == null || !agent.isActiveAndEnabled || !agent.isOnNavMesh)
            return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > detectionRange)
        {
            agent.ResetPath();
            animator.SetBool("Walk", false);
            return;
        }

        if (distance > attackRange)
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);
            animator.SetBool("Walk", true);
        }
        else
        {
            agent.isStopped = true;
            animator.SetBool("Walk", false);
            transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}