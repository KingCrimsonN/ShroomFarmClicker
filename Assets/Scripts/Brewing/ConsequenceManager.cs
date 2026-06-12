using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ConsequenceManager : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private GameObject consequencePrefab;
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private ChestOfFunnies consequences;

    [Header("Performance Settings")]
    [Tooltip("Prevents infinite entries from lagging mobile devices over long sessions")]
    [SerializeField] private int maxLogEntries = 35;

    public static event Action<double> OnConsequenceStarted;

    public void TriggerConsequence()
    {
        string consequence = consequences.consequences[UnityEngine.Random.Range(0, consequences.consequences.Length)];
        TriggerConsequence(consequence);
    }

    public void TriggerConsequence(string consequence)
    {
        // 1. Instantiate without modifying sibling index (appends to the bottom naturally)
        GameObject consequenceObj = Instantiate(consequencePrefab, transform);
        consequenceObj.GetComponent<Consequence>().SetText(consequence);

        // 2. Keep clean memory on mobile devices by deleting old entries from the top
        if (transform.childCount > maxLogEntries)
        {
            Destroy(transform.GetChild(0).gameObject);
        }

        // 3. Force UI to track down to the newest item
        StartCoroutine(SnapScrollToBottom());
    }

    private IEnumerator SnapScrollToBottom()
    {
        // Wait until the end of the frame so the UI Layout components finish processing sizing math
        yield return new WaitForEndOfFrame();

        if (scrollRect != null)
        {
            // 0f forces the scroll window straight to the absolute bottom
            scrollRect.verticalNormalizedPosition = 0f;
        }
    }

}
