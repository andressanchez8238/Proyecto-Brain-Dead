using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponData weaponData;

    private WeaponState weaponState;

    private void Start()
    {
        weaponState.currentAmmo = weaponData.municionMax;

        RefreshUI();
    }
    public void Initialize(WeaponState state)
    {
        weaponState = state;

        RefreshUI();
    }

    public void Shoot()
    {
        if (weaponState.currentAmmo <= 0)
        {
            Debug.Log("Sin balas");
            return;
        }

        weaponState.currentAmmo--;

        Debug.Log($"Disparo. Balas: {weaponState.currentAmmo}");

        RefreshUI();        
    }

    public void Reload()
    {
        AmmoType ammoType = weaponData.ammoType;

        int reserveAmmo = AmmoManager.Instance.GetAmmo(ammoType);

        int neededAmmo = weaponData.municionMax - weaponState.currentAmmo;

        int ammoToLoad = Mathf.Min(neededAmmo, reserveAmmo);

        weaponState.currentAmmo += ammoToLoad;

        AmmoManager.Instance.RemoveAmmo(ammoType, ammoToLoad);

        Debug.Log($"Recargando {ammoToLoad}");

        RefreshUI();
    }

    private void RefreshUI()
    {
        int reserveAmmo = AmmoManager.Instance.GetAmmo(weaponData.ammoType);

        UIAmmo.Instance.UpdateAmmo(weaponData.itemName, weaponState.currentAmmo,reserveAmmo);
    }
}