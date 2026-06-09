using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(ZombieData))]
public class BuffManager : MonoBehaviour
{
    public List<Buff> activeBuffs = new();
    public ZombieData baseEntity;
    private void Awake()
    {
        baseEntity = GetComponent<ZombieData>();
    }
    public void AddBuff(Buff buff)
    {
        Debug.Log("BuffAdded");
        buff.Apply(baseEntity);
        activeBuffs.Add(buff);
        StartCoroutine(RemoveBuff(buff));
    }
    public IEnumerator RemoveBuff(Buff buff)
    {
        yield return new WaitForSeconds(buff.Duration);

        buff.Remove(baseEntity);
        activeBuffs.Remove(buff);
        Debug.Log("BuffRemoved");
    }
}