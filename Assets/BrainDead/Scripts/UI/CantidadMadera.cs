using TMPro;
using UnityEngine;

public class CantidadMadera : MonoBehaviour
{
    TextMeshProUGUI Madera;
    private PlayerInventory Player;
    private void Awake()
    {
        Madera = GetComponent<TextMeshProUGUI>();
        Player=GameObject.FindWithTag("Player").GetComponent<PlayerInventory>();
    }
    void Update()
    {
        Madera.text = ": " + Player.Madera;
    }
}
