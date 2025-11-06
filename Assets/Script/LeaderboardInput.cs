using UnityEngine;
using UnityEngine.UI;       // Diperlukan untuk Button
using TMPro;                // Diperlukan untuk TMP_InputField
using UnityEngine.SceneManagement; // Diperlukan untuk pindah Scene

public class LeaderboardInput : MonoBehaviour
{
    [Header("Referensi UI")]
    // 1. Seret InputField Anda ke slot ini di Inspector
    public TMP_InputField nameInputField;

    // 2. Seret Tombol "Submit" Anda ke slot ini
    public Button submitButton;

    // 3. Ini adalah 'kunci' brankas.
    // Script PlayerScript Anda nanti akan MENGGUNAKAN key ini
    // untuk 'mengambil' nama pemain saat game over.
    private const string PlayerNameKey = "PlayerName";

    void Start()
    {
        // Ini adalah UX (User Experience) yang bagus:
        // Tombol 'Submit' tidak bisa diklik kalau belum ada teks.
        if (submitButton != null)
        {
            submitButton.interactable = false;
        }

        // Tambahkan "pendengar" ke input field
        // Setiap kali user mengetik, panggil fungsi ValidateInput
        if (nameInputField != null)
        {
            nameInputField.onValueChanged.AddListener(ValidateInput);
        }
    }

    /// <summary>
    /// Fungsi ini akan dipanggil setiap kali ketikan di input field berubah.
    /// </summary>
    private void ValidateInput(string text)
    {
        // Tombol 'Submit' hanya aktif jika 'text' TIDAK kosong.
        if (submitButton != null)
        {
            submitButton.interactable = !string.IsNullOrEmpty(text);
        }
    }

    /// <summary>
    /// Fungsi ini yang akan dipanggil oleh Tombol 'Submit' di OnClick().
    /// </summary>
    public void SubmitName()
    {
        // 1. Ambil nama dari input field
        string playerName = nameInputField.text;

        // 2. (Validasi double-check)
        if (string.IsNullOrEmpty(playerName))
        {
            Debug.LogWarning("Nama tidak boleh kosong!");
            return; // Hentikan fungsi
        }

        // 3. Simpan nama ke "brankas" PlayerPrefs
        PlayerPrefs.SetString(PlayerNameKey, playerName);
        PlayerPrefs.Save(); // Memastikan data langsung tersimpan

        Debug.Log("Nama pemain disimpan: " + playerName);

        // 4. Pindah ke Scene Main Menu
        SceneManager.LoadScene("MainMenuScene");
    }
}