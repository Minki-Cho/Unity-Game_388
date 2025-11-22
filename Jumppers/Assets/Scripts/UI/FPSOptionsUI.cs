using UnityEngine;
using UnityEngine.UI;

public class FPSOptionsUI : MonoBehaviour
{
    [SerializeField] private Toggle fpsToggle;

    private void Start()
    {
        // FPSDisplay가 이미 씬에 떠 있는 전제 (Splash에서 DontDestroyOnLoad 했으니까)
        if (fpsToggle == null)
        {
            Debug.LogWarning("[FPSOptionsUI] fpsToggle is not available.");
            return;
        }

        if (FPSDisplay.Instance != null)
        {
            fpsToggle.isOn = FPSDisplay.Instance.IsShowing;
        }
    }

    // Toggle OnValueChanged(bool) 에 연결할 함수
    public void OnFPSToggleChanged(bool value)
    {
        if (FPSDisplay.Instance != null)
        {
            FPSDisplay.Instance.SetShowFPSFromUI(value);
        }
    }
}