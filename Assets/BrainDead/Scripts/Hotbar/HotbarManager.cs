using NUnit.Framework.Interfaces;
using UnityEngine;

public class HotbarManager : MonoBehaviour
{
    public static HotbarManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public HotbarSlot[] slots = new HotbarSlot[4];

    private void Start()
    {
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

                return true;
            }
        }

        return false;
    }
}