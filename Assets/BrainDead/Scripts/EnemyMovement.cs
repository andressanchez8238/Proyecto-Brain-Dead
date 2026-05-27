using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;
    public float speed = 3f;
    public float detectionRange = 10f;

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= detectionRange)
        {
            Vector3 dir = (player.position - transform.position).normalized;
            transform.position += dir * speed * Time.deltaTime;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}