// File name   : GameManager.cs
// Author(s)   : Dohun Lee, Minki Cho, Jinwon Park
// Copyright   : Copyright(C) 2025 DigiPen Institute of Technology
using UnityEngine;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Player Settings")]
    public GameObject player;
    public PlayerController playerController;

    [Header("UI")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI coinText;

    [Header("Score System")]
    public int heightScore = 0;
    public int coinScore = 0;

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
        heightScore = Mathf.FloorToInt(player.transform.position.y - playerStartPoint.y);

        if (heightScore < 0)
            heightScore = 0;

        UpdateScoreUI();
    }

    public void AddCoin(int amount = 1)
    {
        coinScore += amount;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Height: " + heightScore;

        if (coinText != null)
            coinText.text = "Coins: " + coinScore;
    }


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

        playerController.Respawn(respawnPoint);
        Time.timeScale = 1f;
    }
    public void GameClear()
    {
        Debug.Log($"GAME CLEAR!\nHeight: {heightScore}\nCoins: {coinScore}");

        Time.timeScale = 0f;

        var hud = Object.FindFirstObjectByType<HUDController>();
        if (hud != null)
        {
            hud.ShowWin();   
        }
    }
}
