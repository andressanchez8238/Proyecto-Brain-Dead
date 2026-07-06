public class EnemyQueue
{
    private EnemyNode front;
    private EnemyNode rear;

    public int Count { get; private set; }

    public bool IsEmpty()
    {
        return front == null;
    }

    public void Enqueue(ZombieData zombie)
    {
        EnemyNode node = new EnemyNode(zombie);

        if (rear == null)
        {
            front = rear = node;
        }
        else
        {
            rear.next = node;
            rear = node;
        }

        Count++;
    }

    public ZombieData Dequeue()
    {
        if (IsEmpty())
            return null;

        ZombieData zombie = front.data;

        front = front.next;

        if (front == null)
            rear = null;

        Count--;

        return zombie;
    }
}