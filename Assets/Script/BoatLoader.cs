using UnityEngine;
using UnityEngine.SceneManagement;

public class BootLoader : MonoBehaviour
{
    // Key yang akan kita gunakan untuk menyimpan nama
    private const string PlayerNameKey = "PlayerName";

    void Start()
    {
        // Cek apakah data nama pemain sudah ada?
        if (PlayerPrefs.HasKey(PlayerNameKey))
        {
            // Jika ada, pemain tidak perlu isi nama lagi.
            // Langsung lempar ke Main Menu.
            SceneManager.LoadScene("MainMenuScene");
        }
        else
        {
            // Jika tidak ada, pemain HARUS isi nama dulu.
            SceneManager.LoadScene("NameInputScene");
        }
    }
}