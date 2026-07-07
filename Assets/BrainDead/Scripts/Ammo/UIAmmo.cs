using TMPro;
using UnityEngine;

public class UIAmmo : MonoBehaviour
{
    public static UIAmmo Instance;

    [SerializeField] private TMP_Text weaponNameText;
    [SerializeField] private TMP_Text ammoText;

    private void Awake()
    {
        Instance = this;
    }

    // Armas de fuego
    public void UpdateAmmo(string weaponName, int currentAmmo, int reserveAmmo)
    {
        weaponNameText.text = weaponName;
        ammoText.text = $"{currentAmmo} / {reserveAmmo}";
    }

    // Herramientas o armas sin munición
    public void UpdateWeapon(string weaponName)
    {
        weaponNameText.text = weaponName;
        ammoText.text = "--";
    }

    public void Hide()
    {
        weaponNameText.text = "";
        ammoText.text = "";
    }
}