using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI")]
    public TMP_Text goldText; 

    private int totalGold = 0;

    void Awake()
    {
        // ΩÃ±€≈Ê ºº∆√
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        UpdateUI();
    }

    public void AddGold(int amount)
    {
        totalGold += amount;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (goldText != null)
        {
            goldText.text = $"{totalGold} G";
        }
    }
}