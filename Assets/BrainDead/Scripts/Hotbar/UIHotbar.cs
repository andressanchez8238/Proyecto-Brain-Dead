using UnityEngine;
using UnityEngine.UI;

public class UIHotbar : MonoBehaviour
{
    public static UIHotbar Instance;
    public UIHotbarSlot[] slots;
    public int selectedSlot;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        Refresh();
    }
    public void Refresh()
    {
        for (int i = 0; i < 4; i++)
        {
            ItemDataBase item = HotbarManager.Instance.slots[i].item;

            if (item != null)
            {
                slots[i].icon.enabled = true;
                slots[i].icon.sprite = item.icon;
            }
            else
            {
                slots[i].icon.enabled = false;
            }
        }
    }
    public void SelectSlot(int index)
    {
        selectedSlot = index;

        for (int i = 0; i < slots.Length; i++)
        {
            if (i == selectedSlot)
            {
                slots[i].background.color = Color.yellow;
            }
            else
            {
                slots[i].background.color = Color.white;
            }
        }
    }
}
