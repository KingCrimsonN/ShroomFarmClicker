using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Mushroom : MonoBehaviour
{
    [SerializeField]
    private string mushroom_name;
    [SerializeField]
    private MushroomManager.MushroomType mushroom_type;

    private TMP_Text mushroom_text;
    private TMP_Text growth_text;
    [SerializeField]
    private int total_growth;
    [SerializeField] // TODO: REMOVE
    private int growth_per_second;
    [SerializeField] // TODO: REMOVE
    private int growth_per_click;
    [SerializeField]
    private float current_growth;

    private float growth_timer;

    InputActionAsset actions;

    void Awake()
    {
        // actions = GetComponent<PlayerInput>().actions;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mushroom_text = transform.Find("MushroomName").GetComponent<TMP_Text>();
        mushroom_text.text = mushroom_name;
        growth_text = transform.Find("MushroomGrowth").GetComponent<TMP_Text>();
        growth_text.text = current_growth.ToString() + "/" + total_growth.ToString();
        current_growth = 0;
    }

    void OnMouseDown()
    {
        current_growth += UpgradeManager.instance.growthPerClick;
        if (current_growth > total_growth)
        {
            Harvest();
        }
        growth_text.text = current_growth.ToString("F0") + "/" + total_growth.ToString();
    }

    // Harvest the mushroom: 
    // Reset the number
    // Add to inventory
    public void Harvest()
    {
        current_growth = 0;
        MushroomManager.instance.AddMushroom(mushroom_type, 1);
        /*
        check what type 
        depending on type we increase inventory in MushroomManager
        */
    }

    // Update is called once per frame
    // Musroom grows over time
    // Mushroom grows on click
    void Update()
    {
        if (current_growth < total_growth)
        {
            current_growth += UpgradeManager.instance.growthPerSecond * Time.deltaTime;
        }
        growth_timer += Time.deltaTime;
        if (growth_timer >= 1f)
        {
            growth_timer = 0f;
            growth_text.text = current_growth.ToString("F0") + "/" + total_growth.ToString();
        }
    }
}
