using UnityEngine;

public class BuffPickUp : MonoBehaviour
{
    public  BuffType buffType;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out BuffManager buffManager))
        {
            Buff buff = BuffFactory.CreateBuff(buffType);
            buffManager.AddBuff(buff);

            GetComponent<Collider>().enabled = false;
            Destroy(gameObject, 1);
            Debug.Log("Collider");
        }
    }
}
