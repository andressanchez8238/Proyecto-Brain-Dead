using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "BrainDead/Weapon")]
public class WeaponData : ItemDataBase
{
    public int municionMax;

    public float damage;

    public float fireRate;

    public Weapons typeWeapon;

    public AmmoType ammoType;

    public GameObject bulletPrefab;
}
