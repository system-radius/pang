using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// A scriptable object that holds sound information,
/// what clip to play, as well as the volume and pitch.
/// </summary>
[CreateAssetMenu]
public class Sound : ScriptableObject
{
    public AudioClip clip;
    
    [Range(0f, 1f)]
    public float volume;

    [Range(0.1f, 3f)]
    public float pitch;
}
