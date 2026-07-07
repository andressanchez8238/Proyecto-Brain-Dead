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

                if (item is WeaponData weapon)
                {
                    slots[i].weaponState = new WeaponState();

                    slots[i].weaponState.weaponData = weapon;

                    slots[i].weaponState.currentAmmo = weapon.municionMax;
                }

                SortInventory();

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

    public WeaponState GetWeaponState(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= slots.Length)
            return null;

        return slots[slotIndex].weaponState;
    }
    private int GetDamage(HotbarSlot slot)
    {
        if (slot == null)
            return -1;

        if (slot.item == null)
            return -1;

        if (slot.item is WeaponData weapon)
            return weapon.damage;

        return 0;
    }

    public void SortInventory()
    {
        HotbarSlot[] occupied = new HotbarSlot[slots.Length];
        int count = 0;

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                occupied[count] = slots[i];
                count++;
            }
        }

        for (int i = 1; i < count; i++)
        {
            HotbarSlot key = occupied[i];

            int j = i - 1;

            while (j >= 0 && GetDamage(occupied[j]) < GetDamage(key))
            {
                occupied[j + 1] = occupied[j];
                j--;
            }

            occupied[j + 1] = key;
        }

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = new HotbarSlot();
        }

        for (int i = 0; i < count; i++)
        {
            slots[i] = occupied[i];
        }

        UIHotbar.Instance.Refresh();

        Debug.Log("Inventario ordenado con Insertion Sort");
    }
}