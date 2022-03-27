using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A data class that will hold the values for the leaderboard entries.
/// Saves the details on the player preferences.
/// </summary>
[Serializable]
public class Leaderboard
{
    public const int MAX_ENTRIES = 10;

    [Serializable]
    public class Entry
    {
        public string name;
        public int score;
    }

    public List<Entry> entries;

    public Leaderboard()
    {
        entries = new List<Entry>();
        for (int i = 0; i < MAX_ENTRIES; i++)
        {
            entries.Add(new Entry { name = "AAA", score = 0 });
        }
    }

    public static Leaderboard Load()
    {
        string json = PlayerPrefs.GetString("Leaderboards", null);
        Debug.Log(json);
        if (string.IsNullOrEmpty(json))
        {
            return new Leaderboard();
        }
        return JsonUtility.FromJson<Leaderboard>(json);
    }

    public static void Save(Leaderboard leaderboard)
    {
        string json = JsonUtility.ToJson(leaderboard);
        Debug.Log("Saving: " + json);
        PlayerPrefs.SetString("Leaderboards", json);
    }

    public void AddEntry(string name, int score)
    {
        Debug.Log("Before addition:");
        foreach (Entry entry in entries)
        {
            Debug.Log(entry.name + ": " + entry.score);
        }
        entries.Add(new Entry { name = name, score = score });
        Debug.Log("After addition:");
        foreach (Entry entry in entries)
        {
            Debug.Log(entry.name + ": " + entry.score);
        }
        entries = new List<Entry>(entries.OrderByDescending(entry => entry.score).Take(MAX_ENTRIES));
        Debug.Log("After sort:");
        foreach (Entry entry in entries)
        {
            Debug.Log(entry.name + ": " + entry.score);
        }
        Save(this);
    }

    public List<Entry> GetEntries()
    {
        return entries;
    }

}
