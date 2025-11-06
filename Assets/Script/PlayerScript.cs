using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    [Header("Player Settings")]
    public float JumpForce;
    private bool isGrounded = false;
    private bool isAlive = true;

    private Rigidbody2D rb;

    [Header("UI References")]
    public GameObject LosePanel;
    public ScoreManager scoreManager; // tambahkan referensi ke ScoreManager

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Time.timeScale = 1f;

        if (LosePanel != null)
            LosePanel.SetActive(false);
    }

    void Update()
    {
        if (!isAlive)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Time.timeScale = 1f;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * JumpForce);
            isGrounded = false;
        }

        if (Input.GetKeyDown(KeyCode.Delete))
        {
            scoreManager.ResetHighScore();
        }
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

        // simpan highscore melalui ScoreManager
        scoreManager.SaveHighScore();

        if (LosePanel != null)
        {
            LosePanel.SetActive(true);

            TMP_Text panelHighScore = LosePanel.transform.Find("HighScoreText")?.GetComponent<TMP_Text>();
            if (panelHighScore != null)
                panelHighScore.text = "HIGHSCORE : " + scoreManager.GetHighScore().ToString("F");
        }
    }
}
