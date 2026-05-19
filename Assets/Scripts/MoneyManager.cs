using System;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager instance;

    // Use double to avoid game breaking overflows in incremental math
    private double currentMoney;
    public double CurrentMoney => currentMoney;

    // Broadcaster for any UI elements tracking total wealth
    public static event Action<double> OnMoneyChanged;

    void Awake()
    {
        if (instance != null) { Destroy(gameObject); return; }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddMoney(double amount)
    {
        currentMoney += amount;
        if (currentMoney < 0) currentMoney = 0; // Safeguard against negative balances

        OnMoneyChanged?.Invoke(currentMoney);
    }

    public bool HasEnoughMoney(double amount)
    {
        return currentMoney >= amount;
    }
}