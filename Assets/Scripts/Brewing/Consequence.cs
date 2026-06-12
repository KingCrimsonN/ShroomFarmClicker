using DG.Tweening;
using TMPro;
using UnityEngine;

public class Consequence : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text.DOFade(1f, 1f).SetEase(Ease.Linear).OnComplete(() =>
        {
            text.DOFade(0f, 60f).SetEase(Ease.Linear);
        });
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
