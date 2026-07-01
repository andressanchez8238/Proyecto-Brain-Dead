using NUnit.Framework.Interfaces;
using UnityEngine;

public class PickableItem : MonoBehaviour
{
    [SerializeField] private float pickupRadius = 2f;

    public ItemDataBase itemData;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(transform.position, pickupRadius);
    }
}
