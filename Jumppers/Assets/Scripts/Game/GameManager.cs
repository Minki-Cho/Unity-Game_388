using TMPro;
using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Player Settings")]
    public GameObject player;
    public PlayerController playerController;

    [Header("UI")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI coinText;  // ← 코인텍스트 추가

    [Header("Score System")]
    public int heightScore = 0;        // 기존 높이 점수
    public int coinScore = 0;          // 코인 점수

    public float respawnDelay = 2.0f;
    public Transform respawnPoint;

    private Vector3 playerStartPoint;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        playerStartPoint = player.transform.position;
        heightScore = 0;
        coinScore = 0;

        UpdateScoreUI();
    }

    void Update()
    {
        // 높이 기반 점수 계산
        heightScore = Mathf.FloorToInt(player.transform.position.y - playerStartPoint.y);

        if (heightScore < 0)
            heightScore = 0;

        UpdateScoreUI();
    }

    // 🔥 코인 획득 함수
    public void AddCoin(int amount = 1)
    {
        coinScore += amount;
        UpdateScoreUI();
    }

    // 🔥 UI 최신화
    private void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Height: " + heightScore;

        if (coinText != null)
            coinText.text = "Coins: " + coinScore;
    }

    // ----------------------- 기존 코드 유지 -----------------------

    public void GameOver()
    {
        Debug.Log($"Game Over!\nHeight: {heightScore}\nCoins: {coinScore}");

        heightScore = 0;
        coinScore = 0;

        Time.timeScale = 0f;

        var hud = Object.FindFirstObjectByType<HUDController>();
        if (hud != null)
        {
            hud.ShowGameOver();
        }
    }

    public void HandlePlayerDeath()
    {
        StartCoroutine(RespawnCoroutine());
    }

    private IEnumerator RespawnCoroutine()
    {
        Debug.Log("Player died. Respawning in " + respawnDelay + " seconds...");

        yield return new WaitForSeconds(respawnDelay);

        // 플레이어 원상복구
        playerController.Respawn(respawnPoint);
        Time.timeScale = 1f;
    }

    //public void GameClear()
    //{
    //    Debug.Log("GAME CLEAR!");

    //    Time.timeScale = 0f;

    //    var hud = Object.FindFirstObjectByType<HUDController>();
    //    if (hud != null)
    //    {
    //        //hud.ShowGameClear();
    //    }
    //}
}
