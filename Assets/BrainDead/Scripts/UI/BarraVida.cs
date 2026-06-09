using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BarraVida : MonoBehaviour
{
    public Image Relleno;
    private StatsPlayer Player;
    private float BarraLlena;
    public TextMeshProUGUI Vida;

    void Start()
    {
        Relleno = gameObject.GetComponent<Image>();
        Player = GameObject.FindWithTag("Player").GetComponent<StatsPlayer>();
        BarraLlena = Player.MaxVida;
    }
    void Update()
    {
        Relleno.fillAmount = Player.VidaActual / BarraLlena;
        Vida.text= Player.VidaActual+" / "+BarraLlena;
    }
}
