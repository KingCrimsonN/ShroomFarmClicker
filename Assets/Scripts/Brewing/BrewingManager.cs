using UnityEngine;

public class BrewingManager : MonoBehaviour
{

    public static BrewingManager instance;

    public int currentSlot;
    public BrewingSlot[] brewingSlots = new BrewingSlot[3];

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

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddShroom(MushroomManager.MushroomType type)
    {
        brewingSlots[currentSlot].AddShroom(type);
        NextSlot();
    }


    public void NextSlot()
    {
        currentSlot = (currentSlot + 1) % 3;
    }
}
