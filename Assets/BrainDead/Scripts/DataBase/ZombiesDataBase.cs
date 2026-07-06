using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ZombiesDataBase", menuName = "BrainDead/ZombiesDataBase")]
public class ZombiesDataBase : SerializedScriptableObject
{
    public Dictionary<int, ZombieData> zombieDataBase = new Dictionary<int, ZombieData>();

    public ZombieData GetZombie()
    {
        if (zombieDataBase.Count == 0)
        {
            Debug.LogError("La base de datos de zombies está vacía.");
            return null;
        }

        ZombieData[] zombies = zombieDataBase.Values.ToArray();

        return zombies[Random.Range(0, zombies.Length)];
    }
}