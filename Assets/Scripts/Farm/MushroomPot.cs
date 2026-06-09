using DG.Tweening;
using UnityEngine;

public class MushroomPot : MonoBehaviour
{
    [SerializeField] private GameObject readyOverlay;

    [SerializeField] private MushroomManager.MushroomType mushroomType;
    private bool opening = false;

    [SerializeField] private int price;

    [SerializeField] private ShroomPurchase purchasePanel;
    [SerializeField] private GameObject mushroom;

    private bool isPurchased = false;

    void Start()
    {
        purchasePanel = GetComponentInChildren<ShroomPurchase>();
        mushroom = GetComponentInChildren<Mushroom>().gameObject;
        if (!isPurchased)
        {
            mushroom.SetActive(false);
        }
        purchasePanel.InitPanel(mushroomType, price);
        MoneyManager.OnMoneyChanged += CheckMoney;
        CheckMoney(MoneyManager.instance.CurrentMoney);
        purchasePanel.gameObject.transform.localScale = Vector3.zero;
        readyOverlay.SetActive(false);
    }

    void CheckMoney(double money)
    {
        if (isPurchased) return;
        if (MoneyManager.instance.HasEnoughMoney(price))
        {
            readyOverlay.SetActive(true);
            purchasePanel.EnoughMoney();
        }
        else
        {
            readyOverlay.SetActive(false);
            purchasePanel.NotEnoughMoney();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (MoneyManager.instance.HasEnoughMoney(price) && !isPurchased)
        {
            readyOverlay.SetActive(true);
        }
    }

    void OnMouseDown()
    {
        if (isPurchased) return;
        purchasePanel.Open();
        // opening = true;

    }

    public void Purchase()
    {
        if (MoneyManager.instance.HasEnoughMoney(price))
        {
            purchasePanel.Close();
            isPurchased = true;
            MoneyManager.instance.AddMoney(-price);
            readyOverlay.SetActive(false);
            mushroom.SetActive(true);
        }
    }


}
