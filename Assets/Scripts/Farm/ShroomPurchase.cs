using DG.Tweening;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShroomPurchase : MonoBehaviour
{
    Button button;
    public bool isOpening = false;
    public bool isOpen = false;
    private TMP_Text titleText;
    private TMP_Text priceText;

    [SerializeField] private Image mushroomSprite;
    void Awake()
    {
        button = GetComponentInChildren<Button>();
        button.interactable = false;
        EventSystem.current.SetSelectedGameObject(gameObject);
        titleText = GetComponentsInChildren<TMP_Text>()[0];
        priceText = GetComponentsInChildren<TMP_Text>()[1];
    }

    public void SetMushroomVisual(MushroomManager.MushroomType type)
    {
        if (mushroomSprite == null) return;
        mushroomSprite.sprite = MushroomManager.instance.mushroomSprites.sprites[(int)type];
    }

    void Start()
    {

    }

    public void Open()
    {
        if (isOpening || isOpen) return;
        isOpening = true;
        gameObject.transform.DOScale(Vector3.one, 0.2f)
        .SetEase(Ease.InOutBack)
        .OnComplete(() => { isOpening = false; isOpen = true; });
    }

    public void Close()
    {

        if (isOpening || !isOpen) return;
        print("Closing");
        isOpening = true;
        gameObject.transform.DOScale(Vector3.zero, 0.2f)
        .SetEase(Ease.InOutBack)
        .OnComplete(() => { isOpening = false; isOpen = false; });
    }

    private void HideIfClickedOutside(GameObject panel)
    {
        if (isOpening) return;
        if (Input.GetMouseButton(0) && panel.activeSelf &&
            !RectTransformUtility.RectangleContainsScreenPoint(
                panel.GetComponent<RectTransform>(),
                Input.mousePosition,
                Camera.main))
        {
            Close();
            // panel.transform.localScale = Vector3.zero;
        }
    }

    public void InitPanel(MushroomManager.MushroomType type, int price)
    {
        SetTitle(type);
        priceText.text = $"${price:0}";
        SetMushroomVisual(type);
    }

    private void SetTitle(MushroomManager.MushroomType type)
    {
        switch (type)
        {
            case MushroomManager.MushroomType.Champignon:
                titleText.text = "Champignon";
                break;
            case MushroomManager.MushroomType.AngerMushroom:
                titleText.text = "Anger Mushroom";
                break;
            case MushroomManager.MushroomType.WizardMushroom:
                titleText.text = "Wizard Mushroom";
                break;
            case MushroomManager.MushroomType.EmploymentMushroom:
                titleText.text = "Employment Mushroom";
                break;
        }
    }

    public void Update()
    {
        HideIfClickedOutside(gameObject);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        gameObject.transform.localScale = Vector3.zero;
    }

    public void EnoughMoney()
    {
        button.interactable = true;
    }

    public void NotEnoughMoney()
    {
        button.interactable = false;
    }


}
