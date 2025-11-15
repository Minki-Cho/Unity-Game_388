using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class HUDController : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI scoreText;

    [Header("Game Over / Win Panels")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject winPanel;

    [Header("Scene Names")]
    [SerializeField] private string mainMenuSceneName = "MainMenu";

    private GameManager gm;

    private void Awake()
    {
        gm = GameManager.Instance;
        if (gm == null)
        {
            Debug.LogError("[HUDController] GameManager.Cant find Instance!");
        }
    }

    private void Start()
    {
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        if (winPanel != null) winPanel.SetActive(false);
    }

    private void Update()
    {
        if (gm == null) return;

        if (scoreText != null)
        {
            scoreText.text = $"Score: {gm.score}";
        }
    }

    public void ShowGameOver()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
    }

    public void ShowWin()
    {
        if (winPanel != null)
            winPanel.SetActive(true);
    }

    public void OnClick_Restart()
    {
        Debug.Log("[HUDController] Pushed Restart Button ");
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