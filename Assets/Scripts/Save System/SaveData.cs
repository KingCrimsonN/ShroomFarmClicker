[System.Serializable]
public class SaveData
{
    public double money;

    // Arrays map perfectly to our Enum integers (e.g., index 0 = Red, 1 = Green)
    public int[] upgradeLevels;
    public int[] mushroomCounts;
    public bool[] mushroomPurchased;
    public float[] mushroomGrowth;

    // Store time as a standardized string 
    public string lastSaveTimestamp;
}