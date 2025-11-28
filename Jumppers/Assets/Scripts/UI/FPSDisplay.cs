//File name    : FPSDisplay.cs
//Author(s)    : Jin Park
//Copyright    : Copyright (C) 2025 DigiPen Institute of Technology 

using UnityEngine;
using TMPro;

public class FPSDisplay : MonoBehaviour
{
    public static FPSDisplay Instance { get; private set; }

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI fpsText;

    [Header("Settings")]
    [SerializeField] private bool startEnabled = true;

    private bool showFPS;
    private float deltaTime;

    public bool IsShowing => showFPS;

    private const string PlayerPrefsKey = "ShowFPS";

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); 

        if (fpsText == null)
        {
            Debug.LogWarning("[FPSDisplay] fpsText doesn't set on Inspector");
        }

        if (PlayerPrefs.HasKey(PlayerPrefsKey))
        {
            bool saved = PlayerPrefs.GetInt(PlayerPrefsKey, 1) == 1;
            SetShowFPS(saved, save: false);
        }
        else
        {
            SetShowFPS(startEnabled, save: false);
        }
    }

    private void Update()
    {
        if (!showFPS || fpsText == null)
            return;

        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;

        fpsText.text = $"{fps:0} FPS";
    }

    private void SetShowFPS(bool value, bool save)
    {
        showFPS = value;

        if (fpsText != null)
            fpsText.gameObject.SetActive(showFPS);

        if (save)
        {
            PlayerPrefs.SetInt(PlayerPrefsKey, showFPS ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    public void SetShowFPSFromUI(bool value)
    {
        SetShowFPS(value, save: true);
    }
}