using UnityEngine;

public class Cauldron : MonoBehaviour
{
    private bool ready;
    private SpriteRenderer sprite;
    [SerializeField] private Sprite readySprite;
    private Sprite defaultSprite;

    [SerializeField] private GameObject potionsell;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        defaultSprite = sprite.sprite;
    }

    public void MakeReady()
    {
        ready = true;
        sprite.sprite = readySprite;
    }

    public void OnMouseDown()
    {
        if (ready)
        {
            string potionName;
            int potionPrice;
            BrewingManager.instance.BrewPotion(out potionPrice, out potionName);
            sprite.sprite = defaultSprite;
            ready = false;
            GameObject potion = Instantiate(potionsell, transform.position, Quaternion.identity); ;
            potion.GetComponent<PotionSellPopup>().SetPrice(potionPrice);
            potion.GetComponent<PotionSellPopup>().SetPotionName(potionName);

        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
