using TMPro;
using UnityEngine;

// Class for the selling popup 
// Needs to fetch the potion based on the mushrooms used
public class PotionSellPopup : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    private int price;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, 2f);
    }

    public void SetPrice(int price)
    {
        this.price = price;
        text.text = $"${price}";
    }

    // Update is called once per frame
    void Update()
    {

    }
}
