using TMPro;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    [Header("Identity")]
    [SerializeField] private string mushroomName;
    [SerializeField] private MushroomManager.MushroomType mushroomType;

    [Header("UI References")] // Drag these in the Inspector! No more transform.Find
    [SerializeField] private TMP_Text nameLabel;
    [SerializeField] private TMP_Text growthLabel;

    [Header("Growth Settings")]
    [SerializeField] private float totalGrowth = 10f;
    private float currentGrowth;

    private float uiUpdateTimer;
    private const float UI_UPDATE_INTERVAL = 0.2f; // Update UI 5 times a sec instead of every frame (Mobile optimization)

    void Start()
    {
        if (nameLabel != null) nameLabel.text = mushroomName;
        UpdateGrowthDisplay();
    }

    void Update()
    {
        // Continuous passive growth
        if (currentGrowth < totalGrowth)
        {
            currentGrowth += UpgradeManager.instance.growthPerSecond * Time.deltaTime;

            // Optional: If you want passive growth to auto-harvest when full:
            // if (currentGrowth >= totalGrowth) Harvest();
        }

        // Performance Optimization: Don't update strings every single frame on mobile.
        uiUpdateTimer += Time.deltaTime;
        if (uiUpdateTimer >= UI_UPDATE_INTERVAL)
        {
            uiUpdateTimer = 0f;
            UpdateGrowthDisplay();
        }
    }

    void OnMouseDown()
    {
        currentGrowth += UpgradeManager.instance.growthPerClick;

        if (currentGrowth >= totalGrowth)
        {
            Harvest();
        }

        UpdateGrowthDisplay();
    }

    public void Harvest()
    {
        // Subtract instead of setting to 0 to preserve leftover progress
        currentGrowth = Mathf.Max(0, currentGrowth - totalGrowth);

        MushroomManager.instance.AddMushroom(mushroomType, 1);
    }

    private void UpdateGrowthDisplay()
    {
        if (growthLabel != null)
        {
            // Clamping display so it doesn't show visually weird numbers over the max layout
            float displayGrowth = Mathf.Min(currentGrowth, totalGrowth);
            growthLabel.text = $"{displayGrowth:F0}/{totalGrowth:F0}";
        }
    }
}