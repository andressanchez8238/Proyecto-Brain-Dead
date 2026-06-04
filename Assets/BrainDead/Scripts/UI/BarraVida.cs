using UnityEngine;
using UnityEngine.UI;

public class BarraVida : MonoBehaviour
{
    public Image Relleno;
    private StatsPlayer Player;
    private float BarraLlena;

    void Start()
    {
        Relleno = gameObject.GetComponent<Image>();
        Player = GameObject.FindWithTag("Player").GetComponent<StatsPlayer>();
        BarraLlena = Player.MaxVida;
    }

    // Update is called once per frame
    void Update()
    {
        Relleno.fillAmount = Player.VidaActual / BarraLlena;
    }
}
