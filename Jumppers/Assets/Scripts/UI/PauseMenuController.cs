//File name    : PauseMenuController.cs
//Author(s)    : Jin Park
//Copyright    : Copyright (C) 2025 DigiPen Institute of Technology 

using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject pausePanel;

    [Header("Settings")]
    [SerializeField] private KeyCode pauseKey = KeyCode.Escape;

    [Header("Scene Names")]
    [SerializeField] private string mainMenuSceneName = "MainMenu";

    private bool isPaused = false;

    private void Start()
    {
        if (pausePanel != null)
            pausePanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(pauseKey))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Pause()
    {
        if (isPaused) return;

        isPaused = true;
        Time.timeScale = 0f;
        if (pausePanel != null)
            pausePanel.SetActive(true);
    }

    public void Resume()
    {
        if (!isPaused) return;

        isPaused = false;
        Time.timeScale = 1f;
        if (pausePanel != null)
            pausePanel.SetActive(false);
    }

    public void OnClick_Resume()
    {
        Resume();
    }

    public void OnClick_MainMenu()
    {
        Time.timeScale = 1f;

        if (string.IsNullOrEmpty(mainMenuSceneName))
        {
            Debug.LogError("[PauseMenu] Empty mainMenuSceneName!");
            return;
        }

        SceneManager.LoadScene(mainMenuSceneName);
    }
}