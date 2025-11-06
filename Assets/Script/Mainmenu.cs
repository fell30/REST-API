using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [Header("UI References")]
    // 1. Slot untuk "Welcome, [PlayerName]"
    public TMP_Text welcomeText;

    // 2. Slot untuk "Top Score: [Score]"
    public TMP_Text highScoreText;

    // Key untuk mengambil nama
    private const string PlayerNameKey = "PlayerName";

    void Start()
    {
        // --- MENGISI NAMA PLAYER (SLOT 1) ---
        // Kita ambil nama yang disimpan oleh LeaderboardInput.cs
        string playerName = PlayerPrefs.GetString(PlayerNameKey, "Player");
        if (welcomeText != null)
        {
            welcomeText.text = "Welcome, " + playerName;
        }

        // --- MENGISI HIGH SCORE (SLOT 2) ---
        // Kita panggil "Otak" (LeaderboardManager)
        LeaderboardData data = LeaderboardManager.GetLeaderboardData();
        
        if (highScoreText != null)
        {
            // Cek apakah ada data di leaderboard
            if (data.entries.Count > 0)
            {
                // Ambil entri pertama (Rank 1)
                float topScore = data.entries[0].score;
                highScoreText.text = "Top Score: " + topScore.ToString("F");
            }
            else
            {
                // Tampilkan 0 jika leaderboard masih kosong
                highScoreText.text = "Top Score: 0";
            }
        }
    }

    // --- FUNGSI UNTUK TOMBOL-TOMBOL ---

    // Hubungkan ini ke OnClick() Tombol "Play"
    public void PlayGame()
    {
        SceneManager.LoadScene("GameplayScene");
    }

    // Hubungkan ini ke OnClick() Tombol "Leaderboard"
    public void ShowLeaderboard()
    {
        // Pastikan nama Scene-nya benar
        SceneManager.LoadScene("LeaderboardScene"); 
    }

    // Hubungkan ini ke OnClick() Tombol "Quit"
    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}