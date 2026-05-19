using UnityEngine;

[CreateAssetMenu(fileName = "ZombiesData", menuName = "BrainDead/Zombies")]
public class ZombieData : PrincipalData
{
    public int life;
    public float speed;
    public int damage;
    public Zombies typeZombie;
}
