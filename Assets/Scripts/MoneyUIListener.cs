using TMPro;
using UnityEngine;

public class MoneyUIListener : MonoBehaviour
{
    [SerializeField] private TMP_Text label;
    [SerializeField] private GameObject moneyPlus; //prefab for the floating +X text
    private bool isFirstUpdate = true;

    private void OnEnable()
    {
        isFirstUpdate = true;
        MoneyManager.OnMoneyChanged += HandleMoneyChanged;
        if (MoneyManager.instance != null)
        {
            UpdateText(MoneyManager.instance.CurrentMoney);
        }
    }

    private void HandleMoneyChanged(double newAmount, double amount)
    {
        UpdateText(newAmount);
        if (!isFirstUpdate)
        {
            HandleMoneyAdded(amount); // Reuse the money added logic for the floating text
        }
        isFirstUpdate = false;
    }

    private void HandleMoneyAdded(double amountAdded)
    {
        if (moneyPlus != null)
        {
            GameObject plusInstance = Instantiate(moneyPlus, transform);
            TMP_Text plusText = plusInstance.GetComponent<TMP_Text>();
            if (plusText != null)
            {
                plusText.text = $"+{amountAdded:0}";
            }
        }
    }

    private void UpdateText(double amount)
    {
        label.text = $"{amount:0}";
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (MoneyManager.instance != null)
        {
            UpdateText(MoneyManager.instance.CurrentMoney);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
