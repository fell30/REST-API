using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    [Header("Player Settings")]
    public float JumpForce;
    private float score;

    [SerializeField] private bool isGrounded = false;
    private bool isAlive = true;

    private Rigidbody2D rb;

    [Header("UI References")]
    public TMP_Text ScoreTxt;
    public GameObject LosePanel;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        score = 0;
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
        Time.timeScale = 0f; // pause game
        rb.velocity = Vector2.zero;
        if (LosePanel != null)
            LosePanel.SetActive(true);
    }
}
