using TMPro;
using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject player;
    public PlayerController playerController;
    public TextMeshProUGUI scoreText;

    public int score = 0;
    public float respawnDelay = 2.0f;

    private Transform playerStartPoint;
    private int currentHeight;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        playerStartPoint = player.transform;
        currentHeight = 0;
        score = 0;
    }

    void Update()
    {        
        currentHeight = Mathf.FloorToInt(player.transform.position.y - playerStartPoint.position.y);
        if(currentHeight < 0)
        {
            currentHeight = 0;
        }

        score = currentHeight;
        scoreText.text = score.ToString();
    }

    public void GameOver()
    {
        Debug.Log($"Game Over!\nScore: {score}");
        currentHeight = 0;
        score = 0;
    }

    public void HandlePlayerDeath()
    {
        StartCoroutine(RespawnCoroutine());
    }

    private IEnumerator RespawnCoroutine()
    {
        Debug.Log("Player died. Respawning in " + respawnDelay + " seconds...");

        // 1. (선택) 죽는 애니메이션 재생, 화면 페이드 아웃 등

        // 2. 리스폰 딜레이
        yield return new WaitForSeconds(respawnDelay);

        // 3. (선택) 화면 페이드 인

        // 4. 플레이어에게 리스폰 명령
        playerController.Respawn(playerStartPoint.position);
    }
}
