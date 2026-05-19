using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPotionRecipe", menuName = "Alchemy/Potion Recipe")]
public class PotionRecipe : ScriptableObject
{
    public string potionName;
    public int basePrice = 50;

    [Tooltip("Requires exactly 3 mushrooms. Order does not matter.")]
    public List<MushroomManager.MushroomType> requiredIngredients = new List<MushroomManager.MushroomType>(3);

    // Checks if the ingredients in the cauldron match this recipe
    public bool IsMatch(List<MushroomManager.MushroomType> cauldronIngredients)
    {
        if (cauldronIngredients == null || cauldronIngredients.Count != 3) return false;

        // Create temporary copies to sort and compare without destroying original data
        var checkList = new List<MushroomManager.MushroomType>(cauldronIngredients);
        var requiredList = new List<MushroomManager.MushroomType>(requiredIngredients);

        checkList.Sort();
        requiredList.Sort();

        for (int i = 0; i < 3; i++)
        {
            if (checkList[i] != requiredList[i]) return false;
        }
        return true;
    }
}