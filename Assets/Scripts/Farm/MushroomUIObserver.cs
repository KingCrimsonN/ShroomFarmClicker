using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MushroomUIObserver : MonoBehaviour
{
    [SerializeField] private MushroomManager.MushroomType targetType;
    [SerializeField] private TMP_Text labelToUpdate;
    [SerializeField] private Image icon;

    private void OnEnable()
    {
        MushroomManager.OnInventoryChanged += HandleInventoryChanged;
        // Initialize with current value on spawn/enable
        if (MushroomManager.instance != null)
        {
            UpdateText(MushroomManager.instance.GetMushroomCount(targetType));
            UpdateIcon();
        }
    }

    private void OnDisable()
    {
        MushroomManager.OnInventoryChanged -= HandleInventoryChanged;
    }

    private void HandleInventoryChanged(MushroomManager.MushroomType type, int newAmount)
    {
        if (type == targetType)
        {
            UpdateText(newAmount);
        }
    }

    private void UpdateText(int amount)
    {
        labelToUpdate.text = amount.ToString();
    }

    private void UpdateIcon()
    {
        if (icon == null) return;
        icon.sprite = MushroomManager.instance.mushroomSprites.sprites[(int)targetType];
    }
}