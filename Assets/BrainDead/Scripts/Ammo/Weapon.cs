using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform bulletSpawn;

    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AudioClip shootSound;

    private Camera playerCamera;

    public WeaponData weaponData;

    private WeaponState weaponState;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        bulletSpawn = transform.GetComponentInChildren<Transform>();

        foreach (Transform child in GetComponentsInChildren<Transform>())
        {
            if (child.name.StartsWith("SpawnPoint"))
            {
                bulletSpawn = child;

                Debug.Log("Spawn encontrado: " + child.name);

                break;
            }
        }
    }

    public void Initialize(WeaponState state)
    {
        weaponState = state;

        playerCamera = Camera.main;

        RefreshUI();
    }

    public void Shoot()
    {
        if (weaponState == null)
        {
            Debug.LogError("WeaponState es NULL");
            return;
        }

        if (weaponData == null)
        {
            Debug.LogError("WeaponData es NULL");
            return;
        }

        if (weaponState.currentAmmo <= 0)
        {
            Debug.Log("Sin balas");
            return;
        }

        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        Vector3 targetPoint;

        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(100f);

        GameObject bullet = Instantiate(weaponData.bulletPrefab, bulletSpawn.position, Quaternion.identity);

        Bullet bulletScript = bullet.GetComponent<Bullet>();

        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }

        bulletScript.Initialize(weaponData.damage);

        Vector3 direction = (targetPoint-bulletSpawn.position).normalized;

        bullet.transform.forward = direction;

        weaponState.currentAmmo--;

        RefreshUI();
    }
    public void SetWeaponData(WeaponData data)
    {
        weaponData = data;
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

    private void OnDrawGizmosSelected()
    {
        if (bulletSpawn == null)
            return;

        Gizmos.color = Color.cyan;

        Gizmos.DrawRay(bulletSpawn.position, bulletSpawn.forward * 10f);
    }
}