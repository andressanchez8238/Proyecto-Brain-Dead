using UnityEngine;

public class SpeedBuff : Buff
{
    public float Amount = 5;

    public SpeedBuff(float duration , float amount)
    {
        BuffName = "SpeedBuff";
        Duration = duration;
        Amount = amount;
    }

    public override void Apply(ZombieData entity)
    {
        entity.speed += Amount;
    }
    public override void Remove(ZombieData entity)
    {
        entity.speed -= Amount;
    }
}
