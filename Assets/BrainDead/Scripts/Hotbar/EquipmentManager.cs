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

        Weapon weapon = currentObject.GetComponent<Weapon>();

        if (weapon != null)
        {
            weapon.SetWeaponData(item as WeaponData);

            CurrentWeapon = weapon;

            CurrentWeapon.Initialize(state);
        }

        CurrentWeapon = null;

        if (weapon != null)
        {
            CurrentWeapon = weapon;
            CurrentWeapon.Initialize(state);
        }
    }
}