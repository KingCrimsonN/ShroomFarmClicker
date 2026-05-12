using UnityEngine;

public class BrewingManager : MonoBehaviour
{

    public static BrewingManager instance;

    public int currentSlot;
    public BrewingSlot[] brewingSlots = new BrewingSlot[3];

    public Cauldron cauldron;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentSlot = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddShroom(MushroomManager.MushroomType type)
    {
        if (currentSlot > 2)
        {
            return;
        }
        brewingSlots[currentSlot].AddShroom(type);
        NextSlot();
    }


    public void NextSlot()
    {
        currentSlot = currentSlot + 1;
        if (currentSlot > 2)
        {
            cauldron.MakeReady();
        }
    }

    // TODO: Make a potion class with value
    public int BrewPotion()
    {
        float tempMoney = 0;
        foreach (BrewingSlot slot in brewingSlots)
        {
            tempMoney += ((int)slot.mushroomType + 1) * 10f * UpgradeManager.instance.potionPriceMultiplier;
            print(tempMoney);
        }
        MoneyManager.instance.AddMoney(tempMoney);
        ResetSlots();
        return (int)tempMoney;
    }

    public void ResetSlots()
    {
        currentSlot = 0;
        foreach (BrewingSlot slot in brewingSlots)
        {
            slot.ClearSlot();
        }
    }
}
