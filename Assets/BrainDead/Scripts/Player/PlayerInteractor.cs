using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] private EquipmentManager equipmentManager;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float interactDistance = 3f;

    private InputSystem_Actions inputs;

    private void Awake()
    {
        inputs = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        inputs.Enable();
        inputs.Player.Interact.performed += Interact;
        inputs.Player.Slot1.performed += ctx => SelectSlot(0);
        inputs.Player.Slot2.performed += ctx => SelectSlot(1);
        inputs.Player.Slot3.performed += ctx => SelectSlot(2);
        inputs.Player.Slot4.performed += ctx => SelectSlot(3);
    }

    private void OnDisable()
    {
        inputs.Disable();
    }

    private void SelectSlot(int slot)
    {
        ItemDataBase item = HotbarManager.Instance.GetItem(slot);

        if (item == null)
            return;

        equipmentManager.EquipItem(item);

        UIHotbar.Instance.SelectSlot(slot);

        Debug.Log($"Slot seleccionado: {slot + 1}");
    }
    private void Interact(InputAction.CallbackContext ctx)
    {
        Debug.Log("E presionada");
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
        {
            PickableItem item = hit.collider.GetComponent<PickableItem>();

            if (item == null)
            {
                return;
            }  

            bool added = HotbarManager.Instance.AddItem(item.itemData);

            if (added)
            {
                Destroy(item.gameObject);
            }
        }
    }
}
