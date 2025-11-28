//File name    : MainMenuController.cs
//Author(s)    : Jin Park
//Copyright    : Copyright (C) 2025 DigiPen Institute of Technology 

using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [Header("Scene Names")]
    [SerializeField] private string gameSceneName = "Game";  

    public void OnClick_Play()
    {
        Debug.Log("[MainMenu] Play clicked");

        Time.timeScale = 1f;

        if (string.IsNullOrEmpty(gameSceneName))
        {
            Debug.LogError("[MainMenu] No gameSceneName");
            return;
        }

        SceneManager.LoadScene(gameSceneName);
    }

    public void OnClick_Quit()
    {
        Debug.Log("[MainMenu] Quit clicked");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}