using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    [Header("Player Settings")]
    public float JumpForce;
    private float score;
    private float highScore;

    [SerializeField] private bool isGrounded = false;
    private bool isAlive = true;

    private Rigidbody2D rb;

    [Header("UI References")]
    public TMP_Text ScoreTxt;
    public TMP_Text HighScoreTxt;
    public GameObject LosePanel;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        score = 0;
        highScore = PlayerPrefs.GetFloat("HighScore", 0); // ambil data highscore

        Time.timeScale = 1f; // pastikan game tidak pause di awal

        if (LosePanel != null)
            LosePanel.SetActive(false); // sembunyikan UI kalah
    }

    void Update()
    {
        if (!isAlive)
        {
            // Logika 'R' untuk retry sudah dihapus
            // karena kita gunakan tombol UI
            return;
        }

        // lompat
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * JumpForce);
            isGrounded = false;
        }

        if (Input.GetKeyDown(KeyCode.Delete))
        {
            ResetHighScore();
        }

        // skor naik berdasarkan waktu
        score += Time.deltaTime * 4;
        ScoreTxt.text = "SCORE : " + score.ToString("F");

        // update highscore di layar (tanpa menyimpannya dulu)
        if (HighScoreTxt != null)
            HighScoreTxt.text = "HIGHSCORE : " + highScore.ToString("F");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("spike"))
        {
            LoseGame();
        }
    }

    void LoseGame()
    {
        isAlive = false;
        Time.timeScale = 0f;
        rb.velocity = Vector2.zero;

        // cek dan simpan highscore
        //if (score > highScore)
        //{
        //    highScore = score;
        //    PlayerPrefs.SetFloat("HighScore", highScore);
        //   PlayerPrefs.Save();
        //}

        // GANTI DENGAN INI
        // 1. Ambil nama pemain yang sudah disimpan
        string playerName = PlayerPrefs.GetString("PlayerName", "Player");

        // 2. Masukkan ke LeaderboardManager
        LeaderboardManager.AddEntry(playerName, score);
        
        if (LosePanel != null)
        {
            LosePanel.SetActive(true);

            // kalau kamu punya teks highscore di dalam panel kalah, bisa tampilkan di sini juga
            TMP_Text panelHighScore = LosePanel.transform.Find("HighScoreText")?.GetComponent<TMP_Text>();
            if (panelHighScore != null)
                panelHighScore.text = "HIGHSCORE : " + highScore.ToString("F");
        }
    }

    public void ResetHighScore()
    {
        PlayerPrefs.SetFloat("HighScore", 0);
        PlayerPrefs.Save();
        highScore = 0;

        if (HighScoreTxt != null)
            HighScoreTxt.text = "HIGHSCORE : 0";

        Debug.Log("High Score Reset");
    }

    // =======================================================
    // --- FUNGSI BARU UNTUK TOMBOL DI LOSEPANEL ---
    // =======================================================

    /// <summary>
    /// Fungsi ini akan dipanggil oleh tombol "Retry" di LosePanel.
    /// </summary>
    public void RetryGame()
    {
        // PENTING: Normalkan lagi Time Scale sebelum pindah scene
        Time.timeScale = 1f; 
        
        // Reload scene yang aktif sekarang
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// Fungsi ini akan dipanggil oleh tombol "Back to Menu" di LosePanel.
    /// </summary>
    public void BackToMenu()
    {
        // PENTING: Normalkan lagi Time Scale
        Time.timeScale = 1f; 
        
        // Pindah ke scene Main Menu (pastikan namanya "MainMenuScene")
        SceneManager.LoadScene("MainMenuScene"); 
    }
}