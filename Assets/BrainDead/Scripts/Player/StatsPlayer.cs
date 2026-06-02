using UnityEngine;

public class StatsPlayer : MonoBehaviour
{
    public float Stamina =100f;
    public float MaxStamina = 100f;
    public float CambioStamina = 10f;
    public float cooldownStamina;
    public float cooldownStaminaTotal = 5f;
    public bool StaminaRecarga=false;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DisminuirStamina()
    {
        if (Stamina >= 0 &&StaminaRecarga==false)
        {
            Stamina -= Time.deltaTime*CambioStamina;
        }
    }
    public void AumentarStamina()
    {
        if (Stamina < MaxStamina && StaminaRecarga == true)
        {
            Stamina += Time.deltaTime*CambioStamina;
        }
    }
}
