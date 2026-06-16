using UnityEngine;

public class BrewingSlot : MonoBehaviour
{
    [SerializeField] private SpriteRenderer mushroomSprite;

    void Awake()
    {
        // mushroomSprite = GetComponentInChildren<SpriteRenderer>();
    }

    void Start()
    {
        ClearSlot();
    }

    public void SetMushroomVisual(MushroomManager.MushroomType type)
    {
        if (mushroomSprite == null) return;

        mushroomSprite.sprite = MushroomManager.instance.GetSprite(type);
    }

    public void ClearSlot()
    {
        if (mushroomSprite != null)
        {
            mushroomSprite.sprite = null; // Hide visual transparently
        }
    }
}