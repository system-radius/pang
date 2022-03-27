using UnityEngine;

/// <summary>
/// A scriptable object that is supposed to hold
/// string data related to the player.
/// An example is the player name.
/// </summary>
[CreateAssetMenu]
public class PlayerString : ScriptableObject
{
    public string value;
}
