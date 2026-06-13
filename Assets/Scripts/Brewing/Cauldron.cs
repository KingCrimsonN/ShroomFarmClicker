using DG.Tweening;
using UnityEngine;

public class Cauldron : MonoBehaviour
{
    private bool ready;
    private SpriteRenderer sprite;
    [SerializeField] private Sprite readySprite;
    private Sprite defaultSprite;

    Vector3 originalScale;
    Vector3 originalRotation;

    [SerializeField] private GameObject potionsell;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        defaultSprite = sprite.sprite;
        originalScale = transform.localScale;
        originalRotation = transform.localEulerAngles;
    }

    public void MakeReady()
    {
        ready = true;
        sprite.sprite = readySprite;
    }

    public void OnMouseDown()
    {
        Tween shakeTween = DOTween.Sequence()
            .Append(transform.DORotate(new Vector3(0, 0, Random.Range(-15f, 15f)), 0.1f).SetLoops(2, LoopType.Yoyo))
            .Join(transform.DOShakeScale(0.1f, 0.5f))
            .OnComplete(() =>
            {
                transform.localScale = originalScale;
                transform.localEulerAngles = originalRotation;
            });
        if (ready)
        {
            string potionName;
            int potionPrice;
            BrewingManager.instance.BrewPotion(out potionPrice, out potionName);
            sprite.sprite = defaultSprite;
            ready = false;
            GameObject potion = Instantiate(potionsell, transform.position + new Vector3(0, 1f, 0), Quaternion.identity); ;
            potion.GetComponent<PotionSellPopup>().SetPrice(potionPrice);
            potion.GetComponent<PotionSellPopup>().SetPotionName(potionName);

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
}
