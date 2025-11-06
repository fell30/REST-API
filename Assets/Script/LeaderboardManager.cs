using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public static class LeaderboardManager
{
    private const string LeaderboardKey = "LeaderboardData";
    private const int MaxEntries = 10;

    public static LeaderboardData GetLeaderboardData()
    {
        if (!PlayerPrefs.HasKey(LeaderboardKey))
        {
            return new LeaderboardData();
        }

        string json = PlayerPrefs.GetString(LeaderboardKey);
        return JsonUtility.FromJson<LeaderboardData>(json);
    }

    public static void AddEntry(string newName, float newScore)
    {
        LeaderboardData data = GetLeaderboardData();

        data.entries.Add(new LeaderboardEntry { playerName = newName, score = newScore });
        data.entries = data.entries.OrderByDescending(entry => entry.score).ToList();

        if (data.entries.Count > MaxEntries)
        {
            data.entries = data.entries.Take(MaxEntries).ToList();
        }

        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(LeaderboardKey, json);
        PlayerPrefs.Save();

        Debug.Log("Skor baru ditambahkan: " + newName + " - " + newScore);
    }
}
