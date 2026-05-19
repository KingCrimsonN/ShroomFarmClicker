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

        mushroomSprite.sprite = type switch
        {
            MushroomManager.MushroomType.Red => MushroomManager.instance.mushroomSprites.sprites[0],
            MushroomManager.MushroomType.Green => MushroomManager.instance.mushroomSprites.sprites[1],
            MushroomManager.MushroomType.Blue => MushroomManager.instance.mushroomSprites.sprites[2],
            _ => null
        };
    }

    public void ClearSlot()
    {
        if (mushroomSprite != null)
        {
            mushroomSprite.sprite = null; // Hide visual transparently
        }
    }
}