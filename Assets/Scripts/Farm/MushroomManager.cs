using System;
using UnityEngine;

public class MushroomManager : MonoBehaviour
{
    public static MushroomManager instance;

    public ShroomSpriteList mushroomSprites;

    public enum MushroomType
    {
        Red,
        Green,
        Blue
    }

    // A C# Action acts as an event dispatcher. 
    // Any UI script can subscribe to this to know when a specific mushroom count updates.
    public static event Action<MushroomType, int> OnInventoryChanged;

    // Using an array sized by the Enum dynamically prevents hardcoding errors
    public int[] mushroomInventory = new int[Enum.GetNames(typeof(MushroomType)).Length];

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

    public int GetMushroomCount(MushroomType type)
    {
        return mushroomInventory[(int)type];
    }
}