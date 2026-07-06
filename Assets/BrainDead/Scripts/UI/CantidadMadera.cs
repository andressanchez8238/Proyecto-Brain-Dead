using TMPro;
using UnityEngine;

public class CantidadMadera : MonoBehaviour
{
    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        text.text = ": " + PlayerInventory.Instance.Wood;
    }
}
