using UnityEngine;

/// <summary>
/// A scriptable object that is supposed to hold
/// various numeric data that is related to the player.
/// Examples are the life and score data of the player.
/// </summary>
[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
    public double value;
    public float defaultValue = 0;

    public void Reset()
    {
        value = defaultValue;
    }
}
