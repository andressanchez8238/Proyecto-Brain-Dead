public class EnemyNode
{
    public ZombieData data;
    public EnemyNode next;

    public EnemyNode(ZombieData zombie)
    {
        data = zombie;
        next = null;
    }
}