using TMPro;
using UnityEngine;

public class ShroomButton : MonoBehaviour
{
    [SerializeField] private MushroomManager.MushroomType mushroomType;
    private TMP_Text label;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void UpdateLabel()
    {
        label.text = MushroomManager.instance.mushroomInventory[(int)mushroomType].ToString();
    }

    void Start()
    {
        label = GetComponentInChildren<TMP_Text>();
        UpdateLabel();
    }

    public void PutMushroom()
    {
        if (MushroomManager.instance.mushroomInventory[(int)mushroomType] > 0 && BrewingManager.instance.currentSlot < 3)
        {
            MushroomManager.instance.AddMushroom(mushroomType, -1);
            BrewingManager.instance.AddShroom(mushroomType);
            UpdateLabel();
        }
    }
}
