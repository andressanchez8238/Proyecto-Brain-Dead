using UnityEngine;

[CreateAssetMenu(fileName = "ItemDataBase", menuName = "BrainDead/ItemDataBase")]
public abstract class ItemDataBase : ScriptableObject
{
    public int ID;

    public string itemName;

    public Sprite icon;

    public GameObject handPrefab;

    public ItemType itemType;
}
