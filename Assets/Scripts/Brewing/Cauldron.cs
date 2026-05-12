using UnityEngine;

public class Cauldron : MonoBehaviour
{
    private bool ready;
    private SpriteRenderer sprite;

    [SerializeField] private GameObject potionsell;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    public void MakeReady()
    {
        ready = true;
        sprite.color = Color.green;
    }

    public void OnMouseDown()
    {
        if (ready)
        {
            int price = BrewingManager.instance.BrewPotion();
            sprite.color = Color.white;
            ready = false;
            GameObject potion = Instantiate(potionsell, transform.position + new Vector3(0, -1.25f, 0), Quaternion.identity); ;
            potion.GetComponent<PotionSellPopup>().SetPrice(price);

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
