using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponDataBase", menuName = "BrainDead/WeaponDataBase")]
public class WeaponDataBase : SerializedScriptableObject
{
    public Dictionary<int,WeaponData> weaponDataBase=new Dictionary<int,WeaponData>();








}
