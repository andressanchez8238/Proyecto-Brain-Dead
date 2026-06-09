using TMPro;
using UnityEngine;

public class CantidadGranada : MonoBehaviour
{
    TextMeshProUGUI Granada;
    private PlayerInventory Player;
    private void Awake()
    {
        Granada = GetComponent<TextMeshProUGUI>();
        Player = GameObject.FindWithTag("Player").GetComponent<PlayerInventory>();
    }
    void Update()
    {
        Granada.text = ": " + Player.Granada;
    }
}
