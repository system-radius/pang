using UnityEngine;

/// <summary>
/// Handles the population of the leaderboard with rows of data.
/// The UI is specifically set to handle 10 entries at most.
/// </summary>
public class LeaderboardUI : MonoBehaviour
{
    [SerializeField] private LeaderboardEntryUI entryPrefab = null;
    [SerializeField] private Transform container = null;

    [SerializeField] private LeaderboardAccess access = null;

    private LeaderboardEntryUI[] entryUIs = null;

    private void Awake()
    {
        // In the arcade games from before, all slots in the leader
        // board are defined and has default values.
        entryUIs = new LeaderboardEntryUI[Leaderboard.MAX_ENTRIES];
        for (int i = 0; i < Leaderboard.MAX_ENTRIES; i++)
        {
            entryUIs[i] = Instantiate(entryPrefab, container);
        }

        LoadLeaderboard();
    }

    private void LoadLeaderboard()
    {
        // On enable (when the high score panel is enabled),
        // populate the existing rows with actual data.
        Leaderboard leaderboard = Leaderboard.Load();

        // Set the accessed leaderboard.
        access.leaderboard = leaderboard;
        var entries = leaderboard.GetEntries();
        for (int i = 0; i < Leaderboard.MAX_ENTRIES; i++)
        {
            entryUIs[i].SetValue(entries[i], i + 1);
        }
    }
}
