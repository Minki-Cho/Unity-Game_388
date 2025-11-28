// File name   : SceneLoader.cs
// Author(s)   : Minki Cho
// Copyright  : Copyright(C) 2025 DigiPen Institute of Technology
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    public GameObject loadingPanel;
    public Slider loadingBar;
    public TextMeshProUGUI loadingText;

    private bool isAnimating = false;

    public void LoadGameScene()
    {
        StartCoroutine(LoadSceneCoroutine());
    }

    IEnumerator LoadSceneCoroutine()
    {
        loadingPanel.SetActive(true);
        loadingBar.value = 0f;
        loadingText.text = "Loading...";
        isAnimating = true;

        StartCoroutine(AnimateLoadingText());

        yield return new WaitForSeconds(0.1f);

        AsyncOperation op = SceneManager.LoadSceneAsync("Game");
        op.allowSceneActivation = false;

        while (!op.isDone)
        {
            float progress = Mathf.Clamp01(op.progress / 0.9f);
            loadingBar.value = Mathf.Clamp01(progress + 0.001f);

            if (progress >= 1f)
            {
                isAnimating = false;
                loadingText.text = "Loading Complete!";

                yield return new WaitForSeconds(0.25f);
                op.allowSceneActivation = true;
            }

            yield return null;
        }
    }

    IEnumerator AnimateLoadingText()
    {
        string baseText = "Loading";
        int dotCount = 0;

        while (isAnimating)
        {
            dotCount = (dotCount % 3);
            loadingText.text = baseText + new string('.', dotCount);

            yield return new WaitForSeconds(0.3f);
        }
    }
}
