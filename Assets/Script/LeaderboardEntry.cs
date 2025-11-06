using System.Collections.Generic; // <-- Ini WAJIB ada untuk 'List<>'

// [System.Serializable] adalah perintah agar Unity bisa
// menyimpan dan mengambil kelas ini menggunakan JsonUtility

[System.Serializable]
public class LeaderboardEntry
{
    // Data yang kita simpan untuk SATU entri
    public string playerName;
    public float score;
}

[System.Serializable]
public class LeaderboardData
{
    // Ini adalah DAFTAR yang berisi semua entri
    public List<LeaderboardEntry> entries = new List<LeaderboardEntry>();
}