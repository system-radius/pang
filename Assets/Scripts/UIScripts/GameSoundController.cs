using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A UI script that can play sounds. This can be improved.
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class GameSoundController : MonoBehaviour
{
    [SerializeField] private Sound playerHurt = null;

    [SerializeField] private Sound ballPop = null;

    AudioSource source;
    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlayHurt()
    {
        source.clip = playerHurt.clip;
        source.volume = playerHurt.volume;
        source.pitch = playerHurt.pitch;
        source.Play();
    }

    public void PlayPop()
    {
        source.clip = ballPop.clip;
        source.volume = ballPop.volume;
        source.pitch = ballPop.pitch;
        source.Play();
    }
}
