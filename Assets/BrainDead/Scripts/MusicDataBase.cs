using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "MusicDataBase", menuName = "BrainDead/MusicDataBase")]
public class MusicDataBase : SerializedScriptableObject
{
    public Dictionary<string, AudioClip> ClipDatabase = new();

    public AudioClip GetAudio(string audioName)
    {
        if(ClipDatabase.TryGetValue(audioName,out AudioClip clip))
        {
            return clip;
        }
        else
        {
            throw new System.Exception("El audio que intentas obtener no existe");
        }
    }
}
