using UnityEngine;

public class AmmoManager : MonoBehaviour
{
    public static AmmoManager Instance;

    public int pistolAmmo = 90;
    public int rifleAmmo = 120;
    public int shotgunAmmo = 30;

    private void Awake()
    {
        Instance = this;
    }

    public int GetAmmo(AmmoType type)
    {
        switch (type)
        {
            case AmmoType.Pistol:
                return pistolAmmo;

            case AmmoType.Rifle:
                return rifleAmmo;

            case AmmoType.Shotgun:
                return shotgunAmmo;
        }

        return 0;
    }

    public void RemoveAmmo(AmmoType type, int amount)
    {
        switch (type)
        {
            case AmmoType.Pistol:
                pistolAmmo -= amount;
                break;

            case AmmoType.Rifle:
                rifleAmmo -= amount;
                break;

            case AmmoType.Shotgun:
                shotgunAmmo -= amount;
                break;
        }
    }
}