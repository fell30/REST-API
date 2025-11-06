using UnityEngine;
using System.Collections.Generic; // Untuk List<>
using System.Linq; // Untuk .OrderByDescending() dan .Take()

// Ini adalah kelas 'static'. Artinya kita bisa panggil fungsinya
// dari mana saja tanpa perlu menempelkannya ke GameObject.
// Cukup panggil: LeaderboardManager.AddEntry(...)
public static class LeaderboardManager
{
    // Ini 'kunci' brankas utama kita
    private const string LeaderboardKey = "LeaderboardData";
    
    // Kita hanya akan simpan 10 skor teratas
    private const int MaxEntries = 10; 

    /// <summary>
    /// Mengambil seluruh data leaderboard dari PlayerPrefs.
    /// </summary>
    public static LeaderboardData GetLeaderboardData()
    {
        // Cek apakah data sudah ada
        if (!PlayerPrefs.HasKey(LeaderboardKey))
        {
            // Jika belum ada, buat baru yang masih kosong
            return new LeaderboardData();
        }

        // Jika ada, ambil datanya (yang disimpan sebagai string JSON)
        string json = PlayerPrefs.GetString(LeaderboardKey);

        // Ubah string JSON kembali menjadi objek C#
        return JsonUtility.FromJson<LeaderboardData>(json);
    }

    /// <summary>
    /// Menambah entri skor baru ke leaderboard.
    /// </summary>
    public static void AddEntry(string newName, float newScore)
    {
        // 1. Ambil data yang ada sekarang
        LeaderboardData data = GetLeaderboardData();

        // 2. Tambahkan entri baru ke daftar
        data.entries.Add(new LeaderboardEntry { playerName = newName, score = newScore });

        // 3. Urutkan daftarnya dari skor TERTINGGI ke TERENDAH
        data.entries = data.entries.OrderByDescending(entry => entry.score).ToList();

        // 4. Pangkas/Potong daftarnya jika lebih dari 10
        if (data.entries.Count > MaxEntries)
        {
            data.entries = data.entries.Take(MaxEntries).ToList();
        }

        // 5. Ubah kembali daftar C# ini menjadi string JSON
        string json = JsonUtility.ToJson(data);

        // 6. Simpan string JSON itu ke PlayerPrefs
        PlayerPrefs.SetString(LeaderboardKey, json);
        PlayerPrefs.Save();
        
        Debug.Log("Skor baru ditambahkan: " + newName + " - " + newScore);
    }
}