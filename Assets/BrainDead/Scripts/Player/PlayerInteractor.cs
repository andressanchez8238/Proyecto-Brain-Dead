using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractor : MonoBehaviour
{
    [Header("Equipment")]
    [SerializeField] private EquipmentManager equipmentManager;

    [Header("Grenades")]
    [SerializeField] private GameObject grenadePrefab;
    [SerializeField] private Transform grenadeSpawn;
    [SerializeField] private float throwForce = 10f;
    [SerializeField] private float upwardForce = 4f;
    [SerializeField] private float torqueForce = 8f;

    private PickableItem currentItem;

    private InputSystem_Actions inputs;

    private void Awake()
    {
        inputs = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        inputs.Enable();

        inputs.Player.Interact.performed += Interact;

        inputs.Player.Fire.performed += Fire;
        inputs.Player.Reload.performed += Reload;

        inputs.Player.Throw.performed += Throw;

        inputs.Player.Slot1.performed += ctx => SelectSlot(0);
        inputs.Player.Slot2.performed += ctx => SelectSlot(1);
        inputs.Player.Slot3.performed += ctx => SelectSlot(2);
        inputs.Player.Slot4.performed += ctx => SelectSlot(3);
    }

    private void OnDisable()
    {
        inputs.Player.Interact.performed -= Interact;
        
        inputs.Player.Fire.performed -= Fire;
        inputs.Player.Reload.performed -= Reload;

        inputs.Player.Throw.performed -= Throw;

        inputs.Disable();
    }

    private void SelectSlot(int slot)
    {
        ItemDataBase item = HotbarManager.Instance.GetItem(slot);

        if (item == null)
            return;

        WeaponState state = HotbarManager.Instance.GetWeaponState(slot);

        if (item is WeaponData && state == null)
        {
            Debug.LogWarning("Este slot es arma pero no tiene WeaponState");
            return;
        }

        equipmentManager.EquipItem(item, state);

        UIHotbar.Instance.SelectSlot(slot);

        Debug.Log($"Slot seleccionado: {slot + 1}");
    }

    private void Interact(InputAction.CallbackContext ctx)
    {
        if (currentItem == null)
            return;

        bool added = HotbarManager.Instance.AddItem(currentItem.itemData);

        if (added)
        {
            Debug.Log($"Recogiste {currentItem.itemData.itemName}");

            Destroy(currentItem.gameObject);
            currentItem = null;
        }
    }

    private void Fire(InputAction.CallbackContext ctx)
    {
        if (equipmentManager.CurrentWeapon != null)
        {
            equipmentManager.CurrentWeapon.Shoot();
            return;
        }

        if (equipmentManager.CurrentAxe != null)
        {
            equipmentManager.CurrentAxe.Attack();
            return;
        }

        Debug.Log("No hay arma equipada");
    }

    private void Reload(InputAction.CallbackContext ctx)
    {
        if (equipmentManager.CurrentWeapon == null)
            return;

        equipmentManager.CurrentWeapon.Reload();
    }

    private void Throw(InputAction.CallbackContext ctx)
    {
        if (!PlayerInventory.Instance.UseGrenade())
            return;

        GameObject grenade = Instantiate(grenadePrefab, grenadeSpawn.position, grenadeSpawn.rotation);

        Rigidbody rb = grenade.GetComponent<Rigidbody>();

        if (rb == null)
        {
            Debug.LogError("La granada no tiene Rigidbody.");
            return;
        }

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        Transform cam = Camera.main.transform;

        Vector3 throwDirection = cam.forward * throwForce + cam.up * upwardForce;

        rb.AddForce(throwDirection, ForceMode.Impulse);

        rb.AddTorque(Random.insideUnitSphere * torqueForce, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        PickableItem item = other.GetComponent<PickableItem>();

        if (item != null)
        {
            currentItem = item;
            Debug.Log($"Presiona E para recoger {item.itemData.itemName}");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PickableItem item = other.GetComponent<PickableItem>();

        if (item == currentItem)
        {
            currentItem = null;
            Debug.Log("Fuera del rango");
        }
    }
}