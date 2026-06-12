using System;
using TMPro;
using UnityEngine;

public class UINotificationPanel : MonoBehaviour
{
    public static UINotificationPanel instance;

    [SerializeField] private GameObject popupContainer;
    [SerializeField] private TMP_Text offlineSummaryText;

    void Awake()
    {
        instance = this;
        if (popupContainer != null) popupContainer.SetActive(false);
    }

    public void ShowOfflineEarningsPopup(TimeSpan timeAway, int mushroomsGrown)
    {
        if (popupContainer == null || offlineSummaryText == null) return;

        popupContainer.SetActive(true);

        // Format formatting human-readable strings cleanly
        string formattedTime = "";
        if (timeAway.TotalHours >= 1)
            formattedTime = $"{Mathf.FloorToInt((float)timeAway.TotalHours)}h {timeAway.Minutes}m";
        else
            formattedTime = $"{timeAway.Minutes}m {timeAway.Seconds}s";

        offlineSummaryText.text = $"Welcome Back Alchemist!\n\nYou were away for <color=#FFD700>{formattedTime}</color>.\nYour magical farm passively cultivated <color=#00FF00>+{mushroomsGrown}</color> Earth Mushrooms!";
    }
}