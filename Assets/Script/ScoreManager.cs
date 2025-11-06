using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text ScoreTxt;
    public TMP_Text HighScoreTxt;

    private float score;
    private float highScore;

    private void Start()
    {
        score = 0;
        highScore = PlayerPrefs.GetFloat("HighScore", 0);

        UpdateScoreUI();
        UpdateHighScoreUI();
    }

    private void Update()
    {
        // naikkan skor berdasarkan waktu
        score += Time.deltaTime * 4;
        UpdateScoreUI();

        // update highscore sementara di layar
        UpdateHighScoreUI();
    }

    public void SaveHighScore()
    {
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetFloat("HighScore", highScore);
            PlayerPrefs.Save();
        }
    }

    public void ResetHighScore()
    {
        PlayerPrefs.SetFloat("HighScore", 0);
        PlayerPrefs.Save();
        highScore = 0;
        UpdateHighScoreUI();
        Debug.Log("High Score Reset");
    }

    public float GetScore() => score;
    public float GetHighScore() => highScore;

    private void UpdateScoreUI()
    {
        if (ScoreTxt != null)
            ScoreTxt.text = "SCORE : " + score.ToString("F");
    }

    private void UpdateHighScoreUI()
    {
        if (HighScoreTxt != null)
            HighScoreTxt.text = "HIGHSCORE : " + highScore.ToString("F");
    }
}
