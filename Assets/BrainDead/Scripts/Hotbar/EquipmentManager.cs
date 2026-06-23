using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public Weapon CurrentWeapon { get; private set; }

    public Transform handPoint;

    private GameObject currentObject;

    public void EquipItem(ItemDataBase item, WeaponState state)
    {
        if (item == null)
            return;

        if (currentObject != null)
        {
            Destroy(currentObject);
        }

        currentObject = Instantiate(item.handPrefab, handPoint.position, handPoint.rotation,handPoint);

        CurrentWeapon = currentObject.GetComponent<Weapon>();

        if (CurrentWeapon != null)
        {
            CurrentWeapon.Initialize(state);
        }
    }
}