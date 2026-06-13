using System;
using System.Collections.Generic;
using UnityEngine;

public class CookBookManager : MonoBehaviour
{
    public static CookBookManager instance;

    [SerializeField] public List<PotionRecipe> recipeDatabase;

    [SerializeField] public bool[] unlockedRecipes;

    public static event Action<int> OnRecipeUnlocked;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (instance != null) { Destroy(gameObject); return; }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public bool IsRecipeUnlocked(int ID)
    {
        return unlockedRecipes[ID];
    }

    public void UnlockRecipe(int ID)
    {
        if (!IsRecipeUnlocked(ID))
            unlockedRecipes[ID] = true;

        OnRecipeUnlocked?.Invoke(ID);
    }
}
