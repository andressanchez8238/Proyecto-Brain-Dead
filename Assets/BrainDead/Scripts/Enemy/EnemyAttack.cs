using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] ZombieData zombieData;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerCollider"))
        {
            StatsPlayer player = collision.gameObject.GetComponentInParent<StatsPlayer>();
            player.DisminuirVida(zombieData.damage);
        }
    }
}
