using UnityEngine;

public abstract class Buff
{
    public string BuffName;
    public float Duration;

    public abstract void Apply(ZombieData entity);
    public abstract void Remove(ZombieData entity);

}
