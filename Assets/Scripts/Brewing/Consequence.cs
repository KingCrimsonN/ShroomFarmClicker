using DG.Tweening;
using TMPro;
using UnityEngine;

public class Consequence : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text.DOColor(new Color(256, 256, 256, 0f), 60f).SetEase(Ease.Linear);
        Destroy(gameObject, 120f);
    }

    public void SetText(string text)
    {
        this.text.text = text;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
