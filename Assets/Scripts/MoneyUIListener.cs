using TMPro;
using UnityEngine;

public class MoneyUIListener : MonoBehaviour
{
    [SerializeField] private TMP_Text label;

    private void OnEnable()
    {
        MoneyManager.OnMoneyChanged += HandleMoneyChanged;
        if (MoneyManager.instance != null)
        {
            UpdateText(MoneyManager.instance.CurrentMoney);
        }
    }

    private void HandleMoneyChanged(double newAmount)
    {
        UpdateText(newAmount);
    }

    private void UpdateText(double amount)
    {
        label.text = $"${amount:0}";
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
