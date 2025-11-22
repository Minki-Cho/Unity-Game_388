using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class HUDController : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timeText;

    [Header("Game Over / Win Panels")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject winPanel;

    [Header("Scene Names")]
    [SerializeField] private string mainMenuSceneName = "MainMenu";

    private float elapsedTime = 0f;
    private bool isTimerRunning = true;

    private void Start()
    {
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        if (winPanel != null) winPanel.SetActive(false);
    }

    private void Update()
    {
        if (GameManager.Instance == null) return;

        if (scoreText != null)
        {
            scoreText.text = $"Score: {GameManager.Instance.coinScore}";
        }

        if (isTimerRunning && timeText != null)
        {
            elapsedTime += Time.deltaTime;

            int minutes = Mathf.FloorToInt(elapsedTime / 60f);
            int seconds = Mathf.FloorToInt(elapsedTime % 60f);

            timeText.text = $"{minutes:00}:{seconds:00}";
        }
    }

    public void ShowGameOver()
    {
        isTimerRunning = false;
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
    }

    public void ShowWin()
    {
        isTimerRunning = false;
        if (winPanel != null)
            winPanel.SetActive(true);
    }

    public void OnClick_Restart()
    {
        Debug.Log("[HUDController] Pushed Restart Button ");

        elapsedTime = 0f;
        isTimerRunning = true;

        GameManager.Instance.HandlePlayerDeath();
        gameOverPanel.SetActive(false);
        winPanel.SetActive(false);
    }

    public void OnClick_MainMenu()
    {
        Time.timeScale = 1f;

        if (string.IsNullOrEmpty(mainMenuSceneName))
        {
            Debug.LogError("[HUDController] Empty mainMenuSceneName");
            return;
        }

        SceneManager.LoadScene(mainMenuSceneName);
    }
}
