using UnityEngine;

public class HotbarManager : MonoBehaviour
{
    public static HotbarManager Instance;

    public HotbarSlot[] slots = new HotbarSlot[4];

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = new HotbarSlot();
        }
    }

    public bool AddItem(ItemDataBase item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].item = item;

                Debug.Log($"{item.itemName} agregado al slot {i}");

                return true;
            }
        }

        Debug.Log("Hotbar llena");

        return false;
    }

    public ItemDataBase GetItem(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= slots.Length)
            return null;

        return slots[slotIndex].item;
    }
}