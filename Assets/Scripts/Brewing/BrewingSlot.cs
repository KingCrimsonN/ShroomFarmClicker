using UnityEngine;

public class BrewingSlot : MonoBehaviour
{
    MushroomManager.MushroomType mushroomType;
    private SpriteRenderer mushroomSprite;

    public void AddShroom(MushroomManager.MushroomType type)
    {
        mushroomType = type;
        switch (type)
        {
            case MushroomManager.MushroomType.Red:
                mushroomSprite.color = Color.red;
                break;
            case MushroomManager.MushroomType.Green:
                mushroomSprite.color = Color.green;
                break;
            case MushroomManager.MushroomType.Blue:
                mushroomSprite.color = Color.blue;
                break;
        }
    }

    public void ClearSlot()
    {
        mushroomSprite.color = new Color(1, 1, 1, 0);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mushroomSprite = GetComponentInChildren<SpriteRenderer>();
        ClearSlot();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
