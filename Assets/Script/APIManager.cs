using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using TMPro;

[System.Serializable]
public class PlayerData
{
    public int id;
    public string nickname;
    public string createdAt;
}

[System.Serializable]
public class PlayerPostData
{
    public string nickname;
}

[System.Serializable]
public class LeaderboardEntryBackend
{
    public int playerId;
    public string nickname;
    public int points;
}

public class APIManager : MonoBehaviour
{
    private const string BASE_URL = "http://localhost:5292/api/";

    [Header("Optional: UI Feedback")]
    public TMP_Text debugText;

    public IEnumerator PostPlayer(string nickname)
    {
        string url = BASE_URL + "Players";
        PlayerPostData data = new PlayerPostData { nickname = nickname };
        string jsonBody = JsonUtility.ToJson(data);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonBody);

        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            Debug.Log($"[API] Sending POST to {url} with body: {jsonBody}");
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log($"[API] Player created successfully: {request.downloadHandler.text}");
                if (debugText != null)
                    debugText.text = $"✅ Player created: {request.downloadHandler.text}";
            }
            else
            {
                Debug.LogError($"[API] POST Player failed: {request.error}\nResponse: {request.downloadHandler.text}");
                if (debugText != null)
                    debugText.text = $"❌ POST failed: {request.error}\n{request.downloadHandler.text}";
            }
        }
    }

    public IEnumerator GetLeaderboard()
    {
        string url = BASE_URL + "Leaderboard";

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            Debug.Log($"[API] Fetching leaderboard from {url}");
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string json = request.downloadHandler.text;
                Debug.Log($"[API] Leaderboard data: {json}");

                if (debugText != null)
                    debugText.text = $"🏆 Leaderboard:\n{json}";

                LeaderboardEntryBackend[] entries = JsonHelper.FromJson<LeaderboardEntryBackend>(json);
                foreach (var entry in entries)
                {
                    Debug.Log($"{entry.nickname} - {entry.points} points");
                }
            }
            else
            {
                Debug.LogError($"[API] GET Leaderboard failed: {request.error}");
                if (debugText != null)
                    debugText.text = $"❌ GET failed: {request.error}";
            }
        }
    }
}

public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        string newJson = "{ \"array\": " + json + "}";
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
        return wrapper.array;
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] array;
    }
}
