using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // <-- 1. TAMBAHKAN INI

public class LeaderboardUI : MonoBehaviour
{
    public GameObject entryPrefab; 
    public Transform contentContainer; 

    void Start()
    {
        // 1. Ambil data
        LeaderboardData data = LeaderboardManager.GetLeaderboardData();

        // 2. "Cetak" prefab untuk setiap data
        for (int i = 0; i < data.entries.Count; i++)
        {
            LeaderboardEntry entry = data.entries[i];
            GameObject newEntry = Instantiate(entryPrefab, contentContainer);
            
            newEntry.transform.Find("RankText").GetComponent<TMP_Text>().text = (i + 1).ToString();
            newEntry.transform.Find("NameText").GetComponent<TMP_Text>().text = entry.playerName;
            newEntry.transform.Find("ScoreText").GetComponent<TMP_Text>().text = entry.score.ToString("F");
            
            newEntry.SetActive(true); 
        }
    }

    // --- 2. TAMBAHKAN FUNGSI BARU INI ---
    /// <summary>
    /// Fungsi ini akan dipanggil oleh tombol 'Back'
    /// </summary>
    public void BackToMainMenu()
    {
        // Pastikan nama scene Anda 100% cocok dengan file scene
        // (Perhatikan typo yang kita perbaiki sebelumnya!)
        SceneManager.LoadScene("MainMenuScene");
    }
}