using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Upgrade : MonoBehaviour
{

    private int price;

    [SerializeField] private UnityEvent onUpgrade;
    [SerializeField] private TMP_Text priceText;

    void OnMouseDown()
    {
        if (MoneyManager.instance.money < price)
        {
            return;
        }
        onUpgrade?.Invoke();
        MoneyManager.instance.AddMoney(-price);
        price += 10;
        priceText.text = $"{price}";
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        price = 10;
        priceText.text = $"{price}";
    }

    // Update is called once per frame
    void Update()
    {

    }
}
