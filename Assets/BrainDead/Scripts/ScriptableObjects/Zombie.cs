using UnityEngine;

[CreateAssetMenu(fileName = "Zombies", menuName = "Scriptable Objects/Zombies")]
public class Zombie : ScriptableObject
{
    public int ID;
    public int life;
    public float speed;
    public int damage;
    public Zombies typeZombie;
}
