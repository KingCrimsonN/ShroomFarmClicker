using UnityEngine;

public class ConsequenceManager : MonoBehaviour
{
    [SerializeField] private ChestOfFunnies consequences;
    [SerializeField] private GameObject consequencePrefab;

    public void TriggerConsequence()
    {
        string consequence = consequences.consequences[Random.Range(0, consequences.consequences.Length)];
        GameObject consequenceObj = Instantiate(consequencePrefab, transform);
        consequenceObj.GetComponent<Consequence>().SetText(consequence);
        consequenceObj.transform.SetSiblingIndex(0);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
