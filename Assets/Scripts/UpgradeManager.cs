using System;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager instance;

    public enum UpgradeType
    {
        GrowthPerClick,
        GrowthPerSecond,
        PotionPriceMultiplier,
        AutoHarvest
    }

    // Central repository tracking the level of each upgrade type
    private int clickLevel = 0;
    private int passiveLevel = 0;
    private int priceLevel = 0;
    private int autoHarvestLevel = 0;

    // Event broadcast so UI shop buttons know when an upgrade level increases
    public static event Action<UpgradeType, int> OnUpgradeLeveled;

    void Awake()
    {
        if (instance != null) { Destroy(gameObject); return; }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Dynamic Data Calculations based purely on upgrade level states
    public float growthPerClick => 1.0f + (clickLevel * 0.5f);
    public float growthPerSecond => passiveLevel * 0.2f;
    public float potionPriceMultiplier => 1.0f + (priceLevel * 0.15f);

    public int GetUpgradeLevel(UpgradeType type)
    {
        return type switch
        {
            UpgradeType.GrowthPerClick => clickLevel,
            UpgradeType.GrowthPerSecond => passiveLevel,
            UpgradeType.PotionPriceMultiplier => priceLevel,
            UpgradeType.AutoHarvest => autoHarvestLevel,
            _ => 0
        };
    }

    public void PurchaseUpgrade(UpgradeType type)
    {
        switch (type)
        {
            case UpgradeType.GrowthPerClick: clickLevel++; break;
            case UpgradeType.GrowthPerSecond: passiveLevel++; break;
            case UpgradeType.PotionPriceMultiplier: priceLevel++; break;
            case UpgradeType.AutoHarvest: autoHarvestLevel++; break;
        }

        OnUpgradeLeveled?.Invoke(type, GetUpgradeLevel(type));
    }

    public void LoadLevelDataDirectly(UpgradeType type, int level)
    {
        switch (type)
        {
            case UpgradeType.GrowthPerClick: clickLevel = level; break;
            case UpgradeType.GrowthPerSecond: passiveLevel = level; break;
            case UpgradeType.PotionPriceMultiplier: priceLevel = level; break;
        }
    }
}