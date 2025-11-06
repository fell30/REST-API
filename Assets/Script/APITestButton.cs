using UnityEngine;
using UnityEngine.UI;

public class APITestButton : MonoBehaviour
{
    public APIManager apiManager;

    public void OnClick_PostPlayer()
    {
        // Ganti "RafaelFromButton" sesuai nickname test kamu
        StartCoroutine(apiManager.PostPlayer("RafaelFromButton"));
    }

    public void OnClick_GetLeaderboard()
    {
        StartCoroutine(apiManager.GetLeaderboard());
    }
}
