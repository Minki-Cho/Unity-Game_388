using UnityEngine;
using TMPro;

public class HeightScore : MonoBehaviour
{
    public Transform player; 
    public TextMeshProUGUI scoreText;
    private float startY;

    void Start()
    {
        startY = player.position.y;
    }

    void Update()
    {
        float height = player.position.y - startY;

        if (height < 0)
            height = 0;

        scoreText.text = Mathf.FloorToInt(height).ToString();
    }
}
