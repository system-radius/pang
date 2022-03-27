using UnityEngine;
using TMPro;

/// <summary>
/// Component responsible for actually populating rows
/// in the leaderboard with values.
/// </summary>
public class LeaderboardEntryUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI entryName = null;
    [SerializeField] private TextMeshProUGUI entryScore = null;

    public void SetValue(Leaderboard.Entry entry, int index)
    {
        entryName.text = index + ". " + entry.name;
        entryScore.text = entry.score.ToString();
    }
}
