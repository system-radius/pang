using UnityEngine;

/// <summary>
/// A collection of player data that is used
/// for the player action class to effectively pass
/// the data between the actions.
/// </summary>
[CreateAssetMenu]
public class PlayerInfo : ScriptableObject
{
    public PlayerData lives;
    public PlayerData movementSpeed;
    public PlayerData score;

    public bool firing;

    public PlayerState state;

    public void Reset()
    {
        lives.Reset();
        score.Reset();
    }
}
