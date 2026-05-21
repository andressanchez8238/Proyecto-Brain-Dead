using UnityEngine;

public class StatsPlayer : MonoBehaviour
{
    public float Stamina =100f;
    public float MaxStamina = 100f;
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
            Stamina -= 1f;
        }
    }
    public void AumentarStamina()
    {
        if (Stamina <= MaxStamina && StaminaRecarga == true)
        {
            Stamina += 1f;
        }
    }
}
