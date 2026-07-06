using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementBouncing : MonoBehaviour
{
    [Header("Zombie Data")]
    [SerializeField] private ZombieData zombieData;

    public Transform player;

    [Header("Detection")]
    [SerializeField] private float detectionRadius = 8f;
    [SerializeField] private float attackRange = 2f;

    [Header("Jump Configuration")]
    [SerializeField] private Transform meshVisual;
    [SerializeField] private float frequencyJump = 2f;
    [SerializeField] private float heightJump = 1.5f;

    private NavMeshAgent agent;
    private Animator animator;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();

        if (agent != null && zombieData != null)
            agent.speed = zombieData.speed;

        if (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

            if (playerObject != null)
                player = playerObject.transform;
        }
    }

    private void Update()
    {
        if (player == null)
            return;

        if (agent == null || !agent.isActiveAndEnabled || !agent.isOnNavMesh)
            return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > detectionRadius)
        {
            agent.ResetPath();

            if (animator != null) animator.SetBool("Walk", false);

            ResetJump();

            return;
        }

        if (distance > attackRange)
        {
            agent.isStopped = false;

            agent.SetDestination(player.position);

            if (animator != null) animator.SetBool("Walk", true);

            VisualJump();
        }
        else
        {
            agent.isStopped = true;

            if (animator != null) animator.SetBool("Walk", false);

            ResetJump();

            Vector3 lookPosition = player.position;
            lookPosition.y = transform.position.y;

            transform.LookAt(lookPosition);
        }
    }

    private void VisualJump()
    {
        if (meshVisual == null)
            return;

        float jump = Mathf.Abs(Mathf.Sin(Time.time * frequencyJump * Mathf.PI));

        Vector3 pos = meshVisual.localPosition;
        pos.y = jump * heightJump;
        meshVisual.localPosition = pos;
    }

    private void ResetJump()
    {
        if (meshVisual == null)
            return;

        Vector3 pos = meshVisual.localPosition;
        pos.y = Mathf.Lerp(pos.y, 0, Time.deltaTime * 10f);
        meshVisual.localPosition = pos;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position + Vector3.up, transform.forward * 5f);
    }
}