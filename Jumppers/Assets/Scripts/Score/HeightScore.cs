using UnityEngine;
using TMPro;

public class HeightScore : MonoBehaviour
{
    public Transform player;              // 플레이어 Transform
    public TextMeshProUGUI scoreText;     // UI 텍스트
    private float startY;                 // 시작 높이

    void Start()
    {
        startY = player.position.y;       // 게임 시작 시의 y값 저장
    }

    void Update()
    {
        float height = player.position.y - startY;

        // 떨어질 경우 높이를 0 밑으로 못 내려가게
        if (height < 0)
            height = 0;

        // 소수점 없이 정수로 표시
        scoreText.text = Mathf.FloorToInt(height).ToString();
    }
}
