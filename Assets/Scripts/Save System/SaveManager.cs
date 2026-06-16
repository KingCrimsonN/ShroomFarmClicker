using System;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    private const string SAVE_KEY = "MushroomFarm_SaveData";

    void Awake()
    {
        if (instance != null) { Destroy(gameObject); return; }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        LoadGame();
    }

    // CRITICAL FOR MOBILE: Mobile apps rarely close cleanly via "Quit". 
    // They get paused/suspended when a text comes in or home is pressed.
    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus) SaveGame();
    }

    void OnApplicationQuit()
    {
        SaveGame();
    }

    [ContextMenu("Clear Save Data")]
    public void ClearSaveData()
    {
        PlayerPrefs.DeleteKey(SAVE_KEY);
        PlayerPrefs.Save();
        Debug.Log("Save Data Cleared!");
    }

    public void SaveGame()
    {
        if (MoneyManager.instance == null || UpgradeManager.instance == null || MushroomManager.instance == null) return;

        SaveData data = new SaveData();

        // 1. Gather values from current managers
        data.money = MoneyManager.instance.CurrentMoney;

        // Extract Upgrade Levels
        int upgradeCount = Enum.GetValues(typeof(UpgradeManager.UpgradeType)).Length;
        data.upgradeLevels = new int[upgradeCount];
        for (int i = 0; i < upgradeCount; i++)
        {
            data.upgradeLevels[i] = UpgradeManager.instance.GetUpgradeLevel((UpgradeManager.UpgradeType)i);
        }

        // Extract Mushroom Counts
        int mushroomTypeCount = Enum.GetValues(typeof(MushroomManager.MushroomType)).Length;
        data.mushroomCounts = new int[mushroomTypeCount];
        for (int i = 0; i < mushroomTypeCount; i++)
        {
            data.mushroomCounts[i] = MushroomManager.instance.GetMushroomCount((MushroomManager.MushroomType)i);
        }

        //Extract Mushroom Purchases
        data.mushroomPurchased = new bool[mushroomTypeCount];
        for (int i = 0; i < mushroomTypeCount; i++)
        {
            data.mushroomPurchased[i] = MushroomManager.instance.IsMushroomPurchased((MushroomManager.MushroomType)i);
        }

        data.mushroomGrowth = new float[mushroomTypeCount];
        for (int i = 0; i < mushroomTypeCount; i++)
        {
            data.mushroomGrowth[i] = MushroomManager.instance.GetGrowth((MushroomManager.MushroomType)i);
        }

        int recipeCount = CookBookManager.instance.recipeDatabase.Count;
        data.potionsUnlocked = new bool[recipeCount];
        for (int i = 0; i < recipeCount; i++)
        {
            data.potionsUnlocked[i] = CookBookManager.instance.IsRecipeUnlocked(i);
        }

        // 2. Timestamp the file
        data.lastSaveTimestamp = DateTime.UtcNow.ToString("o"); // ISO 8601 standard format string

        // 3. Serialize and write safely to device disk storage
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(SAVE_KEY, json);
        PlayerPrefs.Save();

        Debug.Log("Game Saved Successfully.");
    }

    public void LoadGame()
    {
        if (!PlayerPrefs.HasKey(SAVE_KEY))
        {
            Debug.Log("No save file detected. Starting fresh!");
            return;
        }

        try
        {
            string json = PlayerPrefs.GetString(SAVE_KEY);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            // 1. Inject values back into managers
            MoneyManager.instance.AddMoney(data.money);

            // Reload Upgrade Levels
            for (int i = 0; i < data.upgradeLevels.Length; i++)
            {
                int level = data.upgradeLevels[i];
                // Force upgrade levels directly inside the manager safely
                UpgradeManager.instance.LoadLevelDataDirectly((UpgradeManager.UpgradeType)i, level);
            }

            // Reload Mushroom Counts
            for (int i = 0; i < data.mushroomCounts.Length; i++)
            {
                MushroomManager.instance.LoadInventoryDataDirectly((MushroomManager.MushroomType)i, data.mushroomCounts[i]);
            }

            for (int i = 0; i < data.mushroomGrowth.Length; i++)
            {
                MushroomManager.instance.SetGrowth((MushroomManager.MushroomType)i, (int)data.mushroomGrowth[i]);
            }


            // Reload Mushroom Purchases
            for (int i = 0; i < data.mushroomPurchased.Length; i++)
            {
                MushroomManager.instance.LoadPurchaseDataDirectly((MushroomManager.MushroomType)i, data.mushroomPurchased[i]);
            }

            // Reload Recipe Unlocks
            for (int i = 0; i < data.potionsUnlocked.Length; i++)
            {
                if (data.potionsUnlocked[i])
                    CookBookManager.instance.UnlockRecipe(i);
            }

            // 2. Run Offline Progression Math
            CalculateOfflineProgression(data.lastSaveTimestamp);
        }
        catch (Exception e)
        {
            Debug.LogError($"Save file corruption detected or parse error! Resetting state safely: {e.Message}");
        }
    }

    private void CalculateOfflineProgression(string rawTimestamp)
    {
        if (UpgradeManager.instance.GetUpgradeLevel(UpgradeManager.UpgradeType.GrowthPerSecond) <= 0) return;
        if (string.IsNullOrEmpty(rawTimestamp)) return;

        DateTime lastSaveTime = DateTime.Parse(rawTimestamp);
        TimeSpan timeElapsed = DateTime.UtcNow - lastSaveTime;

        double totalSecondsAway = timeElapsed.TotalSeconds;
        float growthPerSecond = UpgradeManager.instance.growthPerSecond;

        if (totalSecondsAway <= 10 || growthPerSecond <= 0) return; // Prevent calculations on instant reboots

        // Calculate absolute overall points generated while away
        float rawPointsGenerated = (float)totalSecondsAway * growthPerSecond;
        int mushroomsToGive = Mathf.FloorToInt(rawPointsGenerated);

        if (mushroomsToGive > 0)
        {
            foreach (MushroomManager.MushroomType type in Enum.GetValues(typeof(MushroomManager.MushroomType)))
            {
                MushroomManager.instance.mushroomGrowth[(int)type] += mushroomsToGive;
            }

            // Dynamic Hook: Trigger your visual welcome screen popup notifying the player!
            UINotificationPanel.instance?.ShowOfflineEarningsPopup(timeElapsed, mushroomsToGive);
        }
    }
}