using TMPro;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager instance;

    public float growthPerClick;
    public float growthPerSecond;
    public float potionPriceMultiplier;
    public TMP_Text growthPerClickText;
    public TMP_Text growthPerSecondText;
    public TMP_Text potionPriceMultiplierText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        growthPerClick = 1f;
        growthPerSecond = 0f;
        potionPriceMultiplier = 1f;
        growthPerClickText.text = $"Growth per click: {growthPerClick:F1}";
        growthPerSecondText.text = $"Growth per second: {growthPerSecond:F1}";
        potionPriceMultiplierText.text = $"Potion price multiplier: {potionPriceMultiplier:F1}";
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpgradeGrowthPerClick()
    {
        growthPerClick += 0.1f;
        growthPerClickText.text = $"Growth per click: {growthPerClick:F1}";
    }

    public void UpgradeGrowthPerSecond()
    {
        growthPerSecond += 0.1f;
        growthPerSecondText.text = $"Growth per second: {growthPerSecond:F1}";
    }

    public void UpgradePotionPriceMultiplier()
    {
        potionPriceMultiplier += 0.1f;
        potionPriceMultiplierText.text = $"Potion price multiplier: {potionPriceMultiplier:F1}";
    }

    // Update is called once per frame
    void Update()
    {

    }
}
