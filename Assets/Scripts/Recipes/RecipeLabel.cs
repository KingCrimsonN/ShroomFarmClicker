using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeLabel : MonoBehaviour
{
    [SerializeField]
    public PotionRecipe recipe;
    [SerializeField]
    public Image[] mushrooms;
    public TMP_Text recipeName;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CookBookManager.OnRecipeUnlocked += UnlockRecipe;
        if (CookBookManager.instance.IsRecipeUnlocked(recipe.ID))
        {
            UnlockRecipe(recipe.ID);
        }
    }

    public void UnlockRecipe(int ID)
    {
        if (recipe.ID != ID) return;
        recipeName.text = recipe.potionName;
        for (int i = 0; i < 3; i++)
        {
            mushrooms[i].sprite = MushroomManager.instance.GetSprite(recipe.requiredIngredients[i]);
        }
    }

}
