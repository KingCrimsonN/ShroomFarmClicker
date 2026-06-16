using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShroomButton : MonoBehaviour
{
    [SerializeField] private MushroomManager.MushroomType mushroomType;
    [SerializeField] private TMP_Text quantityLabel;
    [SerializeField] private Button buttonComponent; // Reference to the UI Button to enable/disable it dynamically
    [SerializeField] private Image mainSprite;

    void Start()
    {
        buttonComponent.onClick.AddListener(PutMushroomInCauldron);
        // mainSprite = GetComponent<Image>();
        // mainSprite.sprite = MushroomManager.instance.GetSprite(mushroomType);
    }

    private void OnEnable()
    {
        MushroomManager.OnInventoryChanged += HandleInventoryChanged;

        // Setup initialization values securely
        if (MushroomManager.instance != null)
        {
            UpdateUI(MushroomManager.instance.GetMushroomCount(mushroomType));
        }
    }

    private void OnDisable()
    {
        MushroomManager.OnInventoryChanged -= HandleInventoryChanged;
    }

    private void HandleInventoryChanged(MushroomManager.MushroomType type, int newAmount)
    {
        if (type == mushroomType)
        {
            UpdateUI(newAmount);
        }
    }

    private void UpdateUI(int amount)
    {
        if (quantityLabel != null) quantityLabel.text = amount.ToString();

        // Scalability Bonus: Disable the click button dynamically if you're clean out of shrooms!
        if (buttonComponent != null) buttonComponent.interactable = amount > 0;
    }

    // Called via UI Button Click Event
    public void PutMushroomInCauldron()
    {
        // Check if we have the inventory space and if the cauldron can accept it
        int currentCount = MushroomManager.instance.GetMushroomCount(mushroomType);

        if (currentCount > 0 && !BrewingManager.instance.IsCauldronFull)
        {
            // Try adding to cauldron first. If successful, deduct from data tracking layer
            if (BrewingManager.instance.TryAddIngredient(mushroomType))
            {
                MushroomManager.instance.AddMushroom(mushroomType, -1);
            }
        }
    }
}