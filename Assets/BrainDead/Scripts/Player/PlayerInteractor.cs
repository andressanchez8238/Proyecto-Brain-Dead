using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] private EquipmentManager equipmentManager;

    private InputSystem_Actions inputs;

    private void Awake()
    {
        inputs = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        inputs.Enable();

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

        Debug.Log($"Slot seleccionado: {slot + 1}");
    }
}
