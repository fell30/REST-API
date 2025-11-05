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
            // saat kalah, tekan R untuk retry
            if (Input.GetKeyDown(KeyCode.R))
            {
                Time.timeScale = 1f;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            return;
        }

        // lompat
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * JumpForce);
            isGrounded = false;
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
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetFloat("HighScore", highScore);
            PlayerPrefs.Save();
        }

        if (LosePanel != null)
        {
            LosePanel.SetActive(true);

            // kalau kamu punya teks highscore di dalam panel kalah, bisa tampilkan di sini juga
            TMP_Text panelHighScore = LosePanel.transform.Find("HighScoreText")?.GetComponent<TMP_Text>();
            if (panelHighScore != null)
                panelHighScore.text = "HIGHSCORE : " + highScore.ToString("F");
        }
    }
}
