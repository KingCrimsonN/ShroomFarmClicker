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
        if (CheckMushroomPurchase())
        {
            isPurchased = true;
            readyOverlay.SetActive(false);
            mushroom.SetActive(true);
            GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            mushroom.SetActive(false);
        }
        MoneyManager.OnMoneyChanged += CheckMoney;
        purchasePanel.InitPanel(mushroomType, price);
        CheckMoney(MoneyManager.instance.CurrentMoney, 0);
        purchasePanel.gameObject.transform.localScale = Vector3.zero;
        readyOverlay.SetActive(false);

    }

    bool CheckMushroomPurchase()
    {
        return MushroomManager.instance.IsMushroomPurchased(mushroomType);
    }

    void CheckMoney(double money, double amount)
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
        print("POT CLICKED");
        if (isPurchased) return;
        purchasePanel.Open();
        // transform.DOShakePosition(0.1f, 0.5f);
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
            MushroomManager.instance.PurchaseMushroom(mushroomType);
        }
    }


}
