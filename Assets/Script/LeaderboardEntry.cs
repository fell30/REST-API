using System;
using System.Collections.Generic;

[System.Serializable]
public class LeaderboardEntry
{
    public string playerName;
    public float score;
}

[System.Serializable]
public class LeaderboardData
{
    public List<LeaderboardEntry> entries = new List<LeaderboardEntry>();
}
