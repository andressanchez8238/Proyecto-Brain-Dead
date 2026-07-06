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
}
