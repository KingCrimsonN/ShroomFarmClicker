using TMPro;
using UnityEngine;

public class MushroomUIObserver : MonoBehaviour
{
    [SerializeField] private MushroomManager.MushroomType targetType;
    [SerializeField] private TMP_Text labelToUpdate;

    private void OnEnable()
    {
        MushroomManager.OnInventoryChanged += HandleInventoryChanged;
        // Initialize with current value on spawn/enable
        if (MushroomManager.instance != null)
        {
            UpdateText(MushroomManager.instance.GetMushroomCount(targetType));
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
}