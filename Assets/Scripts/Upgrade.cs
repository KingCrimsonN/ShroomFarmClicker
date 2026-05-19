using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Upgrade : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private UpgradeManager.UpgradeType upgradeType;
    [SerializeField] private double baseCost = 10;
    [SerializeField] private float costMultiplier = 1.15f;

    [Header("World Space Text Displays")]
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text costText;

    [Header("Visual Feedback (Mobile)")]
    [SerializeField] private SpriteRenderer objectSprite;
    [SerializeField] private Color affordableColor = Color.white;
    [SerializeField] private Color lockedColor = new Color(0.4f, 0.4f, 0.4f, 1f); // Dimmed look

    private double currentCost;
    private bool canAfford;

    void Awake()
    {
        // Auto-fetch SpriteRenderer if not explicitly assigned
        if (objectSprite == null)
        {
            objectSprite = GetComponent<SpriteRenderer>();
        }
    }

    void OnEnable()
    {
        UpgradeManager.OnUpgradeLeveled += HandleUpgradeLeveled;
        MoneyManager.OnMoneyChanged += HandleMoneyChanged;

        RefreshUpgradeState();
    }

    void OnDisable()
    {
        UpgradeManager.OnUpgradeLeveled -= HandleUpgradeLeveled;
        MoneyManager.OnMoneyChanged -= HandleMoneyChanged;
    }

    // Unity automatically catches touches on mobile colliders via OnMouseDown
    private void OnMouseDown()
    {
        if (canAfford && MoneyManager.instance != null)
        {
            MoneyManager.instance.AddMoney(-currentCost);
            UpgradeManager.instance.PurchaseUpgrade(upgradeType);
        }
    }

    private void CalculateCurrentCost()
    {
        int currentLevel = UpgradeManager.instance.GetUpgradeLevel(upgradeType);
        // Cost Equation: BaseCost * (Multiplier ^ Level)
        currentCost = baseCost * Mathf.Pow(costMultiplier, currentLevel);
    }

    private void RefreshUpgradeState()
    {
        if (UpgradeManager.instance == null) return;

        CalculateCurrentCost();

        // Update world-space floating text meshes safely
        if (titleText != null) titleText.text = $"{FormatUpgradeName(upgradeType.ToString())}\n(Lv. {UpgradeManager.instance.GetUpgradeLevel(upgradeType)})";
        if (costText != null) costText.text = $"${currentCost:F0}";

        UpdateVisuals(MoneyManager.instance != null ? MoneyManager.instance.CurrentMoney : 0);
    }

    private void HandleUpgradeLeveled(UpgradeManager.UpgradeType type, int newLevel)
    {
        if (type == upgradeType) RefreshUpgradeState();
    }

    private void HandleMoneyChanged(double currentMoney)
    {
        UpdateVisuals(currentMoney);
    }

    private void UpdateVisuals(double currentMoney)
    {
        canAfford = currentMoney >= currentCost;

        // Visual feedback substituting standard UI interactivity 
        if (objectSprite != null)
        {
            objectSprite.color = canAfford ? affordableColor : lockedColor;
        }
    }

    // Helper to turn PascalCase enum naming into clean spaced words
    private string FormatUpgradeName(string name)
    {
        return System.Text.RegularExpressions.Regex.Replace(name, "([A-Z])", " $1").Trim();
    }
}