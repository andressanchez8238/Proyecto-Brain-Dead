using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public Weapon CurrentWeapon { get; private set; }

    public Axe CurrentAxe { get; private set; }

    public Transform handPoint;

    private GameObject currentObject;

    public void EquipItem(ItemDataBase item, WeaponState state)
    {
        if (item == null)
            return;

        if (currentObject != null)
            Destroy(currentObject);

        currentObject = Instantiate(item.handPrefab, handPoint.position, handPoint.rotation, handPoint);

        CurrentWeapon = null;
        CurrentAxe = null;

        Weapon weapon = currentObject.GetComponent<Weapon>();

        if (weapon != null)
        {
            CurrentWeapon = weapon;

            weapon.SetWeaponData(item as WeaponData);

            CurrentWeapon.Initialize(state);

            UIAmmo.Instance.UpdateAmmo(weapon.weaponData.itemName, state.currentAmmo, AmmoManager.Instance.GetAmmo(weapon.weaponData.ammoType));

            return;
        }

        Axe axe = currentObject.GetComponent<Axe>();

        if (axe != null)
        {
            CurrentAxe = axe;

            UIAmmo.Instance.UpdateWeapon(axe.axeData.itemName);

            return;
        }

        UIAmmo.Instance.Hide();
    }
}