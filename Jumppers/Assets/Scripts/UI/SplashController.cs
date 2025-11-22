using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private string mainMenuSceneName = "MainMenu";
    [SerializeField] private float minShowTime = 1.0f;  

    private float timer = 0f;
    private bool canSkip = false;

    void Update()
    {
        timer += Time.deltaTime;

        if (!canSkip && timer >= minShowTime)
        {
            canSkip = true;
        }

        if (!canSkip)
            return;

        if (Input.anyKeyDown)
        {
            GoToMainMenu();
        }
    }

    private void GoToMainMenu()
    {
        if (string.IsNullOrEmpty(mainMenuSceneName))
        {
            Debug.LogError("[SplashController] mainMenuSceneName is empty");
            return;
        }

        SceneManager.LoadScene(mainMenuSceneName);
    }
}