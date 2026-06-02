using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementBouncing : MonoBehaviour
{
    public Transform Player;
    public Transform meshVisual;

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
            agente.SetDestination(Player.position);
        }

        VisualJump();
    }
    void VisualJump()
    {
        if (agente.velocity.magnitude > 0.1f && agente.remainingDistance > agente.stoppingDistance)
        {
            float salto = Mathf.Abs(Mathf.Sin(Time.time * heightJump * Mathf.PI));

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
