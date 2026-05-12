using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class MushroomManager : MonoBehaviour
{
    public static MushroomManager instance;
    public enum MushroomType
    {
        Red,
        Green,
        Blue
    }

    [SerializeField]
    private TMP_Text[] labels;

    public int[] mushroomInventory = new int[3];

    public TMP_Text[] farmMushroomLabels;
    public TMP_Text[] brewingMushroomLabels;

    public void AddMushroom(MushroomType type, int amount)
    {
        mushroomInventory[(int)type] += amount;
        UpdateLabels();
    }

    private void UpdateLabels()
    {
        for (int i = 0; i < mushroomInventory.Length; i++)
        {
            farmMushroomLabels[i].text = mushroomInventory[i].ToString();
            brewingMushroomLabels[i].text = mushroomInventory[i].ToString();
        }
    }

    private void Awake()
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
}
