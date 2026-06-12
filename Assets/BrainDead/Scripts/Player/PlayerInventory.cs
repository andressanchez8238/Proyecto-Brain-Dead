using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int Madera;
    public int Granada;

    public void AddMadera(int madera)
    {
        Madera += madera;
    }
    public void RemoveMadera(int madera) 
    {
        Madera -= madera;
    }
    public void AddGranada(int granada)
    {
        Granada += granada;
    }
    public void RemoveGranada(int granada)
    {
        Granada -= granada;
    }

}
