using System;
using UnityEngine;

public class MushroomManager : MonoBehaviour
{
    public static MushroomManager instance;

    public ShroomSpriteList mushroomSprites;

    public enum MushroomType
    {
        Champignon,
        AngerMushroom,
        WizardMushroom,
        LoveMushroom,
        HealingMushroom,
        EmploymentMushroom
    }

    // A C# Action acts as an event dispatcher. 
    // Any UI script can subscribe to this to know when a specific mushroom count updates.
    public static event Action<MushroomType, int> OnInventoryChanged;

    // Using an array sized by the Enum dynamically prevents hardcoding errors
    public int[] mushroomInventory = new int[Enum.GetNames(typeof(MushroomType)).Length];
    public bool[] mushroomPurchased = new bool[Enum.GetNames(typeof(MushroomType)).Length];
    public float[] mushroomGrowth = new float[Enum.GetNames(typeof(MushroomType)).Length];

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddMushroom(MushroomType type, int amount)
    {
        int index = (int)type;
        mushroomInventory[index] += amount;

        // Broadcast to anyone listening (like UI components) that data changed
        OnInventoryChanged?.Invoke(type, mushroomInventory[index]);
    }

    public float GetGrowth(MushroomType type)
    {
        return mushroomGrowth[(int)type];
    }

    public void SetGrowth(MushroomType type, float growth)
    {
        mushroomGrowth[(int)type] = growth;
    }

    public int GetMushroomCount(MushroomType type)
    {
        return mushroomInventory[(int)type];
    }

    public bool IsMushroomPurchased(MushroomType type)
    {
        return mushroomPurchased[(int)type];
    }

    public void PurchaseMushroom(MushroomType type)
    {
        mushroomPurchased[(int)type] = true;
    }

    public void LoadInventoryDataDirectly(MushroomType type, int count)
    {
        // Assuming your internal storage dictionary or array maps integers directly:
        mushroomInventory[(int)type] = count;

        // Broadcast the event immediately so all your UI text components update instantly on startup!
        OnInventoryChanged?.Invoke(type, count);
    }

    public void LoadPurchaseDataDirectly(MushroomType type, bool purchased)
    {
        mushroomPurchased[(int)type] = purchased;
    }
}