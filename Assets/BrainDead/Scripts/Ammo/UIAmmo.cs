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

    public void UpdateAmmo(string weaponName,int currentAmmo,int reserveAmmo)
    {
        weaponNameText.text = weaponName;

        ammoText.text = $"{currentAmmo} / {reserveAmmo}";
    }

    public void Hide()
    {
        weaponNameText.text = "";
        ammoText.text = "";
    }
}