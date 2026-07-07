using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;

    [SerializeField] private ZombieData zombieData;

    [SerializeField] private float detectionRange = 10f;
    [SerializeField] private float attackRange = 2f;

    [SerializeField] private float graphUpdateInterval = 1f;
    private List<GraphNode> currentPath;

    private int currentNodeIndex;
    private float graphUpdateTimer;
    
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

        UpdateGraphPath();
    }
    private void UpdateGraphPath()
    {
        if (player == null)
            return;

        GraphNode start = GraphManager.Instance.GetClosestNode(transform.position);

        GraphNode goal = GraphManager.Instance.GetClosestNode(player.position);

        currentPath = GraphSearch.FindPath(start, goal);

        currentNodeIndex = 0;
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

        graphUpdateTimer += Time.deltaTime;

        if (graphUpdateTimer >= graphUpdateInterval)
        {
            graphUpdateTimer = 0f;
            UpdateGraphPath();
        }

        if (distance > attackRange)
        {
            agent.isStopped = false;

            if (currentPath != null && currentNodeIndex < currentPath.Count)
            {
                GraphNode node = currentPath[currentNodeIndex];

                agent.SetDestination(node.transform.position);

                if (Vector3.Distance(transform.position, node.transform.position) < 1f)
                {
                    currentNodeIndex++;
                }
            }
            else
            {
                agent.SetDestination(player.position);
            }

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
    private void OnDrawGizmosSelected()
    {
        if (currentPath == null)
            return;

        Gizmos.color = Color.cyan;

        for (int i = 0; i < currentPath.Count - 1; i++)
        {
            Gizmos.DrawLine(currentPath[i].transform.position, currentPath[i + 1].transform.position);
        }
    }
}