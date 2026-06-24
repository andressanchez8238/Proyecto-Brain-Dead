using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] private EquipmentManager equipmentManager;

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
        Debug.Log("CLICK IZQUIERDO");

        if (equipmentManager.CurrentWeapon == null)
        {
            Debug.Log("No hay arma equipada");
            return;
        }

        Debug.Log("Intentando disparar");

        equipmentManager.CurrentWeapon.Shoot();
    }

    private void Reload(InputAction.CallbackContext ctx)
    {
        if (equipmentManager.CurrentWeapon == null)
            return;

        equipmentManager.CurrentWeapon.Reload();
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