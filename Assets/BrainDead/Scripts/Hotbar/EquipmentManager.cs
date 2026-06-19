using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public Transform handPoint;

    private GameObject currentObject;

    public void EquipItem(ItemDataBase item)
    {
        if (item == null)
            return;

        if (currentObject != null)
        {
            Destroy(currentObject);
        }

        currentObject = Instantiate(item.handPrefab, handPoint.position, handPoint.rotation, handPoint);
    }
}