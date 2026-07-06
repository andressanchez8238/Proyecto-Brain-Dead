using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float explosionDelay = 3f;
    public float explosionRadius = 5f;
    public int damage = 50;

    private void Start()
    {
        Invoke(nameof(Explode), explosionDelay);
    }

    void Explode()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider hit in hits)
        {
            EnemyHealth enemy = hit.GetComponent<EnemyHealth>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }

        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}