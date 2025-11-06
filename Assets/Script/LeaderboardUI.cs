using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LeaderboardUI : MonoBehaviour
{
    public GameObject entryPrefab;
    public Transform contentContainer;

    void Start()
    {
        LeaderboardData data = LeaderboardManager.GetLeaderboardData();

        for (int i = 0; i < data.entries.Count; i++)
        {
            LeaderboardEntry entry = data.entries[i];
            GameObject newEntry = Instantiate(entryPrefab, contentContainer);

            newEntry.transform.Find("RankText").GetComponent<TMP_Text>().text = (i + 1).ToString();
            newEntry.transform.Find("NameText").GetComponent<TMP_Text>().text = entry.playerName;
            newEntry.transform.Find("ScoreText").GetComponent<TMP_Text>().text = entry.score.ToString("F0");

            newEntry.SetActive(true);
        }
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
