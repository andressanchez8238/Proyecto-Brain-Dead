using UnityEngine;

[CreateAssetMenu(fileName = "Zombies", menuName = "Scriptable Objects/Zombies")]
public class Zombie : ScriptableObjectPrincipal
{
    public int life;
    public float speed;
    public int damage;
    public Zombies typeZombie;
}
