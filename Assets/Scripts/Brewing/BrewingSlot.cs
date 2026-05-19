using UnityEngine;

public class BrewingSlot : MonoBehaviour
{
    [SerializeField] private SpriteRenderer mushroomSprite;

    void Awake()
    {
        mushroomSprite = GetComponentInChildren<SpriteRenderer>();
    }

    void Start()
    {
        ClearSlot();
    }

    public void SetMushroomVisual(MushroomManager.MushroomType type)
    {
        if (mushroomSprite == null) return;

        mushroomSprite.color = type switch
        {
            MushroomManager.MushroomType.Red => Color.red,
            MushroomManager.MushroomType.Green => Color.green,
            MushroomManager.MushroomType.Blue => Color.blue,
            _ => Color.white
        };
    }

    public void ClearSlot()
    {
        if (mushroomSprite != null)
        {
            mushroomSprite.color = new Color(1, 1, 1, 0); // Hide visual transparently
        }
    }
}