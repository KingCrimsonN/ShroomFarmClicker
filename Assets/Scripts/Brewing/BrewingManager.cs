using System;
using System.Collections.Generic;
using UnityEngine;

public class BrewingManager : MonoBehaviour
{
    public static BrewingManager instance;

    [Header("Slots Configuration")]
    [SerializeField] private BrewingSlot[] brewingSlots = new BrewingSlot[3];
    [SerializeField] private Cauldron cauldron;

    [Header("Recipe Database")]
    [SerializeField] private List<PotionRecipe> recipeDatabase;
    [SerializeField] private int failedPotionPrice = 5;

    // Tracking internal state securely
    private List<MushroomManager.MushroomType> ingredientsInCauldron = new List<MushroomManager.MushroomType>();

    public int CurrentSlotCount => ingredientsInCauldron.Count;
    public bool IsCauldronFull => ingredientsInCauldron.Count >= 3;

    [SerializeField] private ConsequenceManager consequenceManager;

    void Awake()
    {
        if (instance != null) { Destroy(gameObject); return; }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public bool TryAddIngredient(MushroomManager.MushroomType type)
    {
        if (IsCauldronFull) return false;

        ingredientsInCauldron.Add(type);

        // Update the visual slot representation
        int assignedSlotIndex = ingredientsInCauldron.Count - 1;
        brewingSlots[assignedSlotIndex].SetMushroomVisual(type);

        if (IsCauldronFull)
        {
            cauldron.MakeReady();
        }

        return true;
    }

    public void RefundAndResetSlots()
    {
        // Give back mushrooms to inventory because the brew was cancelled
        foreach (var type in ingredientsInCauldron)
        {
            MushroomManager.instance.AddMushroom(type, 1);
        }
        ResetSlotsDataOnly();
    }

    public void BrewPotion(out int finalPriceOut, out string potionName)
    {
        if (!IsCauldronFull)
        {
            finalPriceOut = 0;
            potionName = null;
            return;
        }

        // 1. Determine potion type and pricing dynamically from data
        PotionRecipe matchedRecipe = null;
        foreach (var recipe in recipeDatabase)
        {
            if (recipe.IsMatch(ingredientsInCauldron))
            {
                matchedRecipe = recipe;
                break;
            }
        }
        potionName = matchedRecipe != null ? matchedRecipe.potionName : "Useless potion";
        float calculatedPrice = matchedRecipe != null ? matchedRecipe.basePrice : failedPotionPrice;

        // Apply Global Modifiers
        calculatedPrice *= UpgradeManager.instance.potionPriceMultiplier;
        int finalPrice = Mathf.RoundToInt(calculatedPrice);
        finalPriceOut = finalPrice;

        // 2. Add Money
        MoneyManager.instance.AddMoney(finalPrice);

        // 3. Clear data without refunding (since it was successfully turned into a potion)
        ResetSlotsDataOnly();

        // 4. Trigger consequences
        consequenceManager.TriggerConsequence();
    }

    private void ResetSlotsDataOnly()
    {
        ingredientsInCauldron.Clear();
        foreach (BrewingSlot slot in brewingSlots)
        {
            slot.ClearSlot();
        }
    }
}