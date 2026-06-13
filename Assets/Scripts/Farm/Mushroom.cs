using DG.Tweening;
using TMPro;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    [Header("Identity")]
    [SerializeField] private string mushroomName;
    [SerializeField] private MushroomManager.MushroomType mushroomType;
    [SerializeField] private SpriteRenderer potSprite;
    [SerializeField] private SpriteRenderer mushroomSprite;

    [Header("UI References")] // Drag these in the Inspector! No more transform.Find
    [SerializeField] private TMP_Text nameLabel;
    [SerializeField] private TMP_Text growthLabel;
    [SerializeField] private GameObject harvestLabel;

    [Header("Growth Settings")]
    [SerializeField] private float totalGrowth = 10f;
    private float currentGrowth;

    private float uiUpdateTimer;
    private const float UI_UPDATE_INTERVAL = 0.2f; // Update UI 5 times a sec instead of every frame (Mobile optimization)

    void Start()
    {
        // mushroomSprite = GetComponentInChildren<SpriteRenderer>();
        if (nameLabel != null) nameLabel.text = mushroomName;
        UpdateGrowthDisplay();
        UpdateSprite();
        ResetSpriteScale();
        FetchGrowth();
    }

    void FetchGrowth()
    {
        float growth = MushroomManager.instance.GetGrowth(mushroomType);
        if (UpgradeManager.instance.GetUpgradeLevel(UpgradeManager.UpgradeType.AutoHarvest) > 0)
        {
            // If auto-harvest is unlocked, we calculate how many full harvests occurred during offline time
            int mushroomsGrown = (int)(growth / totalGrowth);
            MushroomManager.instance.AddMushroom(mushroomType, mushroomsGrown);
            growth -= mushroomsGrown * totalGrowth; // Remove the harvested growth
            currentGrowth = growth;
            MushroomManager.instance.SetGrowth(mushroomType, growth); // Update the manager with leftover growth
        }
        else
        {
            if (growth >= totalGrowth)
            {
                currentGrowth = totalGrowth;
                MushroomManager.instance.SetGrowth(mushroomType, growth);
                return;
            }
            currentGrowth = growth;
            MushroomManager.instance.SetGrowth(mushroomType, growth);
        }
    }

    void ResetSpriteScale()
    {
        mushroomSprite.gameObject.transform.localScale = Vector3.zero; // Start invisible and grow in
    }

    void Update()
    {
        // Continuous passive growth
        if (currentGrowth < totalGrowth)
        {
            currentGrowth += UpgradeManager.instance.growthPerSecond * Time.deltaTime;

            // Optional: If you want passive growth to auto-harvest when full:
            if (currentGrowth >= totalGrowth && UpgradeManager.instance.GetUpgradeLevel(UpgradeManager.UpgradeType.AutoHarvest) > 0) Harvest();
            harvestLabel.SetActive(currentGrowth + 1 >= totalGrowth);
        }

        // Performance Optimization: Don't update strings every single frame on mobile.
        uiUpdateTimer += Time.deltaTime;
        if (uiUpdateTimer >= UI_UPDATE_INTERVAL)
        {
            mushroomSprite.gameObject.transform.localScale = Vector3.one * (currentGrowth / totalGrowth) * (1f + 0.1f * Mathf.Sin(Time.time * 5f)); // Subtle breathing animation
            uiUpdateTimer = 0f;
            UpdateGrowthDisplay();
        }
    }

    void OnMouseDown()
    {
        print("MUSHROOM CLICKED");
        potSprite.transform.DOShakeScale(0.1f, 0.5f).OnComplete(() =>
                {
                    potSprite.transform.localScale = Vector3.one; // Ensure it ends at the correct scale
                });

        currentGrowth = Mathf.Min(totalGrowth, currentGrowth + UpgradeManager.instance.growthPerClick);
        MushroomManager.instance.SetGrowth(mushroomType, currentGrowth);

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
        ResetSpriteScale();
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

    private void UpdateSprite()
    {
        if (mushroomSprite == null) return;
        mushroomSprite.sprite = MushroomManager.instance.mushroomSprites.sprites[(int)mushroomType];
    }
}