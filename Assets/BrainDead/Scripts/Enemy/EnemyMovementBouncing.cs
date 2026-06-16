using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementBouncing : MonoBehaviour
{
    public Transform Player;
    public float speed = 3f;
    public Transform meshVisual;

    [Header("Detection Radio")]
    public float detectionRadius = 8f;

    [Header("Jump Configuration")]
    public float frequencyJump = 2f;
    public float heightJump = 1.5f;

    private NavMeshAgent agente;

    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Player != null)
        {
            float distanciaAlJugador = Vector3.Distance(transform.position, Player.position);

            if (distanciaAlJugador <= detectionRadius)
            {
                agente.SetDestination(Player.position);
            }
            else
            {
                if (agente.hasPath)
                {
                    agente.ResetPath();
                }
            }

            VisualJump();
        }
        void VisualJump()
        {
            if (agente.velocity.magnitude > 0.1f && agente.remainingDistance > agente.stoppingDistance)
            {
                float salto = Mathf.Abs(Mathf.Sin(Time.time * frequencyJump * Mathf.PI));

                Vector3 posicionLocal = meshVisual.localPosition;
                posicionLocal.y = salto * heightJump;
                meshVisual.localPosition = posicionLocal;
            }
            else
            {
                Vector3 posicionLocal = meshVisual.localPosition;
                posicionLocal.y = Mathf.Lerp(posicionLocal.y, 0, Time.deltaTime * 10f);
                meshVisual.localPosition = posicionLocal;
            }
        } 
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
