using UnityEngine;

/// <summary>
/// A scriptable object necessary to have an access
/// to the leaderboard data which is retrieved from
/// the main scene. This also contains the current
/// player data so that it may be saved later.
/// </summary>
[CreateAssetMenu]
public class LeaderboardAccess : ScriptableObject
{
    public Leaderboard leaderboard;

    [SerializeField] private PlayerData score = null;

    [SerializeField] private PlayerString playerName = null;

    public void SaveScore()
    {
        leaderboard.AddEntry(playerName.value, (int)score.value);
    }
}
