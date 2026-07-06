using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance;

    public int Wood { get; private set; }

    public int Grenades { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        AddGrenades(10);
    }

    #region Wood

    public void AddWood(int amount)
    {
        Wood += amount;
    }

    public void RemoveWood(int amount)
    {
        Wood = Mathf.Max(0, Wood - amount);
    }

    #endregion

    #region Grenades

    public void AddGrenades(int amount)
    {
        Grenades += amount;
    }

    public bool UseGrenade()
    {
        if (Grenades <= 0)
            return false;

        Grenades--;

        return true;
    }

    #endregion
}