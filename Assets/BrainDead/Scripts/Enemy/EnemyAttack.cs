using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] ZombieData zombieData;

    private Animator animator;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetTrigger("Attack");
            StatsPlayer player = collision.gameObject.GetComponentInParent<StatsPlayer>();
            player.DisminuirVida(zombieData.damage);
        }
    }
    /*public void TakeDamage(float damage)
    {
        health -= damage;

        animator.SetTrigger("Hit");

        if (health <= 0)
        {
            animator.SetTrigger("Death");
        }
    }*/
}
