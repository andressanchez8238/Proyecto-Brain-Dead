using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float attackRange = 2f;

    public Transform player;
    public float speed = 3f;
    private NavMeshAgent agent;
    private Animator animator;

    [Header("Detection Radio")]
    public float detectionRange = 10f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= detectionRange)
        {
            agent.SetDestination(player.position);
        }
        else
        {
            if (agent.hasPath)
            {
                agent.ResetPath();
            }
        }

        animator.SetBool("Walk", agent.velocity.magnitude > 0.1f);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position + Vector3.up, transform.forward * 5f);
    }
}