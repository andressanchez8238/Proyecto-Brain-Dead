using TMPro;
using UnityEngine;

public class CantidadGranada : MonoBehaviour
{
    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        text.text = ": " + PlayerInventory.Instance.Grenades;
    }
}