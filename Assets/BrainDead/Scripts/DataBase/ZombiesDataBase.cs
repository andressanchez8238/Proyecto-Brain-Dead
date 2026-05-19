using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ZombiesDataBase", menuName = "BrainDead/ZombiesDataBase")]
public class ZombiesDataBase : SerializedScriptableObject
{
    public Dictionary<int, ZombieData> zombieDataBase = new Dictionary<int, ZombieData>();

    public ZombieData GetZombie()
    {
        ZombieData zombie =zombieDataBase[Random.Range(0,zombieDataBase.Count)];



        return null;
    }


}
