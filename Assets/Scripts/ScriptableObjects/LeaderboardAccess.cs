using UnityEngine;

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
