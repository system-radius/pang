using UnityEngine;

/// <summary>
/// A scriptable object that is supposed to hold
/// various numeric data that is related to the game itself.
/// Examples are the game level and time limit.
/// </summary>
[CreateAssetMenu]
public class GameData : ScriptableObject
{
    public float value;
    public float defaultValue;

    public void Reset()
    {
        value = defaultValue;
    }
}
