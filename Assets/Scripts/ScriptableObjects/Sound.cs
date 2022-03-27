using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu]
public class Sound : ScriptableObject
{
    public AudioClip clip;
    
    [Range(0f, 1f)]
    public float volume;

    [Range(0.1f, 3f)]
    public float pitch;
}
