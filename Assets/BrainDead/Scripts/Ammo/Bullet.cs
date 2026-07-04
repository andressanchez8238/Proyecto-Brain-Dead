using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 50;
    [SerializeField] float lifeTime = 5;

    int damage;

    public void Initialize(int newDamage)
    {
        damage = newDamage;
    }

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Golpeó: " + other.name);

        EnemyHealth enemy = other.GetComponentInParent<EnemyHealth>();

        if (enemy != null)
        {
            Debug.Log("Encontró EnemyHealth");

            enemy.TakeDamage(damage);
        }
    }
}