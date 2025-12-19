using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("UI Objects")]
    public GameObject settingsPanel;

    void Start()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(false);
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}