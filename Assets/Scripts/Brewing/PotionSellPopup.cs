using TMPro;
using UnityEngine;

// Class for the selling popup 
// Needs to fetch the potion based on the mushrooms used
public class PotionSellPopup : MonoBehaviour
{
    [SerializeField] private TMP_Text priceText;
    [SerializeField] private TMP_Text potionName;
    private int price;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, 2f);
    }

    public void SetPrice(int price)
    {
        this.price = price;
        priceText.text = $"${price}";
    }

    public void SetPotionName(string name)
    {
        potionName.text = name;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
